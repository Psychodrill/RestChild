// File:    TradeUnionCamper.cs
// Purpose: Definition of Class TradeUnionCamper

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Отдыхающий
   /// </summary>
   [Serializable]
   [DataContract(Name = "tradeUnionCamper")]
   public partial class TradeUnionCamper : IEntityBase
   {
      
      /// <summary>
      /// Уникальный идентификатор
      /// </summary>
      [Display(Description = "Уникальный идентификатор")]
      [Required(ErrorMessage = "\"Уникальный идентификатор\" должно быть заполнено")]
      [Key]
      [DataMember(Name = "id", EmitDefaultValue = false)]
      public virtual long Id { get; set; }
      
      /// <summary>
      /// Адрес регистрации ребёнка
      /// </summary>
      [Display(Description = "Адрес регистрации ребёнка")]
      [MaxLength(1000, ErrorMessage = "\"Адрес регистрации ребёнка\" не может быть больше 1000 символов")]
      [DataMember(Name = "addressChild", EmitDefaultValue = false)]
      public virtual string AddressChild { get; set; }
      
      /// <summary>
      /// Образовательное учреждение
      /// </summary>
      [Display(Description = "Образовательное учреждение")]
      [MaxLength(1000, ErrorMessage = "\"Образовательное учреждение\" не может быть больше 1000 символов")]
      [DataMember(Name = "school", EmitDefaultValue = false)]
      public virtual string School { get; set; }
      
      /// <summary>
      /// Место работы родителя
      /// </summary>
      [Display(Description = "Место работы родителя")]
      [MaxLength(1000, ErrorMessage = "\"Место работы родителя\" не может быть больше 1000 символов")]
      [DataMember(Name = "parentPlaceWork", EmitDefaultValue = false)]
      public virtual string ParentPlaceWork { get; set; }
      
      /// <summary>
      /// Член профсоюза
      /// </summary>
      [Display(Description = "Член профсоюза")]
      [Required(ErrorMessage = "\"Член профсоюза\" должно быть заполнено")]
      [DataMember(Name = "isParentUnionist", EmitDefaultValue = false)]
      public virtual bool IsParentUnionist { get; set; }
      
      /// <summary>
      /// Родственник члена профсоюза
      /// </summary>
      [Display(Description = "Родственник члена профсоюза")]
      [Required(ErrorMessage = "\"Родственник члена профсоюза\" должно быть заполнено")]
      [DataMember(Name = "isRelativeUnionist", EmitDefaultValue = false)]
      public virtual bool IsRelativeUnionist { get; set; }
      
      /// <summary>
      /// Стоимость полная
      /// </summary>
      [Display(Description = "Стоимость полная")]
      [DataMember(Name = "summa", EmitDefaultValue = false)]
      public virtual decimal? Summa { get; set; }
      
      /// <summary>
      /// Средства родителей
      /// </summary>
      [Display(Description = "Средства родителей")]
      [DataMember(Name = "summaParent", EmitDefaultValue = false)]
      public virtual decimal? SummaParent { get; set; }
      
      /// <summary>
      /// Средства профсоюза
      /// </summary>
      [Display(Description = "Средства профсоюза")]
      [DataMember(Name = "summaTradeUnion", EmitDefaultValue = false)]
      public virtual decimal? SummaTradeUnion { get; set; }
      
      /// <summary>
      /// Бюджетные средства
      /// </summary>
      [Display(Description = "Бюджетные средства")]
      [DataMember(Name = "summaBudget", EmitDefaultValue = false)]
      public virtual decimal? SummaBudget { get; set; }
      
      /// <summary>
      /// Средства предприятия
      /// </summary>
      [Display(Description = "Средства предприятия")]
      [DataMember(Name = "summaOrganization", EmitDefaultValue = false)]
      public virtual decimal? SummaOrganization { get; set; }
      
      /// <summary>
      /// Место работы члена профсоюза
      /// </summary>
      [Display(Description = "Место работы члена профсоюза")]
      [MaxLength(1000, ErrorMessage = "\"Место работы члена профсоюза\" не может быть больше 1000 символов")]
      [DataMember(Name = "relativePlaceWork", EmitDefaultValue = false)]
      public virtual string RelativePlaceWork { get; set; }
      
      /// <summary>
      /// Заехал
      /// </summary>
      [Display(Description = "Заехал")]
      [Required(ErrorMessage = "\"Заехал\" должно быть заполнено")]
      [DataMember(Name = "isChecked", EmitDefaultValue = false)]
      public virtual bool IsChecked { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "isScoolNotPresent", EmitDefaultValue = false)]
      public virtual bool IsScoolNotPresent { get; set; }
      
      /// <summary>
      /// Профсоюз (строкой)
      /// </summary>
      [Display(Description = "Профсоюз (строкой)")]
      [MaxLength(1000, ErrorMessage = "\"Профсоюз (строкой)\" не может быть больше 1000 символов")]
      [DataMember(Name = "tradeUnionOrganizationOther", EmitDefaultValue = false)]
      public virtual string TradeUnionOrganizationOther { get; set; }
      
      
      /// <summary>
      /// Список отдыхающих
      /// </summary>
      [ForeignKey("TradeUnion")]
      [DataMember(Name = "tradeUnionId")]
      [Display(Description = "Список отдыхающих")]
      public virtual long? TradeUnionId { get; set; }
      
      
      /// <summary>
      /// Список отдыхающих
      /// </summary>
      [InverseProperty("Campers")]
      [Display(Description = "Список отдыхающих")]
      [DataMember(Name = "tradeUnion", EmitDefaultValue = false)]
      public virtual TradeUnionList TradeUnion { get; set; }
      
      /// <summary>
      /// Ребёнок
      /// </summary>
      [ForeignKey("Child")]
      [DataMember(Name = "childId")]
      [Display(Description = "Ребёнок")]
      public virtual long? ChildId { get; set; }
      
      
      /// <summary>
      /// Ребёнок
      /// </summary>
      [InverseProperty("TradeUnionCamper")]
      [Display(Description = "Ребёнок")]
      [DataMember(Name = "child", EmitDefaultValue = false)]
      public virtual Person Child { get; set; }
      
      /// <summary>
      /// Родитель
      /// </summary>
      [ForeignKey("Parent")]
      [DataMember(Name = "parentId")]
      [Display(Description = "Родитель")]
      public virtual long? ParentId { get; set; }
      
      
      /// <summary>
      /// Родитель
      /// </summary>
      [Display(Description = "Родитель")]
      [DataMember(Name = "parent", EmitDefaultValue = false)]
      public virtual Person Parent { get; set; }
      
      /// <summary>
      /// Член профсоюза
      /// </summary>
      [ForeignKey("Unionist")]
      [DataMember(Name = "unionistId")]
      [Display(Description = "Член профсоюза")]
      public virtual long? UnionistId { get; set; }
      
      
      /// <summary>
      /// Член профсоюза
      /// </summary>
      [Display(Description = "Член профсоюза")]
      [DataMember(Name = "unionist", EmitDefaultValue = false)]
      public virtual Person Unionist { get; set; }
      
      /// <summary>
      /// Статус по отношению к ребёнку
      /// </summary>
      [ForeignKey("TradeUnionStatusByChild")]
      [DataMember(Name = "tradeUnionStatusByChildId")]
      [Display(Description = "Статус по отношению к ребёнку")]
      public virtual long? TradeUnionStatusByChildId { get; set; }
      
      
      /// <summary>
      /// Статус по отношению к ребёнку
      /// </summary>
      [Display(Description = "Статус по отношению к ребёнку")]
      [DataMember(Name = "tradeUnionStatusByChild", EmitDefaultValue = false)]
      public virtual TradeUnionStatusByChild TradeUnionStatusByChild { get; set; }
      
      /// <summary>
      /// Школа
      /// </summary>
      [ForeignKey("SelectedSchool")]
      [DataMember(Name = "selectedSchoolId")]
      [Display(Description = "Школа")]
      public virtual long? SelectedSchoolId { get; set; }
      
      
      /// <summary>
      /// Школа
      /// </summary>
      [Display(Description = "Школа")]
      [DataMember(Name = "selectedSchool", EmitDefaultValue = false)]
      public virtual School SelectedSchool { get; set; }
      
      /// <summary>
      /// Ссылка на файлы
      /// </summary>
      [ForeignKey("LinkToFile")]
      [DataMember(Name = "linkToFileId")]
      [Display(Description = "Ссылка на файлы")]
      public virtual long? LinkToFileId { get; set; }
      
      
      /// <summary>
      /// Ссылка на файлы
      /// </summary>
      [Display(Description = "Ссылка на файлы")]
      [DataMember(Name = "linkToFile", EmitDefaultValue = false)]
      public virtual LinkToFile LinkToFile { get; set; }
      
      /// <summary>
      /// Профсоюз
      /// </summary>
      [ForeignKey("TradeUnionOrganization")]
      [DataMember(Name = "tradeUnionOrganizationId")]
      [Display(Description = "Профсоюз")]
      public virtual long? TradeUnionOrganizationId { get; set; }
      
      
      /// <summary>
      /// Профсоюз
      /// </summary>
      [Display(Description = "Профсоюз")]
      [DataMember(Name = "tradeUnionOrganization", EmitDefaultValue = false)]
      public virtual Organization TradeUnionOrganization { get; set; }

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