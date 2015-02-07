using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Treant.Core;
using Treant.Core.Extenders;
using Treant.Domain;
using Treant.Services.Authentication;
using Treant.ViewModels;

namespace Treant
{
    [Export]
    public class MainViewModel : WindowViewModel
    {
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

        public RelayCommand BoardDoubleClickCommand { get; set; }

        public MainViewModel()
        {
            WindowTitle = String.Format("Treant - Logged in as {0}", Thread.CurrentPrincipal.Identity.Name);

            BoardDoubleClickCommand = new RelayCommand((o) =>
            {
                var board = o as Board;
                var boardView = ControlFactory.CreateWindow<BoardViewModel>();
                boardView.WithDataContext<BoardViewModel>(ctx => ctx.CurrentBoard = board);
                boardView.ShowDialog();
            });

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
    }
}
