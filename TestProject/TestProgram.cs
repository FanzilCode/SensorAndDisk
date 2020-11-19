using System;
using System.IO;

namespace TestProject
{
    class TestProgram
    {

        public static string report = "";
        static bool processIn = true;
        public static void Start(Sensor sensor, Disk disk)
        {
            DateTime time = DateTime.Now;
            DateTime time1 = time;
            while (processIn)
            {
                time = DateTime.Now;
                if (time.Millisecond != time1.Millisecond && Math.Abs(time.Millisecond - time1.Millisecond) % sensor.Response == 0)
                {
                    disk.Rotate(sensor.Response);
                    sensor.Check(disk);
                    report += $"\n{time.ToLongTimeString()}:{time.Millisecond}  {sensor.Command()}";
                    Console.Write($"\n{time.ToLongTimeString()}:{time.Millisecond}  {sensor.Command()}");
                    time1 = time;
                }
            }
        }
        public static void Stop()
        {
            processIn = false;
        }

        public static void ReverseDirectionDisk(Disk disk)
        {
            disk.change_x *= -1;
        }

        public static void EditResponse(Sensor sensor, int NewResponse)
        {
            sensor.Response = NewResponse;
        }

        public static void EditRotation(Disk disk, double NewRotation)
        {
            disk.rotation = NewRotation;
            disk.change_x = 0.36 / NewRotation;
        }

        public static void SaveInFile(string report, string file)
        {
            File.WriteAllText(report, file);
        }
    }
}