using System;
using System.Threading;
using System.Windows.Automation;

namespace AutomationFramework.WEB.NavigationDialogs.DialogExtensions
{
    public class ChromeSaveFileDialog : ISaveFileDialog
    {
        private readonly int _browserProcessId;

        /// <summary>
        /// Find browser element by criteria
        /// </summary>
        /// <param name="parent">Parent element</param>
        private AutomationElement FindElement(AutomationElement parent, Condition condition, TreeScope scope)
        {
            if (parent == null)
                return null;
            DateTime timeout = DateTime.Now.AddSeconds(30);
            do
            {
                AutomationElement element = parent.FindFirst(scope, condition);
                if (element != null)
                    return element;
                Thread.Sleep(100);
            } while (DateTime.Now <= timeout);
            return null;
        }

        /// <summary>
        /// Return Browser Window
        /// </summary>
        private AutomationElement BrowserWindow
        {
            get
            {
                Condition browserWindowCriteria = new AndCondition(
                    new PropertyCondition(AutomationElement.ProcessIdProperty, _browserProcessId),
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window));
                return this.FindElement(AutomationElement.RootElement, browserWindowCriteria, TreeScope.Children);
            }
        }

        /// <summary>
        /// Return Dialog Window
        /// </summary>
        private AutomationElement DialogWindow
        {
            get
            {
                Condition saveWindowCriteria = new AndCondition(
                    new PropertyCondition(AutomationElement.NameProperty, "Save As"),
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window));
                return this.FindElement(BrowserWindow, saveWindowCriteria, TreeScope.Children);
            }
        }

        /// <summary>
        /// Return Save Button
        /// </summary>
        private AutomationElement SaveButton
        {
            get
            {
                Condition saveButtonCriteria = new AndCondition(
                    new PropertyCondition(AutomationElement.NameProperty, "Save"),
                    new PropertyCondition(AutomationElement.LocalizedControlTypeProperty, "button"),
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button)
                );
                return this.FindElement(DialogWindow, saveButtonCriteria, TreeScope.Children);
            }
        }

        /// <summary>
        /// Return Cancel Button
        /// </summary>
        private AutomationElement CancelButton
        {
            get
            {
                Condition cancelButtonCriteria = new AndCondition(
                    new PropertyCondition(AutomationElement.NameProperty, "Cancel"),
                    new PropertyCondition(AutomationElement.LocalizedControlTypeProperty, "button"),
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button)
                );
                return this.FindElement(DialogWindow, cancelButtonCriteria, TreeScope.Children);
            }
        }

        /// <summary>
        /// Return Filename ComboBox
        /// </summary>
        private AutomationElement FileNameComboBox
        {
            get
            {
                Condition fileNameCriteris = new AndCondition(
                    new PropertyCondition(AutomationElement.NameProperty, "File name:"),
                    new PropertyCondition(AutomationElement.LocalizedControlTypeProperty, "combo box"),
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ComboBox)
                );
                return this.FindElement(DialogWindow, fileNameCriteris, TreeScope.Descendants);
            }
        }

        public string FileName
        {
            get
            {
                ValuePattern pattern = (ValuePattern)FileNameComboBox.GetCurrentPattern(ValuePattern.Pattern);
                return pattern.Current.Value;
            }

            set
            {
                ValuePattern pattern = (ValuePattern)FileNameComboBox.GetCurrentPattern(ValuePattern.Pattern);
                pattern.SetValue(value);
            }
        }

        /// <summary>
        /// Click the Cancel button
        /// </summary>
        public void Cancel()
        {
            InvokePattern pattern = (InvokePattern)CancelButton.GetCurrentPattern(InvokePattern.Pattern);
            pattern.Invoke();
        }

        /// <summary>
        /// Click the Save button
        /// </summary>
        public void Save()
        {
            InvokePattern pattern = (InvokePattern)SaveButton.GetCurrentPattern(InvokePattern.Pattern);
            pattern.Invoke();
        }

        /// <summary>
        /// Click the Close button
        /// </summary>
        public void Close()
        {
            throw new NotImplementedException();
        }
    }
}
