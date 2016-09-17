using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WeakReferences
{
    class Program
    {
        public static void PrintMessage(string str)
        {
            Console.Write(str);
        }
        static Manager mn = new Manager();
        static void Main(string[] args)
        {
            WeakDelegate wd = new WeakDelegate(PrintMessage);
            //mn.events += (Action<string>)wd.WRef.Target;
          //  mn.events += Program.PrintMessage;
            
            //wd.DoIt();
            List<Action<string>> l = new List<Action<string>>();
            l.Add((Action<string>)wd.WRef.Target);

            mn.events+=l[0];
            mn.OnEvent("safs");

            wd = null;
            mn.OnEvent("safs");

            Console.ReadKey();
        }
    }
}
