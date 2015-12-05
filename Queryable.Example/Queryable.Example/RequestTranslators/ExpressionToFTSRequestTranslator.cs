using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Queryable.Example.RequestTranslators
{
    public sealed class ExpressionToFTSRequestTranslator : ExpressionVisitor
    {
        private readonly Stack<StringBuilder> _operationBuffers;
        private readonly List<StringBuilder> _completeOperations; 

        private OperationMode _operationMode;

        public ExpressionToFTSRequestTranslator()
        {
            _operationBuffers = new Stack<StringBuilder>();
            _completeOperations = new List<StringBuilder>();
            _operationMode = OperationMode.Default;
        }

        public IEnumerable<string> Translate(Expression exp)
        {
            Visit(exp);

            _completeOperations.AddRange(_operationBuffers.Reverse());

            return _completeOperations.Select(p => p.ToString()).Where(p => !string.IsNullOrEmpty(p));
        }

        private StringBuilder CurrentOperation
        {
            get
            {
                if (_operationBuffers.Count == 0)
                {
                    _operationBuffers.Push(new StringBuilder());
                }

                return _operationBuffers.Peek();
            }
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
            this.Visit(node.Object);
            this.CurrentOperation.Append("(");
            this.Visit(node.Arguments[0]);
            this.CurrentOperation.Append(")");
            _operationMode = oldState;

            return node;
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    this.GuardParameterPosition(node);
                    this.VisitLeftEqualBinaryNode(node);
                    this.CurrentOperation.Append("(");
                    this.VisitRightEqualBinaryNode(node);
                    this.CurrentOperation.Append(")");
                    return node;
                case ExpressionType.AndAlso:
                {
                    this.AddOperation();
                    this.Visit(node.Left);
                    this.CompleteOperation();
                    this.AddOperation();
                    this.Visit(node.Right);
                    this.CompleteOperation();
                    return node;
                }
                default:
                    throw new NotSupportedException(string.Format("Operation {0} is not supported", node.NodeType));
            }
        }

        private void AddOperation()
        {
            _operationBuffers.Push(new StringBuilder());
        }

        private void CompleteOperation()
        {
            var operation = _operationBuffers.Pop();
            _completeOperations.Add(operation);
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

        private void VisitLeftEqualBinaryNode(BinaryExpression node)
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

        private void VisitRightEqualBinaryNode(BinaryExpression node)
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
            this.CurrentOperation.Append(node.Member.Name).Append(":");

            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            switch (_operationMode)
            {
                case OperationMode.StartsWith:
                {
                    this.CurrentOperation.Append(node.Value);
                    this.CurrentOperation.Append('*');
                    break;
                }
                case OperationMode.EndsWith:
                {
                    this.CurrentOperation.Append('*');
                    this.CurrentOperation.Append(node.Value);
                    break;
                }
                case OperationMode.Contains:
                {
                    this.CurrentOperation.Append('*');
                    this.CurrentOperation.Append(node.Value);
                    this.CurrentOperation.Append('*');
                    break;
                }
                default:
                {
                    this.CurrentOperation.Append(node.Value);
                    break;
                }
            }

            return node;
        }
    }
}
