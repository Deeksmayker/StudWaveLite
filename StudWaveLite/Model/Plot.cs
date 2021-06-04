using System;
using System.Collections.Generic;

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
            var random = new Random();

            var zeroCourse = new Dictionary<int, List<MonthEvent>>();
            zeroCourse.Add(8, new List<MonthEvent>
            {
                new MonthEvent
                {
                    TextEvent =
                        "Тут и начинается твоя история. Ты закончил школу. Не сказать что отлично, но на бюджет куда хотел - прошёл, даже место в общаге дали! Ты попрощался с семьёй, со всеми друзьями и отправляешься в путь. Основная задача - выжить и адаптироваться в тяжелом мире первокурсника. Удачи.",
                    FirstChoice = new Choice
                    {
                        ChoiceText = "Понимаю",
                        SuccesAfterChoice = "Ну вот ты приехал и заселился, пора думать",
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
                        "Классическая ситуация. Сразу после поступления тебя зовут на встречу со своей группой. Уже на месте ты замечаешь что люди делятся на группы. К кому же подойти?",
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
                        ChoiceText = "С краю стоят простые парни. Может туда?",
                        SuccesAfterChoice =
                            "Парни оказались совсем не простые. Все поголовно - математики в третьем поколении. Вы разговорились и они даже начали помогать тебе по учёбе. Вот это я понимаю - повезло.",
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
                    TextEvent = "Ничего не предвещало беды. Ты как обычно пришел на пару математики, но в этот раз рука бога указала на тебя. Задание вроде не особо сложное, твои действия",
                    FirstChoice = new Choice
                    {
                        ChoiceText = "Играем по честному. Попробовать ответить самому.",
                        SuccesAfterChoice = "Красава. Порешал этот пример без шансов.",
                        FailAfterChoice = "Бывают в жизни огорчения. Но преподаватель тебе объяснил пример, надеемся что в голове что то осталось",
                        CheckSucces = () => world.Knowledge >= 1,
                        ButtonTextAfterChoice = "Го некст",
                        WorldInteract = b =>
                        {
                            if (b)
                            {
                                world.Teachers++;
                                player.Study += 10;
                                player.Mood += 5;
                            }

                            else
                            {
                                world.Knowledge++;
                                player.Study -= 5;
                            }
                        }
                    },
                    SecondChoice = new Choice()
                    {
                        ChoiceText = "Попробовать отшутиться, авось прокатит",
                        SuccesAfterChoice = "В этот раз повезло. Учитель сказал что на первый раз прощает.", 
                        FailAfterChoice = "Кринжанул по полной. На тебя странно косились до конца пары.",
                        CheckSucces = () => player.Mood >= 50 && world.Sociability >= 1,
                        ButtonTextAfterChoice = "Понимаю",
                        WorldInteract = b =>
                        {
                            if (!b)
                            {
                                world.Sociability--;
                                player.Study -= 10;
                                player.Mood -= 10;
                            }
                        }
                    },
                    ThirdChoice = new Choice
                    {
                        ChoiceText = "Признаться что ничего не знаешь, но сказать что подготовишься к следующему занятию",
                        SuccesAfterChoice = "Учитель оценил твою честность и дал задание на следующую пару",
                        FailAfterChoice = "Учитель сказал, что не знать решения на такую простую задачу - позор, и поставил тебе 0 баллов",
                        CheckSucces = () => player.Mood <= 50 && player.Health <= 70,
                        ButtonTextAfterChoice = "Понимаю",
                        WorldInteract = b =>
                        {
                            if (b)
                            {
                                world.Teachers++;
                                player.Mood -= 5;
                            }

                            else
                            {
                                world.Teachers--;
                                player.Mood -= 10;
                                player.Study -= 10;
                            }
                        }
                    },

                    IsAvailableEvent = () => true
                },
            });

            firstCourse.Add(11, new List<MonthEvent>()
            {
                new MonthEvent()
                {
                    TextEvent = "Сидишь ты на физре, никого не трогаешь, а физрук как выдаст - \"Кто подтянется 20 раз - зачет за семестр вне очереди.\" Тут даже ты призадумался.",
                    FirstChoice = new Choice()
                    {
                        ChoiceText = "А чобы нет, щас покажу",
                        SuccesAfterChoice = "Сделал эти 20 без проблем(почти). Пятак в кармане.",
                        FailAfterChoice = "Мдааа... Начал хорошо - закончил за упокой, буквально. Повредил руку, теперь и баллы нормальные по физре не набрать. Бывает...",
                        CheckSucces = () => player.Health >= 50 && world.Sport >= 2,
                        WorldInteract = b =>
                        {
                            if (b)
                            {
                                player.Mood += 20;
                                player.Study += 10;
                            }
                            else
                            {
                                player.Health -= 20;
                                player.Study -= 5;
                                player.Mood -= 20;
                            }
                        },
                        ButtonTextAfterChoice = "Понимаю"
                    },

                    SecondChoice = new Choice()
                    {
                        ChoiceText = "Попробовать договориться с физруком. Сказать что ты может и не лучший спортсмен, но учишься хорошо, и поработать старостой за эту пятерочку",
                        SuccesAfterChoice = "Он сказал, что слышал о том что ты не последний человек среди первокурсников, и переправил на тебя обязанности старосты, так что если справишься - будет тебе эта пятерка. Ну, придется теперь следить за этими опездалами....",
                        FailAfterChoice = "Ты услышал только одно - \"Ты кто\"",
                        CheckSucces = () => world.Teachers >= 1 && world.Sociability >= 1 && world.Knowledge >= 2,
                        WorldInteract = b =>
                        {
                            if (b)
                            {
                                world.Teachers++;
                                world.Sociability++;
                                player.Mood -= 10;
                                player.Study += 10;
                            }

                            else
                            {
                                player.Mood -= 10;
                            }
                        },
                        ButtonTextAfterChoice = "Понимаю"
                    },

                    ThirdChoice = new Choice()
                    {
                        ChoiceText = "Думаю посижу......",
                        SuccesAfterChoice = "Посидел, бывает",
                        ButtonTextAfterChoice = "И правда бывает..."
                    }
                }
            });

            firstCourse.Add(12, new List<MonthEvent>()
            {
                new MonthEvent()
                {
                    TextEvent = "Вот и новый год не за горами, так что нужно определиться как будешь его праздновать.",
                    FirstChoice = new Choice()
                    {
                        ChoiceText = "Одногрупники как раз собираются снимать котедж, залечу с ними.",
                        SuccesAfterChoice = "Вы хорошо провели новогоднюю ночь, даже ужасных проишествий не было (за исключением той истории с Ваньком и качелей, но не будем о грустном).",
                        WorldInteract = b =>
                        {
                            player.Money -= 2000;
                            player.Mood += 20;
                            player.Health -= 5;
                            world.Sociability++;
                        },
                        ButtonTextAfterChoice = "Неплохо"
                    },

                    SecondChoice = new Choice
                    {
                        ChoiceText = "Думаю пора и к родителям смотаться.",
                        SuccesAfterChoice = "Новый год в кругу семьи. Классика.",
                        WorldInteract = (b) =>
                        {
                            player.Mood += 10;
                            world.Parents++;
                        },
                        ButtonTextAfterChoice = "И правда классика"
                    },

                    ThirdChoice = new Choice
                    {
                        ChoiceText = "Какой новый год, сессия через две недели.",
                        SuccesAfterChoice = "Ну ты сидел и училсяучилсяучилсяучился. молодец, чего сказать....",
                        WorldInteract = b =>
                        {
                            player.Mood -= 20;
                            world.Sociability--;
                        },
                        ButtonTextAfterChoice = "Бывает"
                    }
                }
            });

            firstCourse.Add(1, new List<MonthEvent>()
            {
                new MonthEvent()
                {
                    TextEvent = "Настало время. Зимняя сессия. Как будешь сдавать",
                    FirstChoice = new Choice()
                    {
                        ChoiceText = "Сам, конечно же. Не зря же я поступал.",
                        SuccesAfterChoice = "Не без запинок, но сессия завершена успешна. Хорошая работа.",
                        FailAfterChoice = "Шансы были. Что то сдал, что то завалил. Придется пересдавать.",
                        CheckSucces = () => world.Knowledge >= 3,
                        WorldInteract = b =>
                        {
                            if (b)
                            {
                                player.Mood += 30;
                                player.Study += 30;
                                world.Teachers++;
                                world.Parents++;
                            }

                            else
                            {
                                player.Mood -= 30;
                                player.Study -= 30;
                            }
                        },
                        ButtonTextAfterChoice = "Понимаю"
                    },

                    SecondChoice = new Choice()
                    {
                        ChoiceText = "Ну чаго остается, только попробовать списать",
                        SuccesAfterChoice = "Опасная тематика, но получилось",
                        FailAfterChoice = "Что то в жизни получается, а что то нет. Ауф",
                        CheckSucces = () => random.Next(100) >= 50 && player.Mood >= 50,
                        WorldInteract = b =>
                        {
                            if (b)
                            {
                                player.Mood += 20;
                                player.Study += 20;
                            }

                            else
                            {
                                player.Mood -= 30;
                                player.Study -= 30;
                                world.Teachers--;
                            }
                        },

                        ButtonTextAfterChoice = "И правда"
                    },

                    ThirdChoice = new Choice()
                    {
                        ChoiceText = "Ну, наныть - тоже вариант",
                        SuccesAfterChoice = "Учителя тебя пожалели и поставили этот троебан",
                        FailAfterChoice = "Не убедил. Давай до завтра на пересдаче",
                        CheckSucces = () => world.Teachers >= 1 && random.Next(100) >= 30 || player.Health <= 30,
                        WorldInteract = b =>
                        {
                            if (b)
                            {
                                player.Mood += 10;
                                player.Study += 5;
                            }

                            else
                            {
                                player.Mood -= 30;
                                player.Study -= 30;
                                world.Teachers--;
                            }
                        },
                        ButtonTextAfterChoice = "Понимаю"
                    }
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
