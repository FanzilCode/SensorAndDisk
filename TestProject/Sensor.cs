using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    class Sensor
    {
        static double x0 = 359, x1 = 300, x2 = 239, x3 = 120;

        public int Response { get; set; }
        public bool IsBlack { get; set; }
        public Sensor()
        {
            Response = 50;
        }
        public Sensor(int response)
        {
            Response = response;
        }

        public void Check(Disk disk)
        {
            if ((disk.x_current <= x0 && disk.x_current >= x1) || (disk.x_current <= x2 && disk.x_current >= x3) || (disk.x_current == 0))
            {
                IsBlack = true;
            }
            else IsBlack = false;
        }
        public string Command()
        {
            if (IsBlack) return "Черный";
            return "Белый";
        }
    }
}
