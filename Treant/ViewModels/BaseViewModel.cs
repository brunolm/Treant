namespace Treant.ViewModels
{
    using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

    [PropertyChanged.ImplementPropertyChanged]
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public virtual MessageBoxResult ConfirmDeletion()
        {
            return MessageBox.Show("Are you sure you want to delete?", "Confirm deletion"
                , MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}
