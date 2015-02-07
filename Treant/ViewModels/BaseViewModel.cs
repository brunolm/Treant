namespace Treant.ViewModels
{
    using System;
    using System.ComponentModel;

    public abstract class BaseViewModel : INotifyPropertyChanged
    {
#pragma warning disable 67
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 67
    }
}
