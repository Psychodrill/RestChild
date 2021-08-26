using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace RestChild.EGASIntegration
{
    /// <summary>
    ///     Репозиторий работы с ЕГАС
    /// </summary>
    public static class IntegrationRepository
    {
        /// <summary>
        ///     Кол-во строк загруждаемых в одну итерацию
        /// </summary>
        const int GetRowCount = 300;

        /// <summary>
        ///     Логин доступа к ЕГАС
        /// </summary>
        private static readonly string UserName = ConfigurationManager.AppSettings["EGASUserName"] ?? "ais_det_otd";

        /// <summary>
        ///     Пароль доступа к ЕГАС
        /// </summary>
        private static readonly string Password = ConfigurationManager.AppSettings["EGASPassword"] ?? "ef84OaLO";

        /// <summary>
        ///     Идентификатор каталога школ в ЕГАС
        /// </summary>
        private static readonly int CatalogId = Convert.ToInt32(ConfigurationManager.AppSettings["EGASCatalogId"] ?? "1128");

        /// <summary>
        ///     Идентификатор global_id каталога школ в ЕГАС
        /// </summary>
        private static readonly string global_id_ident = ConfigurationManager.AppSettings["EGAS_global_id_ident"] ?? "-1";

        /// <summary>
        ///     Идентификатор name каталога школ в ЕГАС
        /// </summary>
        private static readonly string name_ident = ConfigurationManager.AppSettings["EGAS_name_ident"] ?? "15806";

        /// <summary>
        ///     Идентификатор id каталога школ в ЕГАС
        /// </summary>
        private static readonly string id_ident = ConfigurationManager.AppSettings["EGAS_id_ident"] ?? "26238";

        /// <summary>
        ///     Загрузить и обновить школы
        /// </summary>
        public static void UpdateSchools(IUnitOfWork unitOfWork)
        {
            using (EGASPromDuo.soapClient client = new EGASPromDuo.soapClient())
            {
                List<long> added_ids = new List<long>();

                int count = 0;

                using (new OperationContextScope(client.InnerChannel))
                {
                    HttpRequestMessageProperty requestMessage = new HttpRequestMessageProperty();
                    requestMessage.Headers["username"] = UserName;
                    requestMessage.Headers["password"] = Password;

                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessage;

                    do
                    {
                        var schools = client.getCatalogItems(CatalogId, count + 1, count + GetRowCount, true, 0, null, null, 0, false, null, false);

                        if (schools.Length < 1)
                            break;

                        count += schools.Length;

                        foreach(var catitem in schools)
                        {
                            int global_id = 0;
                            string name = null;
                            int id = 0;
                            Parallel.ForEach(catitem.ehdCatalogItem, (attr) =>
                            {
                                if (string.Equals(attr.id, global_id_ident, StringComparison.OrdinalIgnoreCase))
                                {
                                    global_id = Convert.ToInt32(attr.value);
                                }
                                else if (string.Equals(attr.id, name_ident, StringComparison.OrdinalIgnoreCase))
                                {
                                    name = Convert.ToString(attr.value);
                                }
                                else if (string.Equals(attr.id, id_ident, StringComparison.OrdinalIgnoreCase))
                                {
                                    id = Convert.ToInt32(attr.value);
                                }

                                //заготовка на будущюее
                                //else if (string.Equals(attr.id, "-2", StringComparison.OrdinalIgnoreCase))
                                //{
                                //    system_object_id = Convert.ToInt32(attr.value);
                                //}
                                //else if (string.Equals(attr.id, "15805", StringComparison.OrdinalIgnoreCase))
                                //{
                                //    fullName = Convert.ToString(attr.value);
                                //}
                                //else if (string.Equals(attr.id, "26239", StringComparison.OrdinalIgnoreCase))
                                //{
                                //    IDEKIS = Convert.ToInt32(attr.value);
                                //}
                                //else if (string.Equals(attr.id, "15808", StringComparison.OrdinalIgnoreCase))
                                //{
                                //    INN = Convert.ToString(attr.value);
                                //}
                                //else if (string.Equals(attr.id, "15809", StringComparison.OrdinalIgnoreCase))
                                //{
                                //    KPP = Convert.ToString(attr.value);
                                //}
                                //else if (string.Equals(attr.id, "15810", StringComparison.OrdinalIgnoreCase))
                                //{
                                //    OGRN = Convert.ToString(attr.value);
                                //}
                                //else if (string.Equals(attr.id, "15813", StringComparison.OrdinalIgnoreCase))
                                //{
                                //    chiefName = Convert.ToString(attr.value);
                                //}
                                //else if (string.Equals(attr.id, "15814", StringComparison.OrdinalIgnoreCase))
                                //{
                                //    address = Convert.ToString(attr.value);
                                //}
                                //else if (string.Equals(attr.id, "18241", StringComparison.OrdinalIgnoreCase))
                                //{
                                //    phone = Convert.ToString(attr.value);
                                //}
                                //else if (string.Equals(attr.id, "18242", StringComparison.OrdinalIgnoreCase))
                                //{
                                //    email = Convert.ToString(attr.value);
                                //}
                                //else if (string.Equals(attr.id, "15819", StringComparison.OrdinalIgnoreCase))
                                //{
                                //    website = Convert.ToString(attr.value);
                                //}
                            });

                            if (global_id > 0 && id > 0 && !string.IsNullOrWhiteSpace(name))
                            {
                                added_ids.Add(-global_id);

                                var school = unitOfWork.GetSet<Domain.School>().FirstOrDefault(ss => ss.Id == -global_id);
                                if (school != null)
                                {
                                    school.Name = name;
                                    school.SourcePk = id;
                                }
                                else
                                {
                                    school = new Domain.School
                                    {
                                        Id = -global_id,
                                        Name = name,
                                        SourcePk = id,
                                        OrganizationGuid = Guid.NewGuid(),
                                        Status = (long)OrganizationStatusEnum.StatusActual,
                                        DateChange = DateTime.Now,
                                    };
                                    unitOfWork.AddEntity(school, false);
                                }
                                unitOfWork.SaveChanges();
                            }
                        }
                    }
                    while (count > 0);

                    foreach (var school in unitOfWork.GetSet<Domain.School>().Where(ss => ss.Status != (long)OrganizationStatusEnum.StatusDeleted && ss.Status != null).ToList())
                    {
                        if (added_ids.Contains(school.Id))
                            continue;

                        school.Status = (long)OrganizationStatusEnum.StatusDeleted;
                        unitOfWork.SaveChanges();
                    }
                }
            }
        }
    }
}
