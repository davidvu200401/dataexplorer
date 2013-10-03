﻿using System.Windows;
using DataExplorer.Domain.Events;
using DataExplorer.Domain.ScatterPlots;
using DataExplorer.Domain.Views;

namespace DataExplorer.Application.ScatterPlots.Commands
{
    public class ZoomToFullExtentCommand : IZoomToFullExtentCommand
    {
        private const double DefaultZoom = 1.2d;
        private const double DefaultWorldX = 0d;
        private const double DefaultWorldY = 0d;
        private const double DefaultWorldWidth = 1d;
        private const double DefaultWorldHeight = 1d;
        private readonly IViewRepository _viewRepository;

        public ZoomToFullExtentCommand(IViewRepository viewRepository)
        {
            _viewRepository = viewRepository;
        }

        public void Execute()
        {
            var scatterPlot = _viewRepository.Get<ScatterPlot>();

            var viewExtent = scatterPlot.GetViewExtent();

            var aspectRatio = viewExtent.Width / viewExtent.Height;

            var width = aspectRatio > 1d
                ? DefaultZoom * aspectRatio 
                : DefaultZoom;

            var height = aspectRatio > 1d
                ? DefaultZoom
                : DefaultZoom * (1d / aspectRatio);

            var x = DefaultWorldX - (width - DefaultWorldWidth) / 2;
            var y = DefaultWorldY - (height - DefaultWorldHeight) / 2;

            var newViewExtent = new Rect(x, y, width, height);

            scatterPlot.SetViewExtent(newViewExtent);

            DomainEvents.Raise(new ScatterPlotChangedEvent());
        }
    }
}
