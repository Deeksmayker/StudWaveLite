using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                player.Study += 10;
                player.Health -= 10;
                player.Mood -= 20;
            }

            else if (world.IsEventStage)
            {
                currentEvent.FirstChoice.WorldInteract(currentEvent.FirstChoice.CheckSucces());
                dateInfo.Month++;
                player.Money += 2000;
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
                player.Study -= 10;
                player.Health += 10;
                player.Mood -= 10;
            }

            else if (world.IsEventStage)
            {
                currentEvent.SecondChoice.WorldInteract(currentEvent.SecondChoice.CheckSucces());
                dateInfo.Month++;
                player.Money += 2000;
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
                player.Mood += 20;
            }

            else if (world.IsEventStage)
            {
                currentEvent.ThirdChoice.WorldInteract(currentEvent.ThirdChoice.CheckSucces());
                dateInfo.Month++;
                player.Money += 2000;
            }
        }
    }
}
