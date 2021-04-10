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
            StudWaveLite.Model.Player.Hunger = 101;
            Assert.AreEqual(100, StudWaveLite.Model.Player.Hunger);

            StudWaveLite.Model.Player.Energy += 101;
            Assert.AreEqual(100, StudWaveLite.Model.Player.Energy);

            StudWaveLite.Model.Player.Mood = 10000000;
            Assert.AreEqual(100, StudWaveLite.Model.Player.Mood);

            StudWaveLite.Model.Player.Study = 999999999;
            Assert.AreEqual(100, StudWaveLite.Model.Player.Study);
        }
    }
}