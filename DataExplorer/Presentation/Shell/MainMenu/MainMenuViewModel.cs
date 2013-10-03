﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataExplorer.Presentation.Shell.MainMenu.FileMenu;
using DataExplorer.Presentation.Shell.MainMenu.ViewMenu;

namespace DataExplorer.Presentation.Shell.MainMenu
{
    public class MainMenuViewModel : IMainMenuViewModel
    {
        private readonly IFileMenuViewModel _fileMenuViewModel;
        private readonly IViewMenuViewModel _viewMenuViewModel;

        public MainMenuViewModel(
            IFileMenuViewModel fileMenuViewModel, 
            IViewMenuViewModel viewMenuViewModel)
        {
            _fileMenuViewModel = fileMenuViewModel;
            _viewMenuViewModel = viewMenuViewModel;
        }

        public IFileMenuViewModel FileMenuViewModel
        {
            get { return _fileMenuViewModel; }
        }

        public IViewMenuViewModel ViewMenuViewModel
        {
            get { return _viewMenuViewModel; }
        }
    }
}
