using Plugin.BLE.Abstractions.Contracts;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Plugin.BLE;
using System.Collections.ObjectModel;
using System;
using Xamarin.Forms;
using System.Threading.Tasks;

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

        public event EventHandler<DisplayEventArgs> BleStatus;
        protected virtual void DisplayAlertEvent(DisplayEventArgs e)
        {
            EventHandler<DisplayEventArgs> handler = BleStatus;
            handler?.Invoke(this, e);
        }

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
                DisplayAlertEvent(new DisplayEventArgs("Error","Your Bluetooth is turned off"));
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
                //We have to test if the device is scanning 
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
                DisplayAlertEvent(new DisplayEventArgs("Error", ex.Message));
            }

        }
    }
    public class DisplayEventArgs : EventArgs
    {
        public string Message { get; set; }
        public string Title { get; set; }
        public DisplayEventArgs(string title, string message)
        {
            Message = message;
            Title = title;
        }
    }
}
