using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Models;

namespace Tests
{
    [TestClass]
    public class AlienTest
    {
        [TestMethod]
        public void AlienLifePointReachZero()
        {
            Alien alien = new Alien(50,0,0,0,"","");
            alien.AlienDestroy();
            Assert.AreEqual("",alien.Model);
        }

    }
}