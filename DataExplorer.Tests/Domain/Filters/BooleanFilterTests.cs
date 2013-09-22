﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataExplorer.Domain.Columns;
using DataExplorer.Domain.Filters;
using DataExplorer.Tests.Domain.Columns;
using NUnit.Framework;

namespace DataExplorer.Tests.Domain.Filters
{
    [TestFixture]
    public class BooleanFilterTests
    {
        private Column _column;
        private BooleanFilter _filter;

        [SetUp]
        public void SetUp()
        {
            _column = new ColumnBuilder().Build();
        }

        [Test]
        public void TestDefaultConstructorShouldAddAllValues()
        {
            _filter = new BooleanFilter(_column);
            //Assert.That(filter.Values.Contains(null));
            Assert.That(_filter.Values.Contains(false));
            Assert.That(_filter.Values.Contains(true));
        }

        //[Test]
        //public void TestValueShouldReturnFalseIfNullIsIncluded()
        //{
        //    var filter = new BooleanFilter(null);
        //    Assert.That(filter.Values.Contains(null));
        //}

        [Test]
        public void TestValueShouldReturnFalseIfFalseIsIncluded()
        {
            _filter = new BooleanFilter(_column, false);
            Assert.That(_filter.Values.Contains(false));
        }

        [Test]
        public void TestValueShouldReturnTrueIfTrueIsIncluded()
        {
            _filter = new BooleanFilter(_column, true);
            Assert.That(_filter.Values.Contains(true));
        }
    }
}
