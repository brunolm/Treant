using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Treant.MockViewModels
{
    [PropertyChanged.ImplementPropertyChanged]
    public class MainViewModel
    {
        public MainViewModel()
        {
            Tabs = new ObservableCollection<TabItem>(new List<TabItem>
            {
                new TabItem { Header = "Home", Content = "Content" },
                new TabItem { Header = "C#", Content = "C# Content" },
                new TabItem { Header = "WPF", Content = "WPF Content" },
            });
        }

        public ObservableCollection<TabItem> Tabs { get; set; }
    }
}
