using RestChild.Web.Models.VisitQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RestChild.Web.Services.Contract
{
   [ServiceContract]
   public interface IVisitTargetsService
   {
      /// <summary>
      /// Получение целей визита
      /// </summary>
      /// <returns></returns>
      [OperationContract]
      IEnumerable<VisitTarget> GetVisitTargets();
   }
}
