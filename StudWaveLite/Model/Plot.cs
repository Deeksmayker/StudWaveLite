using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudWaveLite.Model
{
    public class Plot
    {
        public Plot(Player player, World world, DateInfo dateInfo)
        {
            GetPlotDictionary(player, world, dateInfo);
        }

        //Первый ключ - курс, во втором словаре ключ - месяц. На каждый месяц минимум одно событие.
        private static Dictionary<int, Dictionary<int, List<MonthEvent>>> plotDictionary;

        public void GetPlotDictionary(Player player, World world, DateInfo dateInfo)
        {
            var zeroCourse = new Dictionary<int, List<MonthEvent>>();
            zeroCourse.Add(8, new List<MonthEvent>
            {
                new MonthEvent
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
                }
            });

            var firstCourse = new Dictionary<int, List<MonthEvent>>();
            firstCourse.Add(9, new List<MonthEvent>
            {
                new MonthEvent
                {
                    TextEvent =
                        "По классике, после поступления тебя зовут на встречу со своей группой. Уже на месте ты замечаешь что люди делятся на группы. К кому же подойти?",
                    FirstChoice = new Choice
                    {
                        ChoiceText = "Прямо в центре стоят спортсмены, думаю мне туда",
                        SuccesAfterChoice =
                            "Ну ты подошел, вы разговорились и в итоге они начали тебя таскать на пробежки по утрам и тренировки на турничках. Повезло повезло....",
                        WorldInteract = b =>
                        {
                            world.Sport++;
                            world.Sociability++;
                        },
                        ButtonTextAfterChoice = "И правда повезло....."
                    },
                    SecondChoice = new Choice
                    {
                        ChoiceText = "С краю стоят какие то умники. Может туда?",
                        SuccesAfterChoice =
                            "Ну ты подошел, вы разговорились и в итоге ты стал проводить с ними достаточно много времени. Вы даже стали вместе делать контрольные. Вот это я понимаю - повезло.",
                        WorldInteract = b =>
                        {
                            world.Knowledge++;
                            world.Sociability++;
                        },
                        ButtonTextAfterChoice = "И правда повезло...."
                    },
                    ThirdChoice = new Choice
                    {
                        ChoiceText = "Никто не заинтересовал, буду отдыхать один.",
                        SuccesAfterChoice =
                            "Ну ты постоял, постоял и ушел. Потом в голове крутился только один вопрос - а шёл то зачем.",
                        WorldInteract = b => { world.Sociability--; },
                        ButtonTextAfterChoice = "Бывает..."
                    },

                    IsAvailableEvent = () => true
                }
            });

            firstCourse.Add(10, new List<MonthEvent>
            {
                new MonthEvent
                {
                    IsAvailableEvent = () => true
                }
                
            });

            plotDictionary = new Dictionary<int, Dictionary<int, List<MonthEvent>>>();
            plotDictionary.Add(0, zeroCourse);
            plotDictionary.Add(1, firstCourse);
        }

        public MonthEvent GetAvailableMonthEvent(int course, int month)
        {
            foreach (var monthEvent in plotDictionary[course][month])
            {
                if (monthEvent.IsAvailableEvent())
                {
                    return monthEvent;
                }
            }

            return null;
        }
    }
}
