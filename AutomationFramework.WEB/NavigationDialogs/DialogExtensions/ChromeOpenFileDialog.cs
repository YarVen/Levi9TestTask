using System.Windows.Automation;

namespace AutomationFramework.WEB.NavigationDialogs.DialogExtensions
{
    public class ChromeOpenFileDialog: OpenFileDialogBase
    {
        public ChromeOpenFileDialog() : base("Open")
        {
        }

        protected override ControlType GetOpenButtonControlType()
        {
            return ControlType.SplitButton;
        }
    }
}
