namespace Treant
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Threading;
    using System.Windows.Controls;
    using Treant.Core;
    using Treant.Core.Messaging;
    using Treant.Messages;
    using Treant.ViewModels;

    [Export(typeof(IListen<BoardOpenMessage>))]
    public class MainViewModel : WindowViewModel, IListen<BoardOpenMessage>
    {
        public MainViewModel()
        {
            var homeTabItem = ControlFactory.CreateTab<BoardsViewModel>();
            homeTabItem.Header = "Home";
            homeTabItem.ToolTip = String.Format("Treant - Logged in as {0}", Thread.CurrentPrincipal.Identity.Name);

            Tabs = new ObservableCollection<TabItem>(new List<TabItem>
            {
                homeTabItem
            });

            SelectedTab = homeTabItem;

            WindowTitle = "Boards - Treant";
        }

        public ObservableCollection<TabItem> Tabs { get; set; }

        public TabItem SelectedTab { get; set; }

        public void Handle(BoardOpenMessage message)
        {
            var tabItem = Tabs.FirstOrDefault(t => 
            {
                var tabData = (t.Content as Control).DataContext;
                var msgData = (message.BoardView.Content as Control).DataContext;

                if (!(tabData is BoardViewModel) || !(msgData is BoardViewModel))
                    return false;

                return (tabData as BoardViewModel).CurrentBoard.ID == (msgData as BoardViewModel).CurrentBoard.ID;
            });

            if (tabItem == null)
            {
                Tabs.Add(message.BoardView);
            }
            else
            {
                SelectedTab = tabItem;
            }
        }
    }
}
