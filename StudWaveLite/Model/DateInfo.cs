using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StudWaveLite.Model
{
    public class DateInfo
    {
        private enum Months
        {
            Январь = 1,
            Февраль = 2,
            Март = 3, 
            Апрель = 4,
            Май = 5,
            Июнь = 6, 
            Июль = 7,
            Август = 8,
            Сентябрь = 9,
            Октябрь = 10,
            Ноябрь = 11,
            Декабрь = 12
        }

        public DateInfo(int month, int hour, int minute)
        {
            var a = (Months) 1;
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
