using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Models;

namespace Tests
{
    [TestClass]
    public class VesselTest
    {
        [TestMethod]
        public void VesselLifePointsNormal()
        {
            Vessel vessel = new Vessel(3, 3, 0, 0, "", "");
            Assert.AreEqual(3, vessel.LifePoints);
        }

        [TestMethod]
        public void VesselLifePointsMax()
        {
            Vessel vessel = new Vessel(4, 3, 0, 0, "", "");
            Assert.AreEqual(3, vessel.LifePoints);
        }

        [TestMethod]
        public void VesselLifePointsMin()
        {
            Vessel vessel = new Vessel(-1, 3, 0, 0, "", "");
            Assert.AreEqual(0, vessel.LifePoints);
        }
    }
}