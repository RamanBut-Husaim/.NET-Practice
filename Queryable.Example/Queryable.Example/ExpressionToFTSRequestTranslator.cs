using System;
using System.Linq.Expressions;
using System.Text;

namespace Queryable.Example
{
    public class ExpressionToFTSRequestTranslator : ExpressionVisitor
    {
        private StringBuilder resultString;

        public string Translate(Expression exp)
        {
            resultString = new StringBuilder();
            Visit(exp);

            return resultString.ToString();
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof (System.Linq.Queryable)
                && node.Method.Name == "Where")
            {
                var predicate = node.Arguments[1];
                Visit(predicate);

                return node;
            }
            return base.VisitMethodCall(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    this.GuardParameterPosition(node);

                    this.VisitLeftBinary(node);
                    resultString.Append("(");
                    this.VisitRightBinary(node);
                    resultString.Append(")");
                    break;

                default:
                    throw new NotSupportedException(string.Format("Operation {0} is not supported", node.NodeType));
            }
            ;

            return node;
        }

        private void GuardParameterPosition(BinaryExpression node)
        {
            if (node.Left.NodeType == ExpressionType.MemberAccess && node.Right.NodeType == ExpressionType.Constant)
            {
                return;
            }

            if (node.Left.NodeType == ExpressionType.Constant && node.Right.NodeType == ExpressionType.MemberAccess)
            {
                return;
            }

            throw new NotSupportedException(
                "Available combinations for parameters: 1) left - member; right - constant 2) left - constant; right - member");
        }

        private void VisitLeftBinary(BinaryExpression node)
        {
            if (node.Left.NodeType == ExpressionType.MemberAccess)
            {
                this.Visit(node.Left);
            }
            else
            {
                this.Visit(node.Right);
            }
        }

        private void VisitRightBinary(BinaryExpression node)
        {
            if (node.Right.NodeType == ExpressionType.Constant)
            {
                this.Visit(node.Right);
            }
            else
            {
                this.Visit(node.Left);
            }
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            resultString.Append(node.Member.Name).Append(":");

            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            resultString.Append(node.Value);

            return node;
        }
    }
}
