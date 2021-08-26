using RestChild.Admin.Web.Models;
using RestChild.Admin.Web.Properties;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Domain;
using RestChild.Extensions.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RestChild.Admin.Web.Controllers.WebApi
{
   public class SecurityJournalApiController : BaseController
   {
      private DateTime logLimit = DateTime.Now.AddDays(-RestChild.Security.SecuritySettings.TimeLogStorage);

      internal CommonPagedList<AccountHistoryLogin> GetJournalEntrance(SecurityJournalEntranceFilterModel filter)
      {
         var pageSize = Settings.Default.TablePageSize;
         var pageNumber = filter != null ? filter.PageNumber : 1;
         var startRecord = (pageNumber - 1) * pageSize;

         var query = UnitOfWork.GetSet<AccountHistoryLogin>().Where(ss => ss.DateEnter >= logLimit).AsQueryable();

         if (filter != null)
         {
            if(filter.DateFrom.HasValue)
            {
               query = query.Where(ss => ss.DateEnter >= filter.DateFrom.Value);
            }
            if(filter.DateTo.HasValue)
            {
               var _some = new DateTime(filter.DateTo.Value.Year, filter.DateTo.Value.Month, filter.DateTo.Value.Day).AddDays(1);
               query = query.Where(ss => ss.DateEnter < _some);
            }
            if(filter.LoginStatus > 0)
            {
               if(filter.LoginStatus == 1)
               {
                  query = query.Where(ss => ss.IsAuthorized);
               }
               else if(filter.LoginStatus == 2)
               {
                  query = query.Where(ss => !ss.IsAuthorized);
               }
            }
         }

         var totalCount = query.Count();
         var entity =
            query.OrderByDescending(t => t.DateEnter)
               .Skip(startRecord)
               .Take(pageSize)
               .ToList();

         return new CommonPagedList<AccountHistoryLogin>(entity, pageNumber, pageSize, totalCount);
      }

      internal CommonPagedList<AccountHistoryLogin> GetJournalActiveSessions(SecurityJournalActiveSessionsFilterModel filter)
      {
         var pageSize = Settings.Default.TablePageSize;
         var pageNumber = filter != null ? filter.PageNumber : 1;
         var startRecord = (pageNumber - 1) * pageSize;

         var query = UnitOfWork.GetSet<AccountHistoryLogin>().Where(ss => ss.DateExit == null && ss.IsAuthorized && !ss.StopSession && ss.DateEnter >= logLimit).AsQueryable();

         if (filter != null)
         {
            if(!string.IsNullOrWhiteSpace(filter.UserInfo))
            {
               query = query.Where(ss => ss.SessionUid.ToLower().Contains(filter.UserInfo.ToLower()) || ss.Login.ToLower().Contains(filter.UserInfo.ToLower()));
            }
         }

         var totalCount = query.Count();
         var entity =
            query.OrderByDescending(t => t.DateLastActivity)
               .Skip(startRecord)
               .Take(pageSize)
               .ToList();

         return new CommonPagedList<AccountHistoryLogin>(entity, pageNumber, pageSize, totalCount);
      }

      internal CommonPagedList<AccountHistoryLogin> GetJournalStoppedSessions(SecurityJournalActiveSessionsFilterModel filter)
      {
         var pageSize = Settings.Default.TablePageSize;
         var pageNumber = filter != null ? filter.PageNumber : 1;
         var startRecord = (pageNumber - 1) * pageSize;

         var query = UnitOfWork.GetSet<AccountHistoryLogin>().Where(ss => (ss.DateExit != null || ss.StopSession) && ss.IsAuthorized && ss.DateEnter >= logLimit).AsQueryable();

         if (filter != null)
         {
            if (!string.IsNullOrWhiteSpace(filter.UserInfo))
            {
               query = query.Where(ss => ss.SessionUid.ToLower().Contains(filter.UserInfo.ToLower()) || ss.Login.ToLower().Contains(filter.UserInfo.ToLower()));
            }
            if (filter.DateFrom.HasValue)
            {
               query = query.Where(ss => ss.DateEnter >= filter.DateFrom.Value);
            }
            if (filter.DateTo.HasValue)
            {
               var _some = new DateTime(filter.DateTo.Value.Year, filter.DateTo.Value.Month, filter.DateTo.Value.Day).AddDays(1);
               query = query.Where(ss => ss.DateEnter < _some);
            }
         }

         var totalCount = query.Count();
         var entity =
            query.OrderByDescending(t => t.DateLastActivity)
               .Skip(startRecord)
               .Take(pageSize)
               .ToList();

         return new CommonPagedList<AccountHistoryLogin>(entity, pageNumber, pageSize, totalCount);
      }

      [HttpGet]
      public IHttpActionResult SpotSession(string SessionUid)
      {
         if (!Security.HasRight(AccessRightEnum.Security.StopSessions))
         {
            throw new OperationCanceledException();
         }

         RestChild.Security.UserWatcher.UserSessionStop(UnitOfWork, SessionUid);
         return Ok();
      }

      internal CommonPagedList<SecurityJournal> GetJournalSecurityEvents(SecurityJournalDefaultFilterModel filter)
      {
         var pageSize = Settings.Default.TablePageSize;
         var pageNumber = filter != null ? filter.PageNumber : 1;
         var startRecord = (pageNumber - 1) * pageSize;

         var query = UnitOfWork.GetSet<SecurityJournal>().Where(ss => ss.DateEvent >= logLimit && (ss.SecurityJournalTypeId == (long)SecurityJournalEventType.SecurityIntsenders || ss.SecurityJournalTypeId == (long)SecurityJournalEventType.UserDataChange)).AsQueryable();

         if(filter != null)
         {
            if(filter.DateFrom.HasValue)
            {
               query = query.Where(a => a.DateEvent >= filter.DateFrom.Value);
            }
            if(filter.DateTo.HasValue)
            {
               var _some = new DateTime(filter.DateTo.Value.Year, filter.DateTo.Value.Month, filter.DateTo.Value.Day).AddDays(1);
               query = query.Where(ss => ss.DateEvent < _some);
            }
         }

         var totalCount = query.Count();
         var entity = query.OrderByDescending(t => t.DateEvent)
               .Skip(startRecord)
               .Take(pageSize)
               .ToList();

         return new CommonPagedList<SecurityJournal>(entity, pageNumber, pageSize, totalCount);
      }

      internal CommonPagedList<SecurityJournal> GetJournalOutSystemIteractionEvents(SecurityJournalDefaultFilterModel filter)
      {
         var pageSize = Settings.Default.TablePageSize;
         var pageNumber = filter != null ? filter.PageNumber : 1;
         var startRecord = (pageNumber - 1) * pageSize;

         var query = UnitOfWork.GetSet<SecurityJournal>().Where(ss => ss.DateEvent >= logLimit && (ss.SecurityJournalTypeId == (long)SecurityJournalEventType.OutSystemsInteractions)).AsQueryable();

         if (filter != null)
         {
            if (filter.DateFrom.HasValue)
            {
               query = query.Where(a => a.DateEvent >= filter.DateFrom.Value);
            }
            if (filter.DateTo.HasValue)
            {
               var _some = new DateTime(filter.DateTo.Value.Year, filter.DateTo.Value.Month, filter.DateTo.Value.Day).AddDays(1);
               query = query.Where(ss => ss.DateEvent < _some);
            }
         }

         var totalCount = query.Count();
         var entity = query.OrderByDescending(t => t.DateEvent)
               .Skip(startRecord)
               .Take(pageSize)
               .ToList();

         return new CommonPagedList<SecurityJournal>(entity, pageNumber, pageSize, totalCount);
      }

      internal CommonPagedList<SecurityJournal> GetRightsAndRoles(SecurityJournalDefaultFilterModel filter)
      {
         var pageSize = Settings.Default.TablePageSize;
         var pageNumber = filter != null ? filter.PageNumber : 1;
         var startRecord = (pageNumber - 1) * pageSize;

         var query = UnitOfWork.GetSet<SecurityJournal>().Where(ss => ss.DateEvent >= logLimit && ss.SecurityJournalTypeId == (long)SecurityJournalEventType.RightsAndRoles).AsQueryable();

         if (filter != null)
         {
            if (filter.DateFrom.HasValue)
            {
               query = query.Where(a => a.DateEvent >= filter.DateFrom.Value);
            }
            if (filter.DateTo.HasValue)
            {
               var _some = new DateTime(filter.DateTo.Value.Year, filter.DateTo.Value.Month, filter.DateTo.Value.Day).AddDays(1);
               query = query.Where(ss => ss.DateEvent < _some);
            }
         }

         var totalCount = query.Count();
         var entity = query.OrderByDescending(t => t.DateEvent)
               .Skip(startRecord)
               .Take(pageSize)
               .ToList();

         return new CommonPagedList<SecurityJournal>(entity, pageNumber, pageSize, totalCount);
      }

      internal CommonPagedList<SecurityJournal> GetJournalProcesses(SecurityJournalDefaultFilterModel filter)
      {
         var pageSize = Settings.Default.TablePageSize;
         var pageNumber = filter != null ? filter.PageNumber : 1;
         var startRecord = (pageNumber - 1) * pageSize;

         var query = UnitOfWork.GetSet<SecurityJournal>().Where(ss => ss.DateEvent >= logLimit && ss.SecurityJournalTypeId == (long)SecurityJournalEventType.Processes).AsQueryable();

         if (filter != null)
         {
            if (filter.DateFrom.HasValue)
            {
               query = query.Where(a => a.DateEvent >= filter.DateFrom.Value);
            }
            if (filter.DateTo.HasValue)
            {
               var _some = new DateTime(filter.DateTo.Value.Year, filter.DateTo.Value.Month, filter.DateTo.Value.Day).AddDays(1);
               query = query.Where(ss => ss.DateEvent < _some);
            }
         }

         var totalCount = query.Count();
         var entity = query.OrderByDescending(t => t.DateEvent)
               .Skip(startRecord)
               .Take(pageSize)
               .ToList();

         return new CommonPagedList<SecurityJournal>(entity, pageNumber, pageSize, totalCount);
      }
   }
}
