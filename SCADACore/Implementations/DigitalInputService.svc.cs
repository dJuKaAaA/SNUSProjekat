﻿using SCADACore.Context;
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
using SCADACore.Helper;

namespace SCADACore.Implementations
{
    public class DigitalInputService : IDigitalInputService, IScanService
    {
        private readonly IOAddressChecker _addressChecker = new IOAddressChecker();
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
                    digitalInput = GetForIOAddress(ioAddress);

                    var val = digitalInput.Value;
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

        public void ChangeScanStatus(int ioAddress, bool status)
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
            if (_addressChecker.IsAddressTaken(input.IOAddress))
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

        public void SetNewValue(int ioAddress, bool newValue)
        {
            using (var db = new DbIOContext())
            {
                DigitalInput existingDigitalInput = db.DigitalInputs.FirstOrDefault(input => input.IOAddress == ioAddress);
                if (existingDigitalInput == null) throw new IONotExistException(IOType.AnalogInput);

                existingDigitalInput.Value = newValue;

                db.TagReports.Add(new TagReport()
                {
                    TagName = existingDigitalInput.TagName,
                    Timestamp = DateTime.Now.Second,
                    Value = Convert.ToInt32(existingDigitalInput.Value),
                    TagType = IOType.DigitalInput
                });

                db.SaveChanges();
            }
        }

        public void Save(DigitalInput digitalInput)
        {
        }

        public void SetDriverType(int ioAddress, DriverType driverType)
        {
            using (DbIOContext db = new DbIOContext())
            {
                DigitalInput digitalInput = db.DigitalInputs.Where(input => input.IOAddress == ioAddress).FirstOrDefault();
                if (digitalInput == null) throw new IONotExistException(IOType.AnalogInput);

                digitalInput.DriverType = driverType;

                db.SaveChanges();
            }
        }
    }
}