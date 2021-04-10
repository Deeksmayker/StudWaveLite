using System;
using NUnit.Framework;
using StudWaveLite;
using StudWaveLite.Model;

namespace GameTests
{
    public class ModelPlayer
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SimplePlayerPropertysTest()
        {
            var player = new StudWaveLite.Model.Player(101, 234, 2354, 100000, 135235);
            Assert.AreEqual(100, player.Hunger);
            Assert.AreEqual(100, player.Energy);
            Assert.AreEqual(100, player.Mood);
            Assert.AreEqual(100, player.Study);
        }
    }
}