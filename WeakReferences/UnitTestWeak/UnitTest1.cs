using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeakReferences;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace UnitTestWeak
{
    [TestClass]
    public class UnitTest1
    {
        private event Action<int, int, int> actl;
        private dynamic Update(dynamic _event)
        {
            var listEvent  = _event.GetInvocationList();
            foreach(var item in listEvent)
            {
               if(!item.Target.Constants[0].IsAlive)
                    _event -= item;
                
            }
            return _event;
        }
        [TestMethod]
        public void ClearWeakRef()
        {
            Manager testManager = new Manager();
            WeakDelegate wDel = new WeakDelegate((Action<int, int, int>)(testManager.asd));
            testManager = null;
            GC.Collect();
            Assert.IsFalse(wDel.IsAlive);
        }

        private event Action _eventAct;
        [TestMethod]
        public void TestDefaultWeak()
        {
            Manager testManager = new Manager();
            WeakDelegate wDel = new WeakDelegate((Action)(testManager.Nothing));
            _eventAct += (Action)wDel;
            _eventAct.Invoke();
            Assert.IsTrue(wDel.IsAlive);
            testManager = null;
            GC.Collect();
            Assert.IsFalse(wDel.IsAlive);
        }

        private event Action<string> @event;
        [TestMethod]
        public void TestString()
        {
            Manager testManager = new Manager();
            WeakDelegate wDel = new WeakDelegate((Action<string>)(testManager.Message));
            @event += (Action<string>)wDel;
            @event.Invoke("asd");
            Assert.AreEqual(testManager.strMessage, "asd");
        }

        private event Action<int, int, int> @ev;
        [TestMethod]
        public void TestThreeInt()
        {
            Manager testManager = new Manager();
            WeakDelegate wDel = new WeakDelegate((Action<int,int,int>)(testManager.asd));
            @ev += (Action<int,int,int>)wDel;
            @ev.Invoke(2,4,6);
            Assert.AreEqual(testManager.intMessage, 12);
        }
    }
}
