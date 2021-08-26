using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestChild.Comon;
using RestChild.Domain;

namespace RestChild.DAL.RepositoryExtensions
{
    public static class Repository
    {
        public static DateTime AddWorkDays(this IUnitOfWork unitOfWork, DateTime date, int addDays)
        {
            if (addDays == 0)
            {
                return date;
            }

            var curDate = date.Date;
            var index = 0;
            var increment = addDays / Math.Abs(addDays);

            var daysEnd = curDate.AddDays(addDays * 3);

            var startDate = curDate > daysEnd ? daysEnd : curDate;
            var endDate = curDate < daysEnd ? daysEnd : curDate;
            var excludeDays = unitOfWork.GetSet<ExcludeDays>().Where(e => e.Date >= startDate && e.Date <= endDate)
                .ToList();

            while (index != addDays)
            {
                curDate = curDate.AddDays(increment);
                var excludeDay = excludeDays.FirstOrDefault(e => e.Date == curDate);
                if (curDate.DayOfWeek != DayOfWeek.Sunday && curDate.DayOfWeek != DayOfWeek.Saturday)
                {
                    if (excludeDay == null || !excludeDay.IsFreeDay)
                    {
                        index = index + increment;
                    }
                }
                else if (excludeDay != null && !excludeDay.IsFreeDay)
                {
                    index = index + increment;
                }

                if (daysEnd == curDate)
                {
                    daysEnd = curDate.AddDays(addDays * 3);
                    startDate = curDate > daysEnd ? daysEnd : curDate;
                    endDate = curDate < daysEnd ? daysEnd : curDate;
                    excludeDays = unitOfWork.GetSet<ExcludeDays>().Where(e => e.Date >= startDate && e.Date <= endDate)
                        .ToList();
                }
            }

            return curDate;
        }

        /// <summary>
        ///     получить ближайший рабочий день
        /// </summary>
        public static DateTime GetNextWorkDay(this IUnitOfWork unitOfWork, DateTime date)
        {
            var curDate = date.Date;
            var increment = 1;

            var daysEnd = curDate.AddDays(15);

            var startDate = curDate;
            var endDate = daysEnd;
            var excludeDays = unitOfWork.GetSet<ExcludeDays>().Where(e => e.Date >= startDate && e.Date <= endDate)
                .ToList();

            while (true)
            {
                var excludeDay = excludeDays.FirstOrDefault(e => e.Date == curDate);
                if (curDate.DayOfWeek != DayOfWeek.Sunday && curDate.DayOfWeek != DayOfWeek.Saturday)
                {
                    if (excludeDay == null || !excludeDay.IsFreeDay)
                    {
                        return curDate;
                    }
                }
                else if (excludeDay != null && !excludeDay.IsFreeDay)
                {
                    return curDate;
                }

                if (daysEnd == curDate)
                {
                    daysEnd = curDate.AddDays(15);
                    startDate = curDate > daysEnd ? daysEnd : curDate;
                    endDate = curDate < daysEnd ? daysEnd : curDate;
                    excludeDays = unitOfWork.GetSet<ExcludeDays>().Where(e => e.Date >= startDate && e.Date <= endDate)
                        .ToList();
                }

                curDate = curDate.AddDays(increment);
            }
        }

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
        ///     получить пользователя.
        /// </summary>
        public static Account GetAccountByLogin(this IQueryable<Account> query, string login)
        {
            return query.FirstOrDefault(a => a.Login == login && a.IsActive && !a.IsDeleted && a.DateDelete == null);
        }

        /// <summary>
        ///     получить пользователя.
        /// </summary>
        public static Account GetAccountByLogin(this IUnitOfWork unitOfWork, string login)
        {
            return unitOfWork.GetSet<Account>()
                .FirstOrDefault(a => a.Login == login && a.IsActive && !a.IsDeleted && a.DateDelete == null);
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
        public static List<T> GetAll<T>(this IUnitOfWork unitOfWork) where T : class, IEntityBase
        {
            return unitOfWork.GetSet<T>().ToList();
        }

        /// <summary>
        ///     получить по
        /// </summary>
        public static T GetById<T>(this IUnitOfWork unitOfWork, long? id) where T : class, IEntityBase
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
        public static async Task<T> GetByIdAsync<T>(this IUnitOfWork unitOfWork, long? id,
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
        public static long GetLastUpdateTickById<T>(this IUnitOfWork unitOfWork, long? id)
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
