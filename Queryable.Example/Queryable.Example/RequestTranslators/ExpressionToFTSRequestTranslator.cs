using System;
using System.Linq.Expressions;
using System.ServiceModel.Channels;
using System.Text;

namespace Queryable.Example.RequestTranslators
{
    public sealed class ExpressionToFTSRequestTranslator : ExpressionVisitor
    {
        private readonly StringBuilder _resultString;
        private OperationMode _operationMode;

        public ExpressionToFTSRequestTranslator()
        {
            _resultString = new StringBuilder();
            _operationMode = OperationMode.Default;
        }

        public string Translate(Expression exp)
        {
            this.Reset();

            Visit(exp);

            return _resultString.ToString();
        }

        private void Reset()
        {
            _resultString.Clear();
            _operationMode = OperationMode.Default;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof (System.Linq.Queryable) && node.Method.Name == "Where")
            {
                var predicate = node.Arguments[1];
                this.Visit(predicate);

                return node;
            }

            if (node.Method.DeclaringType == typeof (string))
            {
                if (node.Method.Name == "StartsWith")
                {
                    return this.VisitStringInclusionMethod(node, OperationMode.StartsWith);
                }

                if (node.Method.Name == "EndsWith")
                {
                    return this.VisitStringInclusionMethod(node, OperationMode.EndsWith);
                }

                if (node.Method.Name == "Contains")
                {
                    return this.VisitStringInclusionMethod(node, OperationMode.Contains);
                }
            }

            return base.VisitMethodCall(node);
        }

        private Expression VisitStringInclusionMethod(MethodCallExpression node, OperationMode operationMode)
        {
            OperationMode oldState = _operationMode;
            _operationMode = operationMode;
            var resultNode = base.VisitMethodCall(node);
            _operationMode = oldState;

            return resultNode;
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    this.GuardParameterPosition(node);

                    this.VisitLeftBinary(node);
                    _resultString.Append("(");
                    this.VisitRightBinary(node);
                    _resultString.Append(")");
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
            _resultString.Append(node.Member.Name).Append(":");

            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            switch (_operationMode)
            {
                case OperationMode.StartsWith:
                {
                    _resultString.Append(node.Value);
                    _resultString.Append('*');
                    break;
                }
                case OperationMode.EndsWith:
                {
                    _resultString.Append('*');
                    _resultString.Append(node.Value);
                    break;
                }
                case OperationMode.Contains:
                {
                    _resultString.Append('*');
                    _resultString.Append(node.Value);
                    _resultString.Append('*');
                    break;
                }
                default:
                {
                    _resultString.Append(node.Value);
                    break;
                }
            }

            return node;
        }
    }
}
