using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Treant.Core;

namespace Treant.ViewModels.Controls
{
    [PropertyChanged.ImplementPropertyChanged]
    public class ElementCreatorToolBarViewModel
    {
        public RelayCommand AddCommand { get; set; }

        public RelayCommand EditCommand { get; set; }

        public RelayCommand RemoveCommand { get; set; }
    }
}
