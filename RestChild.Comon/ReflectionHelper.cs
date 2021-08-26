using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace RestChild.Comon
{
    public static class ReflectionHelper
    {
        private static readonly Action<Exception> PreserveInternalException;

        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification =
            "Ok here")]
        static ReflectionHelper()
        {
            var preserveStackTrace = typeof(Exception).GetMethod("InternalPreserveStackTrace",
                BindingFlags.Instance | BindingFlags.NonPublic);
            PreserveInternalException =
                (Action<Exception>) Delegate.CreateDelegate(typeof(Action<Exception>), preserveStackTrace);
        }

        /// <summary>
        ///     Проверка, может ли тип быть  null
        /// </summary>
        /// <param name="type">Тип, который проверяем</param>
        /// <returns>Результат проверки</returns>
        public static bool IsNullable(Type type)
        {
            return !type.IsValueType || Nullable.GetUnderlyingType(type) != null;
        }

        public static void PreserveStackTrace(Exception e)
        {
            PreserveInternalException(e);
        }

        /// <summary>
        ///     Получает информацию о члене класса по выражению
        /// </summary>
        /// <param name="func">
        ///     The func.
        /// </param>
        /// <typeparam name="TObject">
        ///     Тип объекта
        /// </typeparam>
        /// <returns>
        ///     информация о члене класса
        /// </returns>
        public static MemberInfo GetMemberInfo<TObject>(Expression<Func<TObject, object>> func)
        {
            var obj = default(TObject);
            return GetMemberInfo(obj, func);
        }

        /// <summary>
        ///     Получает информацию о члене класса по выражению
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="func">
        ///     The func.
        /// </param>
        /// <typeparam name="TObject">
        ///     Тип объекта
        /// </typeparam>
        /// <typeparam name="TProperty">
        ///     Тип свойства/поля и т.п.
        /// </typeparam>
        /// <returns>
        ///     информация о члене класса
        /// </returns>
        public static MemberInfo GetMemberInfo<TObject, TProperty>(this TObject target,
            Expression<Func<TObject, TProperty>> func)
        {
            var unary = func.Body as UnaryExpression;
            var res = unary != null ? unary.Operand : func.Body;

            var me = res as MemberExpression;
            if (me != null)
            {
                return me.Member;
            }

            var mc = res as MethodCallExpression;
            if (mc != null)
            {
                return mc.Method;
            }

            throw new ArgumentException(
                "Невалидное выражение. Поддерживается только ображение к свойству и вызов метода.", "func");
        }

        /// <summary>
        ///     Получает имя члена класса по выражению
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="func">
        ///     The func.
        /// </param>
        /// <typeparam name="TObject">
        ///     Тип объекта
        /// </typeparam>
        /// <typeparam name="TProperty">
        ///     Тип свойства/поля и т.п.
        /// </typeparam>
        /// <returns>
        ///     Имя члена класса
        /// </returns>
        public static string GetMemberName<TObject, TProperty>(this TObject target,
            Expression<Func<TObject, TProperty>> func)
        {
            return GetMemberInfo(target, func).Name;
        }
    }
}
