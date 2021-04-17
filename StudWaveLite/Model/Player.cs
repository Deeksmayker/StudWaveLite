using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudWaveLite.Model
{
    public class Player
    {
        public Player(int health, int hunger, int energy, int mood, int money, int study)
        {
            Health = health;
            Hunger = hunger;
            Energy = energy;
            Mood = mood;
            Money = money;
            Study = study;
        }

        private int health;

        public int Health
        {
            get => health;
            set
            {
                if (value <= 0)
                    throw new Exception("Ты умер, чел");
                if (value > 100)
                    value = 100;
                health = value;
            }
        }

        private int hunger;

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

        private int energy;

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

        private int mood;

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

        private int money;

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

        private int study;

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
