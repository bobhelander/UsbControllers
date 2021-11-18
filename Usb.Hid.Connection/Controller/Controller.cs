using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Usb.Hid.Connection.models;

namespace Usb.Hid.Connection
{
    public partial class Controller : IObservable<ReadBuffer>, IDisposable
    {
        private readonly List<IObserver<ReadBuffer>> observers = new List<IObserver<ReadBuffer>>();

        private readonly ILogger logger;

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
        /// True if the controller only sends events on changes.  Initial state may be unknown until user input is detected.
        /// </summary>
        public bool EventsOnlyReported { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        /// <param name="devicePath">The path of the device.</param>
        /// <param name="logger">Microsoft.Extensions.Logging implementation.  Use null to disable any loggin.</param>
        /// Microsoft.Extensions.Logging
        public Controller(string devicePath, ILogger logger)
        {
            this.logger = logger;
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
            CancellationTokenSource = new CancellationTokenSource();

            // StartNew wraps the task.  Call Unwrap() to get the real task.
            SerialProcessingTask = Task.Factory.StartNew(async () => await ReadSerial(CancellationTokenSource.Token).ConfigureAwait(false),
                CancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default).Unwrap();

            // Log all the ways that this can stop
            SerialProcessingTask.ContinueWith(t => {
                     if (t.IsCanceled) logger?.LogDebug("Controller ReadSerial() cancelled");
                     else if (t.IsFaulted) logger?.LogError($"Controller ReadSerial() Exception: {t.Exception.InnerException?.Message}");
                     else logger?.LogDebug("Controller ReadSerial() complete");
                 });
        }

        /// <summary>
        /// Returns an input report.  Note: Most HID devices do not support this method.
        /// </summary>
        /// <returns></returns>
        public byte[] GetInputReport()
        {
            return this.stream?.InputReport;
        }

        /// <summary>
        /// Attempts to retrieve and place an input report in the stream buffer.  This method would only be used to get the initial values from a device that
        /// is returning NAK until a button or axis movement takes place.
        /// </summary>
        /// <returns></returns>
        public async Task ProcessInputReport()
        {
            try
            {
                var inputReport = GetInputReport();
                if (inputReport?.Length > 0)
                {
                    await this.ProcessSerialMessage(inputReport.Length, inputReport, inputReport.Length, 0).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                logger?.LogError($"Controller Exception: {ex}");
            }
        }

        /// <summary>
        /// Stops the reader.
        /// </summary>
        public void Stop()
        {
            ContinueProcessing = false;
            CancellationTokenSource?.Cancel();
            SerialProcessingTask?.Wait();
        }

        /// <summary>
        /// Releases the associated ressources.
        /// </summary>
        public void Close()
        {
            Stop();
            this.stream?.Dispose();
            CancellationTokenSource?.Dispose();
        }

        public void Dispose() => Close();
    }
}
