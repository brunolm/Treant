namespace Treant.ViewModels
{
    using Treant.Core.Extenders;

    public abstract class WindowViewModel : BaseViewModel
    {
        private string windowTitle;
        public string WindowTitle
        {
            get { return windowTitle; }
            set
            {
                windowTitle = value;
                this.RaisePropertyChanged();
            }
        }
    }
}
