﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataExplorer.Application.Core.Commands;
using DataExplorer.Application.Core.Events;
using DataExplorer.Application.Projects.Events;

namespace DataExplorer.Application.Projects.Commands
{
    public class SaveProjectCommandHandler 
        : ICommandHandler<SaveProjectCommand>
    {
        private readonly IDialogService _dialogService;
        private readonly IDataContext _dataContext;
        private readonly IProjectSerializer _projectSerializer;
        private readonly IXmlFileService _xmlFileService;
        private readonly IEventBus _eventBus;

        public SaveProjectCommandHandler(
            IDialogService dialogService,
            IDataContext dataContext, 
            IProjectSerializer projectSerializer, 
            IXmlFileService xmlFileService,
            IEventBus eventBus)
        {
            _dialogService = dialogService;
            _dataContext = dataContext;
            _projectSerializer = projectSerializer;
            _xmlFileService = xmlFileService;
            _eventBus = eventBus;
        }

        public void Execute(SaveProjectCommand command)
        {
            var filePath = _dialogService.ShowSaveDialog();

            if (filePath == null)
                return;

            var project = _dataContext.GetProject();

            var xProject = _projectSerializer.Serialize(project);

            _xmlFileService.Save(xProject, filePath);

            _eventBus.Raise(new ProjectSavedEvent());
        }
    }
}
