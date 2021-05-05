using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudWaveLite.Model
{
    public class Choice
    {
        public string ChoiceText;
        public string SuccesAfterChoice;
        public string FailAfterChoice;
        public string ButtonTextAfterChoice;

        public Func<bool> CheckSucces;
        public Action<bool> WorldInteract;
    }
}
