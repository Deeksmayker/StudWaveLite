using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StudWaveLite.Model
{
    public class GameDateTime
    {
        public GameDateTime(int month, int hour, int minute)
        {
            Month = month;
            Hour = hour;
            Minute = minute;
        }

        private int month;

        public int Month
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

        private int year = 2021;

        public int Year
        {
            get => year;
            set => throw new Exception("Зачем меняешь год");
        }

        private int hour;

        public int Hour
        {
            get => hour;
            set
            {
                if (value >= 24)
                    value -= 24;
                hour = value;
            }
        }

        private int minute;

        public int Minute
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
