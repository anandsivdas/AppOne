using Plugin.BLE.Abstractions.Contracts;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Plugin.BLE;
using System.Collections.ObjectModel;
using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using AppOne.Views;
using AppOne.Services;
using Rg.Plugins.Popup.Extensions;
using Android.Content;

namespace AppOne.ViewModels
{
    public class DeviceViewModel : INotifyPropertyChanged
    {

        IBluetoothLE ble = CrossBluetoothLE.Current;
        IAdapter adapter = CrossBluetoothLE.Current.Adapter;
        public Command ScanCommand { get; }

        public DeviceViewModel()
        {
            ScanCommand = new Command(ScanBleDevices);
        }

        private IDevice _device;
        public IDevice Device
        {
            get { return _device; }
            set
            {
                _device = value;
            }
        }

        //public event EventHandler<DisplayEventArgs> BleStatus;
        //protected virtual void DisplayAlertEvent(DisplayEventArgs e)
        //{
        //    EventHandler<DisplayEventArgs> handler = BleStatus;
        //    handler?.Invoke(this, e);
        //}

        private ObservableCollection<IDevice> _deviceList = new ObservableCollection<IDevice>();
        public ObservableCollection<IDevice> DeviceList
        {
            get { return _deviceList; }
            set
            {
                _deviceList = value;
                RaisePropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        private IDevice _nativeDevice;
        public IDevice NativeDevice
        {
            get
            {
                return _nativeDevice;
            }
            set
            {
                _nativeDevice = value;
                RaisePropertyChanged();
            }
        }
        private bool _canScan = true;
        public bool CanScan
        {
            get 
            { 
                return _canScan;
            }
            set 
            { 
                _canScan = value;
                RaisePropertyChanged(); 
            }
        }

        private void ScanBleDevices(object sender)
        {
            if (ble.State == BluetoothState.Off)
            {                
                var pop = new AlertView("Scanning failed", "Your bluetooth is turned off. Enable it now?", AlertViewOptions.OkCancel);
                pop.OnAlertClosed += Pop_OnAlertClosed;
                App.Current.MainPage.Navigation.PushPopupAsync(pop, true);
                return;
            }
            CanScan = false;
            try
            {   
                adapter.DeviceDiscovered += (s, a) =>
                {
                    if (!string.IsNullOrEmpty(a.Device.Name))
                    {                        
                        if(!DeviceList.Contains(a.Device))
                            DeviceList.Add(a.Device);
                    }
                };
                //To test if the device is scanning 
                if (!adapter.IsScanning)
                {
                    DeviceList.Clear();
                    var t = new Task(async () => {await adapter.StartScanningForDevicesAsync(); 
                        _ = adapter.StopScanningForDevicesAsync();
                        CanScan = true;
                    });
                    t.Start();
                }
            }
            catch (Exception ex)
            {
                var pop = new AlertView("Something went wrong", ex.Message, AlertViewOptions.OK);
                pop.OnAlertClosed += Pop_OnAlertClosed;
                App.Current.MainPage.Navigation.PushPopupAsync(pop, true);
            }

        }

        private void Pop_OnAlertClosed(object sender, DialogResultEventArgs e)
        {
            if (e.CanContinue)
            {
                OpenBluetoothSettings();
            }
            App.Current.MainPage.Navigation.PopPopupAsync(true);
        }

        /// <summary>
        /// //Code to access the Bluetooth settings in android
        /// </summary>
        private void OpenBluetoothSettings()
        {
            Intent intentOpenBluetoothSettings = new Intent();
            intentOpenBluetoothSettings.SetAction(Android.Provider.Settings.ActionBluetoothSettings);
            intentOpenBluetoothSettings.AddFlags(ActivityFlags.NewTask);
            Android.App.Application.Context.StartActivity(intentOpenBluetoothSettings);
        }

        /// <summary>
        /// //Code to access the Location settings in android
        /// </summary>
        public void OpenLocationSettings()
        {
            Intent intentlocationopen = new Intent();
            intentlocationopen.SetAction(Android.Provider.Settings.ActionLocationSourceSettings);
            intentlocationopen.AddFlags(ActivityFlags.NewTask);
            Android.App.Application.Context.StartActivity(intentlocationopen);
        }
    }
    //public class DisplayEventArgs : EventArgs
    //{
    //    public string Message { get; set; }
    //    public string Title { get; set; }
    //    public DisplayEventArgs(string title, string message)
    //    {
    //        Message = message;
    //        Title = title;
    //    }
    //}
}
