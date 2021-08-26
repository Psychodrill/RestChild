using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.Web.Models.VisitQueue
{
   /// <summary>
   /// Цель визита
   /// </summary>
   public class VisitTarget : IVisitTarget
   {
      /// <summary>
      /// Идентификатор в АИСДО
      /// </summary>
      public long Id { get; set; }

      /// <summary>
      /// Наименование
      /// </summary>
      public string Name { get; set; }

      /// <summary>
      /// Описание (?)
      /// </summary>
      public string Description { get; set; }
   }
}
