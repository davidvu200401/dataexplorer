﻿using System;
using DataExplorer.Application.Layouts.Color.Queries;
using DataExplorer.Application.Views;
using DataExplorer.Domain.Colors;
using DataExplorer.Domain.Tests.Colors;
using DataExplorer.Domain.Tests.Views.ScatterPlots;
using DataExplorer.Domain.Views.ScatterPlots;
using Moq;
using NUnit.Framework;

namespace DataExplorer.Application.Tests.Layouts.Color.Queries
{
    [TestFixture]
    public class GetColorPaletteQueryHandlerTests
    {
        private GetColorPaletteQueryHandler _handler;
        private Mock<IViewRepository> _mockRepository;
        private ScatterPlot _scatterPlot;
        private ScatterPlotLayout _layout;
        private ColorPalette _colorPalette;

        [SetUp]
        public void SetUp()
        {
            _colorPalette = new ColorPaletteBuilder()
                .Build();
            _layout = new ScatterPlotLayoutBuilder()
                .WithColorPalette(_colorPalette)
                .Build();
            _scatterPlot = new ScatterPlotBuilder()
                .WithLayout(_layout)
                .Build();

            _mockRepository = new Mock<IViewRepository>();
            _mockRepository.Setup(p => p.Get<ScatterPlot>())
                .Returns(_scatterPlot);
            
            _handler = new GetColorPaletteQueryHandler(
                _mockRepository.Object);
        }

        [Test]
        public void TestExecuteShouldReturnColorPalette()
        {
            var result = _handler.Execute(new GetColorPaletteQuery());
            Assert.That(result, Is.EqualTo(_colorPalette));
        }
    }
}
