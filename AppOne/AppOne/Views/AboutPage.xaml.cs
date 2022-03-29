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
            var uri = new Uri("https://www.appsloveworld.com/wp-content/uploads/2018/10/640.mp4");
            //CrossMediaManager.Current.Play(uri);
            Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
    }
}