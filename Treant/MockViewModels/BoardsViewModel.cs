using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Treant.Domain;

namespace Treant.MockViewModels
{
    public class BoardsViewModel
    {
        public BoardsViewModel()
        {
            Boards = new ObservableCollection<Board>(new List<Board>
            {
                new Board { Name = "C#" },
                new Board { Name = "WPF" },
                new Board { Name = "People Skills" },
                new Board { Name = "English" },
            });
        }

        public ObservableCollection<Board> Boards { get; set; }
    }
}
