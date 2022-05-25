﻿using LibVLCSharp.Forms.Shared;
using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppOne.Services;
using AppOne.ViewModels;

namespace AppOne.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StreamView : ContentPage
    {
        StreamViewModel viewModel = new StreamViewModel();

        public StreamView()
        {
            InitializeComponent();
            this.BindingContext = viewModel;
            StreamMedia();
        }

    /// <summary>
    /// 
    /// </summary>
    private void StreamMedia()
        {
            try
            {
                Core.Initialize();
                var vlc = new LibVLC();
                MediaPlayer mediaPlayer = new MediaPlayer(vlc);
                VideoViewer.MediaPlayer = mediaPlayer;
                var media = new Media(vlc, new Uri($"rtsp://{GlobalObjects.Instance.CamModel.IpAddress}:{GlobalObjects.Instance.CamModel.Port}/h264_pcm.sdp"));
                media.StateChanged += (o, e) =>
                {
                    if (e.State == VLCState.Ended)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await DisplayAlert("Error", "Stream stopped/paused", "OK");
                        });
                    }
                };
                VideoViewer.MediaPlayer.Play(media);
            }
            catch (Exception ex)
            {
                DisplayAlert("Something went wrong", ex.Message , "OK");
            }
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            VideoViewer.MediaPlayer.Stop();
            App.Current.MainPage = new IpCamView();
        }

        private void RecButton_Clicked(object sender, EventArgs e)
        {
            if (viewModel.IsRecording)
            {
                recordBtn.Source = "icon_startRecord";
                viewModel.IsRecording = false;
            }
            else
            {
                recordBtn.Source = "icon_stopRecord";
                viewModel.IsRecording= true;
            }
        }
    }
}