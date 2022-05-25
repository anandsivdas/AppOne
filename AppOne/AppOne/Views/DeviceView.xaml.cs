using Plugin.BLE.Abstractions.Contracts;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppOne.ViewModels;

namespace AppOne.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeviceView : ContentPage
    {
        DeviceViewModel viewModel = new DeviceViewModel();
        public DeviceView()
        {
            InitializeComponent();
            this.BindingContext = viewModel;
            //viewModel.BleStatus += ViewModel_BLEStatus;
        }

        //private void ViewModel_BLEStatus(object sender, DisplayEventArgs e)
        //{
        //    DisplayAlert(e.Title, e.Message, "OK");
        //}

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new AppShell();
        }

        private void lv_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (lv.SelectedItem == null)
            {
                return;
            }
            viewModel.Device = lv.SelectedItem as IDevice;
        }

        /// <summary>
        /// Connect to a specific device
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private async void btnConnect_Clicked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if(device !=null )
        //        {
        //            await adapter.ConnectToDeviceAsync(device);

        //        }
        //        else
        //        {
        //            DisplayAlert("Notice","No Device selected !", "OK");
        //        }
        //    }
        //    catch(DeviceConnectionException ex)
        //    {
        //        //Could not connect to the device
        //        DisplayAlert("Notice", ex.Message.ToString(), "OK");
        //    }
        //}

        //private async void btnKnowConnect_Clicked(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        await adapter.ConnectToKnownDeviceAsync(new Guid("guid"));

        //    }
        //    catch (DeviceConnectionException ex)
        //    {
        //        //Could not connect to the device
        //        DisplayAlert("Notice", ex.Message.ToString(), "OK");
        //    }
        //}
    }
}