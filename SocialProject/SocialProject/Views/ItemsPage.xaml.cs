using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SocialProject.Models;
using SocialProject.Views;
using SocialProject.ViewModels;

namespace SocialProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;
        private string Categoria;
        public ItemsPage()
        {
            InitializeComponent();
            Inicializar();


        }

        private void Inicializar()
        {
            bAlimentos.Clicked += Alimentos_Clicked;
           // children.
        }

        private void Alimentos_Clicked(object sender, EventArgs e)
        {
            Categoria = "Alimentos";
        }
    }
}