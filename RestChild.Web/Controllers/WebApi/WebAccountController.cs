using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Filter;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Models;
using RestChild.Security;

namespace RestChild.Web.Controllers.WebApi
{
   [Authorize]
   public class WebAccountController : BaseController
   {
      public virtual Account GetAccount(long id)
      {
         return UnitOfWork.GetById<Account>(id);
      }

      [HttpGet]
      public IEnumerable<AccountPublicDto> SearchAccount(string q)
      {
         if (!string.IsNullOrEmpty(q))
         {
            q = q.ToLower();
         }

         return UnitOfWork
            .GetSet<Account>()
            .Where(a => a.IsActive)
            .Where(a => a.Name.ToLower().Contains(q) || a.Login.ToLower().Contains(q))
            .Take(10)
            .OrderBy(a => a.Name)
            .ToList()
            .Select(a => new AccountPublicDto {FullName = $"{a.Name} ({a.Login})", Id = a.Id}).ToList();
      }

      internal Account SaveAccount(Account user)
      {
         if (user == null)
         {
            throw new ArgumentNullException();
         }

         //редактировать может только себя
         if (Security.GetCurrentAccountId() != user.Id)
         {
            throw new AccessViolationException("Нет прав для редактирования пользоватлей.");
         }

         Account userDomain;
         using (var tran = UnitOfWork.GetTransactionScope())
         {
            if (user.Id == 0)
            {
               throw new NotImplementedException();
            }
            else
            {
               userDomain = UnitOfWork.GetById<Account>(user.Id);
               if (userDomain == null)
               {
                  throw new KeyNotFoundException("Не найден пользователь");
               }

               List<string> Fields = new List<string>();

               userDomain.DateUpdate = DateTime.Now;
               userDomain.CreateUserId = Security.GetCurrentAccountId();

               //if (userDomain.Login != user.Login)
               //   Fields.Add("Имя пользователя");
               //userDomain.Login = user.Login;

               if (userDomain.Name != user.Name)
                  Fields.Add("ФИО пользователя");
               userDomain.Name = user.Name;

               if (userDomain.Position != user.Position)
                  Fields.Add("Должность");
               userDomain.Position = user.Position;

               if (userDomain.Email != user.Email)
                  Fields.Add("Электронная почта");
               userDomain.Email = user.Email;

               if (userDomain.Phone != user.Phone)
                  Fields.Add("Телефон");
               userDomain.Phone = user.Phone;
            }

            UnitOfWork.SaveChanges();

            tran.Complete();
         }

         return userDomain;
      }
   }
}
