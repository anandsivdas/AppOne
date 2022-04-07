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
    }
}