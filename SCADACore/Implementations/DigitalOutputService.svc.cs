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
using SCADACore.Helper;

namespace SCADACore.Implementations
{
    public class DigitalOutputService : IDigitalOutputService
    {
        private readonly IOAddressChecker _addressChecker = new IOAddressChecker();

        public DigitalOutput Create(DigitalOutput output)
        {
            // TODO: Throw exceptions
            if (GetByTagName(output.TagName) != null)
            {
                return null;
            }
            if (_addressChecker.IsAddressTaken(output.IOAddress))
            {
                return null;
            }

            using (DbIOContext db = new DbIOContext())
            {
                db.DigitalOutputs.Add(output);
                db.SaveChanges();
            }
            return output;
        }

        public IEnumerable<DigitalOutput> GetAll()
        {
            using (var db = new DbIOContext())
            {
                return db.DigitalOutputs.ToList();
            }
        }

        public DigitalOutput GetByTagName(string tagName)
        {
            using (DbIOContext db = new DbIOContext())
            {
                return db.DigitalOutputs.Where(output => output.TagName == tagName).FirstOrDefault();
            }
        }

        public DigitalOutput GetForIOAddress(int ioAddress)
        {
            using (var db = new DbIOContext())
            {
                DigitalOutput digitalOutput = db.DigitalOutputs.Where(output => output.IOAddress == ioAddress).FirstOrDefault();
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
            DigitalOutput existingDigitalOutput = null;
            using (var db = new DbIOContext())
            {
                existingDigitalOutput = db.DigitalOutputs.FirstOrDefault(output => output.IOAddress == ioAddress);
                if (existingDigitalOutput == null) throw new IONotExistException(IOType.DigitalOutput);

                existingDigitalOutput.Value = newValue;
                db.SaveChanges();
            }

            using (var db = new DbTagReportContext())
            {
                db.TagReports.Add(new TagReport()
                {
                    TagName = existingDigitalOutput.TagName,
                    Timestamp = DateTime.Now.Second,
                    Value = Convert.ToInt32(existingDigitalOutput.Value),
                    TagType = IOType.DigitalOutput
                });
                db.SaveChanges();
            }
        }
    }
}