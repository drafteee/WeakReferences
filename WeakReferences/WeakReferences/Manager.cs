using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WeakReferences
{
    public class Manager
    {
        public void PrintMessage(object o, string str)
        {
            Console.Write(str);
        }

        //public delegate void PrintMessage(string str);
        public event Action<object, string> events;

        /*private void Check(Manager mn, Delegate s)
        {
            //var b = ((WeakDelegate)this.Target).Weak;
           // ((WeakDelegate)s).Weak;
            //var a = (s.Weak.Target);
          //  if (a != null)
         //   {
             //   var c = (WeakDelegate)s.Weak.Target;
            //    c.PrintMessage(this, "fs");
       //     }
            else
            {
                Manager c = mn as Manager;

                c.events -= this.PrintMessage;
            }
        }*/
        public void OnEvent(string str)
        {
            var reff = new WeakReference((Action<object, string>)PrintMessage);
            var a = ((Delegate)reff.Target).Method;

            var w = ((MethodInfo)a).ReturnType;
            var q = events.GetInvocationList();
            if (events != null)
            {
              //  foreach (var s in q)
               // {
                   // Check(this, s);
                //}
                events(this, str);
            }
        }
    }
}
