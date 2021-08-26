// File:    Pupil.cs
// Purpose: Definition of Class Pupil

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Воспитанник
   /// </summary>
   [Serializable]
   [DataContract(Name = "pupil")]
   public partial class Pupil : IEntityBase
   {
      
      /// <summary>
      /// Уникальный идентифкатор
      /// </summary>
      [Display(Description = "Уникальный идентифкатор")]
      [Required(ErrorMessage = "\"Уникальный идентифкатор\" должно быть заполнено")]
      [Key]
      [DataMember(Name = "id", EmitDefaultValue = false)]
      public virtual long Id { get; set; }
      
      /// <summary>
      /// Дата зачисления
      /// </summary>
      [Display(Description = "Дата зачисления")]
      [Required(ErrorMessage = "\"Дата зачисления\" должно быть заполнено")]
      [DataMember(Name = "dateIn", EmitDefaultValue = false)]
      public virtual DateTime DateIn { get; set; }
      
      /// <summary>
      /// Дата отчисления
      /// </summary>
      [Display(Description = "Дата отчисления")]
      [DataMember(Name = "dateOut", EmitDefaultValue = false)]
      public virtual DateTime? DateOut { get; set; }
      
      /// <summary>
      /// Учреждение выбытия
      /// </summary>
      [Display(Description = "Учреждение выбытия")]
      [MaxLength(1000, ErrorMessage = "\"Учреждение выбытия\" не может быть больше 1000 символов")]
      [DataMember(Name = "organisationOut", EmitDefaultValue = false)]
      public virtual string OrganisationOut { get; set; }
      
      /// <summary>
      /// Образовательного учреждения нет в списке
      /// </summary>
      [Display(Description = "Образовательного учреждения нет в списке")]
      [Required(ErrorMessage = "\"Образовательного учреждения нет в списке\" должно быть заполнено")]
      [DataMember(Name = "schoolNotFound", EmitDefaultValue = false)]
      public virtual bool SchoolNotFound { get; set; }
      
      /// <summary>
      /// Образовательное учреждение
      /// </summary>
      [Display(Description = "Образовательное учреждение")]
      [MaxLength(1000, ErrorMessage = "\"Образовательное учреждение\" не может быть больше 1000 символов")]
      [DataMember(Name = "schoolName", EmitDefaultValue = false)]
      public virtual string SchoolName { get; set; }
      
      /// <summary>
      /// Глютеновое питание
      /// </summary>
      [Display(Description = "Глютеновое питание")]
      [Required(ErrorMessage = "\"Глютеновое питание\" должно быть заполнено")]
      [DataMember(Name = "glutenFreeFood", EmitDefaultValue = false)]
      public virtual bool GlutenFreeFood { get; set; }
      
      /// <summary>
      /// Протёртое питание
      /// </summary>
      [Display(Description = "Протёртое питание")]
      [Required(ErrorMessage = "\"Протёртое питание\" должно быть заполнено")]
      [DataMember(Name = "pureedFood", EmitDefaultValue = false)]
      public virtual bool PureedFood { get; set; }
      
      /// <summary>
      /// Питание, дополнительная информация (аллергия и пр.)
      /// </summary>
      [Display(Description = "Питание, дополнительная информация (аллергия и пр.)")]
      [MaxLength(1000, ErrorMessage = "\"Питание, дополнительная информация (аллергия и пр.)\" не может быть больше 1000 символов")]
      [DataMember(Name = "foodAditionals", EmitDefaultValue = false)]
      public virtual string FoodAditionals { get; set; }
      
      /// <summary>
      /// Все необходимые данные заполнены
      /// </summary>
      [Display(Description = "Все необходимые данные заполнены")]
      [Required(ErrorMessage = "\"Все необходимые данные заполнены\" должно быть заполнено")]
      [DataMember(Name = "filled", EmitDefaultValue = false)]
      public virtual bool Filled { get; set; }
      
      /// <summary>
      /// Нарушение Правил
      /// </summary>
      [Display(Description = "Нарушение Правил")]
      [Required(ErrorMessage = "\"Нарушение Правил\" должно быть заполнено")]
      [DataMember(Name = "foul", EmitDefaultValue = false)]
      public virtual bool Foul { get; set; }
      
      /// <summary>
      /// Нарушение Правил с ограничением региона
      /// </summary>
      [Display(Description = "Нарушение Правил с ограничением региона")]
      [Required(ErrorMessage = "\"Нарушение Правил с ограничением региона\" должно быть заполнено")]
      [DataMember(Name = "foulRegionRestriction", EmitDefaultValue = false)]
      public virtual bool FoulRegionRestriction { get; set; }
      
      /// <summary>
      /// Нарушение Правил с ограничением региона c
      /// </summary>
      [Display(Description = "Нарушение Правил с ограничением региона c")]
      [DataMember(Name = "foulRegionRestrictionFrom", EmitDefaultValue = false)]
      public virtual DateTime? FoulRegionRestrictionFrom { get; set; }
      
      /// <summary>
      /// Нарушение Правил с ограничением региона по
      /// </summary>
      [Display(Description = "Нарушение Правил с ограничением региона по")]
      [DataMember(Name = "foulRegionRestrictionTo", EmitDefaultValue = false)]
      public virtual DateTime? FoulRegionRestrictionTo { get; set; }
      
      
      /// <summary>
      /// Воспитанник -> Наркотики
      /// </summary>
      [InverseProperty("Pupil")]
      [DataMember(Name = "drugs", EmitDefaultValue = false)]
      public virtual ICollection<PupilDose> Drugs { get; set; }
      
      /// <summary>
      /// Воспитанник -> Адрес учреждение социальной защиты
      /// </summary>
      [ForeignKey("OrphanageAddress")]
      [DataMember(Name = "orphanageAddressId")]
      [Display(Description = "Воспитанник -> Адрес учреждение социальной защиты")]
      public virtual long? OrphanageAddressId { get; set; }
      
      
      /// <summary>
      /// Воспитанник -> Адрес учреждение социальной защиты
      /// </summary>
      [InverseProperty("Pupils")]
      [Display(Description = "Воспитанник -> Адрес учреждение социальной защиты")]
      [DataMember(Name = "orphanageAddress", EmitDefaultValue = false)]
      public virtual OrphanageAddress OrphanageAddress { get; set; }
      
      /// <summary>
      /// Ребенок -> Воспитанник
      /// </summary>
      [ForeignKey("Child")]
      [DataMember(Name = "childId")]
      [Display(Description = "Ребенок -> Воспитанник")]
      public virtual long? ChildId { get; set; }
      
      
      /// <summary>
      /// Ребенок -> Воспитанник
      /// </summary>
      [InverseProperty("Pupils")]
      [Display(Description = "Ребенок -> Воспитанник")]
      [DataMember(Name = "child", EmitDefaultValue = false)]
      public virtual Child Child { get; set; }
      
      /// <summary>
      /// Воспитанник -> Образовательное учреждение
      /// </summary>
      [ForeignKey("School")]
      [DataMember(Name = "schoolId")]
      [Display(Description = "Воспитанник -> Образовательное учреждение")]
      public virtual long? SchoolId { get; set; }
      
      
      /// <summary>
      /// Воспитанник -> Образовательное учреждение
      /// </summary>
      [InverseProperty("Pupil")]
      [Display(Description = "Воспитанник -> Образовательное учреждение")]
      [DataMember(Name = "school", EmitDefaultValue = false)]
      public virtual School School { get; set; }
      
      /// <summary>
      /// Документы воспитанника
      /// </summary>
      [ForeignKey("LinkToFiles")]
      [DataMember(Name = "linkToFilesId")]
      [Display(Description = "Документы воспитанника")]
      public virtual long? LinkToFilesId { get; set; }
      
      
      /// <summary>
      /// Документы воспитанника
      /// </summary>
      [Display(Description = "Документы воспитанника")]
      [DataMember(Name = "linkToFiles", EmitDefaultValue = false)]
      public virtual LinkToFile LinkToFiles { get; set; }
      
      /// <summary>
      /// История изменений воспитанника
      /// </summary>
      [ForeignKey("HistoryLink")]
      [DataMember(Name = "historyLinkId")]
      [Display(Description = "История изменений воспитанника")]
      public virtual long? HistoryLinkId { get; set; }
      
      
      /// <summary>
      /// История изменений воспитанника
      /// </summary>
      [Display(Description = "История изменений воспитанника")]
      [DataMember(Name = "historyLink", EmitDefaultValue = false)]
      public virtual HistoryLink HistoryLink { get; set; }
      
      /// <summary>
      /// Группа (потребность) -> Воспитанник
      /// </summary>
      [InverseProperty("Pupils")]
      [DataMember(Name = "pupilGroups", EmitDefaultValue = false)]
      public virtual ICollection<PupilGroup> PupilGroups { get; set; }
      
      /// <summary>
      /// Связь версий
      /// </summary>
      [ForeignKey("Entity")]
      [DataMember(Name = "entityId")]
      [Display(Description = "Связь версий")]
      public virtual long? EntityId { get; set; }
      
      
      /// <summary>
      /// Связь версий
      /// </summary>
      [Display(Description = "Связь версий")]
      [DataMember(Name = "entity", EmitDefaultValue = false)]
      public virtual Pupil Entity { get; set; }

      /// <summary>
      /// Последнее сохранение
      /// </summary>
      [Display(Description = "Последнее сохранение")]
      [DataMember(Name = "lastUpdateTick", EmitDefaultValue = false)]
      public virtual long LastUpdateTick { get; set; }
      
      /// <summary>
      /// Внешний ключ
      /// </summary>
      [Display(Description = "Внешний ключ")]
      [DataMember(Name = "eid", EmitDefaultValue = false)]
      [Index]
      public virtual long? Eid { get; set; }      

      /// <summary>
      /// Статус обмена по сущности
      /// </summary>
      [Display(Description = "Статус обмена по сущности")]
      [DataMember(Name = "eidSendStatus", EmitDefaultValue = false)]
      [Index]
      public virtual long? EidSendStatus { get; set; }      

      /// <summary>
      /// Дата синхронизации
      /// </summary>
      [Display(Description = "Дата синхронизации")]
      [DataMember(Name = "eidSyncDate", EmitDefaultValue = false)]
      public virtual DateTime? EidSyncDate { get; set; }      
   }
}