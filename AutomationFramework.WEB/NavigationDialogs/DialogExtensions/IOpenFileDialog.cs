namespace AutomationFramework.WEB.NavigationDialogs.DialogExtensions
{
    public interface IOpenFileDialog
    {
        string FilePath{set;}
        void Close();
        void Open();
    }
}