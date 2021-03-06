﻿using System;
using System.Windows.Media.Imaging;
using DataExplorer.Domain.FilterTrees;
using DataExplorer.Domain.FilterTrees.BooleanFilterTrees;
using DataExplorer.Domain.FilterTrees.DateTimeFilterTrees;
using DataExplorer.Domain.FilterTrees.FloatFilterTrees;
using DataExplorer.Domain.FilterTrees.ImageFilterTrees;
using DataExplorer.Domain.FilterTrees.IntegerFilterTrees;
using DataExplorer.Domain.FilterTrees.StringFilterTrees;
using DataExplorer.Domain.Tests.Columns;
using NUnit.Framework;

namespace DataExplorer.Domain.Tests.FilterTrees
{
    [TestFixture]
    public class FilterTreeFactoryTests
    {
        private FilterTreeFactory _factory;
        
        [SetUp]
        public void SetUp()
        {
            _factory = new FilterTreeFactory();
        }

        // TODO: Create test to handle column with all null values 
        // TODO: because it breaks min / max logic

        [Test]
        public void TestCreateRootShouldCreateBooleanFilterTreeRoot()
        {
            var column = new ColumnBuilder().WithDataType(typeof(Boolean)).Build();
            var result = _factory.CreateRoot(column);
            Assert.That(result is BooleanFilterTreeRoot);
        }

        [Test]
        public void TestCreateRootShouldCreateDateTimeFilterTreeRoot()
        {
            var column = new ColumnBuilder()
                .WithDataType(typeof(DateTime))
                .WithValue(DateTime.MinValue)
                .Build();
            var result = _factory.CreateRoot(column);
            Assert.That(result is DateTimeFilterTreeRoot);
        }

        [Test]
        public void TestCreateRootShouldCreateFloatFilterTreeRoot()
        {
            var column = new ColumnBuilder()
                .WithDataType(typeof(Double))
                .WithValue(double.MinValue)
                .Build();
            var result = _factory.CreateRoot(column);
            Assert.That(result is FloatFilterTreeRoot);
        }

        [Test]
        public void TestCreateRootShouldCreateIntegerFilterTreeRoot()
        {
            var column = new ColumnBuilder()
                .WithDataType(typeof(Int32))
                .WithValue(0)
                .Build();
            var result = _factory.CreateRoot(column);
            Assert.That(result is IntegerFilterTreeRoot);
        }

        [Test]
        public void TestCreateRootShouldCreateStringFilterTreeRoot()
        {
            var column = new ColumnBuilder().WithDataType(typeof(String)).Build();
            var result = _factory.CreateRoot(column);
            Assert.That(result is StringFilterTreeRoot);
        }

        [Test]
        public void TestCreateRootShouldCreateImageFilterTreeRoot()
        {
            var column = new ColumnBuilder()
                .WithDataType(typeof(BitmapImage))
                .Build();
            var result = _factory.CreateRoot(column);
            Assert.That(result is ImageFilterTreeRoot);
        }

        [Test]
        public void TestCreateShouldThrowExceptionIfTypeIsNotSupported()
        {
            var column = new ColumnBuilder().WithDataType(typeof(Object)).Build();
            Assert.That(() => _factory.CreateRoot(column), Throws.ArgumentException);
        }
    }
}
