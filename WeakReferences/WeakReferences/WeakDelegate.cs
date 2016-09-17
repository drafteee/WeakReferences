using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeakReferences
{
    public class WeakDelegate
    {
        private WeakReference wRef;
        private Delegate sRef;
        private static Manager mn = new Manager();

        public void DoIt()
        {
            
        }
        public WeakDelegate(Action<string> action)
        {
            if (action == null)
            {
                wRef = null;
                sRef = action;
            }
            else
            {
                wRef = new WeakReference(action);
                sRef = null;
            }
        }

        public  WeakReference WRef { get{ return wRef; }}
        public  Delegate SRef { get { return sRef; } }


    }
}
