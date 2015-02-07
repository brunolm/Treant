using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Treant.Core
{
    public static class ControlFactory
    {
        public static Control CreateView<TViewModel>()
        {
            var viewModelType = typeof(TViewModel);

            var viewType = viewModelType.Assembly
                .GetTypes()
                .FirstOrDefault(o =>
                {
                    return o.IsSubclassOf(typeof(Control))
                        && (o.Name + "Model").EndsWith(viewModelType.Name);
                });

            if (viewType == null)
                throw new FileNotFoundException(String.Format("Could not locate view for {0}", viewModelType.Name));

            var view = MefBootstrap.Create(viewType) as Control;

            viewType.GetMethod("InitializeComponent").Invoke(view, null);
            view.DataContext = MefBootstrap.Create(viewModelType);

            return view;
        }

        public static Window CreateWindow<TViewModel>()
        {
            return CreateView<TViewModel>() as Window;
        }
    }
}
