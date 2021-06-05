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
            var player = new Player();
            player.Health = 75;
            player.Mood = 65;
            player.Study = 55;
            player.Money = 1000;
            Assert.AreEqual(75, player.Health);
            Assert.AreEqual(65, player.Mood);
            Assert.AreEqual(55, player.Study);
            Assert.AreEqual("₽ 1000", player.GetMoney());
        }
    }
}