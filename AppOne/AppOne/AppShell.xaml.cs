using AppOne.ViewModels;
using AppOne.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AppOne
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        IAuth auth;
        public AppShell()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            //Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
        }

        private void OnMenuItemClicked(object sender, EventArgs e)
        {
            var signOut = auth.SignOut();
            if (signOut)
            {
                Application.Current.MainPage = new LoginPage();
            }
        }
    }
}
