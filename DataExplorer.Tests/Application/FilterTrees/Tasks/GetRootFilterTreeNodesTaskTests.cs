﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataExplorer.Application.FilterTrees;
using DataExplorer.Application.FilterTrees.Tasks;
using DataExplorer.Domain.Columns;
using DataExplorer.Domain.FilterTrees;
using DataExplorer.Tests.Domain.Columns;
using Moq;
using NUnit.Framework;

namespace DataExplorer.Tests.Application.FilterTrees.Tasks
{
    [TestFixture]
    public class GetRootFilterTreeNodesTaskTests
    {
        private GetRootFilterTreeNodesTask _task;
        private Mock<IColumnRepository> _mockRepository;
        private Mock<IFilterTreeNodeFactory> _mockFactory;
        private Column _column;
        private List<Column> _columns;
        private FakeFilterTreeNode _value;

        [SetUp]
        public void SetUp()
        {
            _column = new ColumnBuilder().Build();
            _columns = new List<Column> { _column };
            _value = new FakeFilterTreeNode();
            _mockRepository = new Mock<IColumnRepository>();
            _mockRepository.Setup(p => p.GetAll()).Returns(_columns);
            _mockFactory = new Mock<IFilterTreeNodeFactory>();
            _mockFactory.Setup(p => p.CreateRoot(_column)).Returns(_value);
            _task = new GetRootFilterTreeNodesTask(
                _mockRepository.Object,
                _mockFactory.Object);
        }

        [Test]
        public void TestGetRootFilterTreeNodesShouldReturnNodes()
        {
            var result = _task.GetRoots();
            Assert.That(result, Contains.Item(_value));
        }
    }
}
