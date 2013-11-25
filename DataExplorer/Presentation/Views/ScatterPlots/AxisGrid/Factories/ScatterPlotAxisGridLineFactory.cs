﻿using System;
using System.Collections.Generic;
using System.Linq;
using DataExplorer.Domain.Maps;
using DataExplorer.Domain.ScatterPlots;
using DataExplorer.Presentation.Views.ScatterPlots.AxisGrid.Factories.BooleanAxisGridLines;
using DataExplorer.Presentation.Views.ScatterPlots.AxisGrid.Factories.DateTimeAxisGridLines;
using DataExplorer.Presentation.Views.ScatterPlots.AxisGrid.Factories.FloatAxisGridLines;
using DataExplorer.Presentation.Views.ScatterPlots.AxisGrid.Factories.IntegerAxisGridLines;
using DataExplorer.Presentation.Views.ScatterPlots.AxisGrid.Factories.StringAxisGridLines;

namespace DataExplorer.Presentation.Views.ScatterPlots.AxisGrid.Factories
{
    public class ScatterPlotAxisGridLineFactory : IScatterPlotAxisGridLineFactory
    {
        private readonly IBooleanAxisGridLineFactory _booleanFactory;
        private readonly IDateTimeAxisGridLineFactory _dateTimeFactory;
        private readonly IFloatAxisGridLineFactory _floatFactory;
        private readonly IIntegerAxisGridLineFactory _integerFactory;
        private readonly IStringAxisGridLineFactory _stringFactory;

        public ScatterPlotAxisGridLineFactory(
            IBooleanAxisGridLineFactory booleanFactory,
            IDateTimeAxisGridLineFactory dateTimeFactory,
            IFloatAxisGridLineFactory floatFactory,
            IIntegerAxisGridLineFactory integerFactory,
            IStringAxisGridLineFactory stringFactory)
        {
            _booleanFactory = booleanFactory;
            _dateTimeFactory = dateTimeFactory;
            _floatFactory = floatFactory;
            _integerFactory = integerFactory;
            _stringFactory = stringFactory;
        }

        public IEnumerable<AxisGridLine> Create(Type type, IAxisMap map, IEnumerable<object> values, double lower, double upper)
        {
            if (type == typeof(Boolean))
                return _booleanFactory.Create(map, lower, upper);

            if (type == typeof(DateTime))
                return _dateTimeFactory.Create(map, lower, upper);

            if (type == typeof(Double))
                return _floatFactory.Create(map, lower, upper);

            if (type == typeof(Int32))
                return _integerFactory.Create(map, lower, upper);

            if (type == typeof(String))
                return _stringFactory.Create(map, values.ToList(), lower, upper);

            return new List<AxisGridLine>();
        }
    }
}