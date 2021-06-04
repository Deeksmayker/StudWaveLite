using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StudWaveLite.Model
{
    public class World
    {
        //Игровые стадии в таком порядке: Еда на месяц => Активность в свободное время => Событие => После события => по кругу

        public bool IsFoodStage;
        public bool IsFreeActivityStage;
        public bool IsEventStage = true;
        public bool IsAfterEventStage;

        private int sport;

        public int Sport
        {
            get => sport;
            set => sport = value;
        }

        private int knowledge;

        public int Knowledge
        {
            get => knowledge;
            set => knowledge = value;
        }

        private int sociability;

        public int Sociability
        {
            get => sociability;
            set => sociability = value;

        }

        private int parents;

        public int Parents
        {
            get => parents;
            set => parents = value >= 0 ? value : 0;
        }

        private int teachers;

        public int Teachers
        {
            get;
            set;
        }
    }
}
