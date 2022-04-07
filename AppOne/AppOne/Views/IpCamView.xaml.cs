using AppOne.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppOne.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IpCamView : ContentPage
    {
        IpCamViewModel viewModel = new IpCamViewModel();

        public IpCamView()
        {
            InitializeComponent();
            this.BindingContext = viewModel;
            urlEntry.Unfocused += EnableButton;
            portEntry.Unfocused += EnableButton;
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new AppShell();
        }

        private void EnableButton(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(urlEntry.Text) && !string.IsNullOrEmpty(portEntry.Text))
            {
                viewModel.IsBtnEnabled = true;
            }
            else
            {
                viewModel.IsBtnEnabled = false;
            }
        }
    }
}