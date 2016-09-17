using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeakReferences
{
    public class Manager
    {
        public void PrintMessage(string str)
        {
            Console.Write(str);
        }
        
        //public delegate void PrintMessage(string str);
        public event Action<string> events;

        public void OnEvent(string str)
        {
            events(str);
        }
    }
}
