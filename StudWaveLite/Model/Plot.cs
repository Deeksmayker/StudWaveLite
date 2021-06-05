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

                    SecondChoice = new Choice
                    {
                        ChoiceText = "Понимаю",
                        SuccesAfterChoice = "Ну вот ты приехал и заселился, пора думать",
                        WorldInteract = b => { },
                        ButtonTextAfterChoice = "И правда пора"
                    },

                    ThirdChoice = new Choice
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
                        ChoiceText = "Попробовать договориться с физруком. Сказать что можешь поработать старостой за эту пятерочку",
                        SuccesAfterChoice = "Он сказал, что слышал о том что ты не последний человек среди первокурсников, и переправил на тебя обязанности старосты, так что если справишься - будет тебе эта пятерка. Ну, придется теперь следить за этими опездалами....",
                        FailAfterChoice = "Ты услышал только одно - \"Ты кто\"",
                        CheckSucces = () => world.Teachers >= 1 && world.Sociability >= 1 && world.Knowledge >= 2,
                        WorldInteract = b =>
                        {
                            if (b)
                            {
                                world.Teachers++;
                                world.Sociability++;
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

            firstCourse.Add(2, new List<MonthEvent>()
            {
                new MonthEvent()
                {
                    TextEvent = "После сессии родители позвали домой на праздник. Также в этот момент друзья зазывают на дачу, рыбачить всю неделю. Твой выбор",
                    FirstChoice = new Choice()
                    {
                        ChoiceText = "Родители это святое",
                        SuccesAfterChoice = "Родители были рады тебя видеть и ты встретился со многими родственниками.",
                        WorldInteract = b =>
                        {
                            player.Mood += 10;
                            world.Parents++;
                        },
                        ButtonTextAfterChoice = "Хорошо"
                    },

                    SecondChoice = new Choice()
                    {
                        ChoiceText = "Ну, думаю после сессии можно и с друзьями смотаться",
                        SuccesAfterChoice = "Жесткий ты конечно типарик. Но с друзьями хорошо провел время, хоть родители и не были довольны от слова совсем.",
                        WorldInteract = b =>
                        {
                            world.Parents--;
                            world.Sociability++;
                            player.Mood += 10;
                            player.Money -= 1000;
                        },
                        ButtonTextAfterChoice = "ой-ёй"
                    },

                    ThirdChoice = new Choice()
                    {
                        ChoiceText = "Я устал. Надо отдохнуть от всего",
                        SuccesAfterChoice = "Ну, зато действительно хорошо отдохнул после сессии.",
                        WorldInteract = b =>
                        {
                            world.Parents--;
                            player.Mood += 30;
                            player.Health += 20;
                        },
                        ButtonTextAfterChoice = "Да..."
                    }
                }
            });

            firstCourse.Add(3, new List<MonthEvent>()
            {
                new MonthEvent()
                {
                    TextEvent = "С началом нового семестра у вас появился новый (м)учитель математики. Кто бы мог подумать, но он зверь. И вот уже в какой раз рука бога указывает именно на тебя. Пример - сложнючий тройной интеграл. Твои действия.",
                    FirstChoice = new Choice()
                    {
                        ChoiceText = "Чего думать, щас покажу",
                        SuccesAfterChoice = "В этот раз у примера шансы против тебя были, и нешуточные, но ты его сломил. До основания. Даже новый учитель тебя зауважал после такого предстваления.",
                        FailAfterChoice = "Хотелось бы сказать что шансы были, но это уже лукавство. Что то он тебе пытался объяснить, но ты едва ли что то понял.",
                        CheckSucces = () => world.Knowledge >= 5,
                        WorldInteract = b =>
                        {
                            if (b)
                            {
                                player.Mood += 20;
                                player.Study += 10;
                                world.Teachers++;
                            }

                            else
                            {
                                player.Mood -= 20;
                                player.Study -= 10;
                            }
                        },

                        ButtonTextAfterChoice = "Бывает"
                    },

                    SecondChoice = new Choice()
                    {
                        ChoiceText = "Попробовать договориться. Сказать что за тройку готов на что угодно...",
                        SuccesAfterChoice = "Удивительно, но он оказался снисходителен. Только теперь ты каждую неделю моешь его машину, но это уже совсем другая история.",
                        FailAfterChoice = "Бывают в жизни огорчения. Вот и в твоей случились.",
                        CheckSucces = () => world.Sociability >= 2 && random.Next(100) >= 50,
                        WorldInteract = b =>
                        {
                            if (b)
                            {
                                player.Mood -= 10;
                                player.Study -= 5;
                            }

                            else
                            {
                                player.Mood -= 20;
                                player.Study -= 20;
                            }
                        },

                        ButtonTextAfterChoice = "Бывает..."
                    },

                    ThirdChoice = new Choice()
                    {
                        ChoiceText = "Сказать, что ты спортсмен и выступаешь за честь университета.",
                        SuccesAfterChoice = "Ну, так как это правда, и тебя покрывает сам вуз, тебе ставят троебан и вы расходитесь на этой ноте.",
                        FailAfterChoice = "И кому ты рассказываешь, мальчик. Было бы это хотя бы правдой.......",
                        CheckSucces = () => world.Sport >= 5,
                        WorldInteract = b =>
                        {
                            if (b)
                            {
                                player.Mood += 10;
                                player.Study += 5;
                            }

                            else
                            {
                                player.Mood -= 20;
                                player.Study -= 20;
                            }
                        },

                        ButtonTextAfterChoice = "Понимаю"
                    }
                }
            });

            firstCourse.Add(4, new List<MonthEvent>()
            {
                new MonthEvent()
                {
                    TextEvent = "КТО БЫ МОГ ПОДУМАТЬ, но новый учитель физкультуры тоже оказался не так прост (выглядит вообще люто), поэтому вы бежите эти 5км на время. А время на нормальную оценку - совсем не детское. Что же делать.",
                    FirstChoice = new Choice()
                    {
                        ChoiceText = "Как что - бежать.",
                        SuccesAfterChoice = "Пробежал. Даже без особых проблем, удивительно.",
                        FailAfterChoice = "Ну ты бежал. Первые 2км. Дальше шёл. Не понятно на что расчитывал, конечно.",
                        CheckSucces = () => world.Sport >= 50 && player.Health >= 50,
                        WorldInteract = b =>
                        {
                            if (b)
                            {
                                player.Mood += 10;
                                player.Study += 10;
                            }

                            else
                            {
                                player.Mood -= 10;
                                player.Health -= 10;
                                player.Study -= 10;
                            }
                        },

                        ButtonTextAfterChoice = "Понимаю"
                    },

                    SecondChoice = new Choice()
                    {
                        ChoiceText = "Классический ход. Попробовать договориться.",
                        FailAfterChoice = "Для начала стоило посмотреть на него. Такие не договариваются. Никогда.",
                        CheckSucces = () => false,
                        WorldInteract = b =>
                        {
                            player.Mood -= 20;
                            player.Study -= 10;
                            world.Teachers--;
                        },

                        ButtonTextAfterChoice = "ой-ёй"
                    },

                    ThirdChoice = new Choice()
                    {
                        ChoiceText = "Сказать что плоховато тебе, так что такую историю ты точно не осилишь",
                        SuccesAfterChoice = "Ну, это правда, так что он позволил тебе не бежать этот прикол",
                        FailAfterChoice = "По тебе же видно, что всё не так плохо. Так кого ты обмануть пытаешься.",
                        CheckSucces = () => player.Health <= 30,
                        WorldInteract = b =>
                        {
                            if (b)
                            {
                                player.Mood += 10;
                            }

                            else
                            {
                                player.Mood -= 20;
                                player.Study -= 10;
                                world.Teachers--;
                            }
                        },

                        ButtonTextAfterChoice = "Понимаю"
                    }
                }
            });

            firstCourse.Add(5, new List<MonthEvent>()
            {
                new MonthEvent()
                {
                    TextEvent = "Никто не ожидал + никто не гадал. Но это не важно, ведь вуз решил устроить жесткую подготовку к параду, где каждый ученик должен показать чему он научился за первый курс.",
                    FirstChoice = new Choice()
                    {
                        ChoiceText = "Отправиться помогать составлять план парада.",
                        SuccesAfterChoice = "Ты действительно хорошо помог своими знаниями, поэтому за тебя и замолвили словечко перед деканом. Также ты там многому научился.",
                        FailAfterChoice = "Ну сходил. Ну посмотрел. Ничем не помог, поэтому неясно чего ждать от деканата по отношению к тебе.",
                        CheckSucces = () => world.Knowledge >= 7,
                        WorldInteract = b =>
                        {
                            if (b)
                            {
                                player.Mood += 100;
                                player.Study += 100;
                                world.Teachers += 10;
                            }

                            else
                            {
                                player.Mood -= 30;
                                player.Study -= 30;
                                world.Teachers -= 2;
                            }
                        },
                        ButtonTextAfterChoice = "Понимаю"
                    },

                    SecondChoice = new Choice()
                    {
                        ChoiceText = "Пойти на соревнования перед самим парадом. Для защиты чести университета, как они говорят.",
                        SuccesAfterChoice = "Не просто сходил, еще и всех победил. Тобой были очень довольны. Особенно физрук, который по итогу и замолвил за тебя словечко перед деканатом.",
                        FailAfterChoice = "На что надеялся - не ясно.",
                        CheckSucces = () => world.Sport >= 7,
                        WorldInteract = b =>
                        {
                            if (b)
                            {
                                player.Mood += 100;
                                player.Study += 100;
                                world.Teachers += 10;
                            }

                            else
                            {
                                player.Mood -= 30;
                                player.Study -= 30;
                                world.Teachers -= 2;
                            }
                        },

                        ButtonTextAfterChoice = "Понимаю"
                    },

                    ThirdChoice = new Choice()
                    {
                        ChoiceText = "Пойти обкашлять организационные вопросы",
                        SuccesAfterChoice = "Ты сильно помог с организацией мероприятия и за тебя замолвили словечко перед деканатом",
                        FailAfterChoice = "Не то чтобы ты сильно помог. Если не скзаать совсем не помог.",
                        CheckSucces = () => world.Sociability >= 3 && world.Teachers >= 3,
                        WorldInteract = b =>
                        {
                            if (b)
                            {
                                player.Mood += 100;
                                player.Study += 100;
                                world.Teachers += 10;
                            }

                            else
                            {
                                player.Mood -= 30;
                                player.Study -= 30;
                                world.Teachers -= 2;
                            }
                        },

                        ButtonTextAfterChoice = "Понимаю"
                    }
                }
            });

            firstCourse.Add(6, new List<MonthEvent>()
            {
                new MonthEvent()
                {
                    TextEvent = "Последняя сессия первого курса. Пора.",
                    FirstChoice = new Choice()
                    {
                        ChoiceText = "Решаю сам.",
                        SuccesAfterChoice = "В этот раз пошло как по маслу. Даже удивительно",
                        FailAfterChoice = "Шансы были.",
                        CheckSucces = () => world.Knowledge >= 6,
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
                        ChoiceText = "Делать нечего - списываю",
                        SuccesAfterChoice = "Опасно. Но получилось. В этот раз.",
                        FailAfterChoice = "Не в этот раз.",
                        CheckSucces = () => random.Next(100) >= 70 && player.Mood >= 50,
                        WorldInteract = b =>
                        {
                            if (b)
                            {
                                player.Mood += 20;
                                player.Study += 20;
                            }

                            else
                            {
                                player.Mood -= 40;
                                player.Study -= 40;
                                world.Teachers--;
                            }
                        },

                        ButtonTextAfterChoice = "И правда"
                    },

                    ThirdChoice = new Choice()
                    {
                        ChoiceText = "Проскочу на спорте",
                        SuccesAfterChoice = "Ну, спортсмен он и в африке N",
                        FailAfterChoice = "Куда проскочить собрался. Давай до завтра",
                        CheckSucces = () => world.Sport >= 7 && world.Teachers >= 3,
                        WorldInteract = b =>
                        {
                            if (b)
                            {
                                player.Mood += 10;
                                player.Study += 5;
                            }

                            else
                            {
                                player.Mood -= 50;
                                player.Study -= 50;
                                world.Teachers--;
                            }
                        },
                        ButtonTextAfterChoice = "Понимаю"
                    }
                }
            });

            firstCourse.Add(7, new List<MonthEvent>()
            {
                new MonthEvent()
                {
                    TextEvent = "Ну, на этом и заканчивается твоя жизнь как первокурсника. Дальше всё будет плавнее, так что помощь тебе больше не понадобится. Спасбо за игру.",
                    FirstChoice = new Choice()
                    {
                        ChoiceText = "Давай до завтра",
                    },

                    SecondChoice = new Choice()
                    {
                        ChoiceText = "Давай до завтра",
                    },

                    ThirdChoice = new Choice()
                    {
                        ChoiceText = "Давай до завтра",
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
