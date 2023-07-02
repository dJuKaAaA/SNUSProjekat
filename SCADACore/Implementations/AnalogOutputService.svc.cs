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
        public AnalogOutput Create(AnalogOutput output)
        {
            // TODO: Throw exceptions
            if (GetByTagName(output.TagName) != null)
            {
                return null;
            }
            if (GetForIOAddress(output.IOAddress) != null)
            {
                return null;
            }

            using (DbIOContext db = new DbIOContext())
            {
                db.AnalogOutputs.Add(output);
                db.SaveChanges();
            }
            return output;
        }

        public IEnumerable<AnalogOutput> GetAll()
        {
            using (var db = new DbIOContext())
            {
                return db.AnalogOutputs.ToList();
            }
        }

        public AnalogOutput GetByTagName(string tagName)
        {
            using (DbIOContext db = new DbIOContext())
            {
                return db.AnalogOutputs.Where(output => output.TagName == tagName).FirstOrDefault();
            }
        }

        public AnalogOutput GetForIOAddress(int ioAddress)
        {
            using (DbIOContext db = new DbIOContext())
            {
                AnalogOutput analogOutput = db.AnalogOutputs.Where(input => input.IOAddress == ioAddress).FirstOrDefault();
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