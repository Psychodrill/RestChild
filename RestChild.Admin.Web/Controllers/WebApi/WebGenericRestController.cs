using System;
using System.Net;
using System.Web.Http;
using RestChild.Comon;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;

namespace RestChild.Admin.Web.Controllers.WebApi
{
   /// <summary>
   ///     Универсальный WEBApi контроллер, реализующий CRUD
   /// </summary>
   /// <typeparam name="T"></typeparam>
   public abstract class WebGenericRestController<T> : BaseController where T : class, IEntityBase
   {
      public virtual T Put(long id, [FromBody] T entity)
      {
         if (entity == null)
         {
            throw new ArgumentNullException("entity");
         }

         if (!ModelState.IsValid)
         {
            throw new ArgumentException("entity");
         }

         entity.Id = id;
         entity = UnitOfWork.Update(entity);
         return entity;
      }

      public virtual T Post([FromBody] T entity)
      {
         if (entity == null)
         {
            throw new ArgumentNullException("entity");
         }

         if (!ModelState.IsValid)
         {
            throw new ArgumentException("entity");
         }

         entity = UnitOfWork.AddEntity(entity);
         UnitOfWork.SaveChanges();
         return entity;
      }

      public virtual T Get(long id)
      {
         var entity = UnitOfWork.GetSet<T>().GetById(id);
         if (entity == null)
         {
            throw new HttpResponseException(HttpStatusCode.NotFound);
         }

         return entity;
      }

      internal virtual T GetOrDefault(long id)
      {
         return UnitOfWork.GetSet<T>().GetById(id);
      }
   }
}
