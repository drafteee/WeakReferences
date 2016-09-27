using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WeakReferences
{
    public class WeakDelegate
    {
        WeakReference wRef;
        MethodInfo method;
        CreateDelegate _del;
        public WeakDelegate(Delegate del)
        {
            wRef = new WeakReference(del.Target);
            method = del.Method;
            _del = new CreateDelegate();
        }
        public Delegate Weak{ get{ return _del.SetDelegate(method,wRef);}}
        public bool IsAlive { get { return wRef.IsAlive; } }

        private class CreateDelegate
        {
            private MethodInfo wMethod;
            private WeakReference wRef;
            #region ExpressionTree
            public Delegate SetDelegate(MethodInfo mInfo, WeakReference wRef)
            {
                this.wMethod = mInfo;
                this.wRef = wRef;
                return GenericAction();
            }
            private ParameterExpression[] GetParams() => wMethod.GetParameters().
                    Select(param => Expression.Parameter(param.ParameterType)).ToArray();
            private Type GetActionType()
            {
                var args = new List<Type>(wMethod.GetParameters()
                .Select(param => param.ParameterType));
                args.Add(wMethod.ReturnType);

                return Expression.GetDelegateType(args.ToArray());
            }
            private Delegate GenericAction()
            {
                var _params = GetParams();
                return Expression.Lambda(GetActionType(), BlockAction(_params), _params).Compile();
            }
            private Expression[] CallAction(ParameterExpression[] _params) => 
                    new Expression[] { CallDelegate(_params) };
            private MemberExpression GetTarget()=> Expression.Property(Expression.Convert(
                    Expression.Constant(wRef), typeof(WeakReference)), "Target");
            private MethodCallExpression CallDelegate(ParameterExpression[] _params) => Expression.Call(
                    instance: Expression.Convert(GetTarget(), wMethod.DeclaringType),
                    method: wMethod,
                    arguments: _params);
            private ConditionalExpression BlockAction(ParameterExpression[] _params)=>Expression.IfThen(
                    Expression.IsTrue(GetIsAlive()), Expression.Block(GetVariablesList(_params), CallAction(_params)));
            private MemberExpression GetIsAlive() => Expression.Property(Expression.Convert(
                    Expression.Constant(wRef),typeof(WeakReference)), "IsAlive");
            private List<ParameterExpression> GetVariablesList(ParameterExpression[] argumentsType)=>
                    new List<ParameterExpression>(argumentsType.Select(argument => 
                                                                        Expression.Variable(argument.Type)));
            #endregion
        }

        public static implicit operator Delegate(WeakDelegate v)
        {
            return v.Weak;
        }
    }
}
