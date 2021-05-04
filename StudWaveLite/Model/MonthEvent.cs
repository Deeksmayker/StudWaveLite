using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudWaveLite.Model
{
    public class MonthEvent
    {
        public string TextEvent;
        public Choice FirstChoice;
        public Choice SecondChoice;
        public Choice ThirdChoice;

        public Func<bool> IsAvailableEvent;
    }
}
