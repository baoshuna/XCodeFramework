using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace XCodeFramework.Core.Extensions
{
    public static class PredicateExpressionExtensions
    {
        public static Expression<Func<T, bool>> True<T>()
        {
            return it => true;
        }

        public static Expression<Func<T, bool>> False<T>()
        {
            return it => false;
        }

        public static Expression<T> Compose<T>(this Expression<T> left, Expression<T> right, Func<Expression, Expression, Expression> merge)
        {
            ReadOnlyCollection<ParameterExpression> leftParameters = left.Parameters;
            ReadOnlyCollection<ParameterExpression> rightParameters = right.Parameters;

            Dictionary<ParameterExpression, ParameterExpression> rightParameterMap = leftParameters
                .Select((leftParameter, index) => new
                {
                    leftParameter = leftParameter,
                    rightParameter = rightParameters[index]
                })
                .ToDictionary(it => it.rightParameter, it => it.leftParameter);

            Expression leftBody = left.Body;
            Expression rightBody = ParameterRebinderExpressionVisitor.ReplaceParameters(rightParameterMap, right.Body);

            return Expression.Lambda<T>(merge(leftBody, rightBody), leftParameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            return left.Compose(right, Expression.And);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            return left.Compose(right, Expression.Or);
        }
    }

    public class ParameterRebinderExpressionVisitor : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> _parameterMap;

        public ParameterRebinderExpressionVisitor(Dictionary<ParameterExpression, ParameterExpression> parameterMap)
        {
            _parameterMap = parameterMap ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> parameterMap, Expression expression)
        {
            return new ParameterRebinderExpressionVisitor(parameterMap).Visit(expression);
        }

        protected override Expression VisitParameter(ParameterExpression parameter)
        {
            ParameterExpression parameterReplacement;
            if (_parameterMap.TryGetValue(parameter, out parameterReplacement))
            {
                parameter = parameterReplacement;
            }

            return base.VisitParameter(parameter);
        }
    }
}