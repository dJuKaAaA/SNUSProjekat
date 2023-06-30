using SCADACore.Context;
using SCADACore.Interfaces;
using SCADACore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SCADACore.Execptions;

namespace SCADACore.Implementations
{
    public class DigitalOutputService : IDigitalOutputService
    {
        public IEnumerable<DigitalOutput> GetAll()
        {
            using (var db = new DbIOContext())
            {
                return db.DigitalOutputs.ToList();
            }
        }

        public DigitalOutput GetForIOAddress(int ioAddress)
        {
            using (var db = new DbIOContext())
            {
                DigitalOutput digitalOutput = db.DigitalOutputs.Where(output => output.IOAddress == ioAddress).FirstOrDefault();
                if (digitalOutput == null) throw new IONotExistException(IOType.DigitalOutput);
                return digitalOutput;
            }
        }

        public void Save(DigitalOutput digitalOutput)
        {
            using (DbIOContext db = new DbIOContext())
            {
                DigitalOutput existingDigitalOutput = db.DigitalOutputs.Where(output => output.TagName == digitalOutput.TagName).FirstOrDefault();
                if (existingDigitalOutput != null) throw new IOAlreadyExistException(IOType.DigitalOutput);

                db.DigitalOutputs.Add(digitalOutput);
                db.SaveChanges();
            }
        }

        public void SetNewValue(int ioAddress, bool newValue)
        {
            using (var db = new DbIOContext())
            {
                DigitalOutput existingDigitalOutput = db.DigitalOutputs.FirstOrDefault(output => output.IOAddress == ioAddress);
                if (existingDigitalOutput == null) throw new IONotExistException(IOType.DigitalOutput);

                existingDigitalOutput.Value = newValue;
                db.SaveChanges();
            }
        }
    }
}