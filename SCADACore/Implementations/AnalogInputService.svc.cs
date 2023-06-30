using SCADACore.Context;
using SCADACore.Interfaces;
using SCADACore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using SCADACore.Execptions;

namespace SCADACore.Implementations
{
    public class AnalogInputService : IAnalogInputService, IScanService
    {
        private Dictionary<int, Thread> _threadScannerContainter = new Dictionary<int, Thread>();

        // GOOD
        public IEnumerable<AnalogInput> GetAll()
        {
            using (DbIOContext db = new DbIOContext())
            {
                List<AnalogInput> analogInputs = db.AnalogInputs.ToList();
                return analogInputs;
            }
        }

        // GOOD
        public AnalogInput GetForIOAddress(int ioAddress)
        {
            using (DbIOContext db = new DbIOContext())
            {
                AnalogInput analogInput = db.AnalogInputs.Where(input => input.IOAddress == ioAddress).FirstOrDefault();
                if (analogInput == null) throw new IONotExistException();
                return analogInput;
            }
        }

        // GOOD
        public void Save(AnalogInput analogInput)
        {
            using (DbIOContext db = new DbIOContext())
            {
                AnalogInput existingAnalogInput = db.AnalogInputs.Where(input => input.TagName == analogInput.TagName).FirstOrDefault();
                if (existingAnalogInput != null) throw new IOAlreadyExistException();

                db.AnalogInputs.Add(analogInput);
                db.SaveChanges();
            }
        }

        // GOOD
        public void StartScan(int ioAdress)
        {
            AnalogInput analogInput = GetForIOAddress(ioAdress);
            ChangeScanStatus(ioAdress, true);

            IScanCallback proxy = OperationContext.Current.GetCallbackChannel<IScanCallback>();

            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(analogInput.ScanTime);
                    AnalogOutput analogOutput = GetAnalogOutputByAddress(ioAdress);

                    var val = analogOutput.Value;
                    proxy.AnalogScanDone(ioAdress, val);
                }
            });
            _threadScannerContainter.Add(ioAdress, thread);
            thread.Start();
        }

        // GOOD
        public void EndScan(int ioAdress)
        {
            ChangeScanStatus(ioAdress, false);
            Thread thread = _threadScannerContainter[ioAdress];
            thread.Abort();
            _threadScannerContainter.Remove(ioAdress);
        }

        // GOOD
        private AnalogOutput GetAnalogOutputByAddress(int ioAddress)
        {
            using (DbIOContext db = new DbIOContext())
            {
                AnalogOutput analogOutput = db.AnalogOutputs.Where(input => input.IOAddress == ioAddress).FirstOrDefault();
                if (analogOutput == null) throw new IONotExistException();
                return analogOutput;
            }
        }

        // GOOD
        private void ChangeScanStatus(int ioAddress, bool status)
        {
            using (var dbContext = new DbIOContext())
            {
                AnalogInput existiongAnalogInput = dbContext.AnalogInputs.FirstOrDefault(input => input.IOAddress == ioAddress);
                if (existiongAnalogInput == null) throw new IONotExistException();

                existiongAnalogInput.OnScan = status;
                dbContext.SaveChanges();
            }
        }
    }
}