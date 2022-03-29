using AppOne.ViewModels;
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
                string token = await auth.LoginWithEmailAndPassword(EmailInput.Text, PasswordInput.Text);
                if (token != string.Empty)
                {
                    Application.Current.MainPage = new AppShell();
                }
                else
                {
                    await DisplayAlert("Authentication Failed", "Please check the username / password", "Ok");
                }
            }
            else
            {
               await DisplayAlert("Error", "Please enter valid credentials", "Ok");
            }
        }
    }
}