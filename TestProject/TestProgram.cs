using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Threading;

namespace TestProject
{
    class TestProgram
    {
        static List<bool> query = new List<bool>();

        public static string report = "";

        public static void Start(Disk disk, Sensor sensor)
        {
            DateTime time = DateTime.Now;

            DateTime time1 = time;

            DateTime time2 = time;
            string report1 = "";
            string report2 = "";
            while (true)
            {
                time = DateTime.Now;
                int dif = Math.Abs(time.Millisecond - time2.Millisecond);
                if (dif != 0 && dif%sensor.Response == 0)
                {

                    disk.Rotate(dif);

                    disk.Check();
                    
                    query.Add(disk.IsBlack);

                    int dif1 = Math.Abs(time1.Millisecond - DateTime.Now.Millisecond);
                    if ( (query.Count >= 3000/sensor.Response))
                    {
                        time1 = DateTime.Now;
                        Delete();
                        sensor.DetermineIsDirectionOfRotation(query);
                        if (sensor.IsRotate)
                        {
                            report1 = $"Диск вращается ";

                            report1 += (sensor.ClockWise && disk.change_x > 0) ? ($"по часовой стрелке") : ($"против часовой стрелки");
                        }
                        else report1 = $"Диск остановлен";
                        if(report1 != report2)
                        {
                            report2 = report1;

                            report1 = $"\n{time1.ToLongTimeString()}:{time1.Millisecond} " + report1;
                            report += report1;

                            Console.Write(report1);
                        }

                    }
                    time2 = time;
                }
            }
        }
        public static void Delete()
        {
            bool first = query[0];
            while (query[0] == first)
            {
                query.RemoveAt(0);
                if (query.Count == 0) break;
            }
        }
        public static void Stop(Disk disk)
        {
            disk.change_x = 0;
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