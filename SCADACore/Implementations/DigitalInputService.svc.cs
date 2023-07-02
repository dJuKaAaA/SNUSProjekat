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
        private readonly Dictionary<int, Thread> _threadScannerContainer = new Dictionary<int, Thread>();

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
                return digitalInput;
            }
        }

        public void StartScan(int ioAddress)
        {
            DigitalInput digitalInput = GetForIOAddress(ioAddress);
            ChangeScanStatus(ioAddress, true);

            IScanCallback proxy = OperationContext.Current.GetCallbackChannel<IScanCallback>();

            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(digitalInput.ScanTime);
                    DigitalOutput analogOutput = GetDigitalOutputByAddress(ioAddress);

                    var val = analogOutput.Value;
                    proxy.DigitalScanDone(ioAddress, val);
                }
            });

            _threadScannerContainer.Add(ioAddress, thread);
            thread.Start();
        }

        public void EndScan(int ioAddress)
        {
            ChangeScanStatus(ioAddress, false);
            Thread thread = _threadScannerContainer[ioAddress];
            thread.Abort();
            _threadScannerContainer.Remove(ioAddress);
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

        public DigitalInput Create(DigitalInput input)
        {
            // TODO: Throw exceptions
            if (GetByTagName(input.TagName) != null)
            {
                return null;
            }
            if (GetForIOAddress(input.IOAddress) != null)
            {
                return null;
            }

            using (DbIOContext db = new DbIOContext())
            {
                db.DigitalInputs.Add(input);
                db.SaveChanges();
            }
            return input;
        }

        public DigitalInput GetByTagName(string tagName)
        {
            using (DbIOContext db = new DbIOContext())
            {
                return db.DigitalInputs.Where(input => input.TagName == tagName).FirstOrDefault();
            }
        }

        public void EndAllScans()
        {
            foreach (KeyValuePair<int, Thread> kvp in _threadScannerContainer)
            {
                Thread thread = kvp.Value;
                thread.Abort();
                ChangeScanStatus(kvp.Key, false);
            }
            _threadScannerContainer.Clear();
        }
    }
}