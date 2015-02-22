namespace Treant.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Windows;
    using Treant.Core;
    using Treant.Core.Extenders;
    using Treant.Domain;
    using Treant.Services;
    using Treant.ViewModels.Controls;

    [Export]
    public class BoardViewModel : TabItemViewModel
    {
        private BoardService boardService;

        [ImportingConstructor]
        public BoardViewModel(BoardService boardService)
        {
            this.boardService = boardService;

            ToolBarViewModel = new ElementCreatorToolBarViewModel
            {
                AddCommand = new RelayCommand(AddCommandExecute),
                EditCommand = new RelayCommand(EditCommandExecute, EditCommandCanExecute),
                RemoveCommand = new RelayCommand(RemoveCommandExecute, RemoveCommandCanExecute),
            };
        }

        private Board currentBoard;
        public Board CurrentBoard
        {
            get
            {
                return currentBoard;
            }
            set
            {
                this.currentBoard = value;

                TaskItems = new ObservableCollection<TaskItem>(this.boardService.GetTaskItems(value));
            }
        }

        public ObservableCollection<TaskItem> TaskItems { get; private set; }

        public TaskItem SelectedTaskItem { get; set; }

        public ElementCreatorToolBarViewModel ToolBarViewModel { get; set; }

        private bool RemoveCommandCanExecute(object arg)
        {
            return SelectedTaskItem != null;
        }

        private void RemoveCommandExecute(object obj)
        {
            if (base.ConfirmDeletion() == MessageBoxResult.Yes
                && boardService.Remove(SelectedTaskItem))
            {
                TaskItems.Remove(SelectedTaskItem);
            }
        }

        private bool EditCommandCanExecute(object arg)
        {
            return SelectedTaskItem != null;
        }

        private void EditCommandExecute(object obj)
        {
            ShowEditWindow(SelectedTaskItem);
        }

        private void AddCommandExecute(object obj)
        {
            ShowEditWindow(new TaskItem { OwnerBoardID = CurrentBoard.ID });
        }

        private void ShowEditWindow(TaskItem taskItem)
        {
            bool isNew = taskItem.ID == default(int);

            var editBoardItemsView = ControlFactory.CreateWindow<EditBoardItemViewModel>();

            editBoardItemsView.WithDataContext<EditBoardItemViewModel>(o => o.CurrentTaskItem = taskItem);

            if (editBoardItemsView.ShowDialog() == true && isNew)
            {
                TaskItems.AddSorted(taskItem, o => o.Name);
            }
            else
            {
                ReAttachBoard(editBoardItemsView.GetDataContext<EditBoardItemViewModel>().CurrentTaskItem);
            }
        }

        private void ReAttachBoard(TaskItem taskItem)
        {
            TaskItems.Remove(SelectedTaskItem);
            TaskItems.AddSorted(taskItem, o => o.Name);

            SelectedTaskItem = taskItem;
        }
    }
}
