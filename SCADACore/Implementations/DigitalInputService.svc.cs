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
    public class DigitalInputService : IDigitalInputService, IScanService
    {
        private Dictionary<int, Thread> _threadScannerContainer = new Dictionary<int, Thread>();

        public IEnumerable<DigitalInput> GetAll()
        {
            throw new NotImplementedException();
        }

        public DigitalInput GetForIOAddress(int ioAddress)
        {
            throw new NotImplementedException();
        }

        public void StartScan(int ioAdress)
        {
            DigitalInput digitalInput = GetForIOAddress(ioAdress);
            Thread thread = new Thread(() =>
            {
                Thread.Sleep(digitalInput.ScanTime);
                // Iscitati vrednosti
                IScanCallback proxy = OperationContext.Current.GetCallbackChannel<IScanCallback>();

                var val = true;
                proxy.DigitalScanDone(ioAdress, val);
            });
            _threadScannerContainer.Add(ioAdress, thread);
            thread.Start();
        }

        public void EndScan(int ioAdress)
        {
            // Dodati proveru da ioAdress postoji
            Thread thread = _threadScannerContainer[ioAdress];
            thread.Abort();
            _threadScannerContainer.Remove(ioAdress);
        }
    }
}