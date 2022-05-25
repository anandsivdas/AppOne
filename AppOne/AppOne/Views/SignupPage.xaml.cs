using AppOne.Services;
using Rg.Plugins.Popup.Extensions;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppOne.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {
        IAuth auth;
        public SignupPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
        }

        public void Signup_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(EmailInput.Text) && !string.IsNullOrEmpty(PasswordInput.Text))
            {
                if (Constants.EmailValidityCheck(EmailInput.Text))
                {
                    var user = auth.SignUpWithEmailAndPassword(EmailInput.Text, PasswordInput.Text);
                    if (user != null)
                    {
                        AlertViewDisplay("Great!", "Your account has been added successfully", AlertViewOptions.OK);
                        var signOut = auth.SignOut();
                        if (signOut)
                        {
                            Application.Current.MainPage = new LoginPage();
                        }
                    }
                }
                else
                {
                    AlertViewDisplay("Login failed", "Please enter a valid email address", Services.AlertViewOptions.OK);
                }
            }
            else
            {
                AlertViewDisplay("Error", "Please fill both fields to proceed", AlertViewOptions.OK);
            }

        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();
        }

        private void AlertViewDisplay(string title, string message, AlertViewOptions alertType)
        {
            var pop = new AlertView(title, message, alertType);
            pop.OnAlertClosed += Pop_OnAlertClosed;
            App.Current.MainPage.Navigation.PushPopupAsync(pop, true);
        }

        private void Pop_OnAlertClosed(object sender, DialogResultEventArgs e)
        {
            App.Current.MainPage.Navigation.PopPopupAsync(true);
        }
    }
}