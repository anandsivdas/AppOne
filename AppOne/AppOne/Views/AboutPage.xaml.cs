using Rg.Plugins.Popup.Extensions;
using System;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppOne.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private void Camera_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new IpCamView();
        }

        private void BLE_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new DeviceView();
        }

        //private void PopupClicked(object sender, EventArgs e)
        //{
        //    var pop = new AlertView("Test","You have passed the test",Services.AlertViewOptions.YesNo);
        //    pop.OnAlertClosed += Pop_OnAlertClosed;
        //    App.Current.MainPage.Navigation.PushPopupAsync(pop,true);
        //}

        //private void Pop_OnAlertClosed(object sender, DialogResultEventArgs e)
        //{
        //    if (e.CanContinue)
        //    {
        //        DisplayAlert("Yes Clicked", "", "OK");
        //    }
        //    else
        //    {
        //        App.Current.MainPage.Navigation.PopPopupAsync(true);
        //    }
        //}
    }
}