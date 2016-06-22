#region References

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Raspberry.IO.GeneralPurpose
{
    /// <summary>
    /// Provides extension methods for <see cref="ProcessorPin"/> and <see cref="ProcessorPins"/> objects.
    /// </summary>
    public static class ProcessorPinExtensionMethods
    {
        #region Methods

        /// <summary>
        /// Enumerates the specified pins.
        /// </summary>
        /// <param name="pins">The pins.</param>
        /// <returns>The pins.</returns>
        public static IEnumerable<int> Enumerate(this ProcessorPins pins)
        {
            return Enumerable.Range(0, pins.Count)
                .Where(p => (pins.Get(p)))
                .ToArray();
        }

        /// <summary>
        /// Logical AND opertion on pins with int
        /// </summary>
        public static ProcessorPins And(ProcessorPins pins, uint value)
        {
            var result = new ProcessorPins(32);
            foreach (var p in pins.Enumerate())
            {
                var val = ((uint)(1 << (int)p) & value);
                result.Set(p, val > 0 ? true : false);
            }
            return result;
        }

        #endregion
    }
}