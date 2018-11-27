using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NWord2Vec;

namespace ProjectTest
{
    [TestClass]
    public class ExtensionMethodsTest
    {
        [TestMethod]
        public void TestVectorPower()
        {
            var x = new float[] { 1, 2, 3 };
            var result = x.Pow(2);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(4, result[1]);
            Assert.AreEqual(9, result[2]);
        }
    }
}
