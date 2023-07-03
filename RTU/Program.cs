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

        private const int secondsToUpdate = 3 * 1000;

        private static void Main(string[] args)
        {
            //GenerateTestData();

            Console.WriteLine("RTU running...");
            while (true)
            {
                IEnumerable<DigitalInput> digitalInputs = digitalInputServiceClient.GetAll();
                IEnumerable<AnalogInput> analogInputs = analogInputServiceClient.GetAll();
                Thread.Sleep(secondsToUpdate);

                foreach (AnalogInput analogInput in analogInputs)
                {
                    if (analogInput.DriverType != AnalogInputService.DriverType.RealTime)
                    {
                        continue;
                    }

                    Thread thread = new Thread(() =>
                    {
                        double newValue = GenerateRandomDouble();
                        analogInputServiceClient.SetNewValue(analogInput.IOAddress, newValue);
                        Console.WriteLine("AnalogIntput: " + analogInput.IOAddress.ToString() + ", NewValue: " + newValue);
                    });
                    thread.Start();
                }

                foreach (DigitalInput digitalInput in digitalInputs)
                {
                    if (digitalInput.DriverType != DigitalInputService.DriverType.RealTime)
                    {
                        continue;
                    }

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

        private static double GenerateRandomDouble()
        {
            Random random = new Random();
            //double randomNumber = random.NextDouble() * random.Next(-1000, 1000);
            double randomNumber = random.NextDouble() * random.Next(0, 1000);
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

            analogInputServiceClient.Create(analogOutput1);
            analogInputServiceClient.Create(analogOutput2);

            Console.WriteLine("Adding DigitalInput...");
            digitalInputServiceClient.Create(digitalOutput1);
            digitalInputServiceClient.Create(digitalOutput2);

            Console.WriteLine("Outputs saved!");
        }
    }
}