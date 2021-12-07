using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day06.SeaCreatures
{
    public class LanternFishBirthCycles
    {
        private List<Lanternfish> _LaternFishThatAreAlive = new List<Lanternfish>();
        private int _CurrentDay = 0;
        public void InishalLaternFish(string PuzzleOneInput)
        {
            string[] fish = PuzzleOneInput.Split(',',StringSplitOptions.RemoveEmptyEntries);
            foreach (string aFish in fish)
                this._LaternFishThatAreAlive.Add(new Lanternfish(int.Parse(aFish)));
        }

        public int SimulateNumberOfDays(int DaysToSimulateUpTo)
        {
            while(this._CurrentDay < DaysToSimulateUpTo)
            {
                this._CurrentDay++;
                List<Lanternfish> newFishOnThisDay = new List<Lanternfish>();

                foreach(Lanternfish lanternfish in this._LaternFishThatAreAlive)
                {
                    Lanternfish? newLanterFish = lanternfish.IncreaseAge();
                    if(newLanterFish != null)
                        newFishOnThisDay.Add(newLanterFish);
                }

                this._LaternFishThatAreAlive.AddRange(newFishOnThisDay);

                // test datat
                //foreach(var aFish in this._LaternFishThatAreAlive)
                //{
                //    Console.Write(aFish.CurrentAge + ", ");
                //}
                //Console.WriteLine();
            }

            return this._LaternFishThatAreAlive.Count;
        }

     
    }
}
