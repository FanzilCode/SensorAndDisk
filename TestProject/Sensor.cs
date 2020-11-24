using System;
using System.Collections.Generic;

namespace TestProject
{
    class Sensor
    {
        public int Response { get; set; } = 50;
        public bool IsRotate { get; set; }
        public bool ClockWise { get; set; }

        public void DetermineIsDirectionOfRotation(List<bool> query)
        {
            int[] countsOfGroups = new int[4];
            bool[] Groups = new bool[4];
            IsRotate = true;
            for(int i = 0; i<4; i++)
            {
                if (query.Count > 0)
                {
                    Groups[i] = query[0];
                    countsOfGroups[i] = CountOfGroups(ref query);
                }
                else
                {
                    countsOfGroups[i] = 0;
                    IsRotate = false;
                }
            }
            Console.WriteLine(); 
            if (IsRotate)
            {
                bool firstGroup = countsOfGroups[0] > countsOfGroups[2];
                bool secondGroup = countsOfGroups[1] > countsOfGroups[3];
                
                ClockWise = !(firstGroup && Groups[0] && secondGroup);
            }
        }
        private int CountOfGroups(ref List<bool> query)
        {
            bool first = query[0];
            int count = 0;
            while (query[0] == first)
            {
                query.RemoveAt(0);
                count++;
                if (query.Count == 0) break;
            }
            return count;
        }
    }
}
