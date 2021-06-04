using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudWaveLite.Model
{
    public class Game
    {
        public void FirstButtonWorldInteract(Player player, World world, DateInfo dateInfo, MonthEvent currentEvent)
        {
            if (world.IsFoodStage)
            {
                player.Money -= 5000;
                player.Health += 10;
                player.Mood -= 5;
            }

            else if (world.IsFreeActivityStage)
            {
                world.Knowledge++;
                player.Study += 10;
                player.Health -= 10;
                player.Mood -= 20;
            }

            else if (world.IsEventStage)
            {
                currentEvent.FirstChoice.WorldInteract(currentEvent.FirstChoice.CheckSucces());
                dateInfo.Month++;
                player.Money += 5000 + world.Parents * 1000;
            }
        }

        public void SecondButtonWorldInteract(Player player, World world, DateInfo dateInfo, MonthEvent currentEvent)
        {
            if (world.IsFoodStage)
            {
                player.Money -= 8000;
                player.Health += 5;
                player.Mood += 10;
            }

            else if (world.IsFreeActivityStage)
            {
                world.Sport++;
                player.Study -= 10;
                player.Health += 10;
                player.Mood -= 10;
            }

            else if (world.IsEventStage)
            {
                currentEvent.SecondChoice.WorldInteract(currentEvent.SecondChoice.CheckSucces());
                dateInfo.Month++;
                player.Money += 5000 + world.Parents * 1000;
            }
        }

        public void ThirdButtonWorldInteract(Player player, World world, DateInfo dateInfo, MonthEvent currentEvent)
        {
            if (world.IsFoodStage)
            {
                player.Money -= 2000;
                player.Health -= 20;
                player.Mood -= 20;
            }

            else if (world.IsFreeActivityStage)
            {
                player.Study -= 20;
                player.Health -= 10;
                player.Mood += 10;
            }

            else if (world.IsEventStage)
            {
                currentEvent.ThirdChoice.WorldInteract(currentEvent.ThirdChoice.CheckSucces());
                dateInfo.Month++;
                player.Money += 5000 + world.Parents * 1000;
            }
        }

        public Tuple<bool, string, string> CheckGameOver(Player player)
        {
            if (player.Health <= 0)
                return Tuple.Create(true,
                    "Умер. Как это возможно - спрашививать нужно у тебя.",
                    "ой-ёй.");
            

            if (player.Mood <= 0)
                return Tuple.Create(true,
                    "Раньше надо было колёсики пить. В таком состоянии только дед инсайд посты строчить. Давай до завтра.",
                    "Бывает");
            

            if (player.Study <= 0)
                return Tuple.Create(true,
                    "Говорят в армейке хоть кормят бесплатно.",
                    "Повезло, получается.....");
            

            if (player.Money <= 0)
                return Tuple.Create(true,
                    "Не рассчитал бюджет на месяц, набрал микро-займов и вот тебя уже везут в багажнике в неизвестном направлении. Не думал что в наше время такое бывает.",
                    "Вот и я не думал.....");

            return Tuple.Create(false, "null", "null");
        }
    }
}
