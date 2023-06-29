using SCADACore.Interfaces;
using SCADACore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace SCADACore.Implementations
{
    public class AnalogInputService : IAnalogInputService, IScanService
    {
        private Dictionary<int, Thread> _threadScannerContainter = new Dictionary<int, Thread>();

        public IEnumerable<AnalogInput> GetAll()
        {
            throw new NotImplementedException();
        }

        public AnalogInput GetForIOAddress(int ioAddress)
        {
            throw new NotImplementedException();
        }

        public void StartScan(int ioAdress)
        {
            AnalogInput analogInput = GetForIOAddress(ioAdress);
            Thread thread = new Thread(() =>
            {
                Thread.Sleep(analogInput.ScanTime);
                // Iscitati vrednosti
                IScanCallback proxy = OperationContext.Current.GetCallbackChannel<IScanCallback>();

                var val = 10;
                proxy.AnalogScanDone(ioAdress, val);
            });
            _threadScannerContainter.Add(ioAdress, thread);
            thread.Start();
        }

        public void EndScan(int ioAdress)
        {
            // Dodati proveru da ioAdress postoji
            Thread thread = _threadScannerContainter[ioAdress];
            thread.Abort();
            _threadScannerContainter.Remove(ioAdress);
        }
    }
}