using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudWaveLite.Model
{
    public static class GameDateTime
    {
        private static int month = 9;

        public static int Month
        {
            get => month;
            set
            {
                if (value > 12)
                {
                    value -= 12;
                    year++;
                }

                month = value;
            }
        }

        private static int year = 2021;

        public static int Year
        {
            get => year;
            set => throw new Exception("Зачем меняешь год");
        }

        private static int hour;

        public static int Hour
        {
            get => hour;
            set
            {
                if (value >= 24)
                    value -= 24;
                hour = value;
            }
        }

        private static int minute;

        public static int Minute
        {
            get => minute;
            set
            {
                if (value >= 60)
                {
                    value -= 60;
                    hour++;
                }

                minute = value;
            }
        }
    }
}
