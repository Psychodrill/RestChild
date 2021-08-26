using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private const string ConnectionStringName = "RestChild";

        private static readonly string[] DeleteRecordNotControl =
            {"ReportSheet", "AnalyticsViewRow", "DeletedRecord", "BtiAddress"};

        private static readonly string[] NotChangeLastUpdateTick =
            {"Tour", "Request", "ListOfChilds", "Bout", "Hotels", "BtiAddress"};

        private readonly Context context;
        private DbContextTransaction transaction;

        public UnitOfWork()
        {
            context = new Context(BuildConnectionString());
            context.Database.CommandTimeout = 3600;
        }

        /// <summary>
        ///     не обновлять LastUpdateTick
        /// </summary>
        public bool NotUpdateLut { get; set; }

        public TransactionScope GetTransactionScope()
        {
            return GetTransactionScope(IsolationLevel.ReadCommitted);
        }

        public TransactionScope GetTransactionScope(IsolationLevel isolationLevel)
        {
            return new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions {IsolationLevel = isolationLevel});
        }

        public Database Database => Context.Database;

        public DbContext Context => context;

        public IDbSet<T> GetSet<T>() where T : class, IEntityBase
        {
            return Context.Set<T>();
        }

        public void BeginTransaction()
        {
            if (transaction != null)
            {
                throw new TransactionException("The transaction has been already begun");
            }

            transaction = Context.Database.BeginTransaction();
        }

        public void UpdateProperty<T, TProperty>(T entity, Expression<Func<T, TProperty>> expr)
            where T : class, IEntityBase
        {
            Context.Entry(entity).Property(expr).IsModified = true;
        }

        public async Task<T> AddEntityAsync<T>(T entity, CancellationToken token, bool saveChanges = true) where T : class, IEntityBase
        {
            entity.EidSendStatus = (long) EidSendStatusEnum.Changed;
            entity.LastUpdateTick = DateTime.Now.Ticks;

            Context.Set<T>().Add(entity);

            if (saveChanges)
            {
                await SaveChangesAsync(token);
                Context.Entry(entity).State = EntityState.Detached;
                entity = await Context.Set<T>().FindAsync(token, entity.Id);
            }

            return entity;
        }

        public T AddEntity<T>(T entity, bool saveChanges = true) where T : class, IEntityBase
        {
            entity.EidSendStatus = (long) EidSendStatusEnum.Changed;
            entity.LastUpdateTick = DateTime.Now.Ticks;

            Context.Set<T>().Add(entity);

            if (saveChanges)
            {
                SaveChanges();
                Context.Entry(entity).State = EntityState.Detached;
                entity = Context.Set<T>().Find(entity.Id);
            }

            return entity;
        }

        /// <summary>
        ///     Метод прикрепляет объект к контексту.
        ///     Если существует прикрепленный объект того же типа с тем же Id, то прикрепление не происходит.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns>entity или найденный объект</returns>
        public T GetOrAttachEntity<T>(T entity) where T : class, IEntityBase
        {
            var localEntity = Context.Set<T>().Local.FirstOrDefault(x => x.Id == entity.Id)
                              ?? Context.Set<T>().Attach(entity);
            return localEntity;
        }

        public ICollection<T> GetOrAttachCollection<T>(ICollection<T> collection) where T : class, IEntityBase
        {
            var result = new List<T>();
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var element in collection)
            {
                result.Add(GetOrAttachEntity(element));
            }

            return result;
        }

        public T AttachEntity<T>(T entity) where T : class, IEntityBase
        {
            return Context.Set<T>().Attach(entity);
        }

        public void DetachEntity<T>(T entity) where T : class, IEntityBase
        {
            Context.Entry(entity).State = EntityState.Detached;
        }

        public void DetachAllEntitys()
        {
            foreach (var entry in Context.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }
        }

        public void EntityStateChange<T>(T entity, EntityState state) where T : class, IEntityBase
        {
            Context.Entry(entity).State = state;
        }

        public void Delete<T>(T entity) where T : class, IEntityBase
        {
            Context.Entry(entity).State = EntityState.Deleted;
            SaveChanges();
        }

        public void Delete<T>(IEnumerable<T> entities) where T : class, IEntityBase
        {
            foreach (var entity in entities)
            {
                context.Set<T>().Attach(entity);
                Context.Entry(entity).State = EntityState.Deleted;
            }

            SaveChanges();
        }

        /// <summary>
        ///     сохранение изменений
        /// </summary>
        public void SaveChanges()
        {
            ProcessOnSave();
            Context.SaveChanges();
        }

        /// <summary>
        ///     сохранение изменений
        /// </summary>
        public async Task SaveChangesAsync(CancellationToken token)
        {
            ProcessOnSave();
            await Context.SaveChangesAsync(token);
        }

        public void AutoDetectChangesDisable()
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
        }

        public void AutoDetectChangesEnable()
        {
            Context.Configuration.AutoDetectChangesEnabled = true;
        }

        public void Commit()
        {
            SaveChanges();

            transaction?.Commit();
        }

        public void RollBack()
        {
            transaction?.Rollback();
        }

        public void RollBack(bool allChanges)
        {
            RollBack();
            if (!allChanges)
            {
                return;
            }

            foreach (var entry in Context.ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }
        }

        public void Dispose()
        {
            transaction?.Dispose();
            Context.Dispose();
        }

        public T Update<T>(T entity) where T : class, IEntityBase
        {
            if (!entity.Eid.HasValue)
            {
                var syncFields =
                    GetSet<T>()
                        .Where(t => t.Id == entity.Id && t.Eid.HasValue)
                        .Select(t => new {t.Eid, t.EidSendStatus, t.EidSyncDate})
                        .FirstOrDefault();

                entity.Eid = syncFields?.Eid ?? entity.Eid;
                entity.EidSendStatus = syncFields?.EidSendStatus ?? entity.EidSendStatus;
                entity.EidSyncDate = syncFields?.EidSyncDate ?? entity.EidSyncDate;
            }

            Context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
            Context.Entry(entity).State = EntityState.Detached;
            entity = Context.Set<T>().Find(entity.Id);
            return entity;
        }

        public void CopyDtoToEntity<T>(T sourceEntity, T destinationEntity) where T : class, IEntityBase
        {
            var srcProperties = sourceEntity.GetType().GetProperties();
            var dstProperties = destinationEntity.GetType().GetProperties();

            foreach (var srcProperty in srcProperties)
            {
                var dstProperty =
                    dstProperties.FirstOrDefault(x =>
                        x.Name == srcProperty.Name && x.MemberType == srcProperty.MemberType);
                if (dstProperty != null)
                {
                    if (typeof(IEntityBase).IsAssignableFrom(dstProperty.PropertyType))
                    {
                        var dbSet = Context.Set(dstProperty.PropertyType);
                        dbSet.Local.Clear();
                        dbSet.Attach(srcProperty.GetValue(sourceEntity));
                        dstProperty.SetValue(destinationEntity, srcProperty.GetValue(sourceEntity));
                    }
                    else if (typeof(IEnumerable<IEntityBase>).IsAssignableFrom(dstProperty.PropertyType))
                    {
                        var srcRelations = srcProperty.GetValue(sourceEntity);
                        var dstRelations = dstProperty.GetValue(destinationEntity);
                        var genericArg = dstProperty.PropertyType.GenericTypeArguments.First();
                        if (dstRelations == null)
                        {
                            dstProperty.SetValue(destinationEntity, Activator.CreateInstance(genericArg));
                        }

                        dstRelations?.GetType().GetMethod("Clear")?.Invoke(dstRelations, null);
                        if (srcRelations == null)
                        {
                            continue;
                        }

                        var attached = GetType()
                            .GetMethod("GetOrAttachCollection")?
                            .MakeGenericMethod(genericArg)
                            .Invoke(this, new[] {srcRelations});
                        dstRelations?.GetType().GetMethod("AddRange")?.Invoke(dstRelations, new[] {attached});
                    }
                    else
                    {
                        dstProperty.SetValue(destinationEntity, srcProperty.GetValue(sourceEntity));
                    }
                }
            }
        }

        public void CopyCollection<T>(ICollection<T> sourceCollection, ICollection<T> destinationCollection)
            where T : class, IEntityBase
        {
            if (destinationCollection == null)
            {
                destinationCollection = new List<T>();
            }

            destinationCollection.Clear();
            if (sourceCollection == null)
            {
                return;
            }

            foreach (var item in GetOrAttachCollection(sourceCollection))
            {
                destinationCollection.Add(item);
            }
        }

        public long GetNextNumber(string key)
        {
            long number = 0;
            var isOk = false;
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                while (!isOk)
                {
                    isOk = true;
                    try
                    {
                        using (var unit = new UnitOfWork())
                        {
                            number = unit.GetNextNumberInternal(key);
                        }
                    }
                    catch
                    {
                        isOk = false;
                    }
                }
            }

            return number;
        }

        public ICollection<T> MergeCollection<T>(ICollection<T> sourceCollection, ICollection<T> destinationCollection,
            Action<T, T> mergeFunction = null) where T : class, IEntityBase
        {
            return MergeCollectionStatic(sourceCollection, destinationCollection, mergeFunction);
        }


        private void ProcessOnSave()
        {
            if (!NotUpdateLut)
            {
                // получение измененных
                var entities =
                    Context.ChangeTracker.Entries<IEntityBase>()
                        .Where(c => c.State == EntityState.Added || c.State == EntityState.Modified)
                        .Select(c => c.Entity)
                        .ToList();

                var forDelete =
                    Context.ChangeTracker.Entries<IEntityBase>()
                        .Where(c => c.State == EntityState.Deleted)
                        .Select(c => c.Entity)
                        .ToList();

                // фиксация удаленных записей
                foreach (var e in forDelete)
                {
                    var className = ObjectContext.GetObjectType(e.GetType()).Name;
                    if (!DeleteRecordNotControl.Contains(className))
                    {
                        Context.Entry(new DeletedRecord
                        {
                            ClassName = className,
                            Uid = e.Id,
                            Eid = e.Eid,
                            LastUpdateTick = DateTime.Now.Ticks,
                            EidSendStatus = (long) EidSendStatusEnum.Changed
                        }).State = EntityState.Added;
                    }
                }

                // фиксация факта изменений
                foreach (var entity in entities)
                {
                    var className = ObjectContext.GetObjectType(entity.GetType()).Name;
                    if (!NotChangeLastUpdateTick.Contains(className))
                    {
                        entity.LastUpdateTick = DateTime.Now.Ticks;
                    }

                    entity.EidSendStatus = entity.EidSendStatus == (long) EidSendStatusEnum.NotChanged
                        ? (long) EidSendStatusEnum.Changed
                        : entity.EidSendStatus == (long) EidSendStatusEnum.Updateed
                            ? (long) EidSendStatusEnum.NotChanged
                            : entity.EidSendStatus ?? (long) EidSendStatusEnum.Changed;
                }
            }
        }

        public long GetNextNumberInternal(string key)
        {
            var query = Context.Set<Numerator>().Where(n => n.Key == key);
            var id = query.Any() ? query.Max(n => n.Id) + 1 : 1;
            var entity = new Numerator {Id = id, Key = key};
            Context.Set<Numerator>().Add(entity);
            var index = 0;

            while (index < 10)
            {
                try
                {
                    SaveChanges();
                    return entity.Id;
                }
                catch
                {
                    entity.Id++;
                    index++;
                }
            }

            throw new ApplicationException("Не могу получить номер по ключу");
        }

        public static ICollection<T> MergeCollectionStatic<T>(ICollection<T> sourceCollection,
            ICollection<T> destinationCollection,
            Action<T, T> mergeFunction = null,
            Action<T> addFunction = null,
            Action<T> deleteFunction = null) where T : class, IEntityBase
        {
            if (destinationCollection == null)
            {
                return sourceCollection ?? new Collection<T>();
            }

            if (sourceCollection == null || !sourceCollection.Any())
            {
                if (deleteFunction != null)
                {
                    var destinations = destinationCollection.ToList();
                    foreach (var destination in destinations)
                    {
                        deleteFunction(destination);
                    }
                }

                destinationCollection.Clear();
                return destinationCollection;
            }

            var idSource = sourceCollection.Select(s => s.Id).ToArray();
            var idDest = destinationCollection.Select(s => s.Id).ToArray();
            var idToMerge = idSource.Intersect(idDest);

            var destDelete = destinationCollection.Where(d => !idSource.Contains(d.Id)).ToList();
            foreach (var item in destDelete)
            {
                if (deleteFunction != null)
                {
                    deleteFunction(item);
                }
                else
                {
                    destinationCollection.Remove(item);
                }
            }

            var sourceToAdd = sourceCollection.Where(d => !idDest.Contains(d.Id)).ToList();
            foreach (var item in sourceToAdd)
            {
                if (addFunction != null)
                {
                    addFunction(item);
                }
                else
                {
                    destinationCollection.Add(item);
                }
            }

            if (mergeFunction != null)
            {
                foreach (var item in idToMerge)
                {
                    var id = item;
                    var itemDest = destinationCollection.FirstOrDefault(d => d.Id == id);
                    var itemSource = sourceCollection.FirstOrDefault(d => d.Id == id);
                    mergeFunction(itemSource, itemDest);
                }
            }

            return destinationCollection;
        }


        private string BuildConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
        }
    }
}
