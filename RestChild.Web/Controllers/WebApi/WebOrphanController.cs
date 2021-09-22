using System;
using System.Collections.Generic;
using System.Linq;
using RestChild.Comon.Dto.Commercial;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.Web.Models.Orphans;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers.WebApi
{
    /// <summary>
    ///     API блока сирот
    /// </summary>
    public class WebOrphanController : BaseController
    {
        /// <summary>
        ///     Поиск детских домов
        /// </summary>
        internal CommonPagedList<OrphanageResultListModel> GetOrphanage(OrphanageFilterModel filter, int pageSize = -1,
            bool forExcel = false)
        {
            if (pageSize < 0)
            {
                pageSize = Settings.Default.TablePageSize;
            }

            var pageNumber = filter.PageNumber;
            var startRecord = (pageNumber - 1) * pageSize;

            var query = UnitOfWork.GetSet<Organization>().Where(ss => !ss.IsDeleted && ss.Orphanage == true)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Address))
            {
                query = query.Where(ss =>
                    ss.OrphanageOrganizationAddresses.Any(sx =>
                        sx.Address.Name.ToLower().Contains(filter.Address.ToLower())));
            }

            if (!string.IsNullOrWhiteSpace(filter.DirectorName))
            {
                query = query.Where(ss => ss.HeadPerson.ToLower().Contains(filter.DirectorName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.ShortName))
            {
                query = query.Where(ss => ss.ShortName.ToLower().Contains(filter.ShortName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(ss => ss.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            if (Security.HasRightForSomeOrganization(AccessRightEnum.Orphans.Main))
            {
                var orgs = AccessRightEnum.Orphans.Main.GetSecurityOrganiztion();
                if (orgs.Length > 0)
                {
                    query = query.Where(c => orgs.Contains(c.Id));
                }
            }

            var totalCount = query.Count();

            query = query.OrderBy(ss => ss.Id);

            if (pageSize > 0)
            {
                query = query.Skip(startRecord).Take(pageSize);
            }

            var entity = new List<OrphanageResultListModel>();

            if (forExcel)
            {
                entity = query.ToList().Select(ss => new OrphanageResultListModel
                {
                    Id = ss.Id,
                    ShortName = ss.ShortName,
                    Name = ss.Name,
                    DirectorName = ss.HeadPerson,
                    Phone = ss.Phone,
                    EMail = ss.Email,
                    FioRfr = string.Join("\n\r",
                        ss.OrganisatonCollaborators
                            ?.Where(oc =>
                                oc.EntityId == null &&
                                oc.PositionId == (long)OrphanageCollaboratorType.ResponsibleForRest)
                            .OrderBy(ox => ox.Id).Select(oc => oc.Applicant.GetFio()).ToList()),
                    EMailRfr = string.Join("\n\r",
                        ss.OrganisatonCollaborators
                            ?.Where(oc =>
                                oc.EntityId == null &&
                                oc.PositionId == (long)OrphanageCollaboratorType.ResponsibleForRest)
                            .OrderBy(ox => ox.Id).Select(oc => oc.Applicant.Email).ToList()),
                    PhoneRfr = string.Join("\n\r", ss.OrganisatonCollaborators
                        ?.Where(oc =>
                            oc.EntityId == null && oc.PositionId == (long)OrphanageCollaboratorType.ResponsibleForRest)
                        .OrderBy(ox => ox.Id).Select(oc =>
                        {
                            var res = new string[2];
                            res[0] = oc?.Applicant?.Phone;
                            res[1] = oc?.AditionalPhone;
                            return string.Join("; ", res.Where(ox => !string.IsNullOrWhiteSpace(ox)));
                        }).ToArray())
                }).ToList();
            }
            else
            {
                entity = query.Select(ss => new OrphanageResultListModel
                {
                    Id = ss.Id,
                    ShortName = ss.ShortName,
                    Name = ss.Name,
                    DirectorName = ss.HeadPerson,
                    Phone = ss.Phone,
                    EMail = ss.Email,
                }).ToList();
            }

            if (entity.Any())
            {
                var addresses = UnitOfWork.GetSet<OrphanageAddress>()
                    .Where(ss => query.Select(sx => sx.Id).Distinct().Contains((long)ss.OrganisationId))
                    .Select(ss => new { ss.OrganisationId, ss.Address.Name }).GroupBy(sx => (long)sx.OrganisationId)
                    .ToDictionary(sx => sx.Key, ax => ax.Select(sx => sx.Name));

                for (var i = 0; i < entity.Count; i++)
                {
                    if (addresses.ContainsKey(entity[i].Id))
                    {
                        entity[i].Address = addresses[entity[i].Id].ToArray();
                    }
                }
            }

            return new CommonPagedList<OrphanageResultListModel>(entity, pageNumber,
                pageSize > 0 ? pageSize : totalCount, totalCount);
        }

        /// <summary>
        ///     Поиск сотрудников детских домов
        /// </summary>
        internal CommonPagedList<OrphanageCollaboratorsResultListModel> GetOrphanageCollaborators(
            OrphanageCollaboratorsFilterModel filter)
        {
            var pageSize = Settings.Default.TablePageSize;
            var pageNumber = filter.PageNumber;
            var startRecord = (pageNumber - 1) * pageSize;

            var query = UnitOfWork.GetSet<OrganisatorCollaborator>().Where(ss =>
                    (ss.OrganisatonAddress.OrganisationId == filter.OrphanageId ||
                     ss.OrganisatonId == filter.OrphanageId) && (ss.EntityId == null || ss.EntityId == ss.Id))
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                var nms = filter.Name.Split(' ');
                foreach (var nm in nms)
                {
                    query = query.Where(ss =>
                        ss.Applicant.FirstName.ToLower().Contains(nm.ToLower()) ||
                        ss.Applicant.LastName.ToLower().Contains(nm.ToLower()) ||
                        ss.Applicant.MiddleName.ToLower().Contains(nm.ToLower()));
                }
            }

            if (!filter.Deleted)
            {
                query = query.Where(ss => !ss.Applicant.IsDeleted);
            }

            var totalCount = query.Count();

            query = query.OrderBy(ss => ss.Applicant.LastName);

            query = query.Skip(startRecord).Take(pageSize);

            var entity = query.Select(ss => new OrphanageCollaboratorsResultListModel
            {
                Id = ss.Id,
                Name = (ss.Applicant.LastName + " " + ss.Applicant.FirstName + " " + ss.Applicant.MiddleName).Trim(),
                Position = ss.OrganisationPosition,
                IsDeleted = ss.Applicant.IsDeleted
            }).ToList();

            return new CommonPagedList<OrphanageCollaboratorsResultListModel>(entity, pageNumber, pageSize, totalCount);
        }

        /// <summary>
        ///     Поиск воспитанников детских домов
        /// </summary>
        internal CommonPagedList<OrphanagePupilsResultListModel> GetPupils(OrphanagePupilsFilterModel filter)
        {
            var pageSize = Settings.Default.TablePageSize;
            var pageNumber = filter.PageNumber;
            var startRecord = (pageNumber - 1) * pageSize;

            var query = UnitOfWork.GetSet<Pupil>().Where(ss =>
                ss.Child.ChildListId == null && ss.OrphanageAddress.OrganisationId == filter.OrphanageId &&
                (ss.Child.EntityId == null || ss.Child.EntityId == ss.Child.Id)).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                var aarr = filter.Name.Split(' ');
                foreach (var n in aarr)
                {
                    query = query.Where(ss =>
                        ss.Child.FirstName.ToLower().Contains(n.ToLower()) ||
                        ss.Child.LastName.ToLower().Contains(n.ToLower()) ||
                        ss.Child.MiddleName.ToLower().Contains(n.ToLower()));
                }
            }

            if (filter.AgeFrom.HasValue && filter.AgeFrom.Value > 0)
            {
                var d1 = DateTime.Now.AddYears(filter.AgeFrom.Value * -1);
                query = query.Where(ss => ss.Child.DateOfBirth <= d1);
            }

            if (filter.AgeTo.HasValue && filter.AgeTo.Value > 0)
            {
                var d1 = DateTime.Now.AddYears(((filter.AgeTo.Value + 1) * -1));
                query = query.Where(ss => ss.Child.DateOfBirth >= d1);
            }

            if (filter.GroupId.HasValue && filter.GroupId.Value > 0)
            {
                if (filter.IsInGroup)
                    query = query.Where(ss => ss.PupilGroups.Any(sx => sx.Id == filter.GroupId));
                else
                    query = query.Where(ss => !ss.PupilGroups.Any(sx => sx.Id == filter.GroupId));
            }

            if (filter.ListId.HasValue && filter.ListId.Value > 0)
            {
                //query = query.Where(ss => !ss.PupilGroups.Any(sx => sx.Requests.Any(sy => sy.Lists.Any(sz => sz.Id == filter.ListId.Value))));
            }

            if (filter.IsNotInGroup)
            {
                query = query.Where(ss => !ss.PupilGroups.Any());
            }

            if (filter.IsMale.HasValue)
            {
                query = query.Where(ss => ss.Child.Male == filter.IsMale.Value);
            }

            if (!filter.IncludeOut)
            {
                query = query.Where(ss => ss.DateOut == null);
            }

            if (filter.IsFilled)
            {
                query = query.Where(ss => ss.Filled);
            }

            if (!filter.Deleted)
            {
                query = query.Where(ss => !ss.Child.IsDeleted);
            }

            var totalCount = query.Count();

            query = query.OrderBy(ss => ss.Child.LastName).ThenBy(ss => ss.Child.FirstName)
                .ThenBy(ss => ss.Child.MiddleName);

            query = query.Skip(startRecord).Take(pageSize);

            var entity = query.Select(ss => new OrphanagePupilsResultListModel
            {
                Id = ss.Id,
                Name = (ss.Child.LastName + " " + ss.Child.FirstName + " " + ss.Child.MiddleName).Trim(),
                DateOfBirth = ss.Child.DateOfBirth,
                IsDeleted = ss.Child.IsDeleted
            }).ToList();


            return new CommonPagedList<OrphanagePupilsResultListModel>(entity, pageNumber, pageSize, totalCount);
        }

        /// <summary>
        ///     Поиск групп (потребностей)
        /// </summary>
        internal void FillGroups(OrphanageGroupsFilterModel filter)
        {
            var query = PrepareQueryPupilGroup(filter);

            var totalCount = query.Count();

            if (filter.PageSize > 0)
            {
                query = query.Skip(filter.StartRecord).Take(filter.PageSize);
            }

            var result = query.ToList();

            filter.Results = new CommonPagedList<OrphanageGroupsResultListModel>(result.Select(ss =>
                    new OrphanageGroupsResultListModel
                    {
                        Id = ss.Id,
                        Name = ss.Name,
                        Status = ss.State?.Name,
                        Year = ss.YearOfRest?.Year.ToString(),
                        VacationPeriod = ss.VacationPeriod?.Name,
                        FormOfRest = ss.FormOfRest?.Name,
                        OrphanageName = ss.Organization?.Name,
                        RegionsOfRest =
                            ss.Requests?.Select(sx => sx.PlaceOfRest?.Name + " " + sx.TimeOfRest?.Name).ToList() ??
                            new List<string>(0)
                    }).ToList(), filter.PageNumber, filter.PageSize > 0 ? filter.PageSize : Math.Max(10, totalCount),
                totalCount);


            filter.YearsOfRest = UnitOfWork.GetSet<YearOfRest>().Where(ss => !ss.IsClosed || ss.Id == filter.YearOfRest)
                .ToDictionary(ss => ss.Id, sx => sx.Name);
            filter.FormsOfRest = UnitOfWork.GetSet<FormOfRest>().Where(ss => !ss.IsDeleted)
                .ToDictionary(ss => ss.Id, sx => sx.Name);
            filter.RegionsOfRest = UnitOfWork.GetSet<PlaceOfRest>()
                .Where(ss => ss.IsActive || ss.Id == filter.RegionOfRest).ToDictionary(ss => ss.Id, sx => sx.Name);
            filter.States = UnitOfWork.GetSet<StateMachineState>()
                .Where(ss => ss.StateMachineId == (long)StateMachineEnum.PupilGroup)
                .ToDictionary(ss => ss.Id, sx => sx.Name);
            filter.TimesOfRest = UnitOfWork.GetSet<TimeOfRest>().Where(ss => ss.IsActive || ss.Id == filter.TimeOfRest)
                .ToDictionary(ss => ss.Id, sx => sx.Name);
            filter.OrphanageName = UnitOfWork.GetSet<Organization>().Where(ss => ss.Id == filter.OrphanageId)
                .Select(ss => ss.Name).FirstOrDefault();
            filter.VacationPeriods = UnitOfWork.GetSet<PupilGroupVacationPeriod>()
                .Where(ss => !ss.IsDeleted || ss.Id == filter.VacationPeriod)
                .ToDictionary(ss => ss.Id, sx => sx.Name);
        }

        /// <summary>
        ///     Поиск групп (потребностей) формирование запроса
        /// </summary>
        internal IQueryable<PupilGroup> PrepareQueryPupilGroup(OrphanageGroupsFilterModel filter)
        {
            var query = UnitOfWork.GetSet<PupilGroup>().AsQueryable();

            if (!filter.OrphanageId.HasValue)
            {
                var orgs = AccessRightEnum.Orphans.PupilGroup.GetSecurityOrganiztion();
                if (orgs.Length > 0)
                {
                    query = query.Where(c => orgs.Contains(c.OrganizationId));
                }
            }
            else
            {
                query = query.Where(ss => ss.OrganizationId == filter.OrphanageId);
            }

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                var nameArr = filter.Name.Split(' ');
                foreach (var name in nameArr.Where(ss => !string.IsNullOrWhiteSpace(ss)))
                {
                    query = query.Where(ss => ss.Name.ToLower().Contains(name.ToLower()));
                }
            }

            if (filter.YearOfRest.HasValue && filter.YearOfRest.Value > 0)
            {
                query = query.Where(ss => ss.YearOfRestId == filter.YearOfRest);
            }

            if (filter.FormOfRest.HasValue && filter.FormOfRest.Value > 0)
            {
                query = query.Where(ss => ss.FormOfRestId == filter.FormOfRest);
            }

            if (filter.RegionOfRest.HasValue && filter.RegionOfRest.Value > 0)
            {
                query = query.Where(ss => ss.Requests.Any(sx => sx.PlaceOfRestId == filter.RegionOfRest));
            }

            if (filter.StateId.HasValue && filter.StateId.Value > 0)
            {
                query = query.Where(ss => ss.StateId == filter.StateId);
            }
            else
            {
                query = query.Where(ss => ss.StateId != StateMachineStateEnum.PupilGroup.Deleted);
            }

            if (filter.TimeOfRest.HasValue && filter.TimeOfRest.Value > 0)
            {
                query = query.Where(ss => ss.Requests.Any(sx => sx.TimeOfRestId == filter.TimeOfRest));
            }

            if (filter.VacationPeriod.HasValue && filter.VacationPeriod.Value > 0)
            {
                query = query.Where(ss => ss.VacationPeriodId == filter.VacationPeriod);
            }

            query = query.OrderBy(ss => ss.Id);

            return query;
        }

        /// <summary>
        ///     Наполнить справочники группы
        /// </summary>
        internal void FillGroup(OrphanageGroupModel model)
        {
            model.YearsOfRest = UnitOfWork.GetSet<YearOfRest>()
                .Where(ss => !ss.IsClosed || ss.Id == model.Data.YearOfRestId).OrderBy(ss => ss.Name)
                .ToDictionary(ss => ss.Id.ToString(), sx => sx.Name);
            model.FormsOfRest = UnitOfWork.GetSet<FormOfRest>()
                .Where(ss => !ss.IsDeleted || ss.Id == model.FormOfRestId)
                .ToDictionary(ss => ss.Id.ToString(), sx => sx.Name);
            model.VacationPeriods = UnitOfWork.GetSet<PupilGroupVacationPeriod>()
                .Where(ss => !ss.IsDeleted || ss.Id == model.FormOfRestId)
                .ToDictionary(ss => ss.Id.ToString(), sx => sx.Name);
        }

        /// <summary>
        ///     Поиск списка/группы отправки
        /// </summary>
        internal void FillPupilGroupList(OrphanagePupilGroupListFilterModel filter, int PageSize = -1)
        {
            var query = PrepareQueryPupilGroupList(filter, PageSize);

            var totalCount = query.Count();

            if (PageSize > 0)
            {
                var pageNumber = filter.PageNumber;
                var startRecord = (pageNumber - 1) * PageSize;

                query = query.Skip(startRecord).Take(PageSize);
            }

            var result = query.ToList();

            filter.Result = new CommonPagedList<OrphanagePupilGroupListResultListModel>(result.Select(ss =>
                new OrphanagePupilGroupListResultListModel
                {
                    Id = ss.Id,
                    OrphanageName = ss.PupilGroupRequest?.FirstOrDefault()?.PupilGroup?.Organization?.Name,
                    GroupName = ss.PupilGroupRequest?.FirstOrDefault()?.PupilGroup?.Name,
                    YearOfRest = ss.PupilGroupRequest?.FirstOrDefault()?.PupilGroup?.YearOfRest?.Name,
                    FormOfRest = ss.PupilGroupRequest?.FirstOrDefault()?.PupilGroup?.FormOfRest?.Name,
                    TourName = ss.PupilGroupRequest?.FirstOrDefault()?.Tour?.Name,
                    StateName = ss.State?.Name
                }).ToList(), filter.PageNumber, PageSize > 0 ? PageSize : Math.Max(10, totalCount), totalCount);

            filter.YearsOfRest = UnitOfWork.GetSet<YearOfRest>().Where(ss => !ss.IsClosed || ss.Id == filter.YearOfRest)
                .ToDictionary(ss => ss.Id, sx => sx.Name);
            filter.FormsOfRest = UnitOfWork.GetSet<FormOfRest>().Where(ss => !ss.IsDeleted)
                .ToDictionary(ss => ss.Id, sx => sx.Name);
            filter.States = UnitOfWork.GetSet<StateMachineState>()
                .Where(ss =>
                    ss.StateMachineId == (long)StateMachineEnum.LimitListState &&
                    ss.Id >= StateMachineStateEnum.PupilGroupList.Formation)
                .ToDictionary(ss => ss.Id, sx => sx.Name);
            filter.OrphanageName = UnitOfWork.GetSet<Organization>().Where(ss => ss.Id == filter.OrphanageId)
                .Select(ss => ss.Name).FirstOrDefault();
        }

        /// <summary>
        ///     Поиск списка (группы отправки) формирование запроса
        /// </summary>
        internal IQueryable<ListOfChilds> PrepareQueryPupilGroupList(OrphanagePupilGroupListFilterModel filter,
            int pageSize = -1)
        {
            var query = UnitOfWork.GetSet<ListOfChilds>().Where(ss =>
                ss.StateId != StateMachineStateEnum.PupilGroupList.Deleted && ss.PupilGroupRequest.Any()).AsQueryable();

            if (!filter.OrphanageId.HasValue)
            {
                var orgs = AccessRightEnum.Orphans.PupilGroupList.GetSecurityOrganiztion();
                if (orgs.Length > 0)
                {
                    query = query.Where(c =>
                        c.PupilGroupRequest.Any(sx => orgs.Contains(sx.PupilGroup.OrganizationId)));
                }
            }
            else
            {
                query = query.Where(ss =>
                    ss.PupilGroupRequest.Any(sx => sx.PupilGroup.OrganizationId == filter.OrphanageId));
            }

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                var nameArr = filter.Name.Split(' ');
                foreach (var name in nameArr.Where(ss => !string.IsNullOrWhiteSpace(ss)))
                {
                    query = query.Where(ss =>
                        ss.PupilGroupRequest.Any(sx => sx.PupilGroup.Name.ToLower().Contains(name.ToLower())));
                }
            }

            if (filter.YearOfRest.HasValue && filter.YearOfRest.Value > 0)
            {
                query = query.Where(ss =>
                    ss.PupilGroupRequest.Any(sx => sx.PupilGroup.YearOfRestId == filter.YearOfRest));
            }

            if (filter.FormOfRestId.HasValue && filter.FormOfRestId.Value > 0)
            {
                query = query.Where(ss =>
                    ss.PupilGroupRequest.Any(sx => sx.PupilGroup.FormOfRestId == filter.FormOfRestId));
            }

            if (filter.StateId.HasValue && filter.StateId.Value > 0)
            {
                query = query.Where(ss => ss.StateId == filter.StateId);
            }

            query = query.OrderBy(ss => ss.Id);

            return query;
        }

        /// <summary>
        ///     Размещения для групп (потребностей)
        /// </summary>
        public IList<BaseResponse> GetTours(long orphanageRequestForPeriodOfRestId)
        {
            var group = UnitOfWork.GetSet<RequestForPeriodOfRest>().GetById(orphanageRequestForPeriodOfRestId);
            var res =
                UnitOfWork.GetSet<Tour>()
                    .Where(t => t.IsActive
                                && t.StateId == StateMachineStateEnum.Tour.Formed
                                && t.YearOfRestId == group.PupilGroup.YearOfRestId
                                && t.TypeOfRestId == group.PupilGroup.FormOfRest.TypeOfRestId
                                && t.Hotels.PlaceOfRestId == group.PlaceOfRestId
                                && t.TimeOfRestId == group.TimeOfRestId)
                    .OrderBy(p => p.Name)
                    .Select(ss => new BaseResponse { Id = ss.Id, Name = ss.Hotels.Name + " " + ss.Name })
                    .ToList();

            return res;
        }

        /// <summary>
        ///     Периоды отдыха для групп (потребностей)
        /// </summary>
        public IList<BaseResponse> GetPupilGroupTimesOfRest(long orphanagePupilGroupId)
        {
            var group = UnitOfWork.GetSet<PupilGroup>().GetById(orphanagePupilGroupId);
            var res =
                UnitOfWork.GetSet<TimeOfRest>()
                    .Where(t => t.IsActive && t.YearOfRestId == group.YearOfRestId &&
                                t.TypeOfRestId == group.FormOfRest.TypeOfRestId)
                    .OrderBy(p => p.Name)
                    .Select(ss => new BaseResponse { Id = ss.Id, Name = ss.Name })
                    .ToList();

            return res;
        }

        public IList<BaseResponse> GetOrphanageOrganisations(string query)
        {
            var orgs = UnitOfWork.GetSet<Organization>().Where(ss => !ss.IsDeleted && ss.Orphanage == true)
                .AsQueryable();
            if (!string.IsNullOrEmpty(query))
            {
                orgs = orgs.Where(ss => ss.Name.ToLower().Contains(query.ToLower()));
            }

            return orgs.OrderBy(p => p.Name).Select(ss => new BaseResponse { Id = ss.Id, Name = ss.Name }).ToList();
        }
    }
}
