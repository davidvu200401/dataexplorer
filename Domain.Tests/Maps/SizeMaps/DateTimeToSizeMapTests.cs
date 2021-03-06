﻿using System;
using DataExplorer.Domain.Layouts;
using DataExplorer.Domain.Maps.SizeMaps;
using NUnit.Framework;

namespace DataExplorer.Domain.Tests.Maps.SizeMaps
{
    [TestFixture]
    public class DateTimeToSizeMapTests
    {
        [Test]
        public void TestMapWithNullValue()
        {
            var map = new DateTimeToSizeMap(DateTime.MinValue, DateTime.MaxValue, 0d, 100d, SortOrder.Ascending);
            var result = map.Map(null);
            Assert.That(result, Is.Null);
        }

        [Test]
        [TestCase("1/1/0001", 0.00d)]
        [TestCase("10/1/2500", 0.25d)]
        [TestCase("7/2/5000", 0.50d)]
        [TestCase("4/2/7500", 0.75d)]
        [TestCase("12/31/9999", 1.00d)]
        public void TestMapWithDateValues(string date, double expected)
        {
            var map = new DateTimeToSizeMap(DateTime.MinValue, DateTime.MaxValue, 0d, 1d, SortOrder.Ascending);
            var value = DateTime.Parse(date);
            var result = map.Map(value);
            Assert.That(result, Is.EqualTo(expected).Within(0.0001));
        }

        [Test]
        [TestCase("1/1/0001 00:00:00", 0.00d)]
        [TestCase("1/1/0001 06:00:00", 0.25d)]
        [TestCase("1/1/0001 12:00:00", 0.50d)]
        [TestCase("1/1/0001 18:00:00", 0.75d)]
        [TestCase("1/2/0001 00:00:00", 1.00d)]
        public void TestMapWithTimeValues(string time, double expected)
        {
            var min = DateTime.Parse("1/1/0001 00:00:00");
            var max = DateTime.Parse("1/2/0001 00:00:00");
            var map = new DateTimeToSizeMap(min, max, 0d, 1d, SortOrder.Ascending);
            var value = DateTime.Parse(time);
            var result = map.Map(value);
            Assert.That(result, Is.EqualTo(expected).Within(0.0001));
        }

        [Test]
        [TestCase("1/1/0001 00:00:00.000", 0.00d)]
        [TestCase("1/1/0001 00:00:00.250", 0.25d)]
        [TestCase("1/1/0001 00:00:00.500", 0.50d)]
        [TestCase("1/1/0001 00:00:00.750", 0.75d)]
        [TestCase("1/1/0001 00:00:01.000", 1.00d)]
        public void TestMapWithMillisecondValues(string time, double expected)
        {
            var min = DateTime.Parse("1/1/0001 00:00:00.000");
            var max = DateTime.Parse("1/1/0001 00:00:01.000");
            var map = new DateTimeToSizeMap(min, max, 0d, 1d, SortOrder.Ascending);
            var value = DateTime.Parse(time);
            var result = map.Map(value);
            Assert.That(result, Is.EqualTo(expected).Within(0.0001));
        }

        [Test]
        [TestCase("1/1/0001", 1.00d)]
        [TestCase("10/1/2500", 0.75d)]
        [TestCase("7/2/5000", 0.50d)]
        [TestCase("4/2/7500", 0.25d)]
        [TestCase("12/31/9999", 0.00d)]
        public void TestMapWithDescendingValues(string date, double expected)
        {
            var map = new DateTimeToSizeMap(DateTime.MinValue, DateTime.MaxValue, 0d, 1d, SortOrder.Descending);
            var value = DateTime.Parse(date);
            var result = map.Map(value);
            Assert.That(result, Is.EqualTo(expected).Within(0.0001));
        }

        [Test]
        public void TestMapInverseWithNullValue()
        {
            var map = new DateTimeToSizeMap(DateTime.MinValue, DateTime.MaxValue, 0d, 1d, SortOrder.Ascending);
            var result = map.MapInverse(null);
            Assert.That(result, Is.Null);
        }

        [Test]
        [TestCase(-0.1d, "0001-01-01 00:00:00.000")]
        [TestCase(1.1d, "9999-12-31 23:59:59.999")]
        public void TestMapInverseWithOutOfRangeValues(double value, string expected)
        {
            var map = new DateTimeToSizeMap(DateTime.MinValue, DateTime.MaxValue, 0d, 1d, SortOrder.Ascending);
            var expectedDate = DateTime.Parse(expected);
            var result = map.MapInverse(value);
            Assert.That(result, Is.EqualTo(expectedDate).Within(TimeSpan.FromMilliseconds(1)));
        }

        [Test]
        [TestCase(0.00d, "1/1/0001")]
        [TestCase(0.25d, "10/1/2500")]
        [TestCase(0.50d, "7/2/5000")]
        [TestCase(0.75d, "4/2/7500")]
        [TestCase(1.00d, "12/31/9999")]
        public void TestMapInverseWithDateValues(double value, string expected)
        {
            var map = new DateTimeToSizeMap(DateTime.MinValue, DateTime.MaxValue, 0d, 1d, SortOrder.Ascending);
            var expectedDate = DateTime.Parse(expected);
            var result = map.MapInverse(value);
            Assert.That(result, Is.EqualTo(expectedDate).Within(TimeSpan.FromDays(1)));
        }

        [Test]
        [TestCase(0.00d, "1/1/0001 00:00:00")]
        [TestCase(0.25d, "1/1/0001 06:00:00")]
        [TestCase(0.50d, "1/1/0001 12:00:00")]
        [TestCase(0.75d, "1/1/0001 18:00:00")]
        [TestCase(1.00d, "1/2/0001 00:00:00")]
        public void TestMapInverseWithTimeValues(double value, string expected)
        {
            var min = DateTime.Parse("1/1/0001 00:00:00");
            var max = DateTime.Parse("1/2/0001 00:00:00");
            var map = new DateTimeToSizeMap(min, max, 0d, 1d, SortOrder.Ascending);
            var expectedDate = DateTime.Parse(expected);
            var result = map.MapInverse(value);
            Assert.That(result, Is.EqualTo(expectedDate));
        }
        [Test]
        [TestCase(0.00d, "1/1/0001 00:00:00.000")]
        [TestCase(0.25d, "1/1/0001 00:00:00.250")]
        [TestCase(0.50d, "1/1/0001 00:00:00.500")]
        [TestCase(0.75d, "1/1/0001 00:00:00.750")]
        [TestCase(1.00d, "1/1/0001 00:00:01.000")]
        public void TestMapInverseWithMillisecondValues(double value, string expected)
        {
            var min = DateTime.Parse("1/1/0001 00:00:00.000");
            var max = DateTime.Parse("1/1/0001 00:00:01.000");
            var map = new DateTimeToSizeMap(min, max, 0d, 1d, SortOrder.Ascending);
            var expectedDate = DateTime.Parse(expected);
            var result = map.MapInverse(value);
            Assert.That(result, Is.EqualTo(expectedDate));
        }

        [Test]
        public void TestMapInverseReturnsNullWhenResultIsNotANumber()
        {
            var map = new DateTimeToSizeMap(DateTime.MinValue, DateTime.MaxValue, 0d, 0d, SortOrder.Ascending);
            var result = map.MapInverse(0d);
            Assert.That(result, Is.Null);
        }

        [Test]
        [TestCase(0.00d, "12/31/9999")]
        [TestCase(0.25d, "4/2/7500")]
        [TestCase(0.50d, "7/2/5000")]
        [TestCase(0.75d, "10/1/2500")]
        [TestCase(1.00d, "1/1/0001")]
        public void TestMapInverseWithDescendingValues(double value, string expected)
        {
            var map = new DateTimeToSizeMap(DateTime.MinValue, DateTime.MaxValue, 0d, 1d, SortOrder.Descending);
            var expectedDate = DateTime.Parse(expected);
            var result = map.MapInverse(value);
            Assert.That(result, Is.EqualTo(expectedDate).Within(TimeSpan.FromDays(1)));
        }
    }
}
