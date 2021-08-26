using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestChild.Comon;

namespace RestChild.Mobile.DAL.RepositoryExtensions
{
    public static class Repository
    {
        /// <summary>
        ///     Применить сортировку по умолчанию
        /// </summary>
        public static IOrderedQueryable<T> ApplyOrder<T>(this IQueryable<T> query) where T : class, IEntityBase
        {
            return query.OrderByDescending(sr => sr.Id);
        }

        /// <summary>
        ///     Применить сортировку по умолчанию
        /// </summary>
        public static IOrderedQueryable<T> ApplyOrder<T>(this IOrderedQueryable<T> query) where T : class, IEntityBase
        {
            return query.ThenByDescending(sr => sr.Id);
        }

        /// <summary>
        ///     получить по
        /// </summary>
        /// <param name="query"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T GetById<T>(this IQueryable<T> query, long id) where T : class, IEntityBase
        {
            return query.FirstOrDefault(a => a.Id == id);
        }

        /// <summary>
        ///     получить все
        /// </summary>
        public static List<T> GetAll<T>(this IUnitOfWorkMobile unitOfWork) where T : class, IEntityBase
        {
            return unitOfWork.GetSet<T>().ToList();
        }

        /// <summary>
        ///     получить по
        /// </summary>
        public static T GetById<T>(this IUnitOfWorkMobile unitOfWork, long? id) where T : class, IEntityBase
        {
            if (!id.HasValue)
            {
                return null;
            }

            return unitOfWork.GetSet<T>().FirstOrDefault(a => a.Id == id);
        }

        /// <summary>
        ///     получить по ИД
        /// </summary>
        /// <returns></returns>
        public static async Task<T> GetByIdAsync<T>(this IQueryable<T> query, long id,
            CancellationToken cancellationToken) where T : class, IEntityBase
        {
            return await query.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        /// <summary>
        ///     получить по ID
        /// </summary>
        public static async Task<T> GetByIdAsync<T>(this IUnitOfWorkMobile unitOfWork, long? id,
            CancellationToken cancellationToken) where T : class, IEntityBase
        {
            if (!id.HasValue)
            {
                return null;
            }

            return await unitOfWork.GetSet<T>().FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        /// <summary>
        ///     получить по
        /// </summary>
        public static long GetLastUpdateTickById<T>(this IUnitOfWorkMobile unitOfWork, long? id)
            where T : class, ILastUpdateTick
        {
            if (!id.HasValue)
            {
                return 0;
            }

            return unitOfWork.GetSet<T>().Where(a => a.Id == id).Select(e => e.LastUpdateTick).FirstOrDefault();
        }


        public static IEnumerable<T> GetPage<T>(this IQueryable<T> query, PagerState pageData)
            where T : class, IEntityBase
        {
            IQueryable<T> ordered = query.ApplyOrder();
            if (!pageData.IsEmpty)
            {
                pageData.SetTotalCount(ordered.Count());
                ordered = ordered.Skip(pageData.SkipNumber).Take(pageData.PerPage);
            }

            return ordered.ToList();
        }
    }
}
