namespace Treant
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.Composition;
    using System.Threading;
    using Treant.Core;
    using Treant.Core.Extenders;
    using Treant.Domain;
    using Treant.Services;
    using Treant.Services.Authentication;
    using Treant.ViewModels;

    [Export]
    public class MainViewModel : WindowViewModel
    {
        [Import]
        private BoardService boardService;

        private ObservableCollection<Board> boards;
        public ObservableCollection<Board> Boards
        {
            get { return boards; }
            set
            {
                boards = value;
                this.RaisePropertyChanged();
            }
        }

        private Board selectedBoard;
        public Board SelectedBoard
        {
            get { return selectedBoard; }
            set
            {
                selectedBoard = value;
                this.RaisePropertyChanged();
            }
        }

        public RelayCommand BoardDoubleClickCommand { get; set; }

        public RelayCommand AddCommand { get; set; }
        
        public RelayCommand RemoveCommand { get; set; }

        [ImportingConstructor]
        public MainViewModel(BoardService boardService)
        {
            this.boardService = boardService;

            WindowTitle = String.Format("Treant - Logged in as {0}", Thread.CurrentPrincipal.Identity.Name);

            BoardDoubleClickCommand = new RelayCommand((o) =>
            {
                var board = o as Board;
                var boardView = ControlFactory.CreateWindow<BoardViewModel>();
                boardView.WithDataContext<BoardViewModel>(ctx => ctx.CurrentBoard = board);
                boardView.ShowDialog();
            });

            AddCommand = new RelayCommand(AddCommandExecute);
            RemoveCommand = new RelayCommand(RemoveCommandExecute, RemoveCommandCanExecute);

            //Boards = new ObservableCollection<Board>(boardService.GetUserBoards());


            Boards = new ObservableCollection<Board>(new List<Board>
            {
                new Board { Name = "C#", Owner = (Thread.CurrentPrincipal.Identity as CustomIdentity).CurrentUser },
                new Board { Name = "WPF", Owner = (Thread.CurrentPrincipal.Identity as CustomIdentity).CurrentUser },
                new Board { Name = "People Skills", Owner = (Thread.CurrentPrincipal.Identity as CustomIdentity).CurrentUser },
                new Board { Name = "Some other board", Owner = (Thread.CurrentPrincipal.Identity as CustomIdentity).CurrentUser },
                new Board { Name = "I can haz many boards", Owner = (Thread.CurrentPrincipal.Identity as CustomIdentity).CurrentUser },
                new Board { Name = "Look I'm a potato", Owner = (Thread.CurrentPrincipal.Identity as CustomIdentity).CurrentUser },
            });
        }

        private bool RemoveCommandCanExecute(object arg)
        {
            return SelectedBoard != null;
        }

        private void RemoveCommandExecute(object obj)
        {
            throw new NotImplementedException();
        }

        private void AddCommandExecute(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
