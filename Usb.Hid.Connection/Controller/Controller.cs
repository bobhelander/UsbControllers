using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Usb.Hid.Connection.models;

namespace Usb.Hid.Connection
{
    public partial class Controller : IObservable<ReadBuffer>
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private List<IObserver<ReadBuffer>> observers = new List<IObserver<ReadBuffer>>();        

        public IDisposable Subscribe(IObserver<ReadBuffer> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);

            return new Unsubscriber(observers, observer);
        }

        private void Notify(ReadBuffer readBuffer)
        {
            foreach (var observer in observers)
                observer.OnNext(readBuffer);
        }

        /// <summary>
        /// The encapsulated stream.
        /// </summary>
        private readonly HidStream stream;

        /// <summary>
        /// Process every report or check against previous
        /// </summary>
        public bool ProcessAllReports { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        /// <param name="devicePath">The path of the device.</param>
        public Controller(string devicePath)
        {
            stream = new HidStream(devicePath);
            featureBuffer = new byte[this.FeatureLength];
        }

        /// <summary>
        /// Initialises the device for asynchronous reads.
        /// </summary>
        public void Initialize()
        {
            for (ulong count = 0; count < readBufferCount; count++)
                this.readBuffers.Add(new byte[this.ReadLength]);

            lastBuffer = new byte[this.ReadLength];

            ContinueProcessing = true;
            SerialProcessingTask = Task.Factory.StartNew(() => ReadSerial(),
                CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default)
                .ContinueWith(t => { log.Error($"Controller Exception: {t.Exception}"); }, TaskContinuationOptions.OnlyOnFaulted);
        }       

        /// <summary>
        /// Releases the associated ressources.
        /// </summary>
        public void Close()
        {
            this.stream?.Dispose();
        }
    }
}
