using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MySqlConn;
using MySqlConnector;
using Plugin.Media;
using Plugin.Media.Abstractions;

using System.Net.Http;

namespace SocialProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistroPage : ContentPage
    { private MediaFile _mediaFile;
        public RegistroPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            Inicializar();

        }

        private void Inicializar()
        {
            ButtonRegistro.Clicked += ButtonReg_Clicked;

        }
        
        private async void ButtonPick_Clicked (object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No PickPhoto", ":(No PickPhoto availble.", "OK");
                return;

            }
            _mediaFile = await CrossMedia.Current.PickPhotoAsync();

            if (_mediaFile == null)
                return;
            LocalPathLabel.Text = _mediaFile.Path;
            fotoPerfil.Source = ImageSource.FromStream(() =>
            { return _mediaFile.GetStream(); });

        }
        private async void ButtonPicUp_Clicked(object sender, EventArgs e)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(_mediaFile.GetStream()), "\"file\"");
            var httpClient = new HttpClient();
            var uploadServiceBaseAddress = "http://serverlocal:57320/api/files/Uploads";
            /*// http://localhost/files/Upload";*/
            var httpResponseMessage = await httpClient.PostAsync(uploadServiceBaseAddress, content);
            //httpClient.setProperty("http.keepAlive", "false");
            RemotePathLabel.Text = await httpResponseMessage.Content.ReadAsStringAsync();

        }

        private void ButtonReg_Clicked(object sender, EventArgs e)
        {
           
            var conString = "host=192.168.0.112;port=3306;user id=appUser;password=appUser;database=socialproject;Allow User Variables=true;";

            using (var conn = new MySqlConnection(conString))
            {
                conn.Open();
                string apellido = Apellido.Text;
                string nombre = Nombre.Text;
                string telefono = Telefono.Text;
                string email = Email.Text;
                DateTime fechaNac = FechaNac.Date;

                string sexo = Sexo.Items[Sexo.SelectedIndex];
                string usuario = userName.Text;
                string pass = password.Text;
                // Insert some data
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO usuarios (apellido,nombre,email,telefono,fechaNac,sexo,password,username) VALUES (@apellido,@nombre,@email,@telefono,@fechaNac,@sexo,@user,@pass)";
                    cmd.Parameters.AddWithValue("apellido", apellido);
                    cmd.Parameters.AddWithValue("nombre", nombre);
                    cmd.Parameters.AddWithValue("telefono", telefono);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("fechaNac", fechaNac);
                    cmd.Parameters.AddWithValue("sexo", sexo);
                    cmd.Parameters.AddWithValue("user", usuario);
                    cmd.Parameters.AddWithValue("pass", pass);
                    cmd.ExecuteNonQuery();

                }
                conn.Close();

            }
        }
    }
}