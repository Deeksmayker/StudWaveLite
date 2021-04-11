using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudWaveLite.Model
{
    public class World
    {
        private int sportStat;

        public int SportStat
        {
            get => sportStat;
            set
            {
                if (value - sportStat > 1)
                    throw new Exception("Выбор за спорт насчитался больше одного");
                if (value < 0)
                    value = 0;
                sportStat = value;
            }
        }
    }
}
