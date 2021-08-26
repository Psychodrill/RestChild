// File:    RequestFileType.cs
// Purpose: Definition of Class RequestFileType

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Тип файла заявления
   /// </summary>
   [Serializable]
   [DataContract(Name = "requestFileType")]
   public partial class RequestFileType : IEntityBase
   {
      
      /// <summary>
      /// Уникальный идентификатор
      /// </summary>
      [Display(Description = "Уникальный идентификатор")]
      [Required(ErrorMessage = "\"Уникальный идентификатор\" должно быть заполнено")]
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.None)]
      [DataMember(Name = "id", EmitDefaultValue = false)]
      public virtual long Id { get; set; }
      
      /// <summary>
      /// Имя типа
      /// </summary>
      [Display(Description = "Имя типа")]
      [MaxLength(1000, ErrorMessage = "\"Имя типа\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Имя типа\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Активность
      /// </summary>
      [Display(Description = "Активность")]
      [Required(ErrorMessage = "\"Активность\" должно быть заполнено")]
      [DataMember(Name = "isActive", EmitDefaultValue = false)]
      public virtual bool IsActive { get; set; }
      
      /// <summary>
      /// Для заявления МПГУ
      /// </summary>
      [Display(Description = "Для заявления МПГУ")]
      [Required(ErrorMessage = "\"Для заявления МПГУ\" должно быть заполнено")]
      [DataMember(Name = "forMpgu", EmitDefaultValue = false)]
      public virtual bool ForMpgu { get; set; }
      
      /// <summary>
      /// Для заявления Оператора
      /// </summary>
      [Display(Description = "Для заявления Оператора")]
      [Required(ErrorMessage = "\"Для заявления Оператора\" должно быть заполнено")]
      [DataMember(Name = "forOperator", EmitDefaultValue = false)]
      public virtual bool ForOperator { get; set; }
      
      /// <summary>
      /// Код документа в ЦХЭД
      /// </summary>
      [Display(Description = "Код документа в ЦХЭД")]
      [MaxLength(1000, ErrorMessage = "\"Код документа в ЦХЭД\" не может быть больше 1000 символов")]
      [DataMember(Name = "codeChed", EmitDefaultValue = false)]
      public virtual string CodeChed { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "codeAsGuf", EmitDefaultValue = false)]
      public virtual string CodeAsGuf { get; set; }
      
      
      /// <summary>
      /// Связь видов отдыха и типов файлов
      /// </summary>
      [InverseProperty("RequestFileTypes")]
      [DataMember(Name = "typeOfRests", EmitDefaultValue = false)]
      public virtual ICollection<TypeOfRest> TypeOfRests { get; set; }

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