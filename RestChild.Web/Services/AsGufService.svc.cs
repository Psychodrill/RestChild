using System;
using System.Linq;
using System.ServiceModel;
using RestChild.Common.Service.ServiceReference;
using RestChild.Comon;
using RestChild.Domain;
using RestChild.Web.Controllers.WebApi;

namespace RestChild.Web.Services
{
   [ServiceBehavior(Name = "Service", Namespace = "http://asguf.mos.ru/rkis_gu/coordinate/v5/",
      ConfigurationName = "SvcConfiguration", AddressFilterMode = AddressFilterMode.Any)]
   public class Service : IService
   {
      private readonly int?[] _notFinalStatuses = {1000, 1003, 1007, 1010};

      private const string _logTitle = "Взаимодействия с АС УР (приём)";

      public void Acknowledgement(ErrorMessage request)
      {
         log4net.LogManager.GetLogger("Service").Info("Acknowledgement");

         var unitOfWork = WindsorHolder.Resolve<IUnitOfWork>();

         Security.Logger.SecurityLogger.AddToLog(unitOfWork, SecurityJournalEventType.OutSystemsInteractions, _logTitle, "Acknowledgement", "", System.ServiceModel.Web.WebOperationContext.Current.IncomingRequest.UserAgent);

         ExchangeBaseRegistry unit =
            unitOfWork.GetSet<ExchangeBaseRegistry>()
               .FirstOrDefault(e => e.RequestGuid == request.ServiceHeader.RelatesTo);
         if (unit == null)
         {
            unit = new ExchangeBaseRegistry {OperationType = "Acknowledgement", IsIncoming = true};
            unit = unitOfWork.AddEntity(unit);
         }

         unit.AcknolegmentGuid = request.ServiceHeader.MessageId;
         unit.IsProcessed = request.NullSafe(r => r.Error.ErrorCode) != "0";

         if (unit.IsProcessed)
         {
            Child childForUpdate = unit.Child;
            Child childForCurUpdate = null;

            if (childForUpdate != null)
            {
               childForCurUpdate = unitOfWork.GetSet<Child>()
                  .FirstOrDefault(
                     c => c.EntityId == childForUpdate.EntityId && c.Request.IsLast && !c.Request.IsDeleted);
            }

            WebExchangeController.UpdateInfoInChild(childForUpdate,
               "Сведения из базового регистра не получены");
            WebExchangeController.UpdateInfoInChild(childForCurUpdate,
               "Сведения из базового регистра не получены");
         }

         unitOfWork.SaveChanges();
         WindsorHolder.Release(unitOfWork);
      }

      public void SendRequest(CoordinateMessage request)
      {
         var unitOfWork = WindsorHolder.Resolve<IUnitOfWork>();

         Security.Logger.SecurityLogger.AddToLog(unitOfWork, SecurityJournalEventType.OutSystemsInteractions, _logTitle, "SendRequest", "", System.ServiceModel.Web.WebOperationContext.Current.IncomingRequest.UserAgent);

         ExchangeBaseRegistry unit =
            unitOfWork.GetSet<ExchangeBaseRegistry>()
               .FirstOrDefault(e => e.RequestGuid == request.ServiceHeader.RelatesTo);
         if (unit == null)
         {
            unit = new ExchangeBaseRegistry {OperationType = "SendRequest", IsIncoming = true};
            unit = unitOfWork.AddEntity(unit);
         }

         if (!unit.ResponseDate.HasValue)
         {
            unit.ResponseGuid = request.ServiceHeader.MessageId;
            unit.ResponseDate = DateTime.Now;
            unit.ResponseText = Serialization.Serializer(request);
         }

         unit.IsProcessed = false;

         unitOfWork.SaveChanges();
         WindsorHolder.Release(unitOfWork);
      }

      public void SendRequests(SendRequestsMessage request)
      {
         var unitOfWork = WindsorHolder.Resolve<IUnitOfWork>();

         Security.Logger.SecurityLogger.AddToLog(unitOfWork, SecurityJournalEventType.OutSystemsInteractions, _logTitle, "SendRequest", "", System.ServiceModel.Web.WebOperationContext.Current.IncomingRequest.UserAgent);

         ExchangeBaseRegistry unit =
            unitOfWork.GetSet<ExchangeBaseRegistry>()
               .FirstOrDefault(e => e.RequestGuid == request.ServiceHeader.RelatesTo);
         if (unit == null)
         {
            unit = new ExchangeBaseRegistry {OperationType = "SendRequests", IsIncoming = true};
            unit = unitOfWork.AddEntity(unit);
         }

         if (!unit.ResponseDate.HasValue)
         {
            unit.ResponseGuid = request.ServiceHeader.MessageId;
            unit.ResponseDate = DateTime.Now;
            unit.ResponseText = Serialization.Serializer(request);
         }

         unit.IsProcessed = false;

         unitOfWork.SaveChanges();
         WindsorHolder.Release(unitOfWork);
      }

      public void SendTask(CoordinateTaskMessage request)
      {
         var unitOfWork = WindsorHolder.Resolve<IUnitOfWork>();

         Security.Logger.SecurityLogger.AddToLog(unitOfWork, SecurityJournalEventType.OutSystemsInteractions, _logTitle, "SendTask", "", System.ServiceModel.Web.WebOperationContext.Current.IncomingRequest.UserAgent);

         ExchangeBaseRegistry unit =
            unitOfWork.GetSet<ExchangeBaseRegistry>()
               .FirstOrDefault(e => e.RequestGuid == request.ServiceHeader.RelatesTo);
         if (unit == null)
         {
            unit = new ExchangeBaseRegistry {OperationType = "SendTask", IsIncoming = true};
            unit = unitOfWork.AddEntity(unit);
         }

         if (!unit.ResponseDate.HasValue)
         {
            unit.ResponseGuid = request.ServiceHeader.MessageId;
            unit.ResponseDate = DateTime.Now;
            unit.ResponseText = Serialization.Serializer(request);
         }

         unit.IsProcessed = false;

         unitOfWork.SaveChanges();
         WindsorHolder.Release(unitOfWork);
      }

      public void SendTasks(SendTasksMessage request)
      {
         var unitOfWork = WindsorHolder.Resolve<IUnitOfWork>();

         Security.Logger.SecurityLogger.AddToLog(unitOfWork, SecurityJournalEventType.OutSystemsInteractions, _logTitle, "SendTask", "", System.ServiceModel.Web.WebOperationContext.Current.IncomingRequest.UserAgent);

         ExchangeBaseRegistry unit =
            unitOfWork.GetSet<ExchangeBaseRegistry>()
               .FirstOrDefault(e => e.RequestGuid == request.ServiceHeader.RelatesTo);
         if (unit == null)
         {
            unit = new ExchangeBaseRegistry {OperationType = "SendTasks", IsIncoming = true};
            unit = unitOfWork.AddEntity(unit);
         }

         if (!unit.ResponseDate.HasValue)
         {
            unit.ResponseGuid = request.ServiceHeader.MessageId;
            unit.ResponseDate = DateTime.Now;
            unit.ResponseText = Serialization.Serializer(request);
         }

         unit.IsProcessed = false;

         unitOfWork.SaveChanges();
         WindsorHolder.Release(unitOfWork);
      }

      public void SetFilesAndStatus(CoordinateStatusMessage request)
      {
         var unitOfWork = WindsorHolder.Resolve<IUnitOfWork>();

         Security.Logger.SecurityLogger.AddToLog(unitOfWork, SecurityJournalEventType.OutSystemsInteractions, _logTitle, "SetFilesAndStatus", "", System.ServiceModel.Web.WebOperationContext.Current.IncomingRequest.UserAgent);

         ExchangeBaseRegistry unit =
            unitOfWork.GetSet<ExchangeBaseRegistry>()
               .FirstOrDefault(e => e.ServiceNumber == request.ServiceHeader.ServiceNumber);

         if (unit == null || unit.RequestGuid != request.StatusMessage?.RequestId ||
             _notFinalStatuses.Contains(request.StatusMessage?.StatusCode))
         {
            unit = new ExchangeBaseRegistry
            {
               ServiceNumber = request.ServiceHeader.ServiceNumber,
               OperationType = unit == null ? "SetFilesAndStatus" : "SetFilesAndStatusGuidProblem",
               IsIncoming = true,
               ExchangeBaseRegistryTypeId = unit?.ExchangeBaseRegistryTypeId,
               ResponseGuid = request.ServiceHeader.MessageId,
               ResponseDate = DateTime.Now,
               ResponseText = Serialization.Serializer(request)
            };

            unitOfWork.AddEntity(unit);
         }
         else if (!unit.ResponseDate.HasValue)
         {
            unit.ResponseGuid = request.ServiceHeader.MessageId;
            unit.ResponseDate = DateTime.Now;
            unit.ResponseText = Serialization.Serializer(request);
            unit.IsProcessed = false;
         }
         else if (unit.IsProcessed)
         {
            unit.ResponseText = Serialization.Serializer(request);
            unit.ResponseGuid = request.ServiceHeader.MessageId;
            unit.ResponseDate = DateTime.Now;
            unit.IsProcessed = false;
         }

         unitOfWork.SaveChanges();
         WindsorHolder.Release(unitOfWork);
      }

      public void SetFilesAndStatuses(SetFilesAndStatusesMessage request)
      {
         var unitOfWork = WindsorHolder.Resolve<IUnitOfWork>();

         Security.Logger.SecurityLogger.AddToLog(unitOfWork, SecurityJournalEventType.OutSystemsInteractions, _logTitle, "SetFilesAndStatuses", "", System.ServiceModel.Web.WebOperationContext.Current.IncomingRequest.UserAgent);

         ExchangeBaseRegistry unit =
            unitOfWork.GetSet<ExchangeBaseRegistry>()
               .FirstOrDefault(e => e.RequestGuid == request.ServiceHeader.RelatesTo);
         if (unit == null)
         {
            unit = new ExchangeBaseRegistry {OperationType = "SetFilesAndStatuses", IsIncoming = true};
            unit = unitOfWork.AddEntity(unit);
         }

         if (!unit.ResponseDate.HasValue)
         {
            unit.ResponseGuid = request.ServiceHeader.MessageId;
            unit.ResponseDate = DateTime.Now;
            unit.ResponseText = Serialization.Serializer(request);
         }

         unitOfWork.SaveChanges();
         WindsorHolder.Release(unitOfWork);
      }

      public GetRequestListOutMessage GetRequestList(GetRequestListInMessage request)
      {
         throw new NotImplementedException();
      }

      public GetRequestsOutMessage GetRequests(GetRequestsInMessage request)
      {
         throw new NotImplementedException();
      }
   }
}
