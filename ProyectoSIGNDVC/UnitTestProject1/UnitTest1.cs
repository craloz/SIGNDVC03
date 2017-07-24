using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ProyectoSIGNDVC.Models.Direccion.GetAllEstadoDireccion();
            Assert.Equals(1,2);
        }
    }
}
