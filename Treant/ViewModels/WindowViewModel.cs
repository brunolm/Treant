namespace Treant.ViewModels
{
    using System.Windows;
    using Treant.Core;
    using Treant.Core.Extenders;

    public abstract class WindowViewModel : BaseViewModel
    {
        private string windowTitle;
        public virtual string WindowTitle
        {
            get { return windowTitle; }
            set
            {
                windowTitle = value;
                this.RaisePropertyChanged();
            }
        }

        public virtual RelayCommand CancelCommand { get; private set; }

        public WindowViewModel()
        {
            CancelCommand = new RelayCommand((o) => (o as Window).Close());
        }

        public virtual MessageBoxResult ConfirmDeletion()
        {
            return MessageBox.Show("Are you sure you want to delete?", "Confirm deletion"
                , MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
        }
    }
}
