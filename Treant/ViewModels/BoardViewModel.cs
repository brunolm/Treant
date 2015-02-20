namespace Treant.ViewModels
{
    using System;
    using Treant.Core;
    using Treant.Domain;
    using Treant.ViewModels.Controls;

    public class BoardViewModel : TabItemViewModel
    {
        public BoardViewModel()
        {
            ToolBarViewModel = new ElementCreatorToolBarViewModel
            {
                AddCommand = new RelayCommand(AddCommandExecute),
                EditCommand = new RelayCommand(EditCommandExecute, EditCommandCanExecute),
                RemoveCommand = new RelayCommand(RemoveCommandExecute, RemoveCommandCanExecute),
            };
        }

        public Board CurrentBoard { get; set; }

        public ElementCreatorToolBarViewModel ToolBarViewModel { get; set; }

        private bool RemoveCommandCanExecute(object arg)
        {
            return true; // TODO: impl
        }

        private void RemoveCommandExecute(object obj)
        {
            throw new NotImplementedException();
        }

        private bool EditCommandCanExecute(object arg)
        {
            return true; // TODO: impl
        }

        private void EditCommandExecute(object obj)
        {
            throw new NotImplementedException();
        }

        private void AddCommandExecute(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
