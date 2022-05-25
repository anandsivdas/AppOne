using AppOne.Services;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppOne.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertView : PopupPage
    {
        public event EventHandler<DialogResultEventArgs> OnAlertClosed;
        public AlertViewOptions AlertOption { get; }
        public AlertView(string title, string message, AlertViewOptions alertViewOption)
        {
            InitializeComponent();
            AlertOption = alertViewOption;
            titleMsg.Text = title;
            subTitleMsg.Text = message;
            UpdateOptionsText();
        }

        private void UpdateOptionsText()
        {
            switch (AlertOption)
            {
                case AlertViewOptions.YesNo:
                    OkBtn.Text = "Yes";
                    cancelBtn.Text = "No";
                    break;
                case AlertViewOptions.OkCancel:
                    OkBtn.Text = "OK";
                    cancelBtn.Text = "Cancel";
                    break;
                case AlertViewOptions.OK:
                    cancelBtn.IsVisible = false;
                    break;
                default: break;
            }
        }

        private void OkBtn_Clicked(object sender, EventArgs e)
        {
            OnAlertClosed?.Invoke(this, new DialogResultEventArgs(true));
        }

        private void cancelBtn_Clicked(object sender, EventArgs e)
        {
            OnAlertClosed?.Invoke(this, new DialogResultEventArgs(false));
        }
    }
    public class DialogResultEventArgs : EventArgs
    {
        public bool CanContinue { get; set; }
        public DialogResultEventArgs(bool canContinue)
        {
            CanContinue = canContinue;
        }

    }
}