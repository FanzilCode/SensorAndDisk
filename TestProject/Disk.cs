﻿using System;

namespace TestProject
{
    class Disk
    {

        static double x0 = 359, x1 = 300, x2 = 239, x3 = 120;
        public double rotation { get; set; }
        public double x_current { get; set; }
        public double change_x { get; set; }


        public Disk()
        {
            rotation = 1; x_current = 0; change_x = 0.36 * rotation;
        }
        public void Rotate(int Response)
        {
            x_current = (x_current + change_x * Response + 360) % 360;
        }

    }
}
