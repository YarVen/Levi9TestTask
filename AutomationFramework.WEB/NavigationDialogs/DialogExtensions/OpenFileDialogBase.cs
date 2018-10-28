using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Automation;
using AutomationFramework.WEB.Driver;

namespace AutomationFramework.WEB.NavigationDialogs.DialogExtensions
{
    public abstract class OpenFileDialogBase : IOpenFileDialog
    {

        [DllImport("User32.Dll")]
        public static extern Int32 PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        const int WM_KEYDOWN = 0x0100;
        const int VK_RETURN = 0x0D;

        private readonly string _title;
        private AutomationElement _openDialogElement;
        protected abstract ControlType GetOpenButtonControlType();

       protected OpenFileDialogBase(string title)
        {
            _title = title;
        }

        /// <summary>
        /// Return the parent automation element
        /// </summary>
        private AutomationElement getAutomationElement(AutomationElement parent,Condition condition, TreeScope scope)
        {
            AutomationElement element;
            DateTime timeBound = DateTime.Now + TimeSpan.FromSeconds(20);
            
            do
            {
                element = parent.FindFirst(scope, condition);
                Thread.Sleep(TimeSpan.FromSeconds(1));
            } while (element == null && timeBound > DateTime.Now);
            
            return element;
        }

        /// <summary>
        /// Return the opened dialog window
        /// </summary>
        private AutomationElement OpenDialogWindow
        {
            get
            {
                if (_openDialogElement == null)
                {
                    AutomationElement mainWindowAutomationElement = AutomationElement.FromHandle(SessionService.Session.BrowserProcess.MainWindowHandle);
                    Condition openDialogCriteria = new AndCondition(
                        new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window),
                        new PropertyCondition(AutomationElement.NameProperty, _title));
                    _openDialogElement =
                        getAutomationElement(mainWindowAutomationElement, openDialogCriteria, TreeScope.Descendants);
                }
                return _openDialogElement;
            }
        }

        /// <summary>
        /// Return the file name from dialog window
        /// </summary>
        private AutomationElement FileName
        {
            get
            {
                Condition editCriteris = new AndCondition(
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit),
                    new PropertyCondition(AutomationElement.NameProperty, "File name:")
                    );

                return getAutomationElement(OpenDialogWindow, editCriteris, TreeScope.Descendants);
            }
        }

        /// <summary>
        /// Return the open button
        /// </summary>
        private AutomationElement OpenButton
        {
            get
            {
                Condition editCriteris = new AndCondition(
                    new PropertyCondition(AutomationElement.NameProperty, "Open"),
                    new PropertyCondition(AutomationElement.ControlTypeProperty, GetOpenButtonControlType())
                    );
                return getAutomationElement(OpenDialogWindow, editCriteris,TreeScope.Descendants);
            }
        }

        /// <summary>
        /// Return the close button
        /// </summary>
        private AutomationElement CloseButton
        {
            get
            {
                Condition editCriteris = new AndCondition(
                    new PropertyCondition(AutomationElement.NameProperty, "Cancel"),
                    new PropertyCondition(AutomationElement.ClassNameProperty, "Button"),
                    new PropertyCondition(AutomationElement.LocalizedControlTypeProperty, "button"),
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button)
                    );
                return getAutomationElement(OpenDialogWindow, editCriteris,TreeScope.Descendants);
            }
        }

        public string FilePath
        {
            set
            {
                ValuePattern pattern = (ValuePattern)FileName.GetCurrentPattern(ValuePattern.Pattern);
                pattern.SetValue(value);
            }
        }

        /// <summary>
        /// Open(get) the file in dialog window
        /// </summary>
        public void Open()
        {
            PostMessage((IntPtr) OpenDialogWindow.Current.NativeWindowHandle, WM_KEYDOWN, VK_RETURN, 1);
        }

        /// <summary>
        /// Close the dialog window
        /// </summary>
        public void Close()
        {
            InvokePattern pattern = (InvokePattern)CloseButton.GetCurrentPattern(InvokePattern.Pattern);
            pattern.Invoke();
        }
    }
}