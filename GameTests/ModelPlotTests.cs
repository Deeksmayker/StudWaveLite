using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using StudWaveLite.Model;

namespace GameTests
{
    class ModelPlotTests
    {
        [Test]
        public void SimplePlotTest()
        {
            var plot = new Plot(new Player(), new World(), new DateInfo());
            plot.GetAvailableMonthEvent(0, 8).Should().BeEquivalentTo(new MonthEvent
            {
                TextEvent =
                    "Тут и начинается твоя история. Ты закончил школу. Не сказать что отлично, но на бюджет куда хотел - прошёл, даже место в общаге дали! Ты попрощался с семьёй, со всеми друзьями и отправляешься в путь.",
                FirstChoice = new Choice
                {
                    ChoiceText = "Понимаю",
                    SuccesAfterChoice = "Ну вот ты приехал и заселился, пора думать",
                    CheckSucces = () => true,
                    WorldInteract = b => { },
                    ButtonTextAfterChoice = "И правда пора"
                },
                IsAvailableEvent = () => true
            });
        }
    }
}
