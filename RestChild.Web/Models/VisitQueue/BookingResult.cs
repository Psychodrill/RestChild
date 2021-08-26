namespace RestChild.Web.Models.VisitQueue
{
   /// <summary>
   /// Результат обработки бронирования
   /// </summary>
   public class BookingResult
   {
      /// <summary>
      /// Код результата
      /// </summary>
      public long Code { get; set; }

      /// <summary>
      /// Комментарий к результату
      /// </summary>
      public string Messeage { get; set; }

      /// <summary>
      /// Идентификатор бронирования
      /// </summary>
      public string BookingId { get; set; }
   }
}
