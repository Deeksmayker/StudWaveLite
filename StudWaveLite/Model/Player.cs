using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudWaveLite.Model
{
    class Player
    {
        private static int hunger = 100;

        public static int Hunger
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

        private static int energy = 100;

        public static int Energy
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

        private static int mood = 50;

        public static int Mood
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

        private static int money = 1000;

        public static int Money
        {
            get => money;
            set
            {
                if (value <= 0)
                    throw new Exception("Каво ты купить пытаешься, чел");
                money = value;
            }
        }

        private static int study = 50;

        public static int Study
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
