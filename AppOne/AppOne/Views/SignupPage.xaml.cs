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
    public partial class SignupPage : ContentPage
    {
        IAuth auth;
        public SignupPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
        }

        async void Signup_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(EmailInput.Text) && !string.IsNullOrEmpty(PasswordInput.Text))
            {
                var user = auth.SignUpWithEmailAndPassword(EmailInput.Text, PasswordInput.Text);
                if (user != null)
                {
                    await DisplayAlert("Success", " User account has been added successfully", "Ok");
                    var signOut = auth.SignOut();
                    if (signOut)
                    {
                        Application.Current.MainPage = new LoginPage();
                    }
                }
            }
            else
            {
                await DisplayAlert("Error", "Please enter valid credentials", "Ok");
            }

        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();
        }
    }
}