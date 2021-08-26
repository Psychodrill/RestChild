using System.Linq.Expressions;

namespace RestChild.Comon
{
    public class NullToDefaultVisitor : ExpressionVisitor
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        /// <param name="defaultValue">ВЫражение</param>
        public NullToDefaultVisitor(Expression defaultValue)
        {
            DefaultValue = defaultValue;
        }

        /// <summary>
        ///     По умолчанию
        /// </summary>
        public Expression DefaultValue { get; set; }

        /// <summary>
        ///     Просмотреть
        /// </summary>
        /// <param name="node">Что смотрим</param>
        /// <returns>Выражение</returns>
        public override Expression Visit(Expression node)
        {
            return base.Visit(node);
        }

        /// <summary>
        ///     Просмотреть узел выражения
        /// </summary>
        /// <param name="node">Что смотрим</param>
        /// <returns>Выражение</returns>
        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression != null)
            {
                var expr = Visit(node.Expression);
                Expression test = Expression.Equal(expr, Expression.Default(expr.Type));
                return Expression.Condition(test, Expression.Default(node.Type), node);
            }

            return node;
        }
    }
}
