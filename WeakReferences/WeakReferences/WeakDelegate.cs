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

        public WeakDelegate(Delegate del)
        {
            wRef = new WeakReference(del.Target);
            method = del.Method;
            var b = method.GetParameters().Select(parametr=>Expression.Parameter(parametr.ParameterType)).ToArray();
            var a = method.GetParameters().Select(x=> Expression.Parameter(x.ParameterType)).ToArray();

        }
        public Delegate Weak
        {
            get
            {
                return null;
            }
        }
        public void Method(Delegate del)
        {

        }

        private class CreateDelegate
        {
            private MethodInfo wMethod;
            private WeakReference wRef;

            public CreateDelegate(MethodInfo mInfo,WeakReference wRef)
            {
                this.wMethod = mInfo;
                this.wRef = wRef;
            }

            #region ExpressionTree

          //  public Delegate SetDelegate(MethodInfo mInfo, WeakReference wRef) => mInfo.ReturnType == typeof(void) ? GenerateAction() : GenerateFunc();

            public Delegate GenericAction()
            {
                //Expression.Lambda(typeof(Action<int>),)
                return null;
            }
            #endregion
        }

        public static implicit operator Delegate(WeakDelegate v)
        {
            return v.Weak;
        }
    }

    
}
