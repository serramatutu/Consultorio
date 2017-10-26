using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsultorioAPI.Controllers;

namespace Teste
{
    [TestClass]
    public class Teste
    {
        [TestMethod]
        public void Trolei()
        {
            var controller = new TroleiController();

            Assert.AreEqual((controller.Get() as List<string>).Count, );
        }
    }
}
