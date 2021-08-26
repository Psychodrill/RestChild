using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace RestChild.Comon.Extensions
{
    /// <summary>
    ///     расширения для Linq to Entities
    /// </summary>
    public static class DbSetExtensions
    {
        /// <summary>
        ///     проверка на NULL
        /// </summary>
        private static T NotNull<T>(T value, string parameterName) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        /// <summary>
        ///     добавить если запись не существует
        /// </summary>
        public static T AddIfNotExists<T>(this DbSet<T> set, T entity, Expression<Func<T, bool>> predicate = null)
            where T : class, new()
        {
            NotNull<IDbSet<T>>(set, nameof(set));
            NotNull(predicate, nameof(predicate));
            NotNull(entity, nameof(entity));

            var exists = predicate != null ? set.Any(predicate) : set.Any();
            return !exists ? set.Add(entity) : null;
        }
    }
}
