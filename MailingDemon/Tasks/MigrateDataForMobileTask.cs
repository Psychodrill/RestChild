using System;
using System.Linq;
using System.Xml.Serialization;
using MailingDemon.Services;
using RestChild.Booking.Logic.Services;
using RestChild.Comon.Enumeration;
using RestChild.Mobile.DAL;
using RestChild.Mobile.DAL.Enum;
using RestChild.Mobile.Domain;

namespace MailingDemon.Tasks
{
    /// <summary>
    ///     миграция данных в БД мобильного приложения
    /// </summary>
    [Task]
    public class MigrateDataForMobileTask : BaseTask
    {
        public static string SubServicePath = "/Exchange/api.svc";
        [XmlElement("config")] public ExchangeDataConfig Config { get; set; }

        /// <summary>
        ///     получение клиента
        /// </summary>
        public static IExchangeServiceEx GetServiceClient(string urlForRequest)
        {
            return new ExchangeServiceExImplementation(urlForRequest);
        }

        /// <summary>
        ///     тело задачи на миграцию данных
        /// </summary>
        protected override void Execute()
        {
            Logger.Info("MigrateDataForMobileTask started");
            if (string.IsNullOrWhiteSpace(Config?.SourceUrl))
            {
                Logger.Info("Stop MigrateDataForMobileTask task: Config is null");
                return;
            }

            var sourceClient = GetServiceClient(Config.SourceUrl + SubServicePath);
            try
            {
                ProcessCamp(sourceClient);

                ProcessPersonal(sourceClient, PersonalTypeEnum.Counselor);

                ProcessPersonal(sourceClient, PersonalTypeEnum.AdministratorTour);

                ProcessBout(sourceClient);

                ProcessParty(sourceClient);

                ProcessCamper(sourceClient);
            }
            catch (Exception ex)
            {
                Logger.Error("MigrateDataForMobileTask error", ex);
                throw;
            }

            Logger.Info("MigrateDataForMobileTask finish");
        }

        /// <summary>
        ///     обработка персонала
        /// </summary>
        private void ProcessPersonal(IExchangeServiceEx sourceClient, long personalType)
        {
            using (var mobile = new MobileUnitOfWork())
            {
                mobile.NotUpdateLut = true;

                Logger.Info($"MigrateDataForMobileTask.ProcessPersonal personalType={personalType} started");
                var lastLut = mobile.GetSet<Personal>().Where(p => p.PersonalTypeId == personalType)
                    .Select(e => e.LastUpdateTick).DefaultIfEmpty().Max();
                var param = new ExchangeRequest
                {
                    Key = ExchangeService.Key,
                    LastUpdateTick = lastLut,
                    PersonalTypeId = personalType
                };

                var toRemove = sourceClient.GetPersonalToRemove(param);
                var campsToRemove = personalType == PersonalTypeEnum.Counselor
                    ? mobile.GetSet<Personal>()
                        .Where(c => toRemove.Contains(c.ExternalUidCounselor ?? 0) && c.EidSendStatus != 100 &&
                                    c.PersonalTypeId == personalType)
                        .ToList()
                    : mobile.GetSet<Personal>()
                        .Where(c => toRemove.Contains(c.ExternalUidAdministrator ?? 0) && c.EidSendStatus != 100 &&
                                    c.PersonalTypeId == personalType)
                        .ToList();

                foreach (var remove in campsToRemove)
                {
                    remove.StateId = StateMachineStateEnum.Deleted;
                }

                mobile.SaveChanges();

                mobile.BeginTransaction();

                bool process;
                do
                {
                    process = false;
                    var personals = sourceClient.GetPersonal(param);
                    var ids = personalType == PersonalTypeEnum.AdministratorTour
                        ? personals.Select(a => a.ExternalUidAdministrator).ToArray()
                        : personals.Select(a => a.ExternalUidCounselor).ToArray();

                    var presents = personalType == PersonalTypeEnum.AdministratorTour
                        ? mobile.GetSet<Personal>().Where(b => ids.Contains(b.ExternalUidAdministrator)).ToList()
                        : mobile.GetSet<Personal>().Where(b => ids.Contains(b.ExternalUidCounselor)).ToList();

                    foreach (var personal in personals)
                    {
                        var present = personalType == PersonalTypeEnum.AdministratorTour
                            ? presents.FirstOrDefault(p =>
                                p.ExternalUidAdministrator == personal.ExternalUidAdministrator)
                            : presents.FirstOrDefault(p => p.ExternalUidCounselor == personal.ExternalUidCounselor);

                        personal.StateId = personal.StateId ?? StateMachineStateEnum.Deleted;

                        if (personal.StateId != StateMachineStateEnum.Deleted)
                        {
                            if (personal.StateId < 12000 && personalType == PersonalTypeEnum.Counselor)
                            {
                                personal.StateId += 12000;
                            }

                            if (personal.StateId < 20000 && personalType == PersonalTypeEnum.AdministratorTour)
                            {
                                personal.StateId += 20000;
                            }
                        }

                        if (present == null)
                        {
                            mobile.AddEntity(personal);
                        }
                        else
                        {
                            present.Name = personal.Name;
                            present.Email = personal.Email;
                            present.Male = personal.Male;
                            present.Phone = personal.Phone;
                            present.StateId = personal.StateId;
                            present.LastUpdateTick = personal.LastUpdateTick;
                            if (present.Account != null)
                            {
                                present.Account.Male = personal.Male;
                            }
                        }

                        process = true;
                        param.Id = (personalType == PersonalTypeEnum.AdministratorTour
                            ? personal.ExternalUidAdministrator
                            : personal.ExternalUidCounselor) ?? param.Id;
                    }

                    mobile.SaveChanges();
                    mobile.DetachAllEntitys();
                } while (process);

                mobile.Commit();

                Logger.Info($"MigrateDataForMobileTask.ProcessPersonal personalType={personalType} finish");
            }
        }

        /// <summary>
        ///     обработка заезда
        /// </summary>
        private void ProcessBout(IExchangeServiceEx sourceClient)
        {
            using (var mobile = new MobileUnitOfWork())
            {
                mobile.NotUpdateLut = true;

                Logger.Info("MigrateDataForMobileTask.ProcessBout started");
                var lastLut = mobile.GetSet<Bout>().Select(e => e.LastUpdateTick).DefaultIfEmpty().Max();
                var param = new ExchangeRequest
                {
                    Key = ExchangeService.Key,
                    LastUpdateTick = lastLut
                };
                var toRemove = sourceClient.GetBoutToRemove(param);
                var campsToRemove = mobile.GetSet<Bout>()
                    .Where(c => toRemove.Contains(c.Id) && c.EidSendStatus != 100).ToList();
                foreach (var remove in campsToRemove)
                {
                    remove.EidSendStatus = 100;
                    remove.StateId = StateEnum.Deleted;
                }

                mobile.SaveChanges();

                bool process;
                mobile.BeginTransaction();
                do
                {
                    process = false;
                    var bouts = sourceClient.GetBouts(param);
                    var ids = bouts.Select(a => a.Id).ToArray();
                    var presentBouts = mobile.GetSet<Bout>().Where(b => ids.Contains(b.Id)).ToList();

                    foreach (var bout in bouts)
                    {
                        if (bout.StateId != StateMachineStateEnum.Deleted)
                        {
                            bout.StateId += 200;
                        }

                        bout.StateId = bout.StateId ?? StateMachineStateEnum.Deleted;

                        var present = presentBouts.FirstOrDefault(p => p.Id == bout.Id);

                        var personals = bout.Personals.ToList();
                        bout.Personals = null;

                        if (present == null)
                        {
                            mobile.AddEntity(new Bout
                            {
                                Id = bout.Id,
                                CampId = bout.CampId,
                                Change = bout.Change,
                                Name = bout.Name,
                                DateIncome = bout.DateIncome,
                                DateOutcome = bout.DateOutcome,
                                YearOfCompany = bout.YearOfCompany,
                                StateId = bout.StateId,
                                GroupedTimeId = bout.GroupedTimeId,
                                LastUpdateTick = bout.LastUpdateTick
                            });
                        }
                        else
                        {
                            present.Name = bout.Name;
                            present.Change = bout.Change;
                            present.CampId = bout.CampId;
                            present.DateIncome = bout.DateIncome;
                            present.DateOutcome = bout.DateOutcome;
                            present.YearOfCompany = bout.YearOfCompany;
                            present.LastUpdateTick = bout.LastUpdateTick;
                            present.StateId = bout.StateId;
                            present.GroupedTimeId = bout.GroupedTimeId;
                        }

                        var exists = mobile.GetSet<BoutPersonal>().Where(b => b.BoutId == bout.Id).ToList();

                        var maxId = mobile.GetSet<BoutPersonal>().Select(e => e.Id).DefaultIfEmpty().Max();

                        foreach (var target in personals)
                        {
                            var personal = new BoutPersonal
                            {
                                BoutId = bout.Id,
                                LastUpdateTick = bout.LastUpdateTick
                            };

                            target.BoutId = bout.Id;
                            var inDbPersonal = target.PersonalTypeId == PersonalTypeEnum.Counselor
                                ? mobile.GetSet<Personal>()
                                    .FirstOrDefault(p => p.ExternalUidCounselor == target.PersonalId)
                                : mobile.GetSet<Personal>()
                                    .FirstOrDefault(p => p.ExternalUidAdministrator == target.PersonalId);

                            personal.PersonalId = inDbPersonal?.Id;

                            if (target.PartyId.HasValue)
                            {
                                if (!mobile.GetSet<Party>().Any(p => p.Id == target.PartyId))
                                {
                                    mobile.AddEntity(new Party {Id = target.PartyId.Value, BoutId = bout.Id});
                                }

                                personal.PartyId = target.PartyId;
                            }

                            var item = exists.FirstOrDefault(p =>
                                p.BoutId == personal.BoutId && p.PartyId == personal.PartyId &&
                                p.PersonalId == personal.PersonalId);
                            if (item != null)
                            {
                                exists.Remove(item);
                            }
                            else
                            {
                                personal.Id = ++maxId;
                                mobile.AddEntity(personal);
                            }
                        }

                        foreach (var itemToRemove in exists)
                        {
                            mobile.Delete(itemToRemove);
                        }

                        process = true;
                        param.Id = bout.Id;
                    }

                    mobile.SaveChanges();
                    mobile.DetachAllEntitys();
                } while (process);

                mobile.Commit();
                Logger.Info("MigrateDataForMobileTask.ProcessBout finish");
            }
        }

        /// <summary>
        ///     обработка отдыхающих
        /// </summary>
        private void ProcessParty(IExchangeServiceEx sourceClient)
        {
            using (var mobile = new MobileUnitOfWork())
            {
                mobile.NotUpdateLut = true;

                Logger.Info("MigrateDataForMobileTask.ProcessParty started");
                var lastLut = mobile.GetSet<Party>().Select(e => e.LastUpdateTick).DefaultIfEmpty().Max();
                var param = new ExchangeRequest
                {
                    Key = ExchangeService.Key,
                    LastUpdateTick = lastLut
                };
                var toRemove = sourceClient.GetPartyToRemove(param);
                var campsToRemove = mobile.GetSet<Party>()
                    .Where(c => toRemove.Contains(c.Id) && c.EidSendStatus != 100).ToList();
                foreach (var remove in campsToRemove)
                {
                    remove.Campers.Clear();
                    remove.Peronals.Clear();
                    mobile.SaveChanges();
                    mobile.Delete(remove);
                }

                mobile.SaveChanges();

                mobile.BeginTransaction();
                bool process;
                do
                {
                    process = false;
                    var parties = sourceClient.GetParty(param);
                    var ids = parties.Select(a => a.Id).ToArray();
                    var presentParties = mobile.GetSet<Party>().Where(b => ids.Contains(b.Id)).ToList();

                    foreach (var party in parties)
                    {
                        var present = presentParties.FirstOrDefault(p => p.Id == party.Id);

                        party.StateId = party.StateId ?? StateMachineStateEnum.Deleted;

                        if (party.StateId != StateMachineStateEnum.Deleted)
                        {
                            party.StateId += 200;
                        }


                        if (present == null)
                        {
                            mobile.AddEntity(party);
                        }
                        else
                        {
                            present.Name = party.Name;
                            present.SortOrder = party.SortOrder;
                            present.BoutId = party.BoutId;
                            present.StateId = party.StateId;
                            present.LastUpdateTick = party.LastUpdateTick;
                        }

                        mobile.SaveChanges();
                        param.Id = party.Id;
                        process = true;
                    }

                    mobile.DetachAllEntitys();
                } while (process);

                mobile.Commit();
                Logger.Info("MigrateDataForMobileTask.ProcessParty finish");
            }
        }

        /// <summary>
        ///     обработка отдыхающих
        /// </summary>
        private void ProcessCamper(IExchangeServiceEx sourceClient)
        {
            using (var mobile = new MobileUnitOfWork())
            {
                mobile.NotUpdateLut = true;

                Logger.Info("MigrateDataForMobileTask.ProcessCamper started");
                var lastLut = mobile.GetSet<Camper>().Select(e => e.LastUpdateTick).DefaultIfEmpty().Max();
                var param = new ExchangeRequest
                {
                    Key = ExchangeService.Key,
                    LastUpdateTick = lastLut
                };
                var toRemove = sourceClient.GetCamperToRemove(param);
                var campersToRemove = mobile.GetSet<Camper>()
                    .Where(c => toRemove.Contains((long)c.ChildExtMgtUid) && c.EidSendStatus != 100).ToList();
                foreach (var remove in campersToRemove)
                {
                    remove.EidSendStatus = 100;
                }

                mobile.SaveChanges();

                mobile.BeginTransaction();
                bool process;
                do
                {
                    process = false;
                    var campers = sourceClient.GetCampers(param);
                    var ids = campers.Select(a => a.ChildExtMgtUid).ToArray();
                    var presentCampers = mobile.GetSet<Camper>().Where(b => ids.Contains(b.ChildExtMgtUid)).ToList();

                    foreach (var camper in campers.OrderBy(ss => ss.LastUpdateTick).ToList())
                    {
                        var present = presentCampers.FirstOrDefault(p => p.ChildExtMgtUid == camper.ChildExtMgtUid);

                        if (present == null)
                        {
                            camper.Id = camper.ChildExtCampUid ?? 0;
                            mobile.AddEntity(camper);
                        }
                        else
                        {
                            present.Name = camper.Name;
                            present.DateOfBirth = camper.DateOfBirth;
                            present.BoutId = camper.BoutId;
                            present.PartyId = camper.PartyId;
                            present.Male = camper.Male;
                            present.Email = camper.Email;
                            present.Phone = camper.Phone;
                            present.AttendantExtCampUid = camper.AttendantExtCampUid;
                            present.AttendantExtMgrUid = camper.AttendantExtMgrUid;
                            present.ChildExtCampUid = camper.ChildExtCampUid;
                            present.ChildExtMgtUid = camper.ChildExtMgtUid;
                            present.LastUpdateTick = camper.LastUpdateTick;
                            if (present.Account != null)
                            {
                                present.Account.DateOfBirth = camper.DateOfBirth;
                                present.Account.Male = camper.Male ?? false;

                                if (string.IsNullOrWhiteSpace(present.Account.Phone))
                                {
                                    present.Account.Phone = camper.Phone;
                                }

                                if (string.IsNullOrWhiteSpace(present.Account.Email))
                                {
                                    present.Account.Email = camper.Email;
                                }
                            }
                        }

                        mobile.SaveChanges();
                        process = true;
                        param.LastUpdateTick = camper.LastUpdateTick;
                    }

                    mobile.DetachAllEntitys();
                } while (process);

                mobile.Commit();
                Logger.Info("MigrateDataForMobileTask.ProcessCamper finish");
            }
        }

        /// <summary>
        ///     обработка лагеря
        /// </summary>
        private void ProcessCamp(IExchangeServiceEx sourceClient)
        {
            Logger.Info("MigrateDataForMobileTask.ProcessCamp started");
            using (var mobile = new MobileUnitOfWork())
            {
                mobile.NotUpdateLut = true;
                var lastLut = mobile.GetSet<Camp>().Select(e => e.LastUpdateTick).DefaultIfEmpty().Max();
                var param = new ExchangeRequest
                {
                    Key = ExchangeService.Key,
                    LastUpdateTick = lastLut
                };

                var toRemove = sourceClient.GetCampToRemove(param);
                var campsToRemove = mobile.GetSet<Camp>()
                    .Where(c => toRemove.Contains(c.Id) && c.EidSendStatus != 100).ToList();
                foreach (var remove in campsToRemove)
                {
                    remove.StateId = StateMachineStateEnum.Deleted;
                }

                mobile.SaveChanges();

                mobile.BeginTransaction();

                bool process;
                do
                {
                    process = false;
                    var camps = sourceClient.GetCamps(param);
                    var ids = camps.Select(a => a.Id).ToArray();
                    var presentCamps = mobile.GetSet<Camp>().Where(b => ids.Contains(b.Id)).ToList();

                    foreach (var camp in camps.OrderBy(ss => ss.LastUpdateTick).ToList())
                    {
                        camp.StateId = camp.StateId ?? StateMachineStateEnum.Deleted;

                        if (camp.StateId != StateMachineStateEnum.Deleted)
                        {
                            camp.StateId += 200;
                        }

                        var present = presentCamps.FirstOrDefault(p => p.Id == camp.Id);

                        if (present == null)
                        {
                            mobile.AddEntity(camp);
                        }
                        else
                        {
                            present.Name = camp.Name;
                            present.HotelTypeId = camp.HotelTypeId;
                            present.Address = camp.Address;
                            present.NearestCity = camp.NearestCity;
                            present.LastUpdateTick = camp.LastUpdateTick;
                            present.StateId = camp.StateId;
                            present.Eid = camp.Eid;
                        }

                        mobile.SaveChanges();
                        param.LastUpdateTick = camp.LastUpdateTick;
                        process = true;
                    }

                    mobile.DetachAllEntitys();
                } while (process);

                mobile.Commit();
                Logger.Info("MigrateDataForMobileTask.ProcessCamp finish");
            }
        }
    }
}
