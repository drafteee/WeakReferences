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
        public string strMessage { get { return _string; } }
        private string _string;
        public int intMessage { get { return _int; } }
        private int _int;
        public void asd(int a, int b, int c)
        {
            _int = a + b + c;
        }
        public void Message(string str)
        {
            _string = str;
        }
        public void Nothing()
        {

        }
    }
}
