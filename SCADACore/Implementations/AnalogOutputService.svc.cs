using SCADACore.Context;
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
    public class AnalogOutputService : IAnalogOutputService
    {
        public IEnumerable<AnalogOutput> GetAll()
        {
            throw new NotImplementedException();
        }

        public AnalogOutput GetForIOAddress(int ioAddress)
        {
            throw new NotImplementedException();
        }

        public void Save(AnalogOutput analogOutput)
        {
            using (DbIOContext db = new DbIOContext())
            {
                db.AnalogOutputs.Add(analogOutput);
                db.SaveChanges();
            }
        }
    }
}