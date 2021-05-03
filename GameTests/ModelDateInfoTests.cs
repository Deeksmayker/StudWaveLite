using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using StudWaveLite.Model;

namespace GameTests
{
    class ModelDateInfoTests
    {
        [Test]
        public void SimpleDatePropertysTests()
        {
            var date = new DateInfo(13, 24, 61);
            Assert.AreEqual(1, date.Month);
            Assert.AreEqual(1, date.Hour);
            Assert.AreEqual(1, date.Minute);
        }
    }
}
