namespace Treant.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel.Composition;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using Treant.Core;
    using Treant.Core.Extenders;
    using Treant.Core.Messaging;
    using Treant.Domain;
    using Treant.Messages;
    using Treant.Services;
    using Treant.ViewModels;
    using Treant.ViewModels.Controls;

    [Export]
    public class BoardsViewModel : TabItemViewModel
    {
        private BoardService boardService;

        private EventAggregator eventAggregator;

        [ImportingConstructor]
        public BoardsViewModel(BoardService boardService, EventAggregator eventAggregator)
        {
            this.boardService = boardService;
            this.eventAggregator = eventAggregator;

            BoardOpenCommand = new RelayCommand(BoardOpenCommandExecute);

            ToolBarViewModel = new ElementCreatorToolBarViewModel
            {
                AddCommand = new RelayCommand(AddCommandExecute),
                EditCommand = new RelayCommand(EditCommandExecute, EditCommandCanExecute),
                RemoveCommand = new RelayCommand(RemoveCommandExecute, RemoveCommandCanExecute),
            };

            RefreshCommand = new RelayCommand(RefreshCommandExecute);

            // TODO: Remove
            boardService.CreateDummies();

            Boards = new ObservableCollection<Board>(boardService.GetUserBoards());
        }

        public ObservableCollection<Board> Boards { get; set; }

        public Board SelectedBoard { get; set; }

        public RelayCommand BoardOpenCommand { get; set; }

        public ElementCreatorToolBarViewModel ToolBarViewModel { get; set; }

        public RelayCommand RefreshCommand { get; set; }

        private void BoardOpenCommandExecute(object obj)
        {
            var board = obj as Board;
            var boardView = ControlFactory.CreateTab<BoardViewModel>();

            boardView.Header = board.Name;
            boardView.ToolTip = board.ID.ToString();
            (boardView.Content as Control).WithDataContext<BoardViewModel>(ctx =>
            {
                ctx.CurrentBoard = board;
            });

            eventAggregator.Publish(new BoardOpenMessage(boardView));
        }

        private void RefreshCommandExecute(object obj)
        {
            Boards = new ObservableCollection<Board>(boardService.GetUserBoards());
        }

        private bool EditCommandCanExecute(object arg)
        {
            return SelectedBoard != null;
        }

        private void EditCommandExecute(object obj)
        {
            ShowEditWindow(SelectedBoard);
        }

        private bool RemoveCommandCanExecute(object arg)
        {
            return SelectedBoard != null;
        }

        private void RemoveCommandExecute(object obj)
        {
            if (base.ConfirmDeletion() == MessageBoxResult.Yes
                && boardService.Remove(SelectedBoard))
            {
                Boards.Remove(SelectedBoard);
            }
        }

        private void AddCommandExecute(object obj)
        {
            ShowEditWindow(new Board());
        }

        private void ShowEditWindow(Board board)
        {
            bool isNew = board.ID == default(int);

            var editBoardView = ControlFactory.CreateWindow<EditBoardViewModel>();

            if (board != null)
                editBoardView.WithDataContext<EditBoardViewModel>(o => o.CurrentBoard = board);

            if (editBoardView.ShowDialog() == true && isNew)
            {
                Boards.AddSorted(board, o => o.Name);
            }
            else
            {
                ReAttachBoard(editBoardView.GetDataContext<EditBoardViewModel>().CurrentBoard);
            }
        }

        private void ReAttachBoard(Board board)
        {
            Boards.Remove(SelectedBoard);
            Boards.AddSorted(board, o => o.Name);

            SelectedBoard = board;
        }
    }
}
