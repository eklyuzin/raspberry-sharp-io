using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Raspberry.IO.GeneralPurpose
{
    /// <summary>
    /// Represents a set of pins on the Raspberry Pi Processor
    /// </summary>
    public class ProcessorPins : IEnumerable
    {
        private BitArray _pins;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessorPins"/> class.
        /// </summary>
        public ProcessorPins()
        {
            _pins = new BitArray(GpioConnectionSettings.DefaultPinCount, false);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessorPins"/> class.
        /// </summary>
        public ProcessorPins(int len)
        {
            _pins = new BitArray(len, false);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessorPins"/> class.
        /// </summary>
        public ProcessorPins(BitArray pins)
        {
            _pins = pins;
        }

        public virtual string ToString()
        {
            string str = "";
            foreach (var p in _pins)
            {
                str += string.Format("{0}", (p.Equals(true) ? 1 : 0));
            }
            return str;
        }

        /// <summary>
        /// Pin count
        /// </summary>
        public int Count
        {
            get
            {
                return _pins.Count;
            }
        }

        /// <summary>
        /// No pins selected.
        /// </summary>
        public const uint None = 0;

        /// <summary>
        /// Pin 0 selected.
        /// </summary>
        public const uint Pin0 = 1 << 0;

        /// <summary>
        /// Pin 0 selected.
        /// </summary>
        public const uint Pin00 = Pin0;

        /// <summary>
        /// Pin 1 selected.
        /// </summary>
        public const uint Pin1 = 1 << 1;

        /// <summary>
        /// Pin 1 selected.
        /// </summary>
        public const uint Pin01 = Pin1;

        /// <summary>
        /// Pin 2 selected.
        /// </summary>
        public const uint Pin2 = 1 << 2;

        /// <summary>
        /// Pin 2 selected.
        /// </summary>
        public const uint Pin02 = Pin2;

        /// <summary>
        /// Pin 3 selected.
        /// </summary>
        public const uint Pin3 = 1 << 3;

        /// <summary>
        /// Pin 3 selected.
        /// </summary>
        public const uint Pin03 = Pin3;

        /// <summary>
        /// Pin 4 selected.
        /// </summary>
        public const uint Pin4 = 1 << 4;

        /// <summary>
        /// Pin 4 selected.
        /// </summary>
        public const uint Pin04 = Pin4;

        /// <summary>
        /// Pin 7 selected.
        /// </summary>
        public const uint Pin7 = 1 << 7;

        /// <summary>
        /// Pin 7 selected.
        /// </summary>
        public const uint Pin07 = Pin7;

        /// <summary>
        /// Pin 8 selected.
        /// </summary>
        public const uint Pin8 = 1 << 8;

        /// <summary>
        /// Pin 8 selected.
        /// </summary>
        public const uint Pin08 = Pin8;

        /// <summary>
        /// Pin 9 selected.
        /// </summary>
        public const uint Pin9 = 1 << 9;

        /// <summary>
        /// Pin 9 selected.
        /// </summary>
        public const uint Pin09 = Pin9;

        /// <summary>
        /// Pin 10 selected.
        /// </summary>
        public const uint Pin10 = 1 << 10;

        /// <summary>
        /// Pin 11 selected.
        /// </summary>
        public const uint Pin11 = 1 << 11;

        /// <summary>
        /// Pin 14 selected.
        /// </summary>
        public const uint Pin14 = 1 << 14;

        /// <summary>
        /// Pin 15 selected.
        /// </summary>
        public const uint Pin15 = 1 << 15;

        /// <summary>
        /// Pin 17 selected.
        /// </summary>
        public const uint Pin17 = 1 << 17;

        /// <summary>
        /// Pin 18 selected.
        /// </summary>
        public const uint Pin18 = 1 << 18;

        /// <summary>
        /// Pin 21 selected.
        /// </summary>
        public const uint Pin21 = 1 << 21;

        /// <summary>
        /// Pin 22 selected.
        /// </summary>
        public const uint Pin22 = 1 << 22;

        /// <summary>
        /// Pin 23 selected.
        /// </summary>
        public const uint Pin23 = 1 << 23;

        /// <summary>
        /// Pin 24 selected.
        /// </summary>
        public const uint Pin24 = 1 << 24;

        /// <summary>
        /// Pin 25 selected.
        /// </summary>
        public const uint Pin25 = 1 << 25;

        /// <summary>
        /// Pin 27 selected.
        /// </summary>
        public const uint Pin27 = 1 << 27;

        /// <summary>
        /// Pin 28 selected.
        /// </summary>
        public const uint Pin28 = 1 << 28;

        /// <summary>
        /// Pin 29 selected.
        /// </summary>
        public const uint Pin29 = 1 << 29;

        /// <summary>
        /// Pin 30 selected.
        /// </summary>
        public const uint Pin30 = 1 << 30;

        /// <summary>
        /// Pin 31 selected.
        /// </summary>
        public const uint Pin31 = (uint)1 << 31;

        /// <summary>
        /// GetEnumerator
        /// </summary>
        public IEnumerator GetEnumerator()
        {
            return _pins.GetEnumerator();
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            _pins.SetAll(false);
        }

        /// <summary>
        /// Set
        /// </summary>
        public void Set(int pin, bool v)
        {
            _pins.Set(pin, v);
        }

        /// <summary>
        /// Get
        /// </summary>
        public bool Get(int pin)
        {
            return _pins.Get(pin);
        }

        /// <summary>
        /// Diff
        /// </summary>
        public ProcessorPins Diff(ProcessorPins pinRawValues)
        {
            return new ProcessorPins(_pins.Xor(pinRawValues._pins));
        }
    }
}