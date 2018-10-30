using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

using SocialProject.Models;
using SocialProject.Views;
using SocialProject.ViewModels;


namespace SocialProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        MainViewModel viewModel;

        public MainPage()
        {
      
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            // MySqlConn conn = new MySqlConn("appUser", "appUser");
            BindingContext = viewModel = new MainViewModel();


        }
    }
}