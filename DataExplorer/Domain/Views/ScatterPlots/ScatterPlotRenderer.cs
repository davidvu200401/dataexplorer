﻿using System.Collections.Generic;
using DataExplorer.Domain.Maps;
using DataExplorer.Domain.Rows;

namespace DataExplorer.Domain.Views.ScatterPlots
{
    public class ScatterPlotRenderer : IScatterPlotRenderer
    {
        private readonly IMapFactory _factory;

        public ScatterPlotRenderer(IMapFactory factory)
        {
            _factory = factory;
        }

        public List<Plot> RenderPlots(List<Row> rows, ScatterPlotLayout layout)
        {
            var xAxisMap = layout.XAxisColumn != null 
                ? _factory.CreateAxisMap(layout.XAxisColumn, 0d, 1d) 
                : null;

            var yAxisMap = layout.YAxisColumn != null
                ? _factory.CreateAxisMap(layout.YAxisColumn, 0d, 1d)
                : null;
            
            var plots = new List<Plot>();
            
            foreach (var row in rows)
            {
                var plot = new Plot();

                plot.Id = row.Id;

                plot.X = layout.XAxisColumn != null
                    ? xAxisMap.Map(row[layout.XAxisColumn.Index]) ?? 0.0
                    : 0.0;

                plot.Y = layout.YAxisColumn != null
                    ? yAxisMap.Map(row[layout.YAxisColumn.Index]) ?? 0.0
                    : 0.0;

                plots.Add(plot);
            }
            
            return plots;
        }
    }
}