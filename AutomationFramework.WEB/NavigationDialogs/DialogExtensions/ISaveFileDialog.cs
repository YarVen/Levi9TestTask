namespace AutomationFramework.WEB.NavigationDialogs.DialogExtensions
{
    public interface ISaveFileDialog
    {
        string FileName { get; set; }
        void Cancel();
        void Save();
        void Close();
    }
}