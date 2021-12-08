using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07.Crabs
{
    public class DistanceCounter
    {

        private Hashtable distanceTracker = new Hashtable();
        private int _CurrentCrabHorizontalDistance = 0;

        public DistanceCounter(int InishalCrabsHorizontalDistance)
        {
            this.InishalCrabHorizontalDistance = InishalCrabsHorizontalDistance;
        }

        public int InishalCrabHorizontalDistance { get; private set; }
        public int TotalFuelUsed { get; set; }
        public void AddCrab(int crabsHorizontalDistance)
        {
            int DistanceBetweenCrabs = GetDistanceBetweenTwoNumbers(this.InishalCrabHorizontalDistance, crabsHorizontalDistance);

            if(distanceTracker.ContainsKey(DistanceBetweenCrabs))
                distanceTracker[DistanceBetweenCrabs] = (int)distanceTracker[DistanceBetweenCrabs] + 1;
            else
                distanceTracker.Add(DistanceBetweenCrabs, 1);
        }

        public int CaculateTotalFuelUsed()
        {
            int totalFuel = 0;
            foreach(DictionaryEntry fuel in distanceTracker)
            {
                totalFuel += (int)fuel.Value * (int)fuel.Key;
            }

            return totalFuel;
        }

        public int CaculateTotalFuelUsed_PartTwo()
        {
            if(this.InishalCrabHorizontalDistance == 5)
            {
                int i = 0;
            }
            int totalFuel = 0;
            foreach (DictionaryEntry fuel in distanceTracker)
            {
                int Fuel = ((int)fuel.Key * ((int)fuel.Key + 1)) / 2;
                totalFuel += (int)fuel.Value * Fuel;
            }

            return totalFuel;
        }

        private int GetDistanceBetweenTwoNumbers(int num1, int num2)
        {
            if (num1 > num2)
                return num1 - num2;
            else
                return num2 - num1;
        }
    }
}
