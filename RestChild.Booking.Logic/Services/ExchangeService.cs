using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading;
using log4net;
using Newtonsoft.Json;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Comon.Exchange;
using RestChild.Comon.Services;
using RestChild.DAL;
using RestChild.Domain;

namespace RestChild.Booking.Logic.Services
{
    /// <summary>
    ///    обмен
    /// </summary>
    public class ExchangeService : IExchangeService
    {
        private static readonly Dictionary<string, Tuple<Type, MethodInfo>> Dictionary =
           new Dictionary<string, Tuple<Type, MethodInfo>>();

        private static readonly ReaderWriterLock Rwl = new ReaderWriterLock();

        public static string Key = "635630E1-CCD2-4D45-A441-09115AA020AA";

        private readonly ILog _logger = LogManager.GetLogger(typeof(ExchangeService));

        private bool CanCall(string key)
        {
            return key == Key;
        }

        /// <summary>
        ///    отправить пакет с данными
        /// </summary>
        public ExchangePacket GetPacket(long id, string name, string key)
        {
            if (!CanCall(key))
            {
                return null;
            }

            var type = GetTypeInfo(name);

            if (type == null)
            {
                return null;
            }

            using (var uw = new UnitOfWork())
            {
                uw.AutoDetectChangesDisable();
                var dbSet = uw.Context.Set(type.Item1) as IQueryable<IEntityBase>;
                var entity = dbSet?.FirstOrDefault(s => s.Id == id);
                if (entity == null)
                {
                    return null;
                }

                if (entity is PlaceOfRest rf)
                {
                    rf.Requests = null;
                }

                var instance = type.Item2.Invoke(entity, new object[0]) as IEntityBase;
                var data = Convert.ToBase64String(Compression.CompressString(JsonConvert.SerializeObject(instance)));
                return new ExchangePacket { Packet = data, Name = name, LastUpdateTick = instance?.LastUpdateTick };
            }
        }

        /// <summary>
        ///    получение информации о типах
        /// </summary>
        private static Tuple<Type, MethodInfo> GetTypeInfo(string name)
        {
            Tuple<Type, MethodInfo> type = null;
            Rwl.AcquireReaderLock(1000);
            try
            {
                if (Dictionary.ContainsKey(name))
                {
                    type = Dictionary[name];
                }
            }
            finally
            {
                Rwl.ReleaseReaderLock();
            }

            if (type == null)
            {
                var assembly = Assembly.GetAssembly(typeof(Request));
                var types =
                   assembly.GetTypes()
                      .Where(
                         t =>
                            t.Name != "IEntityBase" && t.Name != "IStateEntity" && t.IsClass && !t.IsAbstract &&
                            !t.IsInterface &&
                            !t.IsSealed);

                Rwl.AcquireWriterLock(1000);
                try
                {
                    foreach (var t in types)
                    {
                        var method = t.GetMethod("CreateCopy", new Type[0]);

                        if (!Dictionary.ContainsKey(t.Name) && method != null)
                        {
                            Dictionary.Add(t.Name, new Tuple<Type, MethodInfo>(t, method));
                        }
                    }

                    if (Dictionary.ContainsKey(name))
                    {
                        type = Dictionary[name];
                    }
                }
                finally
                {
                    Rwl.ReleaseWriterLock();
                }
            }

            return type;
        }

        /// <summary>
        ///    изменения
        /// </summary>
        public long[] GetChangedIds(string name, string key)
        {
            if (!CanCall(key))
            {
                return null;
            }

            var info = GetTypeInfo(name);
            if (info == null)
            {
                return null;
            }

            using (var uw = new UnitOfWork())
            {
                var dbSet = uw.Context.Set(info.Item1) as IQueryable<IEntityBase>;
                var ids = dbSet?.Where(s => s.EidSendStatus == (long)EidSendStatusEnum.Changed).Select(s => s.Id)
                   .Take(100000).ToArray();
                return ids;
            }
        }

        private IList СreateList(Type myType)
        {
            var genericListType = typeof(List<>).MakeGenericType(myType);
            return (IList)Activator.CreateInstance(genericListType);
        }

        /// <summary>
        ///    простановка признака что получено
        /// </summary>
        public void SetNotChanged(long id, string name, string key, long lastUpdateTick = 0)
        {
            if (!CanCall(key))
            {
                return;
            }

            var type = GetTypeInfo(name);

            if (type == null)
            {
                return;
            }

            using (var uw = new UnitOfWork())
            {
                var dbSet = uw.Context.Set(type.Item1) as IQueryable<IEntityBase>;
                var entity = dbSet?.FirstOrDefault(s => s.Id == id);
                if (entity == null)
                {
                    return;
                }

                if (lastUpdateTick != 0 && lastUpdateTick != entity.LastUpdateTick)
                {
                    return;
                }

                entity.EidSendStatus = (long)EidSendStatusEnum.NotChanged;
                entity.EidSyncDate = DateTime.Now;
                uw.Context.SaveChanges();
            }
        }

        /// <summary>
        ///    копирование свойств.
        /// </summary>
        private bool ProcessCollection(UnitOfWork uw, IEntityBase source, IEntityBase target)
        {
            var res = true;
            var type = source.GetType();
            var props = type.GetProperties().Where(prop =>
               prop.PropertyType.IsGenericType &&
               prop.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>)).ToArray();

            foreach (var prop in props)
            {
                var inverseProperty =
                   prop.GetCustomAttributes(true).FirstOrDefault(p => p is InversePropertyAttribute) as
                      InversePropertyAttribute;
                // если не нашли инвентированное свойство то считаем что все ок коллекции обновлять не нужно
                if (inverseProperty == null)
                {
                    prop.SetMethod.Invoke(source, new object[] { null });
                    continue;
                }

                if (source is PlaceOfRest)
                {
                    if (prop.Name == "Requests")
                    {
                        continue;
                    }
                }

                var listType = prop.PropertyType.GetGenericArguments()[0];
                var inverseProp = listType.GetProperties().FirstOrDefault(p => p.Name == inverseProperty.Property);
                // если это не многие ко многим то ничего обновлять не нужно
                if (inverseProp == null || !(inverseProp.PropertyType.IsGenericType &&
                                             inverseProp.PropertyType.GetGenericTypeDefinition() ==
                                             typeof(ICollection<>)))
                {
                    prop.SetMethod.Invoke(source, new object[] { null });
                    continue;
                }

                var currentItems = prop.GetMethod.Invoke(source, new object[0]) as ICollection;

                if (currentItems == null)
                {
                    prop.SetMethod.Invoke(source, new object[] { null });
                    continue;
                }

                object targetItems;
                if (target != null)
                {
                    targetItems = prop.GetMethod.Invoke(target, new object[0]);
                    var clearMethod = targetItems.GetType().GetMethods().FirstOrDefault(m => m.Name == "Clear");
                    clearMethod?.Invoke(targetItems, new object[0]);
                }
                else
                {
                    targetItems = СreateList(listType);
                }

                var addMethod = targetItems.GetType().GetMethods().FirstOrDefault(m => m.Name == "Add");

                foreach (var item in currentItems)
                {
                    var element = item as IEntityBase;
                    if (element != null)
                    {
                        var entity = GetElement(uw, element, listType);
                        if (entity == null)
                        {
                            if (!(Settings.Default.EsNotImport?.Contains(listType.Name) ?? false))
                            {
                                LogManager.GetLogger(typeof(ExchangeService)).Warn(
                                   $"Not send collection of type {listType.Name} for property {prop.Name} in type {type.FullName}");
                                res = false;
                            }
                        }
                        else
                        {
                            addMethod?.Invoke(targetItems, new object[] { entity });
                        }
                    }
                }

                prop.SetMethod.Invoke(source, new[] { targetItems });
            }

            return res;
        }

        //private long? GetElementId(UnitOfWork uw, IEntityBase source, Type type)
        //{
        //	var query = uw.Context.Set(type) as IQueryable<IEntityBase>;
        //	return query?.Where(q => q.Eid == source.Id || q.Id == source.Eid).Select(q => q.Id).FirstOrDefault();
        //}

        private IEntityBase GetElement(UnitOfWork uw, IEntityBase source, Type type)
        {
            if (source == null)
            {
                return null;
            }

            var query = uw.Context.Set(type) as IQueryable<IEntityBase>;
            return query?.FirstOrDefault(q => q.Eid == source.Id || q.Id == source.Eid);
        }

        /// <summary>
        ///    копирование свойств.
        /// </summary>
        private bool ProcessPropertys(UnitOfWork uw, IEntityBase source)
        {
            var res = true;
            var type = source.GetType();
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                var foreignKey =
                   prop.GetCustomAttributes(true).FirstOrDefault(p => p is ForeignKeyAttribute) as ForeignKeyAttribute;
                if (foreignKey != null)
                {
                    var name = foreignKey.Name;
                    var value = prop.GetMethod.Invoke(source, new object[0]) as long?;
                    if (value != null)
                    {
                        var targetProp = props.FirstOrDefault(p => p.Name == name);
                        if (targetProp != null)
                        {
                            var info = GetTypeInfo(targetProp.PropertyType.Name);
                            var sourceValue = targetProp.GetGetMethod().Invoke(source, new object[0]) as IEntityBase;
                            var targetValue = GetElement(uw, sourceValue, info.Item1);
                            if (sourceValue != null && targetValue == null)
                            {
                                if (!(Settings.Default.EsNotImport?.Contains(targetProp.PropertyType.Name) ?? false))
                                {
                                    LogManager.GetLogger(typeof(ExchangeService)).Warn(
                                       $"Not send collection of type {targetProp.PropertyType.Name} for property {prop.Name} in type {type.FullName}");
                                    res = false;
                                }
                            }

                            prop.SetMethod.Invoke(source, new object[] { targetValue?.Id });
                            targetProp.SetMethod.Invoke(source, new object[] { null });
                        }
                        else
                        {
                            res = false;
                            prop.SetMethod.Invoke(source, new object[] { null });
                        }
                    }
                }
            }

            return res;
        }

        /// <summary>
        ///    удалять
        /// </summary>
        public ExchangePacket[] GetPacketsToRemove(string key, string name)
        {
            if (!CanCall(key))
            {
                return null;
            }

            using (var uw = new UnitOfWork())
            {
                return uw.GetSet<DeletedRecord>()
                   .Where(d => d.ClassName == name && d.EidSendStatus == (long)EidSendStatusEnum.Changed)
                   .OrderBy(d => d.Id)
                   .Take(1000)
                   .ToList().Select(s =>
                      new ExchangePacket { Id = s.Uid, PacketId = s.Id, Eid = s.Eid, Name = s.ClassName }).ToArray();
            }
        }

        /// <summary>
        ///    удаление записи по внешнему ключу
        /// </summary>
        public bool RemoveEntity(ExchangePacket packet)
        {
            if (!CanCall(packet.Key))
            {
                return false;
            }

            var type = GetTypeInfo(packet.Name);

            if (type == null)
            {
                return false;
            }

            using (var uw = new UnitOfWork())
            {
                var entity = GetElement(uw, new DeletedRecord { Id = packet.Id ?? 0, Eid = packet.Eid }, type.Item1);
                if (entity == null)
                {
                    return true;
                }

                try
                {
                    uw.Context.Entry(entity).State = EntityState.Deleted;
                    uw.Context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }

            return false;
        }

        /// <summary>
        ///    обновление
        /// </summary>
        /// <param name="packet"></param>
        public ExchangePacket UpdatePacket(ExchangePacket packet)
        {
            if (packet == null)
            {
                return new ExchangePacket { Result = false };
            }

            if (!CanCall(packet.Key))
            {
                return new ExchangePacket { Result = false };
            }

            var info = GetTypeInfo(packet.Name);
            if (info == null)
            {
                return new ExchangePacket { Result = false };
            }

            var item = JsonConvert.DeserializeObject(
               Compression.DecompressString(Convert.FromBase64String(packet.Packet)), info.Item1) as IEntityBase;

            if (item == null)
            {
                return new ExchangePacket { Result = false };
            }

            var result = true;
            string extractedValue = null;
            long? id;
            using (var uw = new UnitOfWork())
            {
                // обновление ключей
                result &= ProcessPropertys(uw, item);

                if (item is RequestFile)
                {
                    var rf = item as RequestFile;
                    rf.RemoteSave = false;
                }

                if (!string.IsNullOrEmpty(packet.ExtractFiled))
                {
                    var propInfo = info.Item1.GetProperty(packet.ExtractFiled);
                    if (propInfo != null)
                    {
                        extractedValue = propInfo.GetMethod.Invoke(item, new object[0]) as string;
                    }
                }

                var entity = GetElement(uw, item, info.Item1);

                var eid = item.Eid;
                item.Eid = item.Id;
                item.Id = eid ?? 0;

                if (entity != null)
                {
                    var method = info.Item1.GetMethods().FirstOrDefault(m => m.Name == "CopyEntity");
                    if (method != null)
                    {
                        item.Id = entity.Id;
                        SaveApplicantChildBoutProps(item, entity);
                        method.Invoke(entity, new object[] { item });
                        result &= ProcessCollection(uw, item, entity);
                        entity.Eid = item.Eid;
                        entity.EidSendStatus =
                           result ? (long)EidSendStatusEnum.NotChanged : (long)EidSendStatusEnum.UpdateedWithNoFk;
                        entity.EidSyncDate = DateTime.Now;
                        uw.Context.SaveChanges();
                    }
                    else
                    {
                        result = false;
                    }

                    id = entity.Id;
                }
                else
                {
                    // добавляем записи
                    result &= ProcessCollection(uw, item, null);
                    var attribute =
                       info.Item1?.GetProperty("Id")?.GetCustomAttributes(true)
                             .FirstOrDefault(p => p is DatabaseGeneratedAttribute) as
                          DatabaseGeneratedAttribute;
                    if (attribute?.DatabaseGeneratedOption == DatabaseGeneratedOption.None)
                    {
                        var q = uw.Context.Set(info.Item1) as IQueryable<IEntityBase>;
                        if (!(q?.Any(a => a.Id == item.Eid) ?? true))
                        {
                            item.Id = item.Eid ?? 0;
                        }
                        else
                        {
                            item.Id = q.Select(s => s.Id).Max() + 1;
                        }
                    }

                    uw.Context.Entry(item).State = EntityState.Added;
                    uw.Context.SaveChanges();
                    id = item.Id;
                }
            }

            return new ExchangePacket { Result = result, Packet = extractedValue, Id = id };
        }

        /// <summary>
        ///    сохранить поля как они были в системе
        /// </summary>
        private static void SaveApplicantChildBoutProps(IEntityBase item, IEntityBase entity)
        {
            if (item is Child source1)
            {
                if (entity is Child target)
                {
                    source1.PartyId = target.PartyId;
                    source1.Payed = source1.Payed || target.Payed;
                    source1.BoutId = target.BoutId;
                }
            }

            if (item is Applicant source)
            {
                if (entity is Applicant target)
                {
                    source.BoutId = target.BoutId;
                }
            }

            if (item is Organization org)
            {
                if (entity is Organization target)
                {
                    org.Orphanage = org.Orphanage ?? target.Orphanage;
                    org.HistoryLinkId = target.HistoryLinkId;
                }
            }
        }
    }
}
