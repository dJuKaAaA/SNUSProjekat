using RTU.AnalogInputService;
using RTU.DigitalInputService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RTU
{
    public class Program
    {
        private static AnalogInputService.AnalogInputServiceClient analogInputServiceClient = new AnalogInputService.AnalogInputServiceClient();
        private static DigitalInputService.DigitalInputServiceClient digitalInputServiceClient = new DigitalInputService.DigitalInputServiceClient();

        private const int secondsToUpdate = 10 * 1000;

        private static void Main(string[] args)
        {
            //GenerateTestData();

            while (true)
            {
                Thread.Sleep(secondsToUpdate);
                var analogInputs = analogInputServiceClient.GetAll();
                var digitalInputs = digitalInputServiceClient.GetAll();

                foreach (AnalogInput analogInput in analogInputs)
                {
                    Thread thread = new Thread(() =>
                    {
                        double newValue = GenerateRandomDouble(analogInput.LowLimit, analogInput.HighLimit);
                        analogInputServiceClient.SetNewValue(analogInput.IOAddress, newValue);
                        Console.WriteLine("AnalogIntput: " + analogInput.IOAddress.ToString() + ", NewValue: " + newValue);
                    });
                    thread.Start();
                }

                foreach (DigitalInput digitalInput in digitalInputs)
                {
                    Thread thread = new Thread(() =>
                    {
                        bool newValue = GenerateRandomBool();
                        digitalInputServiceClient.SetNewValue(digitalInput.IOAddress, newValue);
                        Console.WriteLine("DigitalInput: " + digitalInput.IOAddress.ToString() + ", NewValue: " + newValue);
                    });
                    thread.Start();
                }
            }
        }

        private static double GenerateRandomDouble(double minValue, double maxValue)
        {
            Random random = new Random();
            double randomNumber = random.NextDouble() * (maxValue - minValue) + minValue;
            return randomNumber;
        }

        private static bool GenerateRandomBool()
        {
            Random random = new Random();
            int random0Or1 = random.Next(2);
            return Convert.ToBoolean(random0Or1);
        }

        private static void GenerateTestData()
        {
            AnalogInput analogOutput1 = new AnalogInput { Description = "Opis", LowLimit = 1.0, HighLimit = 100.0, Value = 50.0, IOAddress = 1, TagName = "Tag1", Units = "cm" };
            AnalogInput analogOutput2 = new AnalogInput { Description = "Opis", LowLimit = 1.0, HighLimit = 50.0, Value = 25.0, IOAddress = 2, TagName = "Tag2", Units = "cm" };

            DigitalInput digitalOutput1 = new DigitalInput { TagName = "Tag3", Description = "Opis", IOAddress = 3, Value = true };

            DigitalInput digitalOutput2 = new DigitalInput { TagName = "Tag4", Description = "Opis", IOAddress = 4, Value = false };

            Console.WriteLine("Adding AnalogInput...");

            analogInputServiceClient.Save(analogOutput1);
            analogInputServiceClient.Save(analogOutput2);

            Console.WriteLine("Adding DigitalInput...");
            //digitalInputServiceClient.Save(digitalOutput1);
            //digitalInputServiceClient.Save(digitalOutput2);

            Console.WriteLine("Outputs saved!");
        }
    }
}