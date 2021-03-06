﻿using System.Collections.Generic;
using System.Linq;
using DataExplorer.Presentation.Core.Canvas;
using DataExplorer.Presentation.Core.Canvas.Items;
using NUnit.Framework;

namespace DataExplorer.Presentation.Tests.Core.Canvas
{
    [TestFixture]
    public class CanvasRendererTests
    {
        private CanvasRenderer _canvasRenderer;
        private List<CanvasItem> _plots;
        private CanvasCircle _plot;

        [SetUp]
        public void SetUp()
        {
            _plot = new CanvasCircle();
            _plots = new List<CanvasItem> { _plot };
            _canvasRenderer = new CanvasRenderer();
        }

        [Test]
        public void TestDrawBackgroundShouldDrawBackground()
        {
            var result = _canvasRenderer.DrawBackground(1d, 2d);
            Assert.That(result, Is.TypeOf<VisualItem>());
        }

        [Test]
        public void TestDrawPlotsShouldDrawPlots()
        {
            var results = _canvasRenderer.DrawItems(_plots);
            Assert.That(results.Single(), Is.TypeOf<VisualItem>());
        }
    }
}
