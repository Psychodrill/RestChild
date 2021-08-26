using Newtonsoft.Json;
using RestChild.DAL;
using RestChild.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace MailingDemon.Tasks
{
   [Task]
   public class BookingMosgorturParseTask : BaseTask
   {
      #region BookingMessage

      private struct MSG
      {
         public JData[] data { get; set; }

         public IDictionary<string, JPerson> included { get; set; }
      }

      private struct JData
      {
         public string type { get; set; }
         public string id { get; set;}
         public JDAttribute attributes { get; set; }
         public IDictionary<string, Relationship> relationships { get; set; }
      }

      private struct JDAttribute
      {
         public string title { get; set; }
         public string target { get; set; }
         public string number { get; set; }
         public string date { get; set; }
         public string time { get; set; }
         public string office { get; set; }
         public string address { get; set; }
      }

      private struct Relationship
      {
         public JRData[] data { get; set; }
      }

      public struct JRData
      {
         public string type { get; set; }
         public string id { get; set; }
      }

      public struct JPerson
      {
         public string type { get; set; }
         public string id { get; set; }
         public JRAttribute attributes { get; set; }
      }

      public struct JRAttribute
      {
         public string name { get; set; }
         public string patronymic { get; set; }
         public string surname { get; set; }
         public string sex { get; set; }
         public string birth_date { get; set; }
         public string phone { get; set; }
         public string snils { get; set; }
         public string email { get; set; }
         public string privelege { get; set; }
      }

      #endregion

      protected override void Execute()
      {
         return;
/*
         try
         {
            Logger.Info("Разбор сообщений МосГорТур начало");

            List<ScheduleMessage> Messages = new List<ScheduleMessage>(0);

            using (var unitOfWork = new UnitOfWork())
            {
               Messages = unitOfWork.GetSet<ScheduleMessage>().Where(m => !m.HasError && !m.Processed).ToList();
            }

            foreach(var message in Messages)
            {
               ParseMessage(message);
            }

            Logger.Info("Разбор сообщений МосГорТур конец");
         }
         catch (Exception ex)
         {
            Logger.Error("BookingMosgorturParse error", ex);
         }
*/
      }

      private long AddBooking(RestChild.Comon.IUnitOfWork unitOfWork, JData _data, ref MSG msg)
      {
         throw new NotImplementedException();


         //var _t = unitOfWork.GetSet<MGTVisitTarget>().FirstOrDefault(ss => ss.Name == _data.attributes.target);
         //if (_t == null)
         //{
         //   unitOfWork.AddEntity(new MGTVisitTarget() { IsActive = true, Name = _data.attributes.target });
         //}

         //MGTVisitBooking booking = new MGTVisitBooking()
         //{
         //   BookingCode = _data.id,
         //   Canceled = false,
         //   DateShedule = Convert.ToDateTime(string.Format("{0} {1}", _data.attributes.date, _data.attributes.time).Trim()),
         //   Number = _data.attributes.number,
         //   Office = _data.attributes.office,
         //   Target = _data.attributes.target,
         //   Title = _data.attributes.title,
         //   TypeBooking = _data.type,
         //   Address = _data.attributes.address
         //};

         //booking = unitOfWork.AddEntity(booking);




         //foreach (var _relation in _data.relationships)
         //{
         //   foreach(var _relation_data in _relation.Value.data)
         //   {
         //      var _person = msg.included[_relation_data.id];
         //      MGTVisitBookingPerson person = new MGTVisitBookingPerson()
         //      {
         //         Benefit = _person.attributes.privelege,
         //         DateOfBirth = string.IsNullOrWhiteSpace(_person.attributes.birth_date) ? (DateTime?)null : DateTime.Parse(_person.attributes.birth_date),
         //         Email = _person.attributes.email,
         //         FirstName = _person.attributes.name,
         //         LastName = _person.attributes.surname,
         //         MiddleName = _person.attributes.patronymic,
         //         Male = string.IsNullOrWhiteSpace(_person.attributes.sex) || _person.attributes.sex == "1",
         //         Phone = _person.attributes.phone,
         //         Snils = _person.attributes.snils,
         //         TypePerson = _relation.Key,
         //         PersonType = unitOfWork.GetSet<MGTVisitBookingPersonType>().FirstOrDefault(p => p.Code.ToLower() == _relation.Key),
         //         VisitBookingId = booking.Id
         //      };
         //      unitOfWork.AddEntity(person);
         //   }
         //}
         //return booking.Id;
      }

      private void CancelBooking(RestChild.Comon.IUnitOfWork unitOfWork, JData _data)
      {
         //var booking = unitOfWork.GetSet<MGTVisitBooking>().FirstOrDefault(ss => ss.BookingCode.Trim().ToLower() == _data.id.Trim().ToLower());
         //if (booking == null)
         //{
         //   return;
         //   //throw new Exception("Ошибка типа отмены или заявка не найдена");
         //}
         //booking.Canceled = true;
         //unitOfWork.Update(booking);
      }

      private void SetProssedMark(RestChild.Comon.IUnitOfWork unitOfWork, long msgId, long? bookingId = null)
      {
         //var _msg = unitOfWork.GetSet<ScheduleMessage>().First(ss => ss.Id == msgId);
         //_msg.Processed = true;
         //if (bookingId.HasValue)
         //{
         //   _msg.MGTVisitBookingId = bookingId.Value;
         //}

         //unitOfWork.Update(_msg);

         //unitOfWork.SaveChanges();
      }

      private void ParseMessage(ScheduleMessage Message)
      {
         try
         {
            if (string.IsNullOrWhiteSpace(Message.Message))
               throw new ArgumentNullException("Message");

            MSG msg = JsonConvert.DeserializeObject<MSG>(Message.Message);

            using (var unitOfWork = new UnitOfWork())
            {
               unitOfWork.BeginTransaction();

               foreach (var _data in msg.data)
               {
                  if(_data.type == "confirmBooking")
                  {
                     var bookingId = AddBooking(unitOfWork, _data, ref msg);
                     SetProssedMark(unitOfWork, Message.Id, bookingId);
                  }
                  else if(_data.type == "cancelBooking")
                  {
                     CancelBooking(unitOfWork, _data);
                     SetProssedMark(unitOfWork, Message.Id);
                  }
                  else
                  {
                     throw new Exception("Unknown message type");
                  }
               }
               unitOfWork.Commit();
            }

         }
         catch(Exception ex)
         {
            try
            {
               using (var unitOfWork = new UnitOfWork())
               {
                  var msg = unitOfWork.GetSet<ScheduleMessage>().First(ss => ss.Id == Message.Id);
                  msg.Processed = true;
                  msg.HasError = true;
                  msg.ErrorMessage = ex.Message;
                  unitOfWork.Update(msg);

                  unitOfWork.SaveChanges();
               }
            }
            catch (Exception ez)
            {
               Logger.Error(string.Format("BookingMosgorturParse error (message: {0})", Message.Id), ez);
            }
         }
      }
   }
}
