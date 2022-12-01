using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    public class IncDecExpressionVisitor : ExpressionVisitor
    {
        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Add when NodePassesCondition(node):
                    return Expression.Increment(node.Left);
                case ExpressionType.Subtract when NodePassesCondition(node):
                    return Expression.Decrement(node.Left);
                default:
                    return base.VisitBinary(node);
            }
        }

        private bool NodePassesCondition(BinaryExpression node)
        {
            return node.Left.NodeType == ExpressionType.Parameter &&
                   (node.Right as ConstantExpression)?.Value as int? == 1;
        }
    }
}
