using System;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Raspberry.IO.GeneralPurpose
{
    /// <summary>
    /// Implementation of IO ports over USB-Serial device Ke-USB24A
    /// http://www.kernelchip.ru/Ke-USB24A.php
    /// </summary>
    public class KeUSB24AConnectionDriver : IGpioConnectionDriver
    {
        /// <summary>
        /// The default timeout (5 seconds).
        /// </summary>
        public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(5);

        private readonly string devicePath;
        private SerialPort serialPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeUSB24AConnectionDriver"/> class.
        /// </summary>
        public KeUSB24AConnectionDriver(string device = "/dev/ttyACM0", int readTimeout = 5000)
        {
            if (Environment.OSVersion.Platform != PlatformID.Unix)
                throw new NotSupportedException("KeUSB24AConnectionDriver is only supported in Unix");

            devicePath = device;
            serialPort = new SerialPort(devicePath);
            serialPort.Open();
            serialPort.ReadTimeout = readTimeout;
            serialPort.NewLine = "\x0d\x0a";

            if (!checkDevice())
            {
                throw new System.InvalidOperationException(string.Format("Unable to initialze device: {0}", device));
            }
        }

        private bool checkDevice()
        {
            lock (serialPort)
            {
                var cmd = "$KE";
                int tryCount = 5;
                do
                {
                    Flush();
                    serialPort.WriteLine(cmd);
                    var resp = serialPort.ReadLine();
                    if (resp == "#OK")
                    {
                        return true;
                    }
                } while (tryCount-- != 0);
            }
            return false;
        }

        private void Flush()
        {
            serialPort.DiscardOutBuffer();
            serialPort.DiscardInBuffer();
        }

        /// <summary>
        /// Allocates the specified pin.
        /// </summary>
        /// <param name="pin">The pin.</param>
        /// <param name="direction">The direction.</param>
        public void Allocate(ProcessorPin pin, PinDirection direction)
        {
            //$KE,IO,SET,<LineNumber>,<IoDirection>[,S]
            lock (serialPort)
            {
                Flush();
                var cmd = string.Format("$KE,IO,SET,{0},{1}", (int)pin, direction == PinDirection.Input ? '1' : '0');
                serialPort.WriteLine(cmd);
                var resp = serialPort.ReadLine();
                if (resp != "#IO,SET,OK")
                {
                    throw new System.InvalidOperationException(string.Format("Invalid respose: {0}", resp));
                }
            }
        }

        /// <summary>
        /// Gets driver capabilities.
        /// </summary>
        /// <returns>The capabilites.</returns>
        public GpioConnectionDriverCapabilities GetCapabilities()
        {
            return GpioConnectionDriverCapabilities.None | GpioConnectionDriverCapabilities.CanWorkOnThirdPartyComputers;
        }

        /// <summary>
        /// </summary>
        public ProcessorPins Read(ProcessorPins pins)
        {
            // TODO: implement on $KE,RD,ALL
            var result = new ProcessorPins(pins.Count);
            foreach (var p in pins.Enumerate())
            {
                result.Set(p, Read((ProcessorPin)p));
            }
            return result;
        }

        /// <summary>
        /// </summary>
        public bool Read(ProcessorPin pin)
        {
            lock (serialPort)
            {
                Flush();
                var cmd = string.Format("$KE,RD,{0}", (int)pin);
                serialPort.WriteLine(cmd);
                var resp = serialPort.ReadLine();
                var split = resp.Split(','); //#RD,<LineNumber>,<Value> 
                if (split.Count() == 3 && split[0] == "#RD" && Convert.ToInt32(split[1]) == (int)pin)
                {
                    return split[2] == "1";
                }
                throw new System.InvalidOperationException(string.Format("Invalid respose: {0} (Request: {1})", resp, cmd));
            }
        }

        /// <summary>
        /// </summary>
        public void Release(ProcessorPin pin)
        {
        }

        /// <summary>
        /// </summary>
        public void SetPinDetectedEdges(ProcessorPin pin, PinDetectedEdges edges)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        public void SetPinResistor(ProcessorPin pin, PinResistor resistor)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        public void Wait(ProcessorPin pin, bool waitForUp = true, TimeSpan timeout = default(TimeSpan))
        {
            var startWait = DateTime.UtcNow;
            if (timeout == TimeSpan.Zero)
                timeout = DefaultTimeout;

            while (Read(pin) != waitForUp)
            {
                if (DateTime.UtcNow - startWait >= timeout)
                    throw new TimeoutException("A timeout occurred while waiting for pin status to change");
            }
        }

        /// <summary>
        /// </summary>
        public void Write(ProcessorPin pin, bool value)
        {
            lock (serialPort)
            {
                Flush();
                // $KE,WR,<LineNumber>,<Value>
                var cmd = string.Format("$KE,WR,{0},{1}", (int)pin, value ? '1' : '0');
                serialPort.WriteLine(cmd);
                var resp = serialPort.ReadLine();
                if (resp != "#WR,OK")
                {
                    throw new System.InvalidOperationException(string.Format("Invalid respose: {}", resp));
                }
            }
        }
    }
}
