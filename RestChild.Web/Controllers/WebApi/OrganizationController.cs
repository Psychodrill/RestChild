using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.Http;
using RestChild.Comon;
using RestChild.Comon.Dto.Commercial;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Extensions.Extensions;
using RestChild.Extensions.Filter;
using RestChild.Web.Models;
using RestChild.Web.Properties;
using RestChild.Web.Extensions;
using RestChild.Web.Logic;

namespace RestChild.Web.Controllers.WebApi
{
    /// <summary>
    ///     api контроллер организаций
    /// </summary>
    [Authorize]
    public class OrganizationController : BaseController
    {
        /// <summary>
        ///     достать ведомство
        /// </summary>
        [Route("api/Vedomstvo/{id}")]
        public IEnumerable<Organization> Get(long id, string query)
        {
            var q =
                UnitOfWork.GetSet<Organization>()
                    .Where(x => !x.IsDeleted && x.IsLast && !x.ParentId.HasValue && x.EntityId != id && x.IsVedomstvo);

            if (!string.IsNullOrEmpty(query))
            {
                q = q.Where(x => x.Name.ToLower().Contains(query.ToLower()));
            }

            return q.OrderBy(x => x.Name)
                .Take(Settings.Default.WebBtiStreetsResponseCount)
                .ToList().Select(o => new Organization(o)).ToList();
        }

        /// <summary>
        ///     список организаций
        /// </summary>
        [Route("api/Orgs")]
        public IEnumerable<Organization> Get(string query)
        {
            var res =
                UnitOfWork.GetSet<Organization>()
                    .Where(x => !x.IsDeleted && x.IsLast && x.Name.ToLower().Contains(query.ToLower()))
                    .OrderBy(x => x.Name.Length)
                    .Take(Settings.Default.WebBtiStreetsResponseCount)
                    .ToList().Select(o => new Organization(o)).ToList();
            return res;
        }

        /// <summary>
        ///     список организаций
        /// </summary>
        [Route("api/Orgs")]
        public IEnumerable<Organization> Get(string query, int typeId)
        {
            var q = UnitOfWork.GetSet<Organization>()
                .Where(x => !x.IsDeleted && x.IsLast);

            if (!string.IsNullOrWhiteSpace(query))
            {
                q = q.Where(x => x.Name.ToLower().Contains(query.ToLower()));
            }

            if (typeId == (int) OrganizationTypeEnum.Agency)
            {
                q = q.Where(i => i.IsVedOrganization ?? false);
            }

            if (typeId == (int) OrganizationTypeEnum.Organization)
            {
                q = q.Where(i => !i.IsVedomstvo && (!i.IsTransport.HasValue || !i.IsTransport.Value));
            }

            if (typeId == (int) OrganizationTypeEnum.TransportOrganization)
            {
                q = q.Where(i => !i.IsVedomstvo && i.IsTransport.HasValue && i.IsTransport.Value);
            }

            var orgsId = AccessRightEnum.TradeUnionList.View.GetSecurityOrganiztion();

            var hasTradeUnion = UnitOfWork.GetSet<Organization>().Any(o => orgsId.Contains(o.Id) && o.IsTradeUnion);
            var hasTradeUnionCamp = UnitOfWork.GetSet<Organization>().Any(o => orgsId.Contains(o.Id) && o.IsHotel);

            if (typeId == (int) OrganizationTypeEnum.TradeUnion)
            {
                q = q.Where(i => i.IsTradeUnion);

                if (!Security.HasRight(AccessRightEnum.TradeUnionList.View) && !hasTradeUnionCamp && hasTradeUnion)
                {
                    q = q.Where(x => orgsId.Contains(x.Id));
                }
            }

            if (typeId == (int) OrganizationTypeEnum.TradeUnionCamp)
            {
                q = q.Where(i => i.IsHotel);
                if (!Security.HasRight(AccessRightEnum.TradeUnionList.View) && hasTradeUnionCamp && !hasTradeUnion)
                {
                    q = q.Where(x => orgsId.Contains(x.Id));
                }
            }

            return
                q.OrderBy(x => x.Name.Length)
                    .Take(Settings.Default.WebBtiStreetsResponseCount)
                    .ToList().Select(o => new Organization(o)).ToList();
        }

        /// <summary>
        ///     список банковских реквизитов организации
        /// </summary>
        [Route("api/orgBanks")]
        public IEnumerable<BaseResponse> GetBanks(long orgId)
        {
            var res =
                UnitOfWork.GetSet<OrganizationBank>()
                    .Where(x => x.OrganizationId == orgId && x.LastUpdateTick != 0)
                    .ToList().Select(o => new BaseResponse {Id = o.Id, Name = o.GetInfo()}).OrderBy(b => b.Name)
                    .ToList();
            return res;
        }

        /// <summary>
        ///     список организаций ОИВ
        /// </summary>
        [Route("api/Vedomstvo/Childs")]
        public IEnumerable<Organization> GetChilds(long oivId, string query)
        {
            return
                UnitOfWork.GetSet<Organization>()
                    .Where(x => (x.ParentId == oivId || x.Id == oivId) && !x.IsDeleted && x.IsLast &&
                                x.Name.ToLower().Contains(query.ToLower()))
                    .OrderBy(x => x.Name)
                    .Take(Settings.Default.WebBtiStreetsResponseCount)
                    .ToList().Select(o => new Organization(o)).ToList();
        }

        /// <summary>
        ///     список подведов
        /// </summary>
        [Route("api/Vedomstvo")]
        public ICollection<Organization> GetOivs(string query)
        {
            return
                UnitOfWork.GetSet<Organization>()
                    .Where(x => x.IsVedomstvo && !x.IsDeleted && x.IsLast &&
                                (string.IsNullOrEmpty(query) || x.Name.ToLower().Contains(query.ToLower())))
                    .OrderBy(x => x.Name)
                    .Take(Settings.Default.WebBtiStreetsResponseCount)
                    .ToList().Select(o => new Organization(o)).ToList();
        }

        /// <summary>
        ///     типы траспорта (транспортных организаций)
        /// </summary>
        [Route("api/typeOfTransport")]
        public ICollection<TypeOfTransport> GetTypesOfTransport()
        {
            var result = UnitOfWork.GetSet<TypeOfTransport>().ToArray().Select(t => new TypeOfTransport(t)).ToArray();

            return result;
        }

        /// <summary>
        ///     список агентов
        /// </summary>
        [Route("api/agencies")]
        public ICollection<Organization> GetAgencies(string query)
        {
            var q = UnitOfWork.GetSet<Organization>()
                .Where(i => i.IsVedOrganization.HasValue && i.IsVedOrganization.Value);
            if (!string.IsNullOrEmpty(query))
            {
                var s = query.ToLower();
                q = q.Where(o => o.Name.ToLower().Contains(s));
            }

            var result = q.OrderBy(o => o.Name.Length).ThenBy(o => o.Name).Take(100).ToArray()
                .Select(o => new Organization(o))
                .ToArray();

            return result;
        }

        /// <summary>
        ///     список организаций и подведов
        /// </summary>
        [Route("api/orgsAndVed")]
        public ICollection<Organization> GetOrganizationsAndVedomstvo(string query)
        {
            var result =
                UnitOfWork.GetSet<Organization>()
                    .Where(i => i.Name.Contains(query))
                    .Where(i => i.IsVedomstvo && !i.IsDeleted)
                    .ToArray()
                    .Select(i => new Organization(i))
                    .ToArray();

            return result;
        }

        /// <summary>
        ///     список ОКВЕов
        /// </summary>
        [Route("api/okveds")]
        public ICollection<Okved> GetOkveds(string query)
        {
            var result =
                UnitOfWork.GetSet<Okved>()
                    .Where(i => i.Name.Contains(query) || i.Code.Contains(query)).OrderBy(i => i.Code)
                    .ThenBy(i => i.Name)
                    .ToArray()
                    .Select(i => new Okved(i))
                    .ToArray();

            return result;
        }

        /// <summary>
        ///     список организаций по фильтру
        /// </summary>
        internal CommonPagedList<Organization> List(OrganizationSearchModel filterModel)
        {
            if (!Security.HasRight(AccessRightEnum.Organization.View))
            {
                return null;
            }

            var pageSize = Settings.Default.TablePageSize;
            var startRecord = (filterModel.PageNumber - 1) * pageSize;
            filterModel.Name = (filterModel.Name ?? string.Empty).ToLower();
            var query = UnitOfWork.GetSet<Organization>().Where(org => org.IsLast && !org.IsDeleted);
            query = query.Where(
                org =>
                    org.Name.ToLower().Contains(filterModel.Name) || org.Inn.Contains(filterModel.Name) ||
                    org.Ogrn.Contains(filterModel.Name));

            if (filterModel.OrganizationType == (int) OrganizationTypeEnum.Agency)
            {
                query = query.Where(i => i.IsVedOrganization ?? false);
            }

            if (filterModel.OrganizationType == (int) OrganizationTypeEnum.Organization)
            {
                query = query.Where(i => !i.IsVedomstvo && (!i.IsTransport.HasValue || !i.IsTransport.Value));
            }

            if (filterModel.OrganizationType == (int) OrganizationTypeEnum.TransportOrganization)
            {
                query = query.Where(i => !i.IsVedomstvo && i.IsTransport.HasValue && i.IsTransport.Value);
            }

            if (filterModel.OrganizationType == (int) OrganizationTypeEnum.TradeUnion)
            {
                query = query.Where(i => i.IsTradeUnion);
            }

            if (filterModel.OrganizationType == (int) OrganizationTypeEnum.TradeUnionCamp)
            {
                query = query.Where(i => i.IsHotel);
            }

            if (filterModel.StateDistrictId > 0)
            {
                query = query.Where(i => i.StateDistrictId == filterModel.StateDistrictId);
            }

            if (filterModel.OivId > 0)
            {
                query = query.Where(i => i.ParentId == filterModel.OivId);
            }

            var totalCount = query.Count();

            var entity = query.OrderBy(place => place.Name).Skip(startRecord).Take(pageSize).ToList();

            return new CommonPagedList<Organization>(entity, filterModel.PageNumber, pageSize, totalCount);
        }

        /// <summary>
        ///     достать организацию
        /// </summary>
        public Organization Get(long id)
        {
            return UnitOfWork.GetById<Organization>(id);
        }

        /// <summary>
        ///     последняя версия организации
        /// </summary>
        /// <param name="entity"></param>
        public Organization GetLastVersion(Organization entity)
        {
            return UnitOfWork.GetSet<Organization>()
                .FirstOrDefault(o => o.EntityId == entity.EntityId && o.IsLast && !o.IsDeleted);
        }

        /// <summary>
        ///     обновление банковских реквизитов организации
        /// </summary>
        private bool UpdateBank(Organization persited, List<OrganizationBank> banks)
        {
            if (banks == null)
            {
                return false;
            }

            var bankIds = banks.Select(a => a.Id).ToList();

            var result = false;

            foreach (var bank in persited.Bank.Where(a => !bankIds.Contains(a.Id) && a.LastUpdateTick != 0).ToList())
            {
                if (UnitOfWork.GetSet<Contract>().Any(c => c.OrganizationBankId == bank.Id))
                {
                    bank.LastUpdateTick = 0;
                }
                else
                {
                    persited.Bank.Remove(bank);
                    UnitOfWork.Delete(bank);
                    if (!result)
                        result = true;

                }
            }

            foreach (var bank in banks)
            {
                if (bank.Id == 0)
                {
                    bank.OrganizationId = persited.Id;
                    bank.LastUpdateTick = DateTime.Now.Ticks;

                    persited.Bank.Add(UnitOfWork.AddEntity(bank, false));

                    if (!result)
                        result = true;

                }
                else
                {
                    var saved = persited.Bank.FirstOrDefault(a => a.Id == bank.Id);
                    bank.LastUpdateTick = DateTime.Now.Ticks;
                    saved?.CopyEntity(bank);

                    if (!result && saved != null)
                        result = true;
                }
            }

            return result;
        }

        /// <summary>
        ///     сохранение организации
        /// </summary>
        internal Organization Save(Organization org, IList<long> newTypeOfTransportSet)
        {
            var dateTimeNow = DateTime.Now;

            if (!Security.HasRight(AccessRightEnum.Organization.Edit))
            {
                return null;
            }


            if (org.CuratorId <= 0)
            {
                org.CuratorId = null;
            }

            org.LastUpdateTick = dateTimeNow.Ticks;
            var banks = org.Bank;
            var okvedsIds = org.Okved.Select(o => o.Id).ToList();
            var okveds = UnitOfWork.GetSet<Okved>().Where(o => okvedsIds.Contains(o.Id)).ToList();
            org.Okved = null;
            org.Bank = null;
            using (var ts = UnitOfWork.GetTransactionScope())
            {
                long orgid = org.Id;
                org.HistoryLink = orgid > 0 ? this.WriteHistory(UnitOfWork.GetById<HistoryLink>(org.HistoryLinkId), "Обновление сведений об организации", OrganizationGetDiff(org, UnitOfWork.GetSet<Organization>().Where(ss => ss.Id == orgid).AsNoTracking().FirstOrDefault())) : this.WriteHistory(org.HistoryLink, "Создание организации", string.Empty);
                org.HistoryLinkId = org.HistoryLink.Id;

                org = org.Id > 0 ? UnitOfWork.Update(org) : UnitOfWork.AddEntity(org);
                org.EntityId = org.EntityId ?? org.Id;
                UnitOfWork.Context.Entry(org).Collection(i => i.TypeOfTransport).Load();

                if (org.IsTransport.HasValue && org.IsTransport.Value)
                {
                    var allTypeOfTransports = UnitOfWork.GetSet<TypeOfTransport>().ToDictionary(i => i.Id);
                    var newTransport = newTypeOfTransportSet.Select(i => allTypeOfTransports[i]).ToArray();

                    UnitOfWork.MergeCollection(newTransport, org.TypeOfTransport);
                }
                else
                {
                    if (org.TypeOfTransport.Any())
                    {
                        var transportToDelete = org.TypeOfTransport.ToArray();
                        foreach (var typeOfTransport in transportToDelete)
                        {
                            org.TypeOfTransport.Remove(typeOfTransport);
                        }
                    }
                }

                var banksUpdated = UpdateBank(org, banks?.ToList());

                if (orgid > 0 && banksUpdated)
                {
                    this.WriteHistory(org.HistoryLink, "Обновление сведений об организации", "Обновлены банковские реквизиты организации");
                }

                var pOkvedsIds = org.Okved.Select(o => o.Id).ToList();

                if (pOkvedsIds.Except(okvedsIds).Any() || okvedsIds.Except(pOkvedsIds).Any())
                {
                    this.WriteHistory(org.HistoryLink, "Обновление сведений об организации", "Обновлены ОКВЭДы организации");
                }

                org.Okved.Clear();
                org.Okved.AddRange(okveds);

                UnitOfWork.SaveChanges();
                ts.Complete();
            }

            return org;
        }

        /// <summary>
        ///     фиксация изменений
        /// </summary>
        private string OrganizationGetDiff(Organization entity, Organization persisted)
        {
            var sb = new StringBuilder();

            if (persisted.Name != entity.Name)
            {
                sb.AppendLine($"<li>Изменено название организации, старое значение:'{persisted.Name.FormatEx(string.Empty)}', новое значение:'{entity.Name.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.ShortName != entity.ShortName)
            {
                sb.AppendLine($"<li>Изменено краткое название организации, старое значение:'{persisted.ShortName.FormatEx(string.Empty)}', новое значение:'{entity.ShortName.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.LatinName != entity.LatinName)
            {
                sb.AppendLine($"<li>Изменено название организации на латинице, старое значение:'{persisted.LatinName.FormatEx(string.Empty)}', новое значение:'{entity.LatinName.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Inn != entity.Inn)
            {
                sb.AppendLine($"<li>Изменён ИНН организации, старое значение:'{persisted.Inn.FormatEx(string.Empty)}', новое значение:'{entity.Inn.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Ogrn != entity.Ogrn)
            {
                sb.AppendLine($"<li>Изменён ОГРН организации, старое значение:'{persisted.Ogrn.FormatEx(string.Empty)}', новое значение:'{entity.Ogrn.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Kpp != entity.Kpp)
            {
                sb.AppendLine($"<li>Изменён КПП организации, старое значение:'{persisted.Kpp.FormatEx(string.Empty)}', новое значение:'{entity.Kpp.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Ownership != entity.Ownership)
            {
                sb.AppendLine($"<li>Изменена форма собственности организации, старое значение:'{persisted.Ownership.FormatEx(string.Empty)}', новое значение:'{entity.Ownership.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Phone != entity.Phone)
            {
                sb.AppendLine($"<li>Изменён телефон организации, старое значение:'{persisted.Phone.FormatEx(string.Empty)}', новое значение:'{entity.Phone.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Email != entity.Email)
            {
                sb.AppendLine($"<li>Изменён E-mail организации, старое значение:'{persisted.Email.FormatEx(string.Empty)}', новое значение:'{entity.Email.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.HeadPerson != entity.HeadPerson)
            {
                sb.AppendLine($"<li>Изменён ген. директор организации, старое значение:'{persisted.HeadPerson.FormatEx(string.Empty)}', новое значение:'{entity.HeadPerson.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.ContactPerson != entity.ContactPerson)
            {
                sb.AppendLine($"<li>Изменено контактное лицо организации, старое значение:'{persisted.ContactPerson.FormatEx(string.Empty)}', новое значение:'{entity.ContactPerson.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.CuratorId != entity.CuratorId)
            {
                var curator = UnitOfWork.GetSet<Account>().Where(ss => ss.Id == entity.CuratorId).Select(ss => ss.Name).FirstOrDefault();
                sb.AppendLine($"<li>Изменён куратор организации, старое значение:'{persisted.Curator?.Name.FormatEx(string.Empty)}', новое значение:'{curator.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.StateDistrictId != entity.StateDistrictId)
            {
                var region = UnitOfWork.GetSet<StateDistrict>().Where(ss => ss.Id == entity.StateDistrictId).Select(ss => ss.Name).FirstOrDefault();
                sb.AppendLine($"<li>Изменён регион организации, старое значение:'{persisted.StateDistrict?.Name.FormatEx(string.Empty)}', новое значение:'{region.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Address != entity.Address)
            {
                sb.AppendLine($"<li>Изменён адрес организации, старое значение:'{persisted.Address.FormatEx(string.Empty)}', новое значение:'{entity.Address.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.PostAdderss != entity.PostAdderss)
            {
                sb.AppendLine($"<li>Изменён почтовый адрес организации, старое значение:'{persisted.PostAdderss.FormatEx(string.Empty)}', новое значение:'{entity.PostAdderss.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Comment != entity.Comment)
            {
                sb.AppendLine($"<li>Изменено примечание организации, старое значение:'{persisted.Comment.FormatEx(string.Empty)}', новое значение:'{entity.Comment.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Commission != entity.Commission)
            {
                sb.AppendLine($"<li>Изменена комиссия организации, старое значение:'{persisted.Commission.FormatEx(string.Empty)}', новое значение:'{entity.Commission.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.IsVedOrganization != entity.IsVedOrganization)
            {
                sb.AppendLine($"<li>Изменён признак указывающий что организации является государственным учреждением, старое значение:'{persisted.IsVedOrganization.FormatEx()}', новое значение:'{entity.IsVedOrganization.FormatEx()}'</li>");
            }

            if (persisted.IsVedomstvo != entity.IsVedomstvo)
            {
                sb.AppendLine($"<li>Изменён признак указывающий что организации является ОИВ, старое значение:'{persisted.IsVedomstvo.FormatEx()}', новое значение:'{entity.IsVedomstvo.FormatEx()}'</li>");
            }

            if (persisted.IsContractor != entity.IsContractor)
            {
                sb.AppendLine($"<li>Изменён признак указывающий что организации не относится к государственным учреждением, старое значение:'{persisted.IsContractor.FormatEx()}', новое значение:'{entity.IsContractor.FormatEx()}'</li>");
            }

            if (persisted.IsTradeUnion != entity.IsTradeUnion)
            {
                sb.AppendLine($"<li>Изменён признак указывающий что организации является профсоюзом, старое значение:'{persisted.IsTradeUnion.FormatEx()}', новое значение:'{entity.IsTradeUnion.FormatEx()}'</li>");
            }

            if (persisted.IsHotel != entity.IsHotel)
            {
                sb.AppendLine($"<li>Изменён признак указывающий что организации является профсоюзным лагерем, старое значение:'{persisted.IsHotel.FormatEx()}', новое значение:'{entity.IsHotel.FormatEx()}'</li>");
            }

            if (persisted.IsTransport != entity.IsTransport)
            {
                sb.AppendLine($"<li>Изменён признак указывающий что организации является транспортной организацией, старое значение:'{persisted.IsTransport.FormatEx()}', новое значение:'{entity.IsTransport.FormatEx()}'</li>");
            }

            if (persisted.Orphanage != entity.Orphanage)
            {
                sb.AppendLine($"<li>Изменён признак указывающий что организации является учреждением соц. защиты, старое значение:'{persisted.Orphanage.FormatEx()}', новое значение:'{entity.Orphanage.FormatEx()}'</li>");
            }

            if (persisted.IsInMonitoring != entity.IsInMonitoring)
            {
                sb.AppendLine($"<li>Изменён признак указывающий что организации является участником мониторинга, старое значение:'{persisted.IsInMonitoring.FormatEx()}', новое значение:'{entity.IsInMonitoring.FormatEx()}'</li>");
            }


            var res = sb.ToString();
            if (string.IsNullOrWhiteSpace(res))
            {
                return "Сведения об организации не изменились";
            }

            return $"<ul>{res}</ul>";
        }
    }
}
