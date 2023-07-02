﻿using SCADACore.Context;
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
        private readonly Dictionary<int, Thread> _threadScannerContainer = new Dictionary<int, Thread>();

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
                return analogInput;
            }
        }

        // GOOD
        public void Save(AnalogInput analogInput)
        {
            using (DbIOContext db = new DbIOContext())
            {
                AnalogInput existingAnalogInput = db.AnalogInputs.Where(input => input.TagName == analogInput.TagName).FirstOrDefault();
                if (existingAnalogInput != null) throw new IOAlreadyExistException(IOType.AnalogInput);

                db.AnalogInputs.Add(analogInput);
                db.SaveChanges();
            }
        }

        // GOOD
        public void StartScan(int ioAddress)
        {
            AnalogInput analogInput = GetForIOAddress(ioAddress);
            ChangeScanStatus(ioAddress, true);

            IScanCallback proxy = OperationContext.Current.GetCallbackChannel<IScanCallback>();

            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(analogInput.ScanTime);
                    AnalogOutput analogOutput = GetAnalogOutputByAddress(ioAddress);

                    var val = analogOutput.Value;
                    proxy.AnalogScanDone(ioAddress, val);
                }
            });

            _threadScannerContainer.Add(ioAddress, thread);
            thread.Start();
        }

        // GOOD
        public void EndScan(int ioAddress)
        {
            ChangeScanStatus(ioAddress, false);
            Thread thread = _threadScannerContainer[ioAddress];
            thread.Abort();
            _threadScannerContainer.Remove(ioAddress);
        }

        // GOOD
        private AnalogOutput GetAnalogOutputByAddress(int ioAddress)
        {
            using (DbIOContext db = new DbIOContext())
            {
                AnalogOutput analogOutput = db.AnalogOutputs.Where(input => input.IOAddress == ioAddress).FirstOrDefault();
                if (analogOutput == null) throw new IONotExistException(IOType.AnalogOutput);
                return analogOutput;
            }
        }

        // GOOD
        private void ChangeScanStatus(int ioAddress, bool status)
        {
            using (var dbContext = new DbIOContext())
            {
                AnalogInput existiongAnalogInput = dbContext.AnalogInputs.FirstOrDefault(input => input.IOAddress == ioAddress);
                if (existiongAnalogInput == null) throw new IONotExistException(IOType.AnalogInput);

                existiongAnalogInput.OnScan = status;
                dbContext.SaveChanges();
            }
        }

        public AnalogInput Create(AnalogInput input)
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
                db.AnalogInputs.Add(input);
                db.SaveChanges();
            }
            return input;
        }

        public AnalogInput GetByTagName(string tagName)
        {
            using (DbIOContext db = new DbIOContext())
            {
                return db.AnalogInputs.Where(input => input.TagName == tagName).FirstOrDefault();
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

        public void SetNewValue(int ioAddress, double newValue)
        {
            using (var db = new DbIOContext())
            {
                AnalogInput existingAnalogInput = db.AnalogInputs.FirstOrDefault(output => output.IOAddress == ioAddress);
                if (existingAnalogInput == null) throw new IONotExistException(IOType.AnalogInput);

                existingAnalogInput.Value = newValue;
                db.SaveChanges();
            }
        }
    }
}