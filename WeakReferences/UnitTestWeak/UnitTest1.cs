using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeakReferences;

namespace UnitTestWeak
{
    [TestClass]
    public class UnitTest1
    {
        public void asd(int a,int b,int c)
        {

        }
        [TestMethod]
        public void TestMethod1()
        {
            Delegate ne = new WeakDelegate((Action<int, int, int>)(asd));
            WeakReference wr = new WeakReference(new Action<int, int, int>(asd));
            //ne.Method(new Action<int, int, int>(asd));
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
