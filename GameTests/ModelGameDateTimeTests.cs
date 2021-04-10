using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using StudWaveLite.Model;

namespace GameTests
{
    class ModelGameDateTimeTests
    {
        [Test]
        public void SimpleDatePropertysTests()
        {
            StudWaveLite.Model.GameDateTime.Month = 13;
            Assert.AreEqual(1, StudWaveLite.Model.GameDateTime.Month);
            StudWaveLite.Model.GameDateTime.Hour = 24;
            Assert.AreEqual(0, StudWaveLite.Model.GameDateTime.Hour);
            StudWaveLite.Model.GameDateTime.Minute = 60;
            Assert.AreEqual(0, StudWaveLite.Model.GameDateTime.Minute);
        }
    }
}
