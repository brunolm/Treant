using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Treant.Domain;

namespace Treant.ViewModels
{
    public class BoardViewModel : WindowViewModel
    {
        public Board CurrentBoard { get; set; }
    }
}
