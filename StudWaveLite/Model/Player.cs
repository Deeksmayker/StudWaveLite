using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudWaveLite.Model
{
    class Player
    {
        private int hunger = 100;

        public int Hunger
        { 
            get => hunger;
            set
            {
                if (value <= 0)
                    throw new Exception("Ты умираешь, чел");
                if (value > 100)
                    value = 100;
                hunger = value;
            }
        }

        private int energy = 100;

        public int Energy
        {
            get => energy;
            set
            {
                if (value <= 0)
                    throw new Exception("Ты отрубился, чел");
                if (value > 100)
                    value = 100;
                energy = value;
            }
        }

        private int mood = 50;

        public int Mood
        {
            get => mood;
            set
            {
                if (value <= 0)
                    throw new Exception("Попей колесики, чел");
                if (value > 100)
                    value = 100;
                mood = value;
            }
        }

        private int money = 1000;

        public int Money
        {
            get => money;
            set
            {
                if (value <= 0)
                    throw new Exception("Каво ты купить пытаешься, чел");
                money = value;
            }
        }

        private int study = 50;

        public int Study
        {
            get => study;
            set
            {
                if (value <= 0)
                    throw new Exception("Числанули тебя. В армейку пора, чел");
                if (value > 100)
                    value = 100;
                study = value;
            }
        }
    }
}
