using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.Web.Models.VisitQueue
{
   /// <summary>
   /// Пребронирование
   /// </summary>
   public class Booking
   {
      /// <summary>
      /// ФИО заявителя
      /// </summary>
      public string FIO { get; set; }

      /// <summary>
      /// Идентификатор цели визита
      /// </summary>
      public long VisitTargetId { get; set; }

      /// <summary>
      /// Колличество бронируемых слотов
      /// </summary>
      public long VisitSlotsCount { get; set; }

      /// <summary>
      /// Ячейка визита
      /// </summary>
      public DateTime VisitSlot { get; set; }

      /// <summary>
      /// СНИЛС
      /// </summary>
      public string SNILS { get; set; }
   }
}
