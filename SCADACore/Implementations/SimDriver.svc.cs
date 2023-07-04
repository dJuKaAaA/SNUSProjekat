using SCADACore.Interfaces;
using SCADACore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SCADACore.Implementations
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SimDriver" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SimDriver.svc or SimDriver.svc.cs at the Solution Explorer and start debugging.
    public class SimDriver : ISimDriver
    {
        public double Cosine()
        {
            return 100 * Math.Cos((double)DateTime.Now.Second / 60 * Math.PI);
        }

        public double GenerateValue(DriverType driverType)
        {
            double value = -1;
            switch (driverType)
            {
                case DriverType.Sine:
                    value = Sine();
                    break;
                case DriverType.Cosine:
                    value = Cosine();
                    break;
                case DriverType.Ramp:
                    value = Ramp();
                    break;
            }

            return value;
        }

        public double Ramp()
        {
            return 100 * DateTime.Now.Second / 60;
        }

        public double Sine()
        {
            return 100 * Math.Sin((double)DateTime.Now.Second / 60 * Math.PI);
        }
    }
}
