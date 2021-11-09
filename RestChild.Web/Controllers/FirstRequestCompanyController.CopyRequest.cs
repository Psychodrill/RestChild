using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using RestChild.Booking.Logic.Extensions;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Models;

namespace RestChild.Web.Controllers
{
    /// <summary>
    ///     создание копии заявления
    /// </summary>
    public partial class FirstRequestCompanyController
    {
        /// <summary>
        ///     копирование заявления в сертификаты
        /// </summary>
        public async Task<ActionResult> CreateRequestOnNextYear(long id)
        {
            var source = await UnitOfWork.GetByIdAsync<Request>(id, CancellationToken.None);

            if (source == null)
            {
                return RedirectToAction("RequestList");
            }

            if (source.StatusId != (long)StatusEnum.DecisionMakingCovid
                || !Security.HasRight(AccessRightEnum.Status.FcToDecisionMakingCovid))
            {
                return RedirectToAction("RequestEdit", new { id = source.Id });
            }

            var currentAccountId = Security.GetCurrentAccountId();

            var newYear = source.YearOfRest.Year + 1;
            var nextYear = await UnitOfWork.GetSet<YearOfRest>()
                .FirstOrDefaultAsync(r => r.Year == newYear, CancellationToken.None);

            if (nextYear == null)
            {
                return RedirectToAction("RequestEdit", new { id = source.Id });
            }

            var newEntity = await UnitOfWork.AddEntityAsync(new Request(source)
            {
                Id = 0,
                Applicant = new Applicant(source.Applicant)
                {
                    Id = 0,
                    AddressId = null,
                    Address = source.Applicant?.Address != null ? new Address(source.Applicant.Address) : null,
                    EntityId = null,
                    BoutId = null,
                    IncludeReasonId = null,
                    TourVolumeId = null,
                    Eid = null
                },
                Agent = source.Agent != null
                    ? new Agent(source.Agent)
                    {
                        Id = 0,
                        Eid = null
                    }
                    : null,
                YearOfRestId = nextYear.Id,
                StatusId = (long)StatusEnum.IncludedInList,
                ApplicantId = null,
                AgentId = null,
                CreateUserId = currentAccountId,
                ParentRequestId = source.Id,
                Eid = null,
                HistoryLinkId = null,
                TimeOfRestId = null,
                EntityId = null,
                TourId = null,
                BookingGuid = null,
                HotelsId = null,
                DateIncome = null,
                DateOutcome = null,
                DateRequest = null,
                RequestNumber = null,
                RequestNumberMpgu = null,
                Repared = false,
                SourceId = (long)SourceEnum.Operator,
                NeedEmail = true,
                UpdateDate = DateTime.Now,
                CertificateDate = null,
                CertificateNumber = null
            }, CancellationToken.None);

            foreach (var attendant in source.Attendant ?? new List<Applicant>())
            {
                await UnitOfWork.AddEntityAsync(new Applicant(attendant)
                {
                    RequestId = newEntity.Id,
                    EntityId = null,
                    Id = 0,
                    BoutId = null,
                    TourVolumeId = null,
                    Address = attendant?.Address != null ? new Address(attendant.Address) : null,
                    AddressId = null
                }, CancellationToken.None);
            }

            foreach (var child in source.Child ?? new List<Child>())
            {
                await UnitOfWork.AddEntityAsync(new Child(child)
                {
                    RequestId = newEntity.Id,
                    EntityId = null,
                    Id = 0,
                    BoutId = null,
                    TourVolumeId = null,
                    Address = child?.Address != null ? new Address(child.Address) : null,
                    AddressId = null,
                    ToursId = null,
                    YearOfCompany = nextYear.Year
                }, CancellationToken.None);
            }

            foreach (var place in source.PlacesOfRest ?? new List<RequestPlaceOfRest>())
            {
                await UnitOfWork.AddEntityAsync(new RequestPlaceOfRest(place)
                {
                    RequestId = newEntity.Id,
                    Id = 0
                }, CancellationToken.None);
            }

            UnitOfWork.DetachAllEntitys();
            var saved = await UnitOfWork.GetByIdAsync<Request>(newEntity.Id, CancellationToken.None);
            saved.DateChangeStatus = DateTime.Now;
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);
            saved = UnitOfWork.SendRequest(saved);
            UnitOfWork.WriteHistory(saved.Id,
                $"Выпуск заявления на отдых на {nextYear.Name}г. вместо неиспользованной путёвки по заявлению {source.RequestNumber}",
                currentAccountId);

            source = await UnitOfWork.GetByIdAsync<Request>(source.Id, CancellationToken.None);
            source.StatusId = (long)StatusEnum.Reject;
            source.DeclineReasonId = (long)DeclineReasonEnum.CertificateIssued;
            UnitOfWork.WriteHistory(source.Id,
                $"Выпуск путёвки на отдых в {nextYear.Name}г. вместо неиспользованной путёвки.",
                currentAccountId);
            UnitOfWork.SendChangeStatusByEvent(source, RequestEventEnum.SendCertificateIssuedByParent);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            return RedirectToAction("RequestEdit", new { id = newEntity.Id });
        }

        /// <summary>
        ///     копирование заявления в сертификаты
        /// </summary>
        public async Task<ActionResult> CreateCertificateRequest(long id)
        {
            var source = await UnitOfWork.GetByIdAsync<Request>(id, CancellationToken.None);

            if (source == null)
            {
                return RedirectToAction("RequestList");
            }

            if (source.StatusId != (long)StatusEnum.DecisionMakingCovid
                || !Security.HasRight(AccessRightEnum.Status.FcToDecisionMakingCovid))
            {
                return RedirectToAction("RequestEdit", new { id = source.Id });
            }

            var result = new List<Request>();

            // сформировали коллекцию заявлений для копирования
            var currentAccountId = Security.GetCurrentAccountId();
            if (source.Child.Any())
            {
                foreach (var child in source.Child)
                {
                    var request = new Request
                    {
                        Applicant = new Applicant(source.Applicant)
                        {
                            Id = 0,
                            AddressId = null,
                            Address = child.Address != null ? new Address(child.Address) : null,
                            EntityId = null,
                            BoutId = null,
                            IncludeReasonId = null,
                            TourVolumeId = null
                        },
                        TypeOfRestId = child.DateOfBirth.GetAgeInYears() < 7
                            ? (long)TypeOfRestEnum.MoneyOn3To7
                            : (long)TypeOfRestEnum.MoneyOn7To15,
                        StatusId = (long)StatusEnum.CertificateIssued,
                        YearOfRestId = source.YearOfRestId,
                        CountAttendants = 0,
                        RequestOnMoney = true,
                        ParentRequestId = source.Id,
                        IsFirstCompany = true,
                        NeedEmail = true,
                        NeedSms = false,
                        CreateUserId = currentAccountId,
                        SourceId = (long)SourceEnum.Operator,
                        StatusApplicant = source.StatusApplicant,
                        SsoId = source.SsoId,
                        IsLast = true,
                        IsDraft = false,
                        MainPlaces = 1,
                        Attendant = new List<Applicant>(),
                        Commentary = source.Commentary,
                        AgentApplicant = source.AgentApplicant
                    };

                    if (source.Agent != null)
                    {
                        request.Agent = new Agent(source.Agent);
                    }

                    request.Child = new List<Child>
                    {
                        new Child(child)
                        {
                            Id = 0,
                            RequestId = null,
                            AddressId = null,
                            Address = child.Address != null ? new Address(child.Address) : null,
                            EntityId = null,
                            ToursId = null,
                            BoutId = null,
                            IncludeReasonId = null,
                            TourVolumeId = null
                        }
                    };

                    result.Add(request);
                }
            }
            else
            {
                var request = new Request
                {
                    Applicant = new Applicant(source.Applicant)
                    {
                        Id = 0,
                        AddressId = null,
                        Address = source.Applicant?.Address != null ? new Address(source.Applicant.Address) : null,
                        EntityId = null,
                        BoutId = null,
                        IncludeReasonId = null,
                        TourVolumeId = null
                    },
                    TypeOfRestId = (long)TypeOfRestEnum.MoneyOn18,
                    StatusId = (long)StatusEnum.CertificateIssued,
                    YearOfRestId = source.YearOfRestId,
                    CountAttendants = 0,
                    RequestOnMoney = true,
                    ParentRequestId = source.Id,
                    IsFirstCompany = true,
                    NeedEmail = true,
                    NeedSms = false,
                    CreateUserId = currentAccountId,
                    SourceId = (long)SourceEnum.Operator,
                    StatusApplicant = source.StatusApplicant,
                    SsoId = source.SsoId,
                    IsLast = true,
                    IsDraft = false,
                    MainPlaces = 1,
                    Attendant = new List<Applicant>(),
                    Child = new List<Child>(),
                    Commentary = source.Commentary,
                    AgentApplicant = source.AgentApplicant
                };

                if (source.Agent != null)
                {
                    request.Agent = new Agent(source.Agent);
                }

                result.Add(request);
            }

            foreach (var request in result)
            {
                var saved = ApiController.SaveRequest(request);
                UnitOfWork.DetachAllEntitys();

                saved = await UnitOfWork.GetByIdAsync<Request>(saved.Id, CancellationToken.None);
                saved.DateChangeStatus = DateTime.Now;
                UnitOfWork.SetCertificateNumber(saved);
                await UnitOfWork.SaveChangesAsync(CancellationToken.None);
                saved = UnitOfWork.SendRequest(saved);
                UnitOfWork.WriteHistory(request.Id,
                    $"Выпуск сертификата на отдых вместо неиспользованной путёвки по заявлению {source.RequestNumber}",
                    currentAccountId);
                UnitOfWork.SendChangeStatusByEvent(saved, RequestEventEnum.SendCertificateIssued);
                await UnitOfWork.SaveChangesAsync(CancellationToken.None);
            }

            source = await UnitOfWork.GetByIdAsync<Request>(source.Id, CancellationToken.None);
            source.StatusId = (long)StatusEnum.Reject;
            source.DeclineReasonId = (long)DeclineReasonEnum.CertificateIssued;
            UnitOfWork.WriteHistory(source.Id, "Выпуск сертификатов на отдых вместо неиспользованной путёвки.",
                currentAccountId);
            UnitOfWork.SendChangeStatusByEvent(source, RequestEventEnum.SendCertificateIssuedByParent);
            await UnitOfWork.SaveChangesAsync(CancellationToken.None);

            return RedirectToAction("RequestEdit", new { id = source.Id });
        }

        /// <summary>
        ///     Копирование данных из имеющегося заявления в новое
        /// </summary>
        [HttpPost]
        [Route("FirstRequestCompany/CopyRequestDataToNewRequest")]
        public long CopyRequestDataToNewRequest(RequestCopyModel model)
        {
            var sourceRequest = UnitOfWork.GetById<Request>(model.RequestId);
            var newRequest = new Request
            {
                Child = new List<Child>(),
                Attendant = new List<Applicant>(),
                Applicant = new Applicant(),
                Agent = new Agent(),
                Files = new List<RequestFile>(),
                IsLast = true,
                IsDeleted = false,
                IsDraft = false,
                Status = UnitOfWork.GetById<Status>((long)StatusEnum.Draft),
                StatusId = (long)StatusEnum.Draft,
                Version = 0,
                UpdateDate = DateTime.Now,
                SourceId = sourceRequest.SourceId,
                Source = sourceRequest.Source,
                IsFirstCompany = sourceRequest.IsFirstCompany
            };
            var requests = UnitOfWork.GetSet<Request>();
            requests.Add(newRequest);
            UnitOfWork.SaveChanges();

            // Общие сведения о заявлении
            if (model.TransferGeneralData)
            {
                newRequest.ChangeByScan = sourceRequest.ChangeByScan;
                newRequest.NeedSms = sourceRequest.NeedSms;
                newRequest.NeedEmail = sourceRequest.NeedEmail;
            }

            // Раздел "Цель обращения и время отдыха"
            newRequest.TypeOfRestId = sourceRequest.TypeOfRestId;
            newRequest.YearOfRestId = UnitOfWork.GetSet<YearOfRest>().FirstOrDefault(x => x.Year == DateTime.Now.Year)?.Id;
            if (model.TransferTargetAndTimeOfRestData)
            {
                newRequest.StatusApplicant = sourceRequest.StatusApplicant;
                newRequest.TransferTo = sourceRequest.TransferTo;
                newRequest.TransferFrom = sourceRequest.TransferFrom;
            }

            // Сведения о заявителе
            if (model.TransferApplicantData)
            {
                var copiedApplicant = new Applicant(sourceRequest.Applicant);
                copiedApplicant.Id = -1;
                copiedApplicant.RequestId = null;
                newRequest.Applicant = copiedApplicant;
            }

            // Сведения о представителе заявителя
            if (model.TransferAgentData)
            {
                var copiedAgent = new Agent(sourceRequest.Agent);
                copiedAgent.Id = -1;
                newRequest.Agent = copiedAgent;
            }

            // Сведения о сопровождающих
            if (model.AttendantsIds.Count > 0)
            {
                newRequest.CountAttendants = model.AttendantsIds.Count;
                var attendants = UnitOfWork.GetSet<Applicant>().Where(x => model.AttendantsIds.Any(a => a == x.Id)).ToList();
                foreach (var attendant in attendants)
                {
                    var copiedAttendant = new Applicant(attendant);
                    copiedAttendant.Id = -1;
                    copiedAttendant.RequestId = newRequest.Id;
                    newRequest.Attendant.Add(copiedAttendant);
                }
            }

            // Сведения о детях
            if (model.ChildrenIds.Count > 0)
            {
                newRequest.CountPlace = model.ChildrenIds.Count;
                var childs = UnitOfWork.GetSet<Child>().Where(x => model.ChildrenIds.Any(a => a == x.Id)).ToList();
                foreach (var child in childs)
                {
                    var copiedChild = new Child(child);
                    copiedChild.Id = -1;
                    copiedChild.RequestId = newRequest.Id;
                    newRequest.Child.Add(copiedChild);
                }
            }

            // Банковские реквизиты
            if (model.TransferBankData)
            {
                newRequest.BankName = sourceRequest.BankName;
                newRequest.BankInn = sourceRequest.BankInn;
                newRequest.BankKpp = sourceRequest.BankKpp;
                newRequest.BankBik = sourceRequest.BankBik;
                newRequest.BankAccount = sourceRequest.BankAccount;
                newRequest.BankCorr = sourceRequest.BankCorr;
                newRequest.BankCardNumber = sourceRequest.BankCardNumber;
                newRequest.BankLastName = sourceRequest.BankLastName;
                newRequest.BankFirstName = sourceRequest.BankFirstName;
                newRequest.BankMiddleName = sourceRequest.BankMiddleName;
            }

            // Документы
            if (model.TransferFilesData)
            {
                var sourceFiles = UnitOfWork.GetSet<RequestFile>().Where(x => x.RequestId == sourceRequest.Id);
                foreach (var file in sourceFiles)
                {
                    var copiedFile = new RequestFile(file);
                    copiedFile.Id = -1;
                    copiedFile.RequestId = newRequest.Id;
                    newRequest.Files.Add(copiedFile);
                }
            }
            UnitOfWork.SaveChanges();
            return newRequest.Id;
        }
    }
}
