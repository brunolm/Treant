namespace Treant.ViewModels
{
    using System;
    using System.ComponentModel;

    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
