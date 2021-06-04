using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudWaveLite.Model
{
    public class Player
    {
        private int health = 100;

        public int Health
        {
            get => health;
            set
            {
                if (value > 100)
                    value = 100;
                health = value;
            }
        }

        private int mood = 50;

        public int Mood
        {
            get => mood;
            set
            {
                if (value > 100)
                    value = 100;
                mood = value;
            }
        }

        private int money = 10000;

        public int Money
        {
            get => money;
            set
            {
                money = value;
            }
        }

        public string GetMoney() => "₽ " + money;

        private int study = 50;

        public int Study
        {
            get => study;
            set
            {
                if (value > 100)
                    value = 100;
                study = value;
            }
        }
    }
}
