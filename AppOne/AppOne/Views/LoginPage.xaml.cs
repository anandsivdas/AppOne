using AppOne.Services;
using AppOne.ViewModels;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppOne.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        IAuth auth;
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
            auth = DependencyService.Get<IAuth>();
        }

        async void Login_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(EmailInput.Text) && !string.IsNullOrEmpty(PasswordInput.Text))
            {
                if (Constants.EmailValidityCheck(EmailInput.Text))
                {
                    string token = await auth.LoginWithEmailAndPassword(EmailInput.Text, PasswordInput.Text);
                    if (token != string.Empty)
                    {
                        Application.Current.MainPage = new AppShell();
                    }
                    else
                    {
                        AlertViewDisplay("Authentication Failed", "Please check the username/password you entered", Services.AlertViewOptions.OK);
                    }
                }
                else
                {
                    AlertViewDisplay("Login failed", "Please enter a valid email address", Services.AlertViewOptions.OK);
                }
            }
            else
            {
                AlertViewDisplay("Login failed", "Please fill both the fields to proceed", Services.AlertViewOptions.OK);
            }
        }

        //public bool CredentialsCheck()
        //{
        //    bool validEmail = (Regex.IsMatch(EmailInput.Text, Constants.EmailRegex));
        //    return validEmail ? true : false;
        //}

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