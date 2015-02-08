namespace Treant.Core.Controls
{
    using System.Windows;
    using System.Windows.Controls;

    public class SimpleToolBar : ToolBar
    {
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var overflowGrid = Template.FindName("OverflowGrid", this) as FrameworkElement;
            if (overflowGrid != null)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }
        }
    }

    public class SimpleToolBarTray : ToolBarTray { }
}
