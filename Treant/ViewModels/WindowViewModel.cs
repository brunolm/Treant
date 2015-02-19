namespace Treant.ViewModels
{
    using System.Windows;
    using Treant.Core;
    using Treant.Core.Extenders;

    public abstract class WindowViewModel : BaseViewModel
    {
        public virtual string WindowTitle { get; set; }

        public RelayCommand CancelCommand { get; internal set; }

        public WindowViewModel()
        {
            CancelCommand = new RelayCommand((o) => (o as Window).Close());
        }
    }
}
