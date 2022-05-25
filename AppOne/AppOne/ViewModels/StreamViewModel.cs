using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AppOne.ViewModels
{
    public class StreamViewModel : INotifyPropertyChanged
    {
        private bool isRecording = false;
        public bool IsRecording
        {
            get 
            {
                return isRecording; 
            }
            set 
            {
                isRecording = value;
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
    }
}
