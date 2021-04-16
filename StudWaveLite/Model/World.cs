using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudWaveLite.Model
{
    public class World
    {
        private int sport;

        public int Sport
        {
            get => sport;
            set => sport = value >= 0 ? value : 0;
        }

        private int knowledge;

        public int Knowledge
        {
            get => knowledge;
            set => knowledge = value >= 0 ? value : 0;
        }

        private int sociability;

        public int Sociability
        {
            get => sociability;
            set => sociability = value >= 0 ? value : 0;
            
        }

        private int parents;

        public int Parents
        {
            get => parents;
            set => parents = value >= 0 ? value : 0;
        }
    }
}
