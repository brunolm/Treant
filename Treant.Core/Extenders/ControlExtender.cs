using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Treant.Core.Extenders
{
    public static class ControlExtender
    {
        public static T GetDataContext<T>(this Control control) where T: class
        {
            if (control == null)
                throw new ArgumentNullException("control");

            return control.DataContext as T;
        }

        public static void WithDataContext<T>(this Control control, Action<T> action) where T : class
        {
            action(control.GetDataContext<T>());
        }
    }
}
