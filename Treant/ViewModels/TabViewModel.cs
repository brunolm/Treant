namespace Treant.ViewModels
{
    using System;

    public abstract class TabItemViewModel : BaseViewModel
    {
        public virtual string Header { get; set; }

        public virtual string ToolTip { get; set; }
    }
}
