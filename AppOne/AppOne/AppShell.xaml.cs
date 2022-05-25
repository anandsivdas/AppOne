using AppOne.ViewModels;
using AppOne.Views;
using Rg.Plugins.Popup.Extensions;
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
            //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            //Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            var pop = new AlertView("Logout","Do you really wish to logout?", Services.AlertViewOptions.YesNo);
            pop.OnAlertClosed += Pop_OnAlertClosed;
            await App.Current.MainPage.Navigation.PushPopupAsync(pop, true);
        }

        private void Pop_OnAlertClosed(object sender, DialogResultEventArgs e)
        {
            if (e.CanContinue)
            {
                var signOut = auth.SignOut();
                if (signOut)
                {
                    Application.Current.MainPage = new LoginPage();
                }
            }
            App.Current.MainPage.Navigation.PopPopupAsync(true);
        }
    }
}
