using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WeakReferences
{
    class Program
    {
      
        private MemberExpression GetTarget()
        {
            return null;
        }
        public void PrintMessage(object sender,string str)
        {
            var b = this;
            if (b != null)
            {
                Console.Write(str);
            }
        }
        static Manager mn = new Manager();
        static void Main(string[] args)
        {
            Program pr = new Program();
            
           // WeakDelegate wd = new WeakDelegate(pr.PrintMessage,mn);
           
           // wd.ServiceReference = mn;
            //mn.events += (Action<string>)wd.WRef.Target;
            //  mn.events += Program.PrintMessage;
           // if (wd.Weak.IsAlive) { }
            //wd.DoIt();
            var wRef = new WeakReference((Action<object,string>)mn.PrintMessage);
           
            mn.events += (Action<object,string>)mn.PrintMessage;
            mn.OnEvent("safs");

            GC.Collect();
            GC.WaitForPendingFinalizers();
         //   Expression.IfThenElse((new Expression).)
            Expression.Property(Expression.Convert(Expression.Constant(wRef.Target), typeof(WeakReference)), "IsAlive");

         //   wd = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            
            mn.OnEvent("safs");
            pr = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();


            mn.OnEvent("sfda");
            Console.ReadKey();
        }
    }
}