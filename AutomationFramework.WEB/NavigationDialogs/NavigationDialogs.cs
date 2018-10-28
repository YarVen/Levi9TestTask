using AutomationFramework.WEB.NavigationDialogs.DialogExtensions;

namespace AutomationFramework.WEB.NavigationDialogs
{
    public static class NavigationDialogs
    {
        /// <summary>
        /// Upload file into browser via dialog window
        /// </summary>
        /// <param name="file">Full path to file</param>
        public static void Upload(string file)
        {
            var dialog = DialogFactory.GetOpenFileDialog();
            dialog.FilePath = file;
            dialog.Open();
        }
    }
}
