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
        #region EnumAndProperties

        public enum Months
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

        private int month = 8;

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

                if (value == 9)
                    Course++;

                month = value;
            }
        }

        private int year = 2020;

        public int Year
        {
            get => year;
        }

        private int course = 0;

        public int Course
        {
            get => course;
            set => course = value;
        }

        #endregion

        public string GetDateAndCourse()
        {
            return String.Format("{0} {1} год {2} курс", (Months) month, year, course);
        }
    }
}
