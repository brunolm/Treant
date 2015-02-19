namespace Treant.Messages
{
    using System;
    using System.Windows.Controls;
    using Treant.Domain;

    public class BoardOpenMessage
    {
        public TabItem BoardView { get; set; }

        public BoardOpenMessage(TabItem boardView)
        {
            BoardView = boardView;
        }
    }
}
