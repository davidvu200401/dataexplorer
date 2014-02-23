﻿using System;
using DataExplorer.Application.Columns;
using DataExplorer.Application.Core.Commands;
using DataExplorer.Application.Core.Events;
using DataExplorer.Application.Layouts.General.Events;
using DataExplorer.Application.Views;
using DataExplorer.Domain.Views.ScatterPlots;

namespace DataExplorer.Application.Layouts.Color.Commands
{
    public class SetColorColumnCommandHandler 
        : ICommandHandler<SetColorColumnCommand>
    {
        private readonly IColumnRepository _columnRepository;
        private readonly IViewRepository _viewRepository;
        private readonly IEventBus _eventBus;

        public SetColorColumnCommandHandler(
            IColumnRepository columnRepository, 
            IViewRepository viewRepository, 
            IEventBus eventBus)
        {
            _columnRepository = columnRepository;
            _viewRepository = viewRepository;
            _eventBus = eventBus;
        }

        public void Execute(SetColorColumnCommand command)
        {
            var column = _columnRepository.Get(command.Id);

            var scatterPlot = _viewRepository.Get<ScatterPlot>();

            var layout = scatterPlot.GetLayout();

            layout.ColorColumn = column;

            _eventBus.Raise(new LayoutChangedEvent());
        }
    }
}
