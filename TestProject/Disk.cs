using System;

namespace TestProject
{
    class Disk
    {
        static double x0 = 360, x1 = 300, x2 = 240, x3 = 120;

        public double rotation { get; set; } = 1;
        public double x_current { get; set; }
        public double change_x { get; set; }

        public bool IsBlack { get; set; }

        public Disk()
        {
            rotation = 3; x_current = 0; change_x = rotation * 36;
        }
        public void Rotate(int time)
        {
            x_current = (x_current + change_x * time + 360) % 360;
        }
        public void Check()
        {
            if ((x_current <= x0 && x_current >= x1) || (x_current <= x2 && x_current >= x3) || (x_current == 0))
            {
                IsBlack = true;
            }
            else IsBlack = false;
        }
    }
}
