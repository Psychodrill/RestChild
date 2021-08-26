using System.Collections.Generic;
using RestChild.Domain;
using RestChild.Web.Properties;

namespace RestChild.Web.Models.Limits
{
   /// <summary>
   ///    модель редактирования списков организации
   /// </summary>
   public class ListsOfChildModel : ViewModelBase<ListOfChilds>
   {
      /// <summary>
      ///    конструктор
      /// </summary>
      public ListsOfChildModel() : base(new ListOfChilds
         {Attendants = new List<Applicant>(), Childs = new List<Child>()})
      {
      }

      /// <summary>
      ///    конструктор
      /// </summary>
      public ListsOfChildModel(ListOfChilds data) : base(data)
      {
      }

      /// <summary>
      ///    можно редактировать
      /// </summary>
      public bool CanEdit { get; set; }

      /// <summary>
      ///    статус
      /// </summary>
      public ViewModelState State { get; set; }

      /// <summary>
      ///    Код действия.
      /// </summary>
      public string StringStateCode { get; set; }

      /// <summary>
      ///    Регионы отдыха
      /// </summary>
      public IList<PlaceOfRest> PlaceOfRests { get; set; }

      /// <summary>
      ///    справочник категорий
      /// </summary>
      public IList<ListOfChildsCategory> ListOfChildsCategorys { get; set; }

      /// <summary>
      ///    время отдыха
      /// </summary>
      public IList<TimeOfRest> TimeOfRests { get; set; }

      /// <summary>
      ///    документы для сопровождающих
      /// </summary>
      public IList<DocumentType> DocumentTypesAttendat { get; set; }

      /// <summary>
      ///    документы для детей
      /// </summary>
      public IList<DocumentType> DocumentTypesChild { get; set; }

      /// <summary>
      ///    список ошибок
      /// </summary>
      public List<string> Errors { get; set; }

      /// <summary>
      ///    Дети, найденных в других списках
      /// </summary>
      public IList<Child> ChildrenInAnotherLists { get; set; }

      /// <summary>
      ///    Похожие дети, найденных в других списках
      /// </summary>
      public IList<Child> SimilarChildrenInAnotherLists { get; set; }

      /// <summary>
      ///    Дети, присутствующие в заявлениях на то же время
      /// </summary>
      public IList<Child> ChildrenInSameTimeRequests { get; set; }

      /// <summary>
      ///    Сопровождающие, присутствующие в списках и заявлениях на то же время
      /// </summary>
      public IList<Applicant> ApplicantsInSameTimeRequests { get; set; }

      /// <summary>
      ///    Сопровождающие, найденных в других списках
      /// </summary>
      public IList<Applicant> ApplicantsInAnotherLists { get; set; }

      /// <summary>
      ///    Вид ограничения
      /// </summary>
      public IList<TypeOfRestriction> TypeOfRestrictions { get; set; }

      /// <summary>
      ///    Справочник услуг для детей
      /// </summary>
      public IList<AddonServices> AddonServicesForChilds { get; set; }

      /// <summary>
      ///    Справочник услуг для сопровождающих
      /// </summary>
      public IList<AddonServices> AddonServicesForAttendants { get; set; }


      /// <summary>
      ///    Список возможных заявок для копирования из них отдыхающих
      /// </summary>
      public IList<ListofLimitsToCopy> ListOfLimitsToCopy { get; set; }
   }
}
