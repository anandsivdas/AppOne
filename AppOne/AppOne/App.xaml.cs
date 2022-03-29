using AppOne.Services;
using AppOne.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppOne
{
    public partial class App : Application
    {
        IAuth auth;
        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();

            auth = DependencyService.Get<IAuth>();
            if (auth.IsSignIn())
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new LoginPage();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
