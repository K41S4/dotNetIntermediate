using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    public class ExpressionConverter
    {
        private readonly IncDecExpressionVisitor incDecExpressionVisitor = new IncDecExpressionVisitor();

        private readonly ReplaceParametersExpressionVisitor parametersReplaceExpressionVisitor = new ReplaceParametersExpressionVisitor();

        public Expression Convert(Expression expr, Dictionary<string, object> paramsToReplace)
        {
            var inDecResult = incDecExpressionVisitor.VisitAndConvert(expr, "");
            return parametersReplaceExpressionVisitor.Convert(inDecResult as LambdaExpression, paramsToReplace);
        }
    }
}