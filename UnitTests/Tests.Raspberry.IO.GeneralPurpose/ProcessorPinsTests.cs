using NUnit.Framework;
using Raspberry.IO.GeneralPurpose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Raspberry.IO.GeneralPurpose.Tests
{
    [TestFixture()]
    public class ProcessorPinsTests
    {
        [Test()]
        public void constsTest()
        {
            var nonePin = ProcessorPins.None;
            var pin1 = ProcessorPins.Pin0;
            Assert.AreNotEqual(nonePin, pin1);
        }

        [Test()]
        public void compareEqualTest()
        {
            var pins1 = new ProcessorPins();
            var pins2 = new ProcessorPins();
            Assert.AreEqual(pins1, pins2);
            pins1.Set(0, true);
            pins2.Set(0, true);
            Assert.AreEqual(pins1, pins2);
        }

        [Test()]
        public void compareNETest()
        {
            var pins1 = new ProcessorPins();
            var pins2 = new ProcessorPins();
            pins2.Set(0, true);
            Assert.AreNotEqual(pins1, pins2);
            pins1.Set(1, true);
            Assert.AreNotEqual(pins1, pins2);
        }

        [Test()]
        public void enumerateTest()
        {
            var pins1 = new ProcessorPins();
            pins1.Set(0, true);
            var pins = pins1.Enumerate();
            Assert.AreEqual(1, pins.Count());
            Assert.AreEqual(0, pins.ToArray()[0]);
        }

        [Test()]
        public void enumerateNumTest()
        {
            var pins1 = new ProcessorPins();
            pins1.Set(2, true);
            pins1.Set(7, true);
            var pins = pins1.Enumerate();
            Assert.AreEqual(2, pins.Count());
            Assert.AreEqual(2, pins.ToArray()[0]);
            Assert.AreEqual(7, pins.ToArray()[1]);
        }

        [Test()]
        public void diffTest()
        {
            var pins1 = new ProcessorPins();
            pins1.Set(2, true);
            pins1.Set(7, true);

            var pins2 = new ProcessorPins();
            pins2.Set(2, true);

            var pins = pins2.Diff(pins1);
            Assert.AreEqual(pins1.Count, pins.Count);
            Assert.AreEqual(true, pins.Get(7));
            Assert.AreEqual(false, pins.Get(2));
        }
    }
}