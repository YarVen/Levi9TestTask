namespace AutomationFramework.WEB.NavigationDialogs.DialogExtensions
{

    public static class SaveFileHandler
    {
        /// <summary>
        /// Save file by dialog window in browser
        /// </summary>
        public static void Save()
        {
            var saveDialog = DialogFactory.GetSaveFileDialog();
            var filename = saveDialog.FileName;
        }
    }
}
