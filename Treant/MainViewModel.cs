using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Treant.Core;
using Treant.Core.Extenders;
using Treant.ViewModels;

namespace Treant
{
    [Export]
    public class MainViewModel : BaseViewModel
    {
        public string Title
        {
            get
            {
                return "KEKEK";
            }
        }
    }
}
