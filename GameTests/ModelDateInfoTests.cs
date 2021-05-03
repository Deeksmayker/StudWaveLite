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
            var date = new DateInfo();
            date.Month = 13;
            date.Course++;

            Assert.AreEqual("Январь", ((DateInfo.Months)date.Month).ToString());
            Assert.AreEqual(1, date.Month);
            Assert.AreEqual(2021, date.Year);
            Assert.AreEqual(2, date.Course);
        }
    }
}
