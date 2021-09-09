using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using RestChild.Comon;
using RestChild.Domain;

namespace RestChild.Web.Models.TradeUnion
{
    [DataContract]
    [Serializable]
    public class TradeUnionCamperModel
    {
        public TradeUnionCamperModel()
        {
            ParentDocumentTypeName = "-- Не выбрано --";
            ChildDocumentTypeName = "-- Не выбрано --";
            UnionistDocumentTypeName = "-- Не выбрано --";
            SelectedSchoolName = "-- Не выбрано --";
            TradeUnionOrganizationName = "-- Не выбрано --";
        }

        public TradeUnionCamperModel(TradeUnionCamper entity)
        {
            if (entity == null)
            {
                return;
            }

            Id = entity.Id;
            AddressChild = entity.AddressChild;
            School = entity.School;
            ParentPlaceWork = entity.ParentPlaceWork;
            IsParentUnionist = entity.IsParentUnionist;
            IsRelativeUnionist = entity.IsRelativeUnionist;
            Summa = entity.Summa;
            SummaParent = entity.SummaParent;
            SummaTradeUnion = entity.SummaTradeUnion;
            SummaBudget = entity.SummaBudget;
            SummaOrganization = entity.SummaOrganization;
            RelativePlaceWork = entity.RelativePlaceWork;
            IsChecked = entity.IsChecked;
            IsScoolNotPresent = entity.IsScoolNotPresent;
            TradeUnionOrganizationOther = entity.TradeUnionOrganizationOther;
            TradeUnionId = entity.TradeUnionId;

            ChildId = entity.ChildId;

            var child = entity?.Child;
            if (child != null)
            {
                ChildIdObj = child.Id;
                ChildLastName = child.LastName;
                ChildFirstName = child.FirstName;
                ChildMiddleName = child.MiddleName;
                ChildHaveMiddleName = child.HaveMiddleName;
                ChildDocumentSeria = child.DocumentSeria;
                ChildDocumentNumber = child.DocumentNumber;
                ChildDocumentDateOfIssue = child.DocumentDateOfIssue.FormatEx(string.Empty, string.Empty);
                ChildDocumentSubjectIssue = child.DocumentSubjectIssue;
                ChildDateOfBirth = child.DateOfBirth.FormatEx(string.Empty, string.Empty);
                ChildMale = child.Male;
                ChildPlaceOfBirth = child.PlaceOfBirth;
                ChildSnils = child.Snils;
                ChildPhone = child.Phone;
                ChildEmail = child.Email;
                ChildDocumentTypeId = child.DocumentTypeId;
                ChildAddressId = child.AddressId;

                if (child.Address != null)
                {
                    ChildAddressIdObj = child.Address.Id;
                    ChildAddressName = child.Address.Name;
                    ChildAddressAppartment = child.Address.Appartment;
                    ChildAddressVladenie = child.Address.Vladenie;
                    ChildAddressStreet = child.Address.Street;
                    ChildAddressHouse = child.Address.House;
                    ChildAddressCorpus = child.Address.Corpus;
                    ChildAddressStroenie = child.Address.Stroenie;
                    ChildAddressLatitude = child.Address.Latitude;
                    ChildAddressLongitude = child.Address.Longitude;
                    ChildAddressBtiAddressId = child.Address.BtiAddressId;
                    if (child.Address.BtiAddress != null)
                    {
                        ChildAddressBtiAddressBtiStreetId = child.Address.BtiAddress.BtiStreetId;
                        ChildAddressBtiAddressBtiStreetName = child.Address.BtiAddress.BtiStreet?.Name;
                    }

                    ChildAddressBtiDistrictId = child.Address.BtiDistrictId;
                    ChildAddressBtiRegionId = child.Address.BtiRegionId;

                    ChildAddressFIASId = child.Address.FiasId;
                    ChildAddressDistrict = child.Address.BtiRegion?.Name;
                    ChildAddressRegion = child.Address.BtiDistrict?.Name;
                }
            }

            ParentId = entity.ParentId;
            var parent = entity.Parent;
            if (parent != null)
            {
                ParentIdObj = parent.Id;
                ParentLastName = parent.LastName;
                ParentFirstName = parent.FirstName;
                ParentMiddleName = parent.MiddleName;
                ParentHaveMiddleName = parent.HaveMiddleName;
                ParentDocumentSeria = parent.DocumentSeria;
                ParentDocumentNumber = parent.DocumentNumber;
                ParentDocumentDateOfIssue = parent.DocumentDateOfIssue.FormatEx(string.Empty, string.Empty);
                ParentDocumentSubjectIssue = parent.DocumentSubjectIssue;
                ParentDateOfBirth = parent.DateOfBirth.FormatEx(string.Empty, string.Empty);
                ParentMale = parent.Male;
                ParentPlaceOfBirth = parent.PlaceOfBirth;
                ParentSnils = parent.Snils;
                ParentPhone = parent.Phone;
                ParentEmail = parent.Email;
                ParentDocumentTypeId = parent.DocumentTypeId;
                ParentAddressId = parent.AddressId;
            }

            UnionistId = entity.UnionistId;
            var unionist = entity.Unionist;
            if (unionist != null)
            {
                UnionistIdObj = unionist.Id;
                UnionistLastName = unionist.LastName;
                UnionistFirstName = unionist.FirstName;
                UnionistMiddleName = unionist.MiddleName;
                UnionistHaveMiddleName = unionist.HaveMiddleName;
                UnionistDocumentSeria = unionist.DocumentSeria;
                UnionistDocumentNumber = unionist.DocumentNumber;
                UnionistDocumentDateOfIssue = unionist.DocumentDateOfIssue.FormatEx(string.Empty, string.Empty);
                UnionistDocumentSubjectIssue = unionist.DocumentSubjectIssue;
                UnionistDateOfBirth = unionist.DateOfBirth.FormatEx(string.Empty, string.Empty);
                UnionistMale = unionist.Male;
                UnionistPlaceOfBirth = unionist.PlaceOfBirth;
                UnionistSnils = unionist.Snils;
                UnionistPhone = unionist.Phone;
                UnionistEmail = unionist.Email;
                UnionistDocumentTypeId = unionist.DocumentTypeId;
                UnionistAddressId = unionist.AddressId;
            }

            TradeUnionStatusByChildId = entity.TradeUnionStatusByChildId;
            SelectedSchoolId = entity.SelectedSchoolId;
            TradeUnionOrganizationId = entity.TradeUnionOrganizationId;
            ParentDocumentTypeName = parent?.DocumentType?.Name ?? "-- Не выбрано --";
            ChildDocumentTypeName = child?.DocumentType?.Name ?? "-- Не выбрано --";
            UnionistDocumentTypeName = unionist?.DocumentType?.Name ?? "-- Не выбрано --";
            SelectedSchoolName = entity.SelectedSchool?.Name ?? "-- Не выбрано --";
            TradeUnionOrganizationName = entity.TradeUnionOrganization?.Name ?? "-- Не выбрано --";
        }

        /// <summary>
        ///     Уникальный идентификатор
        /// </summary>
        [Display(Description = "Уникальный идентификатор")]
        [DataMember(Name = "Id")]
        public long Id { get; set; }

        /// <summary>
        ///     Адрес регистрации ребёнка
        /// </summary>
        [Display(Description = "Адрес регистрации ребёнка")]
        [DataMember(Name = "AddressChild")]
        public string AddressChild { get; set; }

        /// <summary>
        ///     Образовательное учреждение
        /// </summary>
        [Display(Description = "Образовательное учреждение")]
        [DataMember(Name = "School")]
        public string School { get; set; }

        /// <summary>
        ///     Место работы родителя
        /// </summary>
        [Display(Description = "Место работы родителя")]
        [DataMember(Name = "ParentPlaceWork")]
        public string ParentPlaceWork { get; set; }

        /// <summary>
        ///     Член профсоюза
        /// </summary>
        [Display(Description = "Член профсоюза")]
        [DataMember(Name = "IsParentUnionist")]
        public bool IsParentUnionist { get; set; }

        /// <summary>
        ///     Родственник члена профсоюза
        /// </summary>
        [Display(Description = "Родственник члена профсоюза")]
        [DataMember(Name = "IsRelativeUnionist")]
        public bool IsRelativeUnionist { get; set; }

        /// <summary>
        ///     Стоимость полная
        /// </summary>
        [Display(Description = "Стоимость полная")]
        [DataMember(Name = "Summa")]
        public decimal? Summa { get; set; }

        /// <summary>
        ///     Средства родителей
        /// </summary>
        [Display(Description = "Средства родителей")]
        [DataMember(Name = "SummaParent")]
        public decimal? SummaParent { get; set; }

        /// <summary>
        ///     Средства профсоюза
        /// </summary>
        [Display(Description = "Средства профсоюза")]
        [DataMember(Name = "SummaTradeUnion")]
        public decimal? SummaTradeUnion { get; set; }

        /// <summary>
        ///     Бюджетные средства
        /// </summary>
        [Display(Description = "Бюджетные средства")]
        [DataMember(Name = "SummaBudget")]
        public decimal? SummaBudget { get; set; }

        /// <summary>
        ///     Средства предприятия
        /// </summary>
        [Display(Description = "Средства предприятия")]
        [DataMember(Name = "SummaOrganization")]
        public decimal? SummaOrganization { get; set; }

        /// <summary>
        ///     Место работы члена профсоюза
        /// </summary>
        [Display(Description = "Место работы члена профсоюза")]
        [MaxLength(1000, ErrorMessage = "\"Место работы члена профсоюза\" не может быть больше 1000 символов")]
        [DataMember(Name = "RelativePlaceWork")]
        public string RelativePlaceWork { get; set; }

        /// <summary>
        ///     Заехал
        /// </summary>
        [Display(Description = "Заехал")]
        [Required(ErrorMessage = "\"Заехал\" должно быть заполнено")]
        [DataMember(Name = "IsChecked")]
        public bool IsChecked { get; set; }

        /// <summary>
        /// </summary>
        [Display(Description = "")]
        [Required(ErrorMessage = "\"\" должно быть заполнено")]
        [DataMember(Name = "IsScoolNotPresent")]
        public bool IsScoolNotPresent { get; set; }

        /// <summary>
        ///     Профсоюз (строкой)
        /// </summary>
        [Display(Description = "Профсоюз (строкой)")]
        [MaxLength(1000, ErrorMessage = "\"Профсоюз (строкой)\" не может быть больше 1000 символов")]
        [DataMember(Name = "TradeUnionOrganizationOther")]
        public string TradeUnionOrganizationOther { get; set; }


        /// <summary>
        ///     Список отдыхающих
        /// </summary>
        [DataMember(Name = "TradeUnionId")]
        [Display(Description = "Список отдыхающих")]
        public long? TradeUnionId { get; set; }

        /// <summary>
        ///     Ребёнок
        /// </summary>
        [DataMember(Name = "ChildId")]
        [Display(Description = "Ребёнок")]
        public long? ChildId { get; set; }

        /// <summary>
        ///     Родитель
        /// </summary>
        [DataMember(Name = "ParentId")]
        [Display(Description = "Родитель")]
        public long? ParentId { get; set; }

        /// <summary>
        ///     Член профсоюза
        /// </summary>
        [DataMember(Name = "UnionistId")]
        [Display(Description = "Член профсоюза")]
        public long? UnionistId { get; set; }

        /// <summary>
        ///     Статус по отношению к ребёнку
        /// </summary>
        [DataMember(Name = "TradeUnionStatusByChildId")]
        [Display(Description = "Статус по отношению к ребёнку")]
        public long? TradeUnionStatusByChildId { get; set; }


        /// <summary>
        ///     Статус по отношению к ребёнку
        /// </summary>
        [Display(Description = "Статус по отношению к ребёнку")]
        [DataMember(Name = "tradeUnionStatusByChild")]
        public TradeUnionStatusByChild TradeUnionStatusByChild { get; set; }

        /// <summary>
        ///     Школа
        /// </summary>
        [DataMember(Name = "SelectedSchoolId")]
        [Display(Description = "Школа")]
        public long? SelectedSchoolId { get; set; }


        /// <summary>
        ///     Школа
        /// </summary>
        [Display(Description = "Школа")]
        [DataMember(Name = "SelectedSchoolName")]
        public string SelectedSchoolName { get; set; }

        /// <summary>
        ///     Профсоюз
        /// </summary>
        [DataMember(Name = "TradeUnionOrganizationId")]
        [Display(Description = "Профсоюз")]
        public long? TradeUnionOrganizationId { get; set; }


        /// <summary>
        ///     Профсоюз
        /// </summary>
        [Display(Description = "Профсоюз")]
        [DataMember(Name = "TradeUnionOrganizationName")]
        public string TradeUnionOrganizationName { get; set; }

        public virtual TradeUnionCamper BuildEntity()
        {
            var res = new TradeUnionCamper
            {
                Id = Id,
                AddressChild = AddressChild,
                School = School,
                ParentPlaceWork = ParentPlaceWork,
                IsParentUnionist = IsParentUnionist,
                IsRelativeUnionist = IsRelativeUnionist,
                Summa = Summa,
                SummaParent = SummaParent,
                SummaTradeUnion = SummaTradeUnion,
                SummaBudget = SummaBudget,
                SummaOrganization = SummaOrganization,
                RelativePlaceWork = RelativePlaceWork,
                IsChecked = IsChecked,
                IsScoolNotPresent = IsScoolNotPresent,
                TradeUnionOrganizationOther = TradeUnionOrganizationOther,
                TradeUnionId = TradeUnionId,
                ChildId = ChildId
            };

            res.Child = new Person
            {
                Id = ChildIdObj ?? res.ChildId ?? 0,
                LastName = ChildLastName,
                FirstName = ChildFirstName,
                MiddleName = ChildMiddleName,
                HaveMiddleName = ChildHaveMiddleName,
                DocumentSeria = ChildDocumentSeria,
                DocumentNumber = ChildDocumentNumber,
                DocumentDateOfIssue = ChildDocumentDateOfIssue.TryParseDateDdMmYyyy(),
                DocumentSubjectIssue = ChildDocumentSubjectIssue,
                DateOfBirth = ChildDateOfBirth.TryParseDateDdMmYyyy(),
                Male = ChildMale,
                PlaceOfBirth = ChildPlaceOfBirth,
                Snils = ChildSnils,
                Phone = ChildPhone,
                Email = ChildEmail,
                DocumentTypeId = ChildDocumentTypeId,
                AddressId = ChildAddressId,
                Address = new Address
                {
                    Id = ChildAddressIdObj,
                    Name = ChildAddressName,
                    Appartment = ChildAddressAppartment,
                    Street = ChildAddressStreet,
                    House = ChildAddressHouse,
                    Corpus = ChildAddressCorpus,
                    Stroenie = ChildAddressStroenie,
                    Latitude = ChildAddressLatitude,
                    Longitude = ChildAddressLongitude,
                    BtiAddressId = ChildAddressBtiAddressId,
                    BtiDistrictId = ChildAddressBtiDistrictId,
                    BtiRegionId = ChildAddressBtiRegionId,
                    FiasId = ChildAddressFIASId,
                    Region = ChildAddressRegion,
                    District = ChildAddressDistrict,
                    Vladenie = ChildAddressVladenie
                }
            };

            res.ParentId = ParentId;
            res.Parent = new Person
            {
                Id = ParentIdObj ?? res.ParentId ?? 0,
                LastName = ParentLastName,
                FirstName = ParentFirstName,
                MiddleName = ParentMiddleName,
                HaveMiddleName = ParentHaveMiddleName,
                DocumentSeria = ParentDocumentSeria,
                DocumentNumber = ParentDocumentNumber,
                DocumentDateOfIssue = ParentDocumentDateOfIssue.TryParseDateDdMmYyyy(),
                DocumentSubjectIssue = ParentDocumentSubjectIssue,
                DateOfBirth = ParentDateOfBirth.TryParseDateDdMmYyyy(),
                Male = ParentMale,
                PlaceOfBirth = ParentPlaceOfBirth,
                Snils = ParentSnils,
                Phone = ParentPhone,
                Email = ParentEmail,
                DocumentTypeId = ParentDocumentTypeId,
                AddressId = ParentAddressId
            };

            res.UnionistId = UnionistId;
            res.Unionist = new Person
            {
                Id = UnionistIdObj ?? res.UnionistId ?? 0,
                LastName = UnionistLastName,
                FirstName = UnionistFirstName,
                MiddleName = UnionistMiddleName,
                HaveMiddleName = UnionistHaveMiddleName,
                DocumentSeria = UnionistDocumentSeria,
                DocumentNumber = UnionistDocumentNumber,
                DocumentDateOfIssue = UnionistDocumentDateOfIssue.TryParseDateDdMmYyyy(),
                DocumentSubjectIssue = UnionistDocumentSubjectIssue,
                DateOfBirth = UnionistDateOfBirth.TryParseDateDdMmYyyy(),
                Male = UnionistMale,
                PlaceOfBirth = UnionistPlaceOfBirth,
                Snils = UnionistSnils,
                Phone = UnionistPhone,
                Email = UnionistEmail,
                DocumentTypeId = UnionistDocumentTypeId,
                AddressId = UnionistAddressId,
            };

            res.TradeUnionStatusByChildId = TradeUnionStatusByChildId;
            res.SelectedSchoolId = SelectedSchoolId;
            res.TradeUnionOrganizationId = TradeUnionOrganizationId;

            return res;
        }

        #region child

        /// <summary>
        ///     Ребёнок
        /// </summary>
        [DataMember(Name = "Child-Id")]
        [Display(Description = "Ребёнок")]
        public long? ChildIdObj { get; set; }

        /// <summary>
        ///     Фамилия
        /// </summary>
        [Display(Description = "Фамилия")]
        [MaxLength(1000, ErrorMessage = "\"Фамилия\" не может быть больше 1000 символов")]
        [DataMember(Name = "Child-LastName")]
        public string ChildLastName { get; set; }

        /// <summary>
        ///     Имя
        /// </summary>
        [Display(Description = "Имя")]
        [MaxLength(1000, ErrorMessage = "\"Имя\" не может быть больше 1000 символов")]
        [DataMember(Name = "Child-FirstName")]
        public string ChildFirstName { get; set; }

        /// <summary>
        ///     Отчество
        /// </summary>
        [Display(Description = "Отчество")]
        [MaxLength(1000, ErrorMessage = "\"Отчество\" не может быть больше 1000 символов")]
        [DataMember(Name = "Child-MiddleName")]
        public string ChildMiddleName { get; set; }

        /// <summary>
        ///     Признак что есть отчество
        /// </summary>
        [Display(Description = "Признак что есть отчество")]
        [Required(ErrorMessage = "\"Признак что есть отчество\" должно быть заполнено")]
        [DataMember(Name = "Child-HaveMiddleName")]
        public bool ChildHaveMiddleName { get; set; }

        /// <summary>
        ///     Вид документа
        /// </summary>
        [DataMember(Name = "Child-DocumentTypeId")]
        [Display(Description = "Вид документа")]
        public long? ChildDocumentTypeId { get; set; }

        /// <summary>
        ///     Вид документа
        /// </summary>
        [DataMember(Name = "Child-DocumentTypeName")]
        [Display(Description = "Вид документа")]
        public string ChildDocumentTypeName { get; set; }

        /// <summary>
        ///     Серия документа
        /// </summary>
        [Display(Description = "Серия документа")]
        [MaxLength(1000, ErrorMessage = "\"Серия документа\" не может быть больше 1000 символов")]
        [DataMember(Name = "Child-DocumentSeria")]
        public string ChildDocumentSeria { get; set; }

        /// <summary>
        ///     Номер документа
        /// </summary>
        [Display(Description = "Номер документа")]
        [MaxLength(1000, ErrorMessage = "\"Номер документа\" не может быть больше 1000 символов")]
        [DataMember(Name = "Child-DocumentNumber")]
        public string ChildDocumentNumber { get; set; }

        /// <summary>
        ///     Дата выдачи документа
        /// </summary>
        [Display(Description = "Дата выдачи документа")]
        [DataMember(Name = "Child-DocumentDateOfIssue")]
        public string ChildDocumentDateOfIssue { get; set; }

        /// <summary>
        ///     Кем выдан документ
        /// </summary>
        [Display(Description = "Кем выдан документ")]
        [MaxLength(1000, ErrorMessage = "\"Кем выдан документ\" не может быть больше 1000 символов")]
        [DataMember(Name = "Child-DocumentSubjectIssue")]
        public string ChildDocumentSubjectIssue { get; set; }

        /// <summary>
        ///     Дата рождения
        /// </summary>
        [Display(Description = "Дата рождения")]
        [DataMember(Name = "Child-DateOfBirth")]
        public string ChildDateOfBirth { get; set; }

        /// <summary>
        ///     Мужской пол
        /// </summary>
        [Display(Description = "Мужской пол")]
        [Required(ErrorMessage = "\"Мужской пол\" должно быть заполнено")]
        [DataMember(Name = "Child-Male")]
        public bool ChildMale { get; set; }

        /// <summary>
        ///     Место рождения
        /// </summary>
        [Display(Description = "Место рождения")]
        [MaxLength(1000, ErrorMessage = "\"Место рождения\" не может быть больше 1000 символов")]
        [DataMember(Name = "Child-PlaceOfBirth")]
        public string ChildPlaceOfBirth { get; set; }

        /// <summary>
        ///     Снилс
        /// </summary>
        [Display(Description = "СНИЛС")]
        [DataMember(Name = "Child-Snils")]
        public string ChildSnils { get; set; }

        /// <summary>
        ///     Телефон
        /// </summary>
        [Display(Description = "Телефон")]
        [MaxLength(1000, ErrorMessage = "\"Телефон\" не может быть больше 1000 символов")]
        [DataMember(Name = "Child-Phone")]
        public string ChildPhone { get; set; }

        /// <summary>
        ///     Электронная почта
        /// </summary>
        [Display(Description = "Электронная почта")]
        [MaxLength(1000, ErrorMessage = "\"Электронная почта\" не может быть больше 1000 символов")]
        [DataMember(Name = "Child-Email")]
        public string ChildEmail { get; set; }

        #region Address

        /// <summary>
        ///     Адрес регистрации
        /// </summary>
        [DataMember(Name = "Child-AddressId")]
        [Display(Description = "Адрес регистрации")]
        public long? ChildAddressId { get; set; }

        /// <summary>
        ///     Уникальный идентификатор
        /// </summary>
        [Display(Description = "Уникальный идентификатор")]
        [Required(ErrorMessage = "\"Уникальный идентификатор\" должно быть заполнено")]
        [Key]
        [DataMember(Name = "Child-Address-Id")]
        public long ChildAddressIdObj { get; set; }

        /// <summary>
        ///     Полное наименование
        /// </summary>
        [Display(Description = "Полное наименование")]
        [MaxLength(1000, ErrorMessage = "\"Полное наименование\" не может быть больше 1000 символов")]
        [DataMember(Name = "Child-Address-Name")]
        public string ChildAddressName { get; set; }

        /// <summary>
        ///     Номер квартиры
        /// </summary>
        [Display(Description = "Номер квартиры")]
        [MaxLength(1000, ErrorMessage = "\"Номер квартиры\" не может быть больше 1000 символов")]
        [DataMember(Name = "Child-Address-Appartment")]
        public string ChildAddressAppartment { get; set; }

        /// <summary>
        ///     Улица
        /// </summary>
        [Display(Description = "Улица")]
        [MaxLength(1000, ErrorMessage = "\"Улица\" не может быть больше 1000 символов")]
        [DataMember(Name = "Child-Address-Street")]
        public string ChildAddressStreet { get; set; }

        /// <summary>
        ///     Дом
        /// </summary>
        [Display(Description = "Дом")]
        [MaxLength(1000, ErrorMessage = "\"Дом\" не может быть больше 1000 символов")]
        [DataMember(Name = "Child-Address-House")]
        public string ChildAddressHouse { get; set; }

        /// <summary>
        ///     Корпус
        /// </summary>
        [Display(Description = "Корпус")]
        [MaxLength(1000, ErrorMessage = "\"Корпус\" не может быть больше 1000 символов")]
        [DataMember(Name = "Child-Address-Corpus")]
        public string ChildAddressCorpus { get; set; }

        /// <summary>
        ///     Строение
        /// </summary>
        [Display(Description = "Строение")]
        [MaxLength(1000, ErrorMessage = "\"Строение\" не может быть больше 1000 символов")]
        [DataMember(Name = "Child-Address-Stroenie")]
        public string ChildAddressStroenie { get; set; }

        /// <summary>
        ///     Владение
        /// </summary>
        [Display(Description = "Владение")]
        [MaxLength(1000, ErrorMessage = "\"Владение\" не может быть больше 1000 символов")]
        [DataMember(Name = "Child-Address-Vladenie")]
        public string ChildAddressVladenie { get; set; }

        /// <summary>
        ///     Широта
        /// </summary>
        [Display(Description = "Широта")]
        [DataMember(Name = "Child-Address-Latitude")]
        public decimal? ChildAddressLatitude { get; set; }

        /// <summary>
        ///     Долгота
        /// </summary>
        [Display(Description = "Долгота")]
        [DataMember(Name = "Child-Address-Longitude")]
        public decimal? ChildAddressLongitude { get; set; }


        /// <summary>
        ///     Адрес БТИ
        /// </summary>
        [DataMember(Name = "Child-Address-BtiAddressId")]
        [Display(Description = "Адрес БТИ")]
        public long? ChildAddressBtiAddressId { get; set; }

        /// <summary>
        ///     Район
        /// </summary>
        [DataMember(Name = "Child-Address-BtiAddress-BtiStreet-Name")]
        [Display(Description = "Район")]
        public string ChildAddressBtiAddressBtiStreetName { get; set; }

        /// <summary>
        ///     Район
        /// </summary>
        [DataMember(Name = "Child-Address-BtiAddress-BtiStreet-Id")]
        [Display(Description = "Район")]
        public long? ChildAddressBtiAddressBtiStreetId { get; set; }

        /// <summary>
        ///     Округ
        /// </summary>
        [DataMember(Name = "Child-Address-BtiDistrictId")]
        [Display(Description = "Округ")]
        public long? ChildAddressBtiDistrictId { get; set; }

        /// <summary>
        ///     Район
        /// </summary>
        [DataMember(Name = "Child-Address-BtiRegionId")]
        [Display(Description = "Район")]
        public long? ChildAddressBtiRegionId { get; set; }

        /// <summary>
        ///     ФИС идентификатор
        /// </summary>
        [DataMember(Name = "Child-Address-FiasId")]
        [Display(Description = "ФИС идентификатор")]
        public string ChildAddressFIASId { get; set; }

        /// <summary>
        ///     Region
        /// </summary>
        [DataMember(Name = "Child-Address-Region")]
        [Display(Description = "Region")]
        public string ChildAddressRegion { get; set; }

        /// <summary>
        ///     District
        /// </summary>
        [DataMember(Name = "Child-Address-District")]
        [Display(Description = "District")]
        public string ChildAddressDistrict { get; set; }

        #endregion

        #endregion

        #region Parent

        /// <summary>
        ///     Родитель
        /// </summary>
        [DataMember(Name = "Parent-Id")]
        [Display(Description = "Ребёнок")]
        public long? ParentIdObj { get; set; }

        /// <summary>
        ///     Фамилия
        /// </summary>
        [Display(Description = "Фамилия")]
        [MaxLength(1000, ErrorMessage = "\"Фамилия\" не может быть больше 1000 символов")]
        [DataMember(Name = "Parent-LastName")]
        public string ParentLastName { get; set; }

        /// <summary>
        ///     Имя
        /// </summary>
        [Display(Description = "Имя")]
        [MaxLength(1000, ErrorMessage = "\"Имя\" не может быть больше 1000 символов")]
        [DataMember(Name = "Parent-FirstName")]
        public string ParentFirstName { get; set; }

        /// <summary>
        ///     Отчество
        /// </summary>
        [Display(Description = "Отчество")]
        [MaxLength(1000, ErrorMessage = "\"Отчество\" не может быть больше 1000 символов")]
        [DataMember(Name = "Parent-MiddleName")]
        public string ParentMiddleName { get; set; }

        /// <summary>
        ///     Признак что есть отчество
        /// </summary>
        [Display(Description = "Признак что есть отчество")]
        [Required(ErrorMessage = "\"Признак что есть отчество\" должно быть заполнено")]
        [DataMember(Name = "Parent-HaveMiddleName")]
        public bool ParentHaveMiddleName { get; set; }

        /// <summary>
        ///     Серия документа
        /// </summary>
        [Display(Description = "Серия документа")]
        [MaxLength(1000, ErrorMessage = "\"Серия документа\" не может быть больше 1000 символов")]
        [DataMember(Name = "Parent-DocumentSeria")]
        public string ParentDocumentSeria { get; set; }

        /// <summary>
        ///     Номер документа
        /// </summary>
        [Display(Description = "Номер документа")]
        [MaxLength(1000, ErrorMessage = "\"Номер документа\" не может быть больше 1000 символов")]
        [DataMember(Name = "Parent-DocumentNumber")]
        public string ParentDocumentNumber { get; set; }

        /// <summary>
        ///     Дата выдачи документа
        /// </summary>
        [Display(Description = "Дата выдачи документа")]
        [DataMember(Name = "Parent-DocumentDateOfIssue")]
        public string ParentDocumentDateOfIssue { get; set; }

        /// <summary>
        ///     Кем выдан документ
        /// </summary>
        [Display(Description = "Кем выдан документ")]
        [MaxLength(1000, ErrorMessage = "\"Кем выдан документ\" не может быть больше 1000 символов")]
        [DataMember(Name = "Parent-DocumentSubjectIssue")]
        public string ParentDocumentSubjectIssue { get; set; }

        /// <summary>
        ///     Дата рождения
        /// </summary>
        [Display(Description = "Дата рождения")]
        [DataMember(Name = "Parent-DateOfBirth")]
        public string ParentDateOfBirth { get; set; }

        /// <summary>
        ///     Мужской пол
        /// </summary>
        [Display(Description = "Мужской пол")]
        [Required(ErrorMessage = "\"Мужской пол\" должно быть заполнено")]
        [DataMember(Name = "Parent-Male")]
        public bool ParentMale { get; set; }

        /// <summary>
        ///     Место рождения
        /// </summary>
        [Display(Description = "Место рождения")]
        [MaxLength(1000, ErrorMessage = "\"Место рождения\" не может быть больше 1000 символов")]
        [DataMember(Name = "Parent-PlaceOfBirth")]
        public string ParentPlaceOfBirth { get; set; }

        /// <summary>
        ///     Снилс
        /// </summary>
        [Display(Description = "СНИЛС")]
        [DataMember(Name = "Parent-Snils")]
        public string ParentSnils { get; set; }

        /// <summary>
        ///     Телефон
        /// </summary>
        [Display(Description = "Телефон")]
        [MaxLength(1000, ErrorMessage = "\"Телефон\" не может быть больше 1000 символов")]
        [DataMember(Name = "Parent-Phone")]
        public string ParentPhone { get; set; }

        /// <summary>
        ///     Электронная почта
        /// </summary>
        [Display(Description = "Электронная почта")]
        [MaxLength(1000, ErrorMessage = "\"Электронная почта\" не может быть больше 1000 символов")]
        [DataMember(Name = "Parent-Email")]
        public string ParentEmail { get; set; }

        /// <summary>
        ///     Вид документа
        /// </summary>
        [DataMember(Name = "Parent-DocumentTypeId")]
        [Display(Description = "Вид документа")]
        public long? ParentDocumentTypeId { get; set; }

        /// <summary>
        ///     Вид документа
        /// </summary>
        [DataMember(Name = "Parent-DocumentTypeName")]
        [Display(Description = "Вид документа")]
        public string ParentDocumentTypeName { get; set; }

        #region Address

        /// <summary>
        ///     Адрес регистрации
        /// </summary>
        [DataMember(Name = "Parent-AddressId")]
        [Display(Description = "Адрес регистрации")]
        public long? ParentAddressId { get; set; }

        /// <summary>
        ///     Уникальный идентификатор
        /// </summary>
        [Display(Description = "Уникальный идентификатор")]
        [Required(ErrorMessage = "\"Уникальный идентификатор\" должно быть заполнено")]
        [Key]
        [DataMember(Name = "Parent-Address-Id")]
        public long ParentAddressIdObj { get; set; }

        /// <summary>
        ///     Полное наименование
        /// </summary>
        [Display(Description = "Полное наименование")]
        [MaxLength(1000, ErrorMessage = "\"Полное наименование\" не может быть больше 1000 символов")]
        [DataMember(Name = "Parent-Address-Name")]
        public string ParentAddressName { get; set; }

        /// <summary>
        ///     Номер квартиры
        /// </summary>
        [Display(Description = "Номер квартиры")]
        [MaxLength(1000, ErrorMessage = "\"Номер квартиры\" не может быть больше 1000 символов")]
        [DataMember(Name = "Parent-Address-Appartment")]
        public string ParentAddressAppartment { get; set; }

        /// <summary>
        ///     Улица
        /// </summary>
        [Display(Description = "Улица")]
        [MaxLength(1000, ErrorMessage = "\"Улица\" не может быть больше 1000 символов")]
        [DataMember(Name = "Parent-Address-Street")]
        public string ParentAddressStreet { get; set; }

        /// <summary>
        ///     Дом
        /// </summary>
        [Display(Description = "Дом")]
        [MaxLength(1000, ErrorMessage = "\"Дом\" не может быть больше 1000 символов")]
        [DataMember(Name = "Parent-Address-House")]
        public string ParentAddressHouse { get; set; }

        /// <summary>
        ///     Корпус
        /// </summary>
        [Display(Description = "Корпус")]
        [MaxLength(1000, ErrorMessage = "\"Корпус\" не может быть больше 1000 символов")]
        [DataMember(Name = "Parent-Address-Corpus")]
        public string ParentAddressCorpus { get; set; }

        /// <summary>
        ///     Строение
        /// </summary>
        [Display(Description = "Строение")]
        [MaxLength(1000, ErrorMessage = "\"Строение\" не может быть больше 1000 символов")]
        [DataMember(Name = "Parent-Address-Stroenie")]
        public string ParentAddressStroenie { get; set; }

        /// <summary>
        ///     Широта
        /// </summary>
        [Display(Description = "Широта")]
        [DataMember(Name = "Parent-Address-Latitude")]
        public decimal? ParentAddressLatitude { get; set; }

        /// <summary>
        ///     Долгота
        /// </summary>
        [Display(Description = "Долгота")]
        [DataMember(Name = "Parent-Address-Longitude")]
        public decimal? ParentAddressLongitude { get; set; }


        /// <summary>
        ///     Адрес БТИ
        /// </summary>
        [DataMember(Name = "Parent-Address-BtiAddressId")]
        [Display(Description = "Адрес БТИ")]
        public long? ParentAddressBtiAddressId { get; set; }

        /// <summary>
        ///     Район
        /// </summary>
        [DataMember(Name = "Parent-Address-BtiAddress-BtiStreet-Name")]
        [Display(Description = "Район")]
        public long? ParentAddressBtiAddressBtiStreetName { get; set; }

        /// <summary>
        ///     Район
        /// </summary>
        [DataMember(Name = "Parent-Address-BtiAddress-BtiStreet-Id")]
        [Display(Description = "Район")]
        public long? ParentAddressBtiAddressBtiStreetId { get; set; }

        /// <summary>
        ///     Округ
        /// </summary>
        [DataMember(Name = "Parent-Address-BtiDistrictId")]
        [Display(Description = "Округ")]
        public long? ParentAddressBtiDistrictId { get; set; }

        /// <summary>
        ///     Район
        /// </summary>
        [DataMember(Name = "Parent-Address-BtiRegionId")]
        [Display(Description = "Район")]
        public long? ParentAddressBtiRegionId { get; set; }

        #endregion

        #endregion

        #region Unionist

        /// <summary>
        ///     Член профсоюза
        /// </summary>
        [DataMember(Name = "Unionist-Id")]
        [Display(Description = "Ребёнок")]
        public long? UnionistIdObj { get; set; }

        /// <summary>
        ///     Фамилия
        /// </summary>
        [Display(Description = "Фамилия")]
        [MaxLength(1000, ErrorMessage = "\"Фамилия\" не может быть больше 1000 символов")]
        [DataMember(Name = "Unionist-LastName")]
        public string UnionistLastName { get; set; }

        /// <summary>
        ///     Имя
        /// </summary>
        [Display(Description = "Имя")]
        [MaxLength(1000, ErrorMessage = "\"Имя\" не может быть больше 1000 символов")]
        [DataMember(Name = "Unionist-FirstName")]
        public string UnionistFirstName { get; set; }

        /// <summary>
        ///     Отчество
        /// </summary>
        [Display(Description = "Отчество")]
        [MaxLength(1000, ErrorMessage = "\"Отчество\" не может быть больше 1000 символов")]
        [DataMember(Name = "Unionist-MiddleName")]
        public string UnionistMiddleName { get; set; }

        /// <summary>
        ///     Признак что есть отчество
        /// </summary>
        [Display(Description = "Признак что есть отчество")]
        [Required(ErrorMessage = "\"Признак что есть отчество\" должно быть заполнено")]
        [DataMember(Name = "Unionist-HaveMiddleName")]
        public bool UnionistHaveMiddleName { get; set; }

        /// <summary>
        ///     Серия документа
        /// </summary>
        [Display(Description = "Серия документа")]
        [MaxLength(1000, ErrorMessage = "\"Серия документа\" не может быть больше 1000 символов")]
        [DataMember(Name = "Unionist-DocumentSeria")]
        public string UnionistDocumentSeria { get; set; }

        /// <summary>
        ///     Номер документа
        /// </summary>
        [Display(Description = "Номер документа")]
        [MaxLength(1000, ErrorMessage = "\"Номер документа\" не может быть больше 1000 символов")]
        [DataMember(Name = "Unionist-DocumentNumber")]
        public string UnionistDocumentNumber { get; set; }

        /// <summary>
        ///     Дата выдачи документа
        /// </summary>
        [Display(Description = "Дата выдачи документа")]
        [DataMember(Name = "Unionist-DocumentDateOfIssue")]
        public string UnionistDocumentDateOfIssue { get; set; }

        /// <summary>
        ///     Кем выдан документ
        /// </summary>
        [Display(Description = "Кем выдан документ")]
        [MaxLength(1000, ErrorMessage = "\"Кем выдан документ\" не может быть больше 1000 символов")]
        [DataMember(Name = "Unionist-DocumentSubjectIssue")]
        public string UnionistDocumentSubjectIssue { get; set; }

        /// <summary>
        ///     Дата рождения
        /// </summary>
        [Display(Description = "Дата рождения")]
        [DataMember(Name = "Unionist-DateOfBirth")]
        public string UnionistDateOfBirth { get; set; }

        /// <summary>
        ///     Мужской пол
        /// </summary>
        [Display(Description = "Мужской пол")]
        [Required(ErrorMessage = "\"Мужской пол\" должно быть заполнено")]
        [DataMember(Name = "Unionist-Male")]
        public bool UnionistMale { get; set; }

        /// <summary>
        ///     Место рождения
        /// </summary>
        [Display(Description = "Место рождения")]
        [MaxLength(1000, ErrorMessage = "\"Место рождения\" не может быть больше 1000 символов")]
        [DataMember(Name = "Unionist-PlaceOfBirth")]
        public string UnionistPlaceOfBirth { get; set; }

        /// <summary>
        ///     Снилс
        /// </summary>
        [Display(Description = "СНИЛС")]
        [DataMember(Name = "Unionist-Snils")]
        public string UnionistSnils { get; set; }

        /// <summary>
        ///     Телефон
        /// </summary>
        [Display(Description = "Телефон")]
        [MaxLength(1000, ErrorMessage = "\"Телефон\" не может быть больше 1000 символов")]
        [DataMember(Name = "Unionist-Phone")]
        public string UnionistPhone { get; set; }

        /// <summary>
        ///     Электронная почта
        /// </summary>
        [Display(Description = "Электронная почта")]
        [MaxLength(1000, ErrorMessage = "\"Электронная почта\" не может быть больше 1000 символов")]
        [DataMember(Name = "Unionist-Email")]
        public string UnionistEmail { get; set; }

        /// <summary>
        ///     Вид документа
        /// </summary>
        [DataMember(Name = "Unionist-DocumentTypeId")]
        [Display(Description = "Вид документа")]
        public long? UnionistDocumentTypeId { get; set; }

        /// <summary>
        ///     Вид документа
        /// </summary>
        [DataMember(Name = "Unionist-DocumentTypeName")]
        [Display(Description = "Вид документа")]
        public string UnionistDocumentTypeName { get; set; }

        #region Address

        /// <summary>
        ///     Адрес регистрации
        /// </summary>
        [DataMember(Name = "Unionist-AddressId")]
        [Display(Description = "Адрес регистрации")]
        public long? UnionistAddressId { get; set; }

        /// <summary>
        ///     Уникальный идентификатор
        /// </summary>
        [Display(Description = "Уникальный идентификатор")]
        [Required(ErrorMessage = "\"Уникальный идентификатор\" должно быть заполнено")]
        [Key]
        [DataMember(Name = "Unionist-Address-Id")]
        public long UnionistAddressIdObj { get; set; }

        /// <summary>
        ///     Полное наименование
        /// </summary>
        [Display(Description = "Полное наименование")]
        [MaxLength(1000, ErrorMessage = "\"Полное наименование\" не может быть больше 1000 символов")]
        [DataMember(Name = "Unionist-Address-Name")]
        public string UnionistAddressName { get; set; }

        /// <summary>
        ///     Номер квартиры
        /// </summary>
        [Display(Description = "Номер квартиры")]
        [MaxLength(1000, ErrorMessage = "\"Номер квартиры\" не может быть больше 1000 символов")]
        [DataMember(Name = "Unionist-Address-Appartment")]
        public string UnionistAddressAppartment { get; set; }

        /// <summary>
        ///     Улица
        /// </summary>
        [Display(Description = "Улица")]
        [MaxLength(1000, ErrorMessage = "\"Улица\" не может быть больше 1000 символов")]
        [DataMember(Name = "Unionist-Address-Street")]
        public string UnionistAddressStreet { get; set; }

        /// <summary>
        ///     Дом
        /// </summary>
        [Display(Description = "Дом")]
        [MaxLength(1000, ErrorMessage = "\"Дом\" не может быть больше 1000 символов")]
        [DataMember(Name = "Unionist-Address-House")]
        public string UnionistAddressHouse { get; set; }

        /// <summary>
        ///     Корпус
        /// </summary>
        [Display(Description = "Корпус")]
        [MaxLength(1000, ErrorMessage = "\"Корпус\" не может быть больше 1000 символов")]
        [DataMember(Name = "Unionist-Address-Corpus")]
        public string UnionistAddressCorpus { get; set; }

        /// <summary>
        ///     Строение
        /// </summary>
        [Display(Description = "Строение")]
        [MaxLength(1000, ErrorMessage = "\"Строение\" не может быть больше 1000 символов")]
        [DataMember(Name = "Unionist-Address-Stroenie")]
        public string UnionistAddressStroenie { get; set; }

        /// <summary>
        ///     Широта
        /// </summary>
        [Display(Description = "Широта")]
        [DataMember(Name = "Unionist-Address-Latitude")]
        public decimal? UnionistAddressLatitude { get; set; }

        /// <summary>
        ///     Долгота
        /// </summary>
        [Display(Description = "Долгота")]
        [DataMember(Name = "Unionist-Address-Longitude")]
        public decimal? UnionistAddressLongitude { get; set; }


        /// <summary>
        ///     Адрес БТИ
        /// </summary>
        [DataMember(Name = "Unionist-Address-BtiAddressId")]
        [Display(Description = "Адрес БТИ")]
        public long? UnionistAddressBtiAddressId { get; set; }

        /// <summary>
        ///     Район
        /// </summary>
        [DataMember(Name = "Unionist-Address-BtiAddress-BtiStreet-Name")]
        [Display(Description = "Район")]
        public long? UnionistAddressBtiAddressBtiStreetName { get; set; }

        /// <summary>
        ///     Район
        /// </summary>
        [DataMember(Name = "Unionist-Address-BtiAddress-BtiStreet-Id")]
        [Display(Description = "Район")]
        public long? UnionistAddressBtiAddressBtiStreetId { get; set; }

        /// <summary>
        ///     Округ
        /// </summary>
        [DataMember(Name = "Unionist-Address-BtiDistrictId")]
        [Display(Description = "Округ")]
        public long? UnionistAddressBtiDistrictId { get; set; }

        /// <summary>
        ///     Район
        /// </summary>
        [DataMember(Name = "Unionist-Address-BtiRegionId")]
        [Display(Description = "Район")]
        public long? UnionistAddressBtiRegionId { get; set; }

        #endregion

        #endregion
    }
}
