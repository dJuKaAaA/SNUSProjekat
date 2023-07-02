using SCADACore.Context;
using SCADACore.Interfaces;
using SCADACore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using SCADACore.Execptions;

namespace SCADACore.Implementations
{
    public class DigitalInputService : IDigitalInputService, IScanService
    {
        private Dictionary<int, Thread> _threadScannerContainer = new Dictionary<int, Thread>();

        public IEnumerable<DigitalInput> GetAll()
        {
            using (var db = new DbIOContext())
            {
                return db.DigitalInputs.ToList();
            }
        }

        public DigitalInput GetForIOAddress(int ioAddress)
        {
            using (var db = new DbIOContext())
            {
                DigitalInput digitalInput = db.DigitalInputs.Where(input => input.IOAddress == ioAddress).FirstOrDefault();
                if (digitalInput == null) throw new IONotExistException(IOType.DigitalInput);
                return digitalInput;
            }
        }

        public void StartScan(int ioAdress)
        {
            DigitalInput digitalInput = GetForIOAddress(ioAdress);
            IScanCallback proxy = OperationContext.Current.GetCallbackChannel<IScanCallback>();
            ChangeScanStatus(ioAdress, true);
            Thread thread = new Thread(() =>
            {
                Thread.Sleep(digitalInput.ScanTime);

                var val = GetDigitalOutputByAddress(ioAdress).Value;
                proxy.DigitalScanDone(ioAdress, val);
            });
            _threadScannerContainer.Add(ioAdress, thread);
            thread.Start();
        }

        public void EndScan(int ioAdress)
        {
            Thread thread = _threadScannerContainer[ioAdress];
            thread.Abort();
            _threadScannerContainer.Remove(ioAdress);
            ChangeScanStatus(ioAdress, false);
        }

        private DigitalOutput GetDigitalOutputByAddress(int ioAddress)
        {
            using (DbIOContext db = new DbIOContext())
            {
                DigitalOutput digitalOutput = db.DigitalOutputs.Where(input => input.IOAddress == ioAddress).FirstOrDefault();
                if (digitalOutput == null) throw new IONotExistException(IOType.DigitalOutput);
                return digitalOutput;
            }
        }

        private void ChangeScanStatus(int ioAddress, bool status)
        {
            using (var dbContext = new DbIOContext())
            {
                DigitalInput existingDigitalInput = dbContext.DigitalInputs.FirstOrDefault(input => input.IOAddress == ioAddress);
                if (existingDigitalInput == null) throw new IONotExistException(IOType.DigitalInput);

                existingDigitalInput.OnScan = status;
                dbContext.SaveChanges();
            }
        }
    }
}