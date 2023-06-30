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
    public class AnalogOutputService : IAnalogOutputService
    {
        public IEnumerable<AnalogOutput> GetAll()
        {
            using (var db = new DbIOContext())
            {
                return db.AnalogOutputs.ToList();
            }
        }

        public AnalogOutput GetForIOAddress(int ioAddress)
        {
            using (DbIOContext db = new DbIOContext())
            {
                AnalogOutput analogOutput = db.AnalogOutputs.Where(input => input.IOAddress == ioAddress).FirstOrDefault();
                if (analogOutput == null) throw new IONotExistException(IOType.AnalogOutput);
                return analogOutput;
            }
        }

        public void Save(AnalogOutput analogOutput)
        {
            using (DbIOContext db = new DbIOContext())
            {
                AnalogOutput existingAnalogOutput = db.AnalogOutputs.Where(output => output.TagName == analogOutput.TagName).FirstOrDefault();
                if (existingAnalogOutput != null) throw new IOAlreadyExistException(IOType.AnalogOutput);

                db.AnalogOutputs.Add(analogOutput);
                db.SaveChanges();
            }
        }

        public void SetNewValue(int ioAddress, double newValue)
        {
            using (var db = new DbIOContext())
            {
                AnalogOutput existingAnalogOutput = db.AnalogOutputs.FirstOrDefault(output => output.IOAddress == ioAddress);
                if (existingAnalogOutput == null) throw new IONotExistException(IOType.AnalogOutput);

                existingAnalogOutput.Value = newValue;
                db.SaveChanges();
            }
        }
    }
}