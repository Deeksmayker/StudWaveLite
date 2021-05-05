using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudWaveLite.Model
{
    public class Plot
    {
        //Первый ключ - курс, во втором словаре ключ - месяц. На каждый месяц минимум одно событие.
        private static Dictionary<int, Dictionary<int, List<MonthEvent>>> plotDictionary;

        public static Dictionary<int, Dictionary<int, List<MonthEvent>>> GetPlotDictionary()
        {
            var zeroCourse = new Dictionary<int, List<MonthEvent>>();
            zeroCourse.Add(8, new List<MonthEvent>
            {
                new MonthEvent
                {
                    TextEvent = "Тут и начинается твоя история. Ты закончил школу. Не сказать что отлично, но на бюджет куда хотел - прошёл.",
                    FirstChoice = new Choice
                    {
                        ChoiceText = "Понимаю",
                        SuccesAfterChoice = "Ну вот ты приехал и заселился, пора думать",
                        CheckSucces = () => true,
                        WorldInteract = b =>
                        {
                            DateInfo.Instance.Course++;
                            DateInfo.Instance.Month++;
                        },
                        ButtonTextAfterChoice = "И правда пора"
                    },
                    IsAvailableEvent = () => true
                }
            });

            var firstCourse = new Dictionary<int, List<MonthEvent>>();
            firstCourse.Add(9, new List<MonthEvent>
            {
                new MonthEvent()
            });

            plotDictionary = new Dictionary<int, Dictionary<int, List<MonthEvent>>>();
            plotDictionary.Add(0, zeroCourse);

            return plotDictionary;
        }
    }
}
