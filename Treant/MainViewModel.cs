using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Treant.Core;
using Treant.Core.Messaging;
using Treant.Messages;
using Treant.ViewModels;

namespace Treant
{
    [Export(typeof(IListen<BoardOpenMessage>))]
    public class MainViewModel : WindowViewModel, IListen<BoardOpenMessage>
    {
        public MainViewModel()
        {
            Tabs = new ObservableCollection<TabItem>(new List<TabItem>
            {
                ControlFactory.CreateTab<BoardsViewModel>(),
            });

            WindowTitle = "Boards - Treant";
        }

        public ObservableCollection<TabItem> Tabs { get; set; }

        public void Handle(BoardOpenMessage message)
        {
            Tabs.Add(message.BoardView);
        }
    }
}
