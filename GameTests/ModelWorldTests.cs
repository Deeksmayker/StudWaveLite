using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using StudWaveLite.Model;

namespace GameTests
{
    class ModelWorldTests
    {
        [Test]
        public void SimpleWorldTests()
        {
            var world = new World();
            world.Knowledge -= 123;
            world.Parents -= 123;
            world.Sociability -= 1423;
            world.Sport--;
            Assert.AreEqual(-123, world.Knowledge);
            Assert.AreEqual(-123, world.Parents);
            Assert.AreEqual(-1423, world.Sociability);
            Assert.AreEqual(-1, world.Sport);
        }
    }
}
