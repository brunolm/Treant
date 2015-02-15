namespace Treant
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel.Composition;
    using System.Threading;
    using System.Windows;
    using System.Windows.Data;
    using Treant.Core;
    using Treant.Core.Extenders;
    using Treant.Domain;
    using Treant.Services;
    using Treant.ViewModels;

    [Export]
    public class MainViewModel : WindowViewModel
    {
        [Import]
        private BoardService boardService;

        [ImportingConstructor]
        public MainViewModel(BoardService boardService)
        {
            this.boardService = boardService;

            WindowTitle = String.Format("Treant - Logged in as {0}", Thread.CurrentPrincipal.Identity.Name);

            BoardOpenCommand = new RelayCommand((o) =>
            {
                var board = o as Board;
                var boardView = ControlFactory.CreateWindow<BoardViewModel>();
                boardView.WithDataContext<BoardViewModel>(ctx => ctx.CurrentBoard = board);
                boardView.Show();
            });

            AddCommand = new RelayCommand(AddCommandExecute);
            EditCommand = new RelayCommand(EditCommandExecute, EditCommandCanExecute);
            RemoveCommand = new RelayCommand(RemoveCommandExecute, RemoveCommandCanExecute);
            RefreshCommand = new RelayCommand(RefreshCommandExecute);

            // TODO: Remove
            boardService.CreateDummies();

            Boards = new ObservableCollection<Board>(boardService.GetUserBoards());
        }

        public ObservableCollection<Board> Boards { get; set; }

        public Board SelectedBoard { get; set; }

        public RelayCommand BoardOpenCommand { get; set; }

        public RelayCommand AddCommand { get; set; }

        public RelayCommand EditCommand { get; set; }

        public RelayCommand RemoveCommand { get; set; }

        public RelayCommand RefreshCommand { get; set; }

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
