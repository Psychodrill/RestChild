using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using RestChild.Comon;
using RestChild.Comon.Dto.Booking;
using RestChild.Comon.Enumeration;
using RestChild.Domain;
using RestChild.Web.Common;
using TimeOfRest = RestChild.Domain.TimeOfRest;

namespace RestChild.Web.Models
{
    /// <summary>
    ///     заявка
    /// </summary>
    public class RequestViewModel : ViewModelBase<Request>
    {
        /// <summary>
        ///     дубли по заявителям
        /// </summary>
        public ICollection<Request> ApplicantDouble;

        /// <summary>
        ///     дубли по сопровождению
        /// </summary>
        public ICollection<Applicant> BadAttendants;

        /// <summary>
        ///     дубли в одно время
        /// </summary>
        public ICollection<Applicant> CrossTimeAttendants;

        /// <summary>
        ///     дубли детей в одно время
        /// </summary>
        public ICollection<Child> CrossTimeChilds;

        public RequestViewModel()
            : base(new Request())
        {
            Child = new List<ChildViewModel>();
            Attendant = new List<ApplicantViewModel>();
            Applicant = new ApplicantViewModel {RequestModel = this};
            Agent = new AgentViewModel();
            FormBlocksValidness = new Dictionary<RequestViewBlockEnum, bool>();
            SimilarChildren = new List<Child>();
            SameChildren = new List<Child>();
            Actions = new List<StatusAction>();
            IsEditable = true;
            FileTypes = new List<RequestFileTypeModel>();
            TypeOfRooms = new List<TypeOfRoomViewModel>();
            InformationVouchers = new List<RequestInformationVoucherModel>();
        }

        public RequestViewModel(Request request)
            : base(request)
        {
            ForbidInsertChilds = request.TypeOfRestId != (long) TypeOfRestEnum.Compensation;
            ForbidInsertAttendants = request.TypeOfRestId != (long) TypeOfRestEnum.Compensation;
            Child = request.Child?.Select(s =>
                    new ChildViewModel(s)
                        {NotForDelete = ForbidInsertChilds, HasNotMiddlename = s.HaveMiddleName})
                .ToList() ?? new List<ChildViewModel>();

            var attendants = request.Attendant?.Where(a => !a.IsAgent).ToArray() ?? new Applicant[0];
            var agent = request.Attendant?.FirstOrDefault(a => a.IsAgent);
            Attendant = attendants.Select(s => new ApplicantViewModel(s)
            {
                RequestModel = this,
                HasNotMiddlename = s.HaveMiddleName,
                NotForDelete = ForbidInsertAttendants
            }).ToList();


            if (!request.TypeOfRest?.NeedAttendant ?? true)
            {
                ParentInvalid = Attendant.FirstOrDefault() ?? new ApplicantViewModel();
                Attendant = new List<ApplicantViewModel>();
            }

            AgentApplicant = request.AgentApplicant ?? false;
            Applicant = new ApplicantViewModel(request.Applicant ?? new Applicant())
            {
                HasNotMiddlename = request.NullSafe(r => r.Applicant.HaveMiddleName),
                RequestModel = this
            };

            Agent = new AgentViewModel(request.Agent, agent);

            FormBlocksValidness = new Dictionary<RequestViewBlockEnum, bool>();
            foreach (var child in Child)
            {
                if (Applicant?.Data.Id == child.Data.ApplicantId)
                {
                    child.AttendantGuid = Applicant.Guid;
                }
                else if (Agent?.DataApplicant?.Id == child.Data.ApplicantId)
                {
                    child.AttendantGuid = Agent.Guid;
                }
                else
                {
                    var attendant = Attendant.FirstOrDefault(x => x.Data.Id == child.Data.ApplicantId);
                    child.AttendantGuid = attendant?.Guid;
                }
            }

            InformationVouchers =
                request.InformationVouchers?.Select(i => new RequestInformationVoucherModel(i)).ToList() ??
                new List<RequestInformationVoucherModel>();

            foreach (var iv in InformationVouchers)
            {
                foreach (var ap in iv.AttendantsPrice)
                {
                    if (Applicant?.Data.Id == ap.ApplicantId)
                    {
                        ap.AttendantGuid = Applicant.Guid;
                    }
                    else if (Agent?.DataApplicant?.Id == ap.ApplicantId)
                    {
                        ap.AttendantGuid = Agent.Guid;
                    }
                    else
                    {
                        var attendant = Attendant.FirstOrDefault(x => x.Data.Id == ap.ApplicantId);
                        ap.AttendantGuid = attendant?.Guid;
                    }
                }
            }

            IsEditable = request.Status != null;
            Actions = new List<StatusAction>();
            TypeOfRooms = new List<TypeOfRoomViewModel>();

            TimeOfRestAddon1LinkId = request.TimesOfRest?.Where(ss => ss.Order == 1).Select(t => t.Id).FirstOrDefault();
            TimeOfRestAddon2LinkId = request.TimesOfRest?.Where(ss => ss.Order == 2).Select(t => t.Id).FirstOrDefault();
            TimeOfRestAddon1Id = request.TimesOfRest?.Where(ss => ss.Order == 1).Select(t => t.TimeOfRestId)
                .FirstOrDefault();
            TimeOfRestAddon2Id = request.TimesOfRest?.Where(ss => ss.Order == 2).Select(t => t.TimeOfRestId)
                .FirstOrDefault();
            TimeOfRestAddon1 = request.TimesOfRest?.Where(ss => ss.Order == 1).Select(t => t.TimeOfRest)
                .FirstOrDefault();
            TimeOfRestAddon2 = request.TimesOfRest?.Where(ss => ss.Order == 2).Select(t => t.TimeOfRest)
                .FirstOrDefault();

            PlaceOfRestAddon1LinkId =
                request.PlacesOfRest?.Where(ss => ss.Order == 1).Select(t => t.Id).FirstOrDefault();
            PlaceOfRestAddon2LinkId =
                request.PlacesOfRest?.Where(ss => ss.Order == 2).Select(t => t.Id).FirstOrDefault();
            PlaceOfRestAddon1Id = request.PlacesOfRest?.Where(ss => ss.Order == 1).Select(t => t.PlaceOfRestId)
                .FirstOrDefault();
            PlaceOfRestAddon2Id = request.PlacesOfRest?.Where(ss => ss.Order == 2).Select(t => t.PlaceOfRestId)
                .FirstOrDefault();
            PlaceOfRestAddon1 = request.PlacesOfRest?.Where(ss => ss.Order == 1).Select(t => t.PlaceOfRest)
                .FirstOrDefault();
            PlaceOfRestAddon2 = request.PlacesOfRest?.Where(ss => ss.Order == 2).Select(t => t.PlaceOfRest)
                .FirstOrDefault();
        }

        /// <summary>
        ///     можно редактировать место и время отдыха
        /// </summary>
        public bool CanEditRegionAndPlacement =>
            Data.StatusId == (long) StatusEnum.IncludedInList && Data.ParentRequestId != null;

        /// <summary>
        ///     заявитель
        /// </summary>
        public ApplicantViewModel Applicant { get; set; }

        /// <summary>
        ///     представитель заявителя
        /// </summary>
        public AgentViewModel Agent { get; set; }

        /// <summary>
        ///     ребенок
        /// </summary>
        public List<ChildViewModel> Child { get; set; }

        /// <summary>
        ///     родители/сопровождающие
        /// </summary>
        public List<ApplicantViewModel> Attendant { get; set; }

        /// <summary>
        ///     родители/сопровождающие
        /// </summary>
        public ApplicantViewModel ParentInvalid { get; set; }

        /// <summary>
        ///     Кем подается заявление
        /// </summary>
        public bool AgentApplicant { get; set; }

        /// <summary>
        ///     максимально допустимое количестов доп мест.
        /// </summary>
        public int MaxCountAddonPlace { get; set; }

        /// <summary>
        ///     Признак необходимости сохранения только прикрепленного файла.
        /// </summary>
        public bool? SaveFileOnly { get; set; }

        /// <summary>
        ///     доступна редактирование.
        /// </summary>
        public bool IsEditable { get; set; }

        /// <summary>
        ///     возможные действия
        /// </summary>
        public IList<StatusAction> Actions { get; set; }

        /// <summary>
        ///     поле для XML
        /// </summary>
        public string BenefitApproveHtml { get; set; }

        /// <summary>
        ///     похожие дети
        /// </summary>
        public IList<Child> BadChildren { get; set; }

        /// <summary>
        ///     похожие дети
        /// </summary>
        public IList<Child> SimilarChildren { get; set; }

        /// <summary>
        ///     Такие же дети
        /// </summary>
        public IList<Child> SameChildren { get; set; }

        /// <summary>
        ///     Такие же СНИЛС
        /// </summary>
        public IList<string> SameAttendantSnils { get; set; }

        /// <summary>
        ///     Такие же сопровождающие
        /// </summary>
        public IList<Applicant> SameAttendants { get; set; }

        /// <summary>
        ///     типы отдыха
        /// </summary>
        public IList<TypeOfRest> TypeOfRestsAll { get; set; }

        /// <summary>
        ///     виды ограничений
        /// </summary>
        public IList<TypeOfRestriction> TypeOfRestrictions { get; set; }

        /// <summary>
        ///     времена отдыха
        /// </summary>
        public IList<TimeOfRest> TimeOfRests { get; set; }

        /// <summary>
        ///     виды льготы
        /// </summary>
        public IList<BenefitType> BenefitTypes { get; set; }

        /// <summary>
        ///     текущие заявочные кампании.
        /// </summary>
        public IList<YearOfRest> RequestCurrentPeriod { get; set; }

        /// <summary>
        ///     ограничение на вид льготы и категорию
        /// </summary>
        public IList<TypeOfRestBenefitRestriction> TypeOfRestBenefitRestrictions { get; set; }

        /// <summary>
        ///     статус по отношению к ребенку
        /// </summary>
        public IList<StatusByChild> StatusByChild { get; set; }

        /// <summary>
        ///     Причины отказа.
        /// </summary>
        public IList<DeclineReason> DeclineReason { get; set; }

        /// <summary>
        ///     Корректность введенной информации (по блокам)
        /// </summary>
        public IDictionary<RequestViewBlockEnum, bool> FormBlocksValidness { get; set; }

        public BookingRequest BookingRequest { get; set; }

        /// <summary>
        ///     Запретить добавление детей
        /// </summary>
        public bool? ForbidInsertChilds { get; set; }

        /// <summary>
        ///     Запретить добавление сопровождающих
        /// </summary>
        public bool? ForbidInsertAttendants { get; set; }

        /// <summary>
        ///     Количество дополнительных мест
        /// </summary>
        public int AddonPlacesCount { get; set; }

        public bool HasBooking { get; set; }

        /// <summary>
        ///     подготовка файлов
        /// </summary>
        public void PrepareFiles(List<RequestFileType> fileTypes)
        {
            var typeOfRestId = Data?.Id > 0 ? Data?.TypeOfRestId : null;

            FileTypes =
                fileTypes.Where(ft => ft.IsActive &&
                                      (!ft.TypeOfRests.Any() || ft.TypeOfRests.Any(f => f.Id == typeOfRestId))
                                      && (Data.TypeOfRestId != (long) TypeOfRestEnum.CommercicalAddonRequest ||
                                          new[]
                                          {
                                              (long) RequestFileTypeEnum.Request,
                                              (long) RequestFileTypeEnum.Applicant,
                                              (long) RequestFileTypeEnum.RightApplicant,
                                              (long) RequestFileTypeEnum.ChildSnils
                                          }.Contains(ft.Id))
                    )
                    .Select(f => new RequestFileTypeModel
                    {
                        Id = f.Id,
                        Name = f.Name.Replace("Документ, подтверждающий", "Подтверждающие")
                            .Replace("Документ, удостоверяющий ", "Удостоверяющие "),
                        Files = new List<RequestFileModel>(),
                        FileTypeGrouping = FileTypeGroupingSet(f)
                    })
                    .OrderBy(f => (int) f.FileTypeGrouping)
                    .ThenBy(f => f.Id)
                    .ToList();

            if (Data?.Files != null)
            {
                // список файлов
                var dict = Data.Files.GroupBy(f => f.RequestFileTypeId)
                    .ToDictionary(g => g.Key ?? 0, d => d.Select(f => new RequestFileModel(f)).ToList());

                foreach (var f in FileTypes)
                {
                    if (dict.ContainsKey(f.Id))
                    {
                        f.Files = dict[f.Id];
                    }
                }

                foreach (var key in dict.Keys.Where(k => FileTypes.All(t => t.Id != k)).ToList())
                {
                    var type = fileTypes.FirstOrDefault(v => v.Id == key);

                    if (type != null)
                    {
                        FileTypes.Add(new RequestFileTypeModel
                        {
                            Id = type.Id,
                            DisableAddFiles = true,
                            Name = type.Name.Replace("Документ, подтверждающий", "Подтверждающие")
                                .Replace("Документ, удостоверяющий ", "Удостоверяющие "),
                            Files = dict[key]
                        });
                    }
                }
            }

            // удаляем сертификаты их добавлять нельзя только скачать
            FileTypes.RemoveAll(
                t =>
                    (t.Id == (long) RequestFileTypeEnum.CertificateOnRest ||
                     t.Id == (long) RequestFileTypeEnum.CertificateOnPayment) &&
                    (t.Files == null || !t.Files.Any()));

            FileTypes = FileTypes.OrderBy(f => f.Id).ToList();
        }

        private RequestFileTypeGrouping FileTypeGroupingSet(RequestFileType fileType)
        {
            if (fileType.Id == (long) RequestFileTypeEnum.Applicant ||
                fileType.Id == (long) RequestFileTypeEnum.ApplicantSnils ||
                fileType.Id == (long) RequestFileTypeEnum.RightApplicant ||
                fileType.Id == (long) RequestFileTypeEnum.PersonsConfirmingPrivilegedCategoryOrphans ||
                fileType.Id == (long) RequestFileTypeEnum.PersonsConfirmingAddressOrphans)
            {
                return RequestFileTypeGrouping.Applicant;
            }

            if (fileType.Id == (long) RequestFileTypeEnum.AttendantIdentity ||
                fileType.Id == (long) RequestFileTypeEnum.AttendantSnils ||
                fileType.Id == (long) RequestFileTypeEnum.AttendantConfirmRepresentationInterests)
            {
                return RequestFileTypeGrouping.Attendant;
            }

            if (fileType.Id == (long) RequestFileTypeEnum.Child ||
                fileType.Id == (long) RequestFileTypeEnum.ChildRegistration ||
                fileType.Id == (long) RequestFileTypeEnum.ChildBenefit ||
                fileType.Id == (long) RequestFileTypeEnum.ChildSnils)
            {
                return RequestFileTypeGrouping.Child;
            }

            return RequestFileTypeGrouping.Other;
        }

        public override Request BuildData()
        {
            Data.Attendant = Attendant.Select(d => d.BuildData()).ToList();

            if (ParentInvalid != null && Child.Any(c =>
                c.Data.BenefitTypeId == (long) BenefitTypeEnum.ParentsInvalid || c.Data.BenefitTypeId == 31 ||
                c.Data.BenefitType?.SameBenefitId == (long) BenefitTypeEnum.ParentsInvalid))
            {
                Data.Attendant.Add(ParentInvalid.BuildData());
            }

            foreach (var child in Child)
            {
                if (Applicant.Guid == child.AttendantGuid)
                {
                    child.Data.ApplicantId = Applicant.Data.Id;
                }
                else if (Agent.Guid == child.AttendantGuid)
                {
                    child.Data.ApplicantId = Agent.DataApplicant.Id;
                }
                else
                {
                    var foundAttendant = Attendant.FirstOrDefault(x => x.Guid == child.AttendantGuid);
                    child.Data.ApplicantId = foundAttendant?.Data.Id;
                }

                child.Data.HaveMiddleName = child.HasNotMiddlename;
            }

            if (Attendant != null)
            {
                foreach (var attendant in Attendant)
                {
                    attendant.Data.HaveMiddleName = attendant.HasNotMiddlename;
                }
            }

            Applicant.Data.HaveMiddleName = Applicant.HasNotMiddlename;

            Data.Child = Child.Select(d => d.BuildData()).ToList();

            if (Data.BeneficiariesId != (long) BeneficiariesEnum.SecondParent &&
                (Data.TypeOfRestId == (long) TypeOfRestEnum.ChildRestCamps ||
                 Data.TypeOfRestId == (long) TypeOfRestEnum.TentChildrenCamp ||
                 Data.TypeOfRestId == (long) TypeOfRestEnum.ChildRestFederalCamps ||
                 Data.TypeOfRestId == (long) TypeOfRestEnum.ChildRest))
            {
                Data.Attendant = new List<Applicant>();
            }

            Data.AgentApplicant = AgentApplicant;
            Data.Applicant = Applicant.BuildData();
            Data.Agent = AgentApplicant ? Agent.BuildData() : null;

            var agentAttendant = Agent?.BuidApplicant();

            if (agentAttendant != null && (Data.AgentApplicant ?? false))
            {
                agentAttendant.RequestId = Data.Id;
                Data.Attendant.Add(agentAttendant);
            }

            // собрали файл.
            if (FileTypes != null)
            {
                Data.Files = new List<RequestFile>();
                foreach (var ft in FileTypes.Where(f => f.Files != null && f.Files.Any()).ToList())
                {
                    foreach (var f in ft.Files.Where(f =>
                            !string.IsNullOrWhiteSpace(f.Data.FileName) && !string.IsNullOrWhiteSpace(f.Data.FileTitle))
                        .ToList())
                    {
                        var file = f.BuildData();
                        file.RequestFileTypeId = ft.Id;
                        Data.Files.Add(file);
                    }
                }
            }
            
            Data.InformationVouchers = InformationVouchers?.Select(i => i.BuildData()).ToList() ??
                                       new List<RequestInformationVoucher>();

            foreach (var iv in Data.InformationVouchers)
            {
                iv.AttendantsPrice = iv.AttendantsPrice ?? new List<RequestInformationVoucherAttendant>();
                foreach (var ap in iv.AttendantsPrice)
                {
                    if (Applicant.Guid == ap.AttendantGuid)
                    {
                        ap.ApplicantId = Applicant.Data.Id;
                    }
                    else
                    {
                        var foundAttendant = Attendant?.FirstOrDefault(x => x.Guid == ap.AttendantGuid);
                        ap.ApplicantId = foundAttendant?.Data.Id;
                    }
                }
            }

            Data.TimesOfRest = new List<RequestsTimeOfRest>();
            Data.PlacesOfRest = new List<RequestPlaceOfRest>();

            var order = 1;
            if (TimeOfRestAddon1Id.HasValue)
            {
                Data.TimesOfRest.Add(
                    new RequestsTimeOfRest
                    {
                        Id = TimeOfRestAddon1LinkId ?? 0,
                        TimeOfRestId = TimeOfRestAddon1Id.Value,
                        RequestId = Data.Id,
                        Order = order++
                    });
            }

            if (TimeOfRestAddon2Id.HasValue)
            {
                Data.TimesOfRest.Add(new RequestsTimeOfRest
                {
                    Id = TimeOfRestAddon2LinkId ?? 0,
                    TimeOfRestId = TimeOfRestAddon2Id.Value,
                    RequestId = Data.Id,
                    Order = order
                });
            }

            order = 1;
            if (PlaceOfRestAddon1Id.HasValue)
            {
                Data.PlacesOfRest.Add(new RequestPlaceOfRest
                {
                    Id = PlaceOfRestAddon1LinkId ?? 0,
                    PlaceOfRestId = PlaceOfRestAddon1Id.Value,
                    RequestId = Data.Id,
                    Order = order++
                });
            }

            if (PlaceOfRestAddon2Id.HasValue)
            {
                Data.PlacesOfRest.Add(new RequestPlaceOfRest
                {
                    Id = PlaceOfRestAddon2LinkId ?? 0,
                    PlaceOfRestId = PlaceOfRestAddon2Id.Value,
                    RequestId = Data.Id,
                    Order = order
                });
            }

            return Data;
        }

        public override string GetErrorDescription(string legend = null, bool htmlMode = true, bool closeBlock = true)
        {
            var res = base.GetErrorDescription(legend, htmlMode, false);
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(res))
            {
                sb.AppendLine(res);
            }

            if (Applicant != null && !(Applicant.IsValid ?? true))
            {
                if (htmlMode)
                {
                    sb.AppendFormat("{0}{1}{2}", ParagraphStart,
                        Applicant.GetErrorDescription("Обнаружены ошибки в заявителе"),
                        ParagraphEnd);
                }
                else
                {
                    sb.AppendLine(Applicant.GetErrorDescription("Обнаружены ошибки в заявителе", false));
                }
            }

            if (Agent != null && !(Agent.IsValid ?? true))
            {
                if (htmlMode)
                {
                    sb.AppendFormat("{0}{1}{2}", ParagraphStart,
                        Agent.GetErrorDescription("Обнаружены ошибки в представителе заявителя"), ParagraphEnd);
                }
                else
                {
                    sb.AppendLine(Agent.GetErrorDescription("Обнаружены ошибки в представителе заявителя", false));
                }
            }

            if (!string.IsNullOrEmpty(AttendantEm))
            {
                if (htmlMode)
                {
                    sb.AppendFormat("{0}{1}{2}", ParagraphStart,
                        AttendantEm,
                        ParagraphEnd);
                }
                else
                {
                    sb.AppendLine(AttendantEm);
                }
            }

            if (Attendant.Any(c => !(c.IsValid ?? true)))
            {
                var attendants = Attendant.Where(c => !(c.IsValid ?? true)).ToList();
                foreach (var attendant in attendants)
                {
                    if (htmlMode)
                    {
                        sb.AppendFormat("{0}{1}{2}", ParagraphStart,
                            attendant.GetErrorDescription($"Обнаружены ошибки в сопровождающем \"{attendant}\""),
                            ParagraphEnd);
                    }
                    else
                    {
                        sb.AppendLine(attendant.GetErrorDescription(
                            $"Обнаружены ошибки в сопровождающем \"{attendant}\"",
                            false));
                    }
                }
            }

            if (Child.Any(c => !(c.IsValid ?? false)))
            {
                var childs = Child.Where(c => !(c.IsValid ?? false)).ToList();
                foreach (var child in childs)
                {
                    if (htmlMode)
                    {
                        sb.AppendFormat("{0}{1}{2}", ParagraphStart,
                            child.GetErrorDescription($"Обнаружены ошибки в ребенке \"{child}\""), ParagraphEnd);
                    }
                    else
                    {
                        sb.AppendLine(child.GetErrorDescription($"Обнаружены ошибки в ребенке \"{child}\"", false));
                    }
                }
            }

            if (htmlMode)
            {
                sb.AppendLine(BlockEnd);
            }

            return sb.ToString();
        }

        #region Регионы отдыха

        /// <summary>
        ///     Идентификатор первого дополнительного региона отдыха
        /// </summary>
        public long? PlaceOfRestAddon1Id { get; set; }

        /// <summary>
        ///     Идентификатор первый связи первого дополнительного региона отдыха
        /// </summary>
        public long? PlaceOfRestAddon1LinkId { get; set; }

        /// <summary>
        ///     Идентификатор второго дополнительного региона отдыха
        /// </summary>
        public long? PlaceOfRestAddon2Id { get; set; }

        /// <summary>
        ///     Идентификатор второй связи первого дополнительного региона отдыха
        /// </summary>
        public long? PlaceOfRestAddon2LinkId { get; set; }

        /// <summary>
        ///     Первого дополнительное место отдыха
        /// </summary>
        public PlaceOfRest PlaceOfRestAddon1 { get; set; }

        /// <summary>
        ///     Второе дополнительное место отдыха
        /// </summary>
        public PlaceOfRest PlaceOfRestAddon2 { get; set; }

        #endregion

        #region Времена отдыха

        /// <summary>
        ///     Идентификатор первого дополнительного времени отдыха
        /// </summary>
        public long? TimeOfRestAddon1Id { get; set; }

        /// <summary>
        ///     Идентификатор первый связи первого дополнительного времени отдыха
        /// </summary>
        public long? TimeOfRestAddon1LinkId { get; set; }

        /// <summary>
        ///     Идентификатор второго дополнительного времени отдыха
        /// </summary>
        public long? TimeOfRestAddon2Id { get; set; }

        /// <summary>
        ///     Идентификатор второй связи первого дополнительного времени отдыха
        /// </summary>
        public long? TimeOfRestAddon2LinkId { get; set; }

        /// <summary>
        ///     Первого дополнительное время отдыха
        /// </summary>
        public TimeOfRest TimeOfRestAddon1 { get; set; }

        /// <summary>
        ///     Второе дополнительное время отдыха
        /// </summary>
        public TimeOfRest TimeOfRestAddon2 { get; set; }

        #endregion

        #region Справочники

        public List<RequestInformationVoucherModel> InformationVouchers { get; set; }

        /// <summary>
        ///     Список кто инвалид
        /// </summary>
        public List<Beneficiaries> Beneficiaries { get; set; }

        /// <summary>
        ///     Список видов отдыха
        /// </summary>
        public List<TypeOfRest> TypeOfRests { get; set; }

        /// <summary>
        ///     список видов отдыха для путевок
        /// </summary>
        public List<TypeRequestInformationVoucher> TypeInformationVoucher { get; set; }

        /// <summary>
        ///     список видов комнат допустимых для создания доп мест.
        /// </summary>
        public List<TypeOfRoomViewModel> TypeOfRooms { get; set; }

        /// <summary>
        ///     типы файлов заявления
        /// </summary>
        public List<RequestFileTypeModel> FileTypes { get; set; }

        /// <summary>
        ///     список размещений
        /// </summary>
        public List<LocationRequest> Location { get; set; }

        /// <summary>
        ///     список возможных регионов отдыха
        /// </summary>
        public IList<PlaceOfRest> PlacesOfRest { get; set; }

        /// <summary>
        ///     список типов транспорта в рамках заявления
        /// </summary>
        public IList<TypeOfTransportInRequest> TypesOfTransportInRequest { get; set; }

        /// <summary>
        ///     список типов лагерей
        /// </summary>
        public IList<TypeOfCamp> TypesOfCamp { get; set; }

        /// <summary>
        ///     список мест отдыха, для которых нужно выбрать тип транспорта
        /// </summary>
        public IList<long> PlacesOfRestRequiringTransportSelection { get; set; }

        /// <summary>
        ///     список целей обращения, для которых нужно выбрать тип транспорта
        /// </summary>
        public IList<long> TypesOfRestRequiringTransportSelection { get; set; }

        #endregion

        #region Сообщения об ошибках заявления

        /// <summary>
        ///     Количество основных мест
        /// </summary>
        [Display(Description = "Размещение, основных мест")]
        public virtual string MainPlacesEm { get; set; }

        /// <summary>
        ///     Количество дополнительных мест
        /// </summary>
        [Display(Description = "Размещение, Дополнительных мест")]
        public virtual string AdditionalPlacesEm { get; set; }

        /// <summary>
        ///     Дети
        /// </summary>
        [Display(Description = "Сведения о детях")]
        public virtual string ChildEm { get; set; }

        /// <summary>
        ///     Дети
        /// </summary>
        [Display(Description = "Путевки")]
        public virtual string InformationVouchersEm { get; set; }

        /// <summary>
        ///     Дети
        /// </summary>
        [Display(Description = "Банковские реквизиты")]
        public virtual string BankEm { get; set; }

        /// <summary>
        ///     Сопровождающие
        /// </summary>
        public virtual string AttendantEm { get; set; }

        /// <summary>
        ///     Заявитель
        /// </summary>
        [Display(Description = "Сведения о заявителе")]
        public virtual string ApplicantEm { get; set; }

        /// <summary>
        ///     Вид отдыха
        /// </summary>
        [Display(Description = "Вид отдыха")]
        public virtual string TypeOfRestEm { get; set; }

        /// <summary>
        ///     Год кампании
        /// </summary>
        [Display(Description = "Год кампании")]
        public virtual string YearOfRestEm { get; set; }


        /// <summary>
        ///     Время отдыха
        /// </summary>
        [Display(Description = "Время отдыха")]
        public virtual string TimeOfRestEm { get; set; }

        /// <summary>
        ///     Дополнительное время отдыха
        /// </summary>
        [Display(Description = "Дополнительное время отдыха")]
        public virtual string TimeOfRestAddonEm { get; set; }

        /// <summary>
        ///     Тематика смены
        /// </summary>
        [Display(Description = "Тематика смены")]
        public virtual string SubjectOfRestEm { get; set; }

        /// <summary>
        ///     Трансфер от города Москвы до места отдыха будет осуществлен
        /// </summary>
        [Display(Description = "Трансфер от города Москвы до места отдыха будет осуществлен")]
        public virtual string TransferToEm { get; set; }

        /// <summary>
        ///     Трансфер от места отдыха до города Москва будет осуществлен
        /// </summary>
        [Display(Description = "Трансфер от места отдыха до города Москва будет осуществлен")]
        public virtual string TransferFromEm { get; set; }

        /// <summary>
        ///     Дополнительное регион отдыха
        /// </summary>
        [Display(Description = "Направление отдыха, дополнительное")]
        public virtual string PlaceOfRestAddonEm { get; set; }

        /// <summary>
        ///     Регион отдыха
        /// </summary>
        [Display(Description = "Направление отдыха, приоритетное")]
        public virtual string PlaceOfRestEm { get; set; }

        /// <summary>
        ///     Представитель
        /// </summary>
        [Display(Description = "Сведения о представителе заявителя")]
        public virtual string AgentEm { get; set; }

        /// <summary>
        ///     Сведения о размещении детей
        /// </summary>
        [Display(Description = "Сведения о размещении детей")]
        public virtual string CountPlaceEm { get; set; }

        /// <summary>
        ///     Сведения о размещении сопровождающих
        /// </summary>
        [Display(Description = "Сведения о размещении сопровождающих")]
        public virtual string CountAttendantsEm { get; set; }

        /// <summary>
        ///     Приоритетный тип транспорта
        /// </summary>
        [Display(Description = "Приоритетный тип транспорта")]
        public virtual string PriorityTypeOfTransportEm { get; set; }

        /// <summary>
        ///     Дополнительный тип транспорта
        /// </summary>
        [Display(Description = "Дополнительный тип транспорта")]
        public virtual string AdditioanlTypeOfTransportEm { get; set; }

        /// <summary>
        ///     Приоритетный тип лагеря
        /// </summary>
        [Display(Description = "Приоритетный тип лагеря")]
        public virtual string TypeOfCampEm { get; set; }

        /// <summary>
        ///     Дополнительный тип лагеря
        /// </summary>
        [Display(Description = "Дополнительный тип лагеря")]
        public virtual string TypeOfCampAddonEm { get; set; }

        public override bool CheckModel(string action = null)
        {
            IsValid = true;
            FormBlocksValidness[RequestViewBlockEnum.TypeAndTimeBlock] = true;
            FormBlocksValidness[RequestViewBlockEnum.PlacesBlock] = true;
            FormBlocksValidness[RequestViewBlockEnum.PlacementBlock] = true;
            FormBlocksValidness[RequestViewBlockEnum.ApplicantBlock] = true;
            FormBlocksValidness[RequestViewBlockEnum.AgentBlock] = true;
            FormBlocksValidness[RequestViewBlockEnum.AttendantBlock] = true;
            FormBlocksValidness[RequestViewBlockEnum.ChildsBlock] = true;
            FormBlocksValidness[RequestViewBlockEnum.Bank] = true;
            FormBlocksValidness[RequestViewBlockEnum.InformationVoucher] = true;
            if (Data == null)
            {
                IsValid = false;
                ErrorMessage = "Нет данных для проверки";
                return IsValid.Value;
            }

            if (!Data.YearOfRestId.HasValue)
            {
                YearOfRestEm = RequaredField;
                IsValid = false;
                FormBlocksValidness[RequestViewBlockEnum.TypeAndTimeBlock] = false;
            }

            if (!Data.TypeOfRestId.HasValue)
            {
                IsValid = false;
                TypeOfRestEm = RequaredField;
                FormBlocksValidness[RequestViewBlockEnum.TypeAndTimeBlock] = false;
            }

            if (!Data.TimeOfRestId.HasValue
                && Data?.TypeOfRestId != (long) TypeOfRestEnum.Compensation
                && Data?.TypeOfRestId != (long) TypeOfRestEnum.CompensationYouthRest
                && !Data.RequestOnMoney)
            {
                IsValid = false;
                TimeOfRestEm = RequaredField;
                FormBlocksValidness[RequestViewBlockEnum.TypeAndTimeBlock] = false;
            }

            if (!Data.TransferFromId.HasValue && Data.IsFirstCompany &&
                Data?.TypeOfRestId != (long) TypeOfRestEnum.Compensation &&
                Data?.TypeOfRestId != (long) TypeOfRestEnum.CompensationYouthRest &&
                !Data.RequestOnMoney)
            {
                IsValid = false;
                TransferFromEm = RequaredField;
                FormBlocksValidness[RequestViewBlockEnum.TypeAndTimeBlock] = false;
            }

            if (!Data.TransferToId.HasValue && Data.IsFirstCompany &&
                Data?.TypeOfRestId != (long) TypeOfRestEnum.Compensation &&
                Data?.TypeOfRestId != (long) TypeOfRestEnum.CompensationYouthRest &&
                !Data.RequestOnMoney)
            {
                IsValid = false;
                TransferToEm = RequaredField;
                FormBlocksValidness[RequestViewBlockEnum.TypeAndTimeBlock] = false;
            }

            //if (!TimeOfRestAddon1Id.HasValue && Data.IsFirstCompany && Data?.TypeOfRestId != (long)TypeOfRestEnum.Compensation && !Data.BookingGuid.HasValue)
            //{
            //	IsValid = false;
            //	TimeOfRestAddonEm = RequaredField;
            //	FormBlocksValidness[RequestViewBlockEnum.TypeAndTimeBlock] = false;
            //}

            //if (!TimeOfRestAddon2Id.HasValue && Data.IsFirstCompany && Data?.TypeOfRestId != (long)TypeOfRestEnum.Compensation && !Data.BookingGuid.HasValue)
            //{
            //	IsValid = false;
            //	TimeOfRestAddonEm = RequaredField;
            //	FormBlocksValidness[RequestViewBlockEnum.TypeAndTimeBlock] = false;
            //}

            if (!Data.PlaceOfRestId.HasValue && Data.IsFirstCompany &&
                Data?.TypeOfRestId != (long) TypeOfRestEnum.Compensation &&
                Data?.TypeOfRestId != (long) TypeOfRestEnum.CompensationYouthRest &&
                !Data.RequestOnMoney)
            {
                IsValid = false;
                PlaceOfRestEm = RequaredField;
                FormBlocksValidness[RequestViewBlockEnum.PlacesBlock] = false;
            }

            if (!PlaceOfRestAddon1Id.HasValue && Data.IsFirstCompany &&
                Data?.TypeOfRestId != (long) TypeOfRestEnum.Compensation &&
                Data?.TypeOfRestId != (long) TypeOfRestEnum.CompensationYouthRest &&
                !Data.BookingGuid.HasValue &&
                !Data.RequestOnMoney)
            {
                IsValid = false;
                PlaceOfRestAddonEm = RequaredField;
                FormBlocksValidness[RequestViewBlockEnum.PlacesBlock] = false;
            }

            if (!PlaceOfRestAddon2Id.HasValue && Data.IsFirstCompany &&
                Data?.TypeOfRestId != (long) TypeOfRestEnum.Compensation &&
                Data?.TypeOfRestId != (long) TypeOfRestEnum.CompensationYouthRest &&
                !Data.BookingGuid.HasValue &&
                !Data.RequestOnMoney)
            {
                IsValid = false;
                PlaceOfRestAddonEm = RequaredField;
                FormBlocksValidness[RequestViewBlockEnum.PlacesBlock] = false;
            }

            var ids = new[] {Data.PlaceOfRestId, PlaceOfRestAddon1Id, PlaceOfRestAddon2Id};
            var places = PlacesOfRest?.Where(p => ids.Contains(p.Id)).ToList().Select(p => p.GroupId ?? p.Id).Distinct()
                .Count();
            if (places != 3 && Data.IsFirstCompany && !Data.RequestOnMoney)
            {
                IsValid = false;
                PlaceOfRestEm = "Места отдыха должны быть из разных групп";
                FormBlocksValidness[RequestViewBlockEnum.PlacesBlock] = false;
            }

            if (Data.TypeOfRest != null)
            {
                if (Data.TypeOfRest.NeedApplicant)
                {
                    if (Data.Applicant == null)
                    {
                        ApplicantEm = "Не указан заявитель";
                        IsValid = false;
                        FormBlocksValidness[RequestViewBlockEnum.ApplicantBlock] = false;
                    }

                    Applicant.TimeOfRest = TimeOfRests.FirstOrDefault(t => t.Id == Data.TimeOfRestId);
                    Applicant.MainPlace = PlacesOfRest.FirstOrDefault(p => p.Id == Data.PlaceOfRestId);

                    if (!Applicant.CheckModel(Data, action))
                    {
                        IsValid = false;
                        FormBlocksValidness[RequestViewBlockEnum.ApplicantBlock] = false;
                    }

                    if (Data.Tour?.ChildAgeFrom != null && Applicant.Data.IsAccomp)
                    {
                        var attendatnYears =
                            StaticHelpers.GetAgeInYears(Applicant.Data.DateOfBirth, Data.Tour.DateIncome);
                        if (attendatnYears < Data.Tour.ChildAgeFrom)
                        {
                            IsValid = false;
                            FormBlocksValidness[RequestViewBlockEnum.ApplicantBlock] = false;
                            Applicant.DateOfBirthEm = "Возраст меньше чем минимальный возраст";
                        }
                    }

                    if (AgentApplicant)
                    {
                        if (Data.Agent == null)
                        {
                            AgentEm = "Не указан представитель заявителя";
                            IsValid = false;
                            FormBlocksValidness[RequestViewBlockEnum.AgentBlock] = false;
                        }

                        if (!Agent.CheckModel(action, this))
                        {
                            IsValid = false;
                            FormBlocksValidness[RequestViewBlockEnum.AgentBlock] = false;
                        }
                    }

                    if (Data.TypeOfRest.NeedAttendant)
                    {
                        foreach (var attendant in Attendant)
                        {
                            attendant.MainPlace = PlacesOfRest.FirstOrDefault(p => p.Id == Data.PlaceOfRestId);
                            attendant.TimeOfRest = TimeOfRests.FirstOrDefault(t => t.Id == Data.TimeOfRestId);

                            if (!attendant.CheckModel(Data, action))
                            {
                                IsValid = false;
                                FormBlocksValidness[RequestViewBlockEnum.AttendantBlock] = false;
                            }

                            if (Data.Tour?.ChildAgeFrom != null)
                            {
                                var attendatnYears =
                                    StaticHelpers.GetAgeInYears(attendant.Data.DateOfBirth, Data.Tour.DateIncome);
                                if (attendatnYears < Data.Tour.ChildAgeFrom)
                                {
                                    IsValid = false;
                                    FormBlocksValidness[RequestViewBlockEnum.AttendantBlock] = false;
                                    attendant.DateOfBirthEm = "Возраст меньше чем минимальный возраст";
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                IsValid = false;
            }

            if ((Data.TypeOfRestId == (long) TypeOfRestEnum.Compensation ||
                 Data?.TypeOfRestId == (long) TypeOfRestEnum.CompensationYouthRest) &&
                (Data.InformationVouchers == null || !Data.InformationVouchers.Any()))
            {
                IsValid = false;
                FormBlocksValidness[RequestViewBlockEnum.InformationVoucher] = false;
                InformationVouchersEm = "В заявлении нет ни одной путевки";
            }

            if (Data.TypeOfRestId != (long) TypeOfRestEnum.CommercicalAddonRequest &&
                Data.TypeOfRestId != (long) TypeOfRestEnum.YouthRestOrphanCamps &&
                Data.TypeOfRestId != (long) TypeOfRestEnum.CompensationYouthRest &&
                Data.TypeOfRestId != (long) TypeOfRestEnum.YouthRestCamps &&
                (Data.Child == null || !Data.Child.Any(c => c.BenefitTypeId.HasValue)))
            {
                IsValid = false;
                ChildEm = "В заявлении нет ни одного ребёнка с льготой";
                FormBlocksValidness[RequestViewBlockEnum.ChildsBlock] = false;
            }
            else if ((Data.TypeOfRestId == (long) TypeOfRestEnum.RestWithParentsComplex ||
                      Data.TypeOfRestId == (long) TypeOfRestEnum.RestWithParentsInvalidOrphanComplex) &&
                     Data.IsFirstCompany && Data.Child.Count <= 1)
            {
                IsValid = false;
                ChildEm = "В заявлении должно быть больше одного ребёнка";
                FormBlocksValidness[RequestViewBlockEnum.ChildsBlock] = false;
            }

            if (Data.TypeOfRestId != (long) TypeOfRestEnum.CommercicalAddonRequest &&
                (Data.Child == null || Data.Child.Any(c => !c.BenefitTypeId.HasValue)))
            {
                IsValid = false;
                ChildEm = "В заявлении есть ребенок без льготы";
                FormBlocksValidness[RequestViewBlockEnum.ChildsBlock] = false;
            }

            var notaryDate = new List<DateTime>();
            if (Agent?.Data?.ProxyDateOfIssure != null)
            {
                notaryDate.Add(Agent.Data.ProxyDateOfIssure.Value);
            }

            if (Agent?.DataApplicant?.ProxyDateOfIssure != null)
            {
                notaryDate.Add(Agent.DataApplicant.ProxyDateOfIssure.Value);
            }

            notaryDate.AddRange(
                Attendant?.Where(a => a.Data?.IsProxy ?? false)
                    .Where(a => a.Data.ProxyDateOfIssure.HasValue)
                    .Select(a => a.Data.ProxyDateOfIssure.Value)
                    .ToArray() ?? new DateTime[0]);

            if (notaryDate.Any() && Child.Any(c => c.Data?.DateOfBirth != null))
            {
                var minNotaryDate = notaryDate.Min();
                var childMinDate = Child.Where(c => c.Data?.DateOfBirth != null).Select(c => c.Data.DateOfBirth.Value)
                    .Min();
                if (minNotaryDate < childMinDate)
                {
                    IsValid = false;
                    ChildEm =
                        "Дата выдачи всех доверенностей на сопровождения должна быть позже чем дата рождения ребёнка.";
                    FormBlocksValidness[RequestViewBlockEnum.ChildsBlock] = false;
                }
            }

            var presentAttendantsCount =
                (Data.Attendant?.Count ?? 0) + (Data.NullSafe(d => d.Applicant.IsAccomp) ? 1 : 0);
            if (Data.BookingGuid.HasValue && Data.CountAttendants != presentAttendantsCount &&
                Data.TypeOfRest.NeedAttendant)
            {
                IsValid = false;
                AttendantEm = "Количество сопровождающих в заявлении не соответствует количеству мест в бронировании";
                FormBlocksValidness[RequestViewBlockEnum.AttendantBlock] = false;
            }

            if (Data.TypeOfRestId == (long) TypeOfRestEnum.CommercicalAddonRequest)
            {
                var campersCount = Data.Applicant != null && Data.Applicant.IsAccomp ? 1 : 0;
                campersCount += Data.Attendant?.Count ?? 0;
                if (campersCount > MaxCountAddonPlace)
                {
                    IsValid = false;
                    AttendantEm =
                        "Количество сопровождающих в заявлении не соответствует количеству мест в бронировании";
                    FormBlocksValidness[RequestViewBlockEnum.AttendantBlock] = false;
                }
            }

            ids = new[] {Data.TimeOfRestId /*, TimeOfRestAddon1Id, TimeOfRestAddon2Id*/};
            var times =
                TimeOfRests?.Where(p => ids.Contains(p.Id))
                    .Where(p => p.Year > 2000 && p.Month >= 1 && p.Month <= 12 && p.DayOfMonth >= 1 &&
                                p.DayOfMonth <= 31)
                    .Select(p => new DateTime(p.Year, p.Month, p.DayOfMonth))
                    .Distinct()
                    .ToList();

            if (Child != null)
            {
                long? maxBenefitTypeId = null;
                long? minBenefitTypeId = null;

                var lowIncomeFamily = new[]
                    {(long?) BenefitTypeEnum.LowIncomeFamily2017, (long?) BenefitTypeEnum.LowIncomeFamily};

                var lowIncomeFamilyCount = 0;

                var benefitDict = BenefitTypes.ToDictionary(b => b.Id, b => b.SameBenefitId ?? b.Id);
                foreach (var childViewModel in Child)
                {
                    if (benefitDict.ContainsKey(childViewModel.Data.BenefitTypeId ?? 0))
                    {
                        var benefit = benefitDict[childViewModel.Data.BenefitTypeId ?? 0];
                        if (!maxBenefitTypeId.HasValue || maxBenefitTypeId < benefit)
                        {
                            maxBenefitTypeId = benefit;
                        }

                        if (!minBenefitTypeId.HasValue || minBenefitTypeId > benefit)
                        {
                            minBenefitTypeId = benefit;
                        }

                        if (lowIncomeFamily.Contains(benefit))
                        {
                            lowIncomeFamilyCount++;
                        }
                    }

                    childViewModel.BenefitTypes = BenefitTypes;
                    childViewModel.TypeOfRestrictions = TypeOfRestrictions;
                    childViewModel.TypeOfRests = TypeOfRestsAll;
                    childViewModel.TypeOfRestBenefitRestrictions = TypeOfRestBenefitRestrictions;
                    childViewModel.TimeOfRests = TimeOfRests;
                    childViewModel.SelectedPeriods = Data.IsFirstCompany && !Data.BookingGuid.HasValue ? times : null;
                    childViewModel.RequestCurrentPeriod = RequestCurrentPeriod;
                    childViewModel.PlacesOfRest = PlacesOfRest;
                    if (!childViewModel.CheckModel(Data, action))
                    {
                        FormBlocksValidness[RequestViewBlockEnum.ChildsBlock] = false;
                        IsValid = false;
                    }
                }

                if (Data.TypeOfRestId == (long) TypeOfRestEnum.RestWithParentsComplex &&
                    maxBenefitTypeId == minBenefitTypeId && lowIncomeFamily.Contains(minBenefitTypeId))
                {
                    foreach (var childViewModel in Child)
                    {
                        childViewModel.IsValid = false;
                        childViewModel.BenefitTypeEm =
                            "Хотя бы у одного ребёнка должна быть льгота отличная от \"Дети из малообеспеченных семей\"";
                    }

                    FormBlocksValidness[RequestViewBlockEnum.ChildsBlock] = false;
                    IsValid = false;
                }

                // логика на сопровождающих
                if (Data.IsFirstCompany && (Data?.TypeOfRest?.NeedAttendant ?? false))
                {
                    var countChild = Child?.Count ?? 0;
                    var countAttendant = Attendant.Count + (Applicant?.Data?.IsAccomp ?? false ? 1 : 0) +
                                         (Agent?.DataApplicant?.IsAccomp ?? false ? 1 : 0);

                    if (Data.CountPlace != countChild)
                    {
                        // количество детей не равно тому что указано в заявлении
                        CountPlaceEm = "Количество детей не совпадает с указанным в заявлении";
                        IsValid = false;
                        FormBlocksValidness[RequestViewBlockEnum.PlacesBlock] = false;
                    }

                    if (Data.CountAttendants != countAttendant)
                    {
                        // количество сопровождающих не равно тому что указано в заявлении
                        CountAttendantsEm = "Количество сопровождающих не совпадает с указанным в заявлении";
                        IsValid = false;
                        FormBlocksValidness[RequestViewBlockEnum.PlacesBlock] = false;
                    }

                    var allowedCountAttendant = countChild - lowIncomeFamilyCount +
                                                (lowIncomeFamilyCount == 0 ? 0 : lowIncomeFamilyCount >= 4 ? 2 : 1);

                    if (countAttendant > allowedCountAttendant)
                    {
                        // количество сопровождающих превышает маскимально допустимое для указанных детей
                        CountAttendantsEm =
                            "Количество сопровождающих превышает маскимально допустимое для указанных детей";
                        IsValid = false;
                        FormBlocksValidness[RequestViewBlockEnum.PlacesBlock] = false;
                    }
                }

                if ((Data.TypeOfRest.IsActive && Data.TypeOfRest.NeedTypeOfTransport) &&
                    (Data.PlaceOfRest.IsActive && Data.PlaceOfRest.NeedTypeOfTransport) &&
                    ((Data.TransferFromId == (long) TypeOfTransferEnum.AsGroupMemberByMoscowBudget) ||
                     (Data.TransferToId == (long) TypeOfTransferEnum.AsGroupMemberByMoscowBudget)))
                {
                    if (Data.PriorityTypeOfTransportInRequestId == null)
                    {
                        PriorityTypeOfTransportEm =
                            "Не указан приоритетный тип транспорта";
                        IsValid = false;
                        FormBlocksValidness[RequestViewBlockEnum.TypeOfTransport] = false;
                    }
                    if (Data.AdditionalTypeOfTransportInRequestId == null)
                    {
                        AdditioanlTypeOfTransportEm =
                            "Не указан дополнительный тип транспорта";
                        IsValid = false;
                        FormBlocksValidness[RequestViewBlockEnum.TypeOfTransport] = false;
                    }
                }

                if (Data.TypeOfRest.IsActive && (Data.TypeOfRest.ParentId ?? -1) == (long) TypeOfRestEnum.ChildRest)
                {
                    if (Data.TypeOfCampId == null)
                    {
                        TypeOfCampEm =
                            "Не указан приоритетный тип лагеря";
                        IsValid = false;
                        FormBlocksValidness[RequestViewBlockEnum.TypeOfCamp] = false;
                    }
                    if (Data.TypeOfCampAddonId == null)
                    {
                        TypeOfCampAddonEm =
                            "Не указан дополнительный тип лагеря";
                        IsValid = false;
                        FormBlocksValidness[RequestViewBlockEnum.TypeOfCamp] = false;
                    }
                }

            }

            return IsValid.Value;
        }

        #endregion
    }

    /// <summary>
    ///     Блоки, из которых состоит заявление
    /// </summary>
    public enum RequestViewBlockEnum
    {
        /// <summary>
        ///     Общие сведения
        /// </summary>
        GeneralBlock,

        /// <summary>
        ///     Вид и время отдыха
        /// </summary>
        TypeAndTimeBlock,

        /// <summary>
        ///     Регион отдыха
        /// </summary>
        PlacesBlock,

        /// <summary>
        ///     Размещение
        /// </summary>
        PlacementBlock,

        /// <summary>
        ///     Сведения о заявителе
        /// </summary>
        ApplicantBlock,

        /// <summary>
        ///     Сведения о представителе заявителя
        /// </summary>
        AgentBlock,

        /// <summary>
        ///     Сведения о сопровождающих
        /// </summary>
        AttendantBlock,

        /// <summary>
        ///     Сведения о детях
        /// </summary>
        ChildsBlock,

        /// <summary>
        ///     Файлы
        /// </summary>
        FileBlock,

        /// <summary>
        ///     Дополнительные услуги
        /// </summary>
        AddonServices,

        /// <summary>
        ///     инфорамация о путевках
        /// </summary>
        InformationVoucher,

        /// <summary>
        ///     Банковские рекивизиты
        /// </summary>
        Bank,

        /// <summary>
        ///     Сведения о погашенном сертификате
        /// </summary>
        Certificate,

        /// <summary>
        ///     Тип транспорта
        /// </summary>
        TypeOfTransport,

        /// <summary>
        ///     Тип лагеря
        /// </summary>
        TypeOfCamp
    }
}
