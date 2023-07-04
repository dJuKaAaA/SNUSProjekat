using SCADACore.Context;
using SCADACore.Implementations;
using SCADACore.Interfaces;
using SCADACore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADACore.Helper
{
    public class IOAddressChecker
    {
        public bool IsAddressTaken(int ioAddress)
        {
            using (DbIOContext db = new DbIOContext())
            {
                if (db.AnalogInputs.Where(input => input.IOAddress == ioAddress).Count() > 0) return true;
                if (db.AnalogOutputs.Where(output => output.IOAddress == ioAddress).Count() > 0) return true;
                if (db.DigitalInputs.Where(input => input.IOAddress == ioAddress).Count() > 0) return true;
                if (db.DigitalOutputs.Where(output => output.IOAddress == ioAddress).Count() > 0) return true;
            }

            return false;
        }
    }
}
