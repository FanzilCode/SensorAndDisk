using System;
using System.Windows;
using System.Windows.Media;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TestProject
{
    public partial class MainWindow : Window
    {
        Sensor sensor = new Sensor();
        Disk disk = new Disk();
        bool enabled = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            
            int response = 0;
            int.TryParse(textBoxResponse.Text.Trim(), out response);
            double rotation = 0;
            Double.TryParse(textBoxRotation.Text.Trim(), out rotation);

            if (enabled)
            {
                if (response < 10 || response > 100)
                {
                    textBoxResponse.ToolTip = "частота реакции датчика должна удовлетворять условиям: целое число, которое не меньше 10 и не больше 100!";
                    textBoxResponse.Background = Brushes.DarkRed;
                }
                else if (rotation < 0.5 || rotation > 3)
                {
                    textBoxRotation.ToolTip = "частота вращения диска должна удовлетворять условиям: число, которое не меньше 0.5 и не больше 3!";
                    textBoxRotation.Background = Brushes.DarkRed;
                }
                else
                {
                    textBoxResponse.ToolTip = "";
                    textBoxResponse.Background = Brushes.Transparent;
                    textBoxRotation.ToolTip = "";
                    textBoxRotation.Background = Brushes.Transparent;

                    await Task.Run(() => TestProgram.EditResponse(sensor, response));
                    await Task.Run(() => TestProgram.EditRotation(disk, rotation));

                    MessageBox.Show("Ввод корректен. Диск вращается");

                }
            }
            else MessageBox.Show("Ошибка. Диск не вращается");

        }
        private async void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            enabled = true;


            textBlock.Text = "Диск вращается по часовой стрелке";


            await Task.Run(() => TestProgram.Start(disk, sensor));

            Check();
        }

        private async void Button_Stop_Click(object sender, RoutedEventArgs e)
        {
           enabled = false;

           await Task.Run(() => TestProgram.Stop(disk));

            textBlock.Text = "Диск остановлен";
        }

        private async void Button_Reverse_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => TestProgram.ReverseDirectionDisk(disk));

            Check();
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText("text.txt", TestProgram.report);
            MessageBox.Show("Результаты были сохранены в файл text.txt");
        }
        private void Check()
        {
            Thread.Sleep(50);
            if (sensor.ClockWise)
            {
                textBlock.Text = "Диск вращается по часовой стрелке";
            }
            else textBlock.Text = "Диск вращается против часовой стрелки";

        }
    }
}