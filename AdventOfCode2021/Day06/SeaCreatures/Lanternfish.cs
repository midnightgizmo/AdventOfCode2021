using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day06.SeaCreatures
{
    public class Lanternfish
    {

        public Lanternfish(int currentAge)
        {
            this.CurrentAge = currentAge;
        }
        //public bool IsFirstCycleOfLive { get; set; }
        public int CurrentAge { get; set; }

        public Lanternfish? IncreaseAge()
        {
            // ages go backwords, so decrease the age by 1
            --CurrentAge;

            // if the lanternfish is less than zero (should be -1)
            // we have to reset its age and create a new LanternFish (its given birth to a new fish)
            if (CurrentAge < 0)
            {
                CurrentAge = 6;
                // new laternfish have an age of 7 and all cycles after will have an age of 6
                return new Lanternfish(8);
            }
            else
                return null;
        }


    }
}
