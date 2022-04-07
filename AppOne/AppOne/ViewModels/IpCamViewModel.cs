using AppOne.Models;
using AppOne.Services;
using AppOne.Views;
using LibVLCSharp.Forms.Shared;
using LibVLCSharp.Shared;
using RtspClientSharp;
using RtspClientSharp.RawFrames;
using RtspClientSharp.RawFrames.Audio;
using RtspClientSharp.RawFrames.Video;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppOne.ViewModels
{
    public class IpCamViewModel : INotifyPropertyChanged
    {
        public Command ConnectCommand => new Command(ConnectIP);

        private IpCamModel _camModel = GlobalObjects.Instance.CamModel;
        public IpCamModel CamModel
        {
            get
            {
                return _camModel;
            }
            set
            {
                _camModel = value;
                RaisePropertyChanged();
            }
        }

        private bool isBtnEnabled = false;
        public bool IsBtnEnabled
        {
            get 
            { 
                return isBtnEnabled; 
            }
            set 
            {
                isBtnEnabled = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// adfasdas
        /// </summary>
        /// <param name="obj">dasfaso</param>
        public void ConnectIP(object obj)
        {
            GlobalObjects.Instance.CamModel = CamModel;
            App.Current.MainPage = new StreamView();
        }

        private async Task ConnectIpCam()
        {
            try
            {
                var serverUri = new Uri($"rtsp://{CamModel.IpAddress}:{CamModel.Port}/h264_pcm.sdp");
                var credentials = new NetworkCredential(CamModel.Username, CamModel.Password);
                var connectionParameters = new ConnectionParameters(serverUri, credentials);
                connectionParameters.RtpTransport = RtpTransportProtocol.TCP;
                using (var rtspClient = new RtspClient(connectionParameters))
                {
                    rtspClient.FrameReceived += (sender, frame) =>
                    {
                    //process (e.g. decode/save to file) encoded frame here or 
                    //make deep copy to use it later because frame buffer (see FrameSegment property) will be reused by client
                    switch (frame)
                        {
                            case RawH264IFrame h264IFrame: 
                            case RawH264PFrame h264PFrame:
                            case RawJpegFrame jpegFrame:
                            case RawAACFrame aacFrame:
                            case RawG711AFrame g711AFrame:
                            case RawG711UFrame g711UFrame:
                            case RawPCMFrame pcmFrame:
                            case RawG726Frame g726Frame: ProcessFrame(frame); break;
                            default: break;
                        }
                    };
                    await rtspClient.ConnectAsync(CancellationToken.None);
                    await rtspClient.ReceiveAsync(CancellationToken.None);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void ProcessFrame(RawFrame frame)
        {
            throw new NotImplementedException();
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
