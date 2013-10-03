﻿using System.Collections.Generic;
using DataExplorer.Domain.ScatterPlots;
using DataExplorer.Domain.Views;

namespace DataExplorer.Application.ScatterPlots.Queries
{
    public class GetPlotsQuery : IGetPlotsQuery
    {
        private readonly IViewRepository _repository;
        private readonly IScatterPlotAdapter _adapter;

        public GetPlotsQuery(
            IViewRepository repository, 
            IScatterPlotAdapter adapter)
        {
            _repository = repository;
            _adapter = adapter;
        }

        public List<PlotDto> GetPlots()
        {
            var scatterPlot = _repository.Get<ScatterPlot>();

            var plots = scatterPlot.GetPlots();

            var plotDtos = _adapter.Adapt(plots);

            return plotDtos;
        }
    }
}
