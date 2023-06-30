using RTU.AnalogOutputService;
using RTU.DigitalOutputService;

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
        private static AnalogOutputService.AnalogOutputServiceClient analogOutputServiceClient = new AnalogOutputService.AnalogOutputServiceClient();
        private static DigitalOutputService.DigitalOutputServiceClient digitalOutputServiceClient = new DigitalOutputService.DigitalOutputServiceClient();

        private const int secondsToUpdate = 10 * 1000;

        private static void Main(string[] args)
        {
            //GenerateTestData();

            var analogOutputs = analogOutputServiceClient.GetAll();
            var digitalOutputs = digitalOutputServiceClient.GetAll();
            foreach (AnalogOutput analogOutput in analogOutputs)
            {
                Thread thread = new Thread(() =>
                {
                    while (true)
                    {
                        double newValue = GenerateRandomDouble(analogOutput.LowLimit, analogOutput.HighLimit);
                        analogOutputServiceClient.SetNewValue(analogOutput.IOAddress, newValue);
                        Thread.Sleep(secondsToUpdate);
                        Console.WriteLine("AnalogOutput: " + analogOutput.IOAddress.ToString() + ", NewValue: " + newValue);
                    }
                });
                thread.Start();
            }
            foreach (DigitalOutput digitalOutput in digitalOutputs)
            {
                Thread thread = new Thread(() =>
                {
                    while (true)
                    {
                        bool newValue = GenerateRandomBool();
                        digitalOutputServiceClient.SetNewValue(digitalOutput.IOAddress, newValue);
                        Thread.Sleep(secondsToUpdate);
                        Console.WriteLine("DigitalOutput: " + digitalOutput.IOAddress.ToString() + ", NewValue: " + newValue);
                    }
                });
                thread.Start();
            }

            Console.ReadKey();
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
            AnalogOutput analogOutput1 = new AnalogOutput { Description = "Opis", LowLimit = 1.0, HighLimit = 100.0, Value = 50.0, IOAddress = 1, TagName = "Tag1", Units = "cm" };
            AnalogOutput analogOutput2 = new AnalogOutput { Description = "Opis", LowLimit = 1.0, HighLimit = 50.0, Value = 25.0, IOAddress = 2, TagName = "Tag2", Units = "cm" };

            DigitalOutput digitalOutput1 = new DigitalOutput { TagName = "Tag3", Description = "Opis", IOAddress = 3, Value = true };

            DigitalOutput digitalOutput2 = new DigitalOutput { TagName = "Tag4", Description = "Opis", IOAddress = 4, Value = false };

            Console.WriteLine("Adding AnalogOutput...");

            analogOutputServiceClient.Save(analogOutput1);
            analogOutputServiceClient.Save(analogOutput2);

            Console.WriteLine("Adding DigitalOutput...");
            digitalOutputServiceClient.Save(digitalOutput1);
            digitalOutputServiceClient.Save(digitalOutput2);

            Console.WriteLine("Outputs saved!");
        }
    }
}