using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

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
            content.Add(new StreamContent(_mediaFile.GetStream()), "\"file\"",$"\"{_mediaFile.Path}\"");
            var httpClient = new HttpClient();
            var uploadServiceBaseAddress = "http://192.168.0.112/api/Files/Upload";
          //  var uploadServiceBaseAddress = "http://socialproject.azurewebsites.net/api/Files/Upload";
                /*/ "http://serverlocal:57320/api/files/Uploads";*/
            /*// http://localhost/files/Upload";*/
            var httpResponseMessage = await httpClient.PostAsync(uploadServiceBaseAddress, content);
            //httpClient.setProperty("http.keepAlive", "false");
            RemotePathLabel.Text = await httpResponseMessage.Content.ReadAsStringAsync();

        }
        private void cargaPerfilMySql()
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

                string persona = Persona.Items[Persona.SelectedIndex];
                string usuario = userName.Text;
                string pass = password.Text;
                string photo = RemotePathLabel.Text;
                // Insert some data
               




                    using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO usuarios (apellido,nombre,email,telefono,fechaNac,persona,password,username,photo) VALUES (@apellido,@nombre,@email,@telefono,@fechaNac,@persona,@user,@pass,@photo)";
                    cmd.Parameters.AddWithValue("apellido", apellido);
                    cmd.Parameters.AddWithValue("nombre", nombre);
                    cmd.Parameters.AddWithValue("telefono", telefono);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("fechaNac", fechaNac);
                    cmd.Parameters.AddWithValue("persona", persona);
                    cmd.Parameters.AddWithValue("user", usuario);
                    cmd.Parameters.AddWithValue("pass", pass);
                    cmd.Parameters.AddWithValue("photo", photo);
                    cmd.ExecuteNonQuery();

                }
                conn.Close();

            }
        }
     
        private void cargaPerfilSql() {
            var conString = "data source=192.168.0.112;user id=appuser;password=Pass1234Word;database=socialproject;Connect Timeout=60";

           // var conString = "data source=socialproject.database.windows.net;user id=appuser;password=Pass1234Word;database=socialproject;Connect Timeout=60";
                                             //;Allow User Variables=tru @"data source=10.82.96.2;initial catalog=Empresa;user id=sa;password=tupassword;
            using (SqlConnection conn = new SqlConnection(conString))
            {
                conn.Open();
                string apellido = Apellido.Text;
                string nombre = Nombre.Text;
                string telefono = Telefono.Text;
                string email = Email.Text;
                DateTime fechaNac = FechaNac.Date;

                string persona = Persona.Items[Persona.SelectedIndex];
                string usuario = userName.Text;
                string pass = password.Text;
                string photo = RemotePathLabel.Text;
                Int32 Id = 0;
                // Insert some data
                SqlDataReader reader;
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    // cmd.CommandText = "INSERT INTO usuarios (username,password) VALUES (@user,@pass)";
                    cmd.CommandText = "SELECT MAX(idUsuario) as MaxId FROM usuarios";
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        
                            Id = (int)reader["MaxId"]; 

                    }
                    reader.Close();
                    Id = Id + 1;
                }


                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO usuarios (idUsuario,apellido,nombre,email,telefono,fechaNac,persona,password,username,photo) VALUES (@id,@apellido,@nombre,@email,@telefono,@fechaNac,@persona,@user,@pass,@photo)";
                    cmd.Parameters.AddWithValue("apellido", apellido);
                    cmd.Parameters.AddWithValue("nombre", nombre);                                                       
                    cmd.Parameters.AddWithValue("telefono", telefono);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("fechaNac", fechaNac);
                    cmd.Parameters.AddWithValue("persona", persona);
                    cmd.Parameters.AddWithValue("user", usuario);
                    cmd.Parameters.AddWithValue("pass", pass);
                    cmd.Parameters.AddWithValue("photo", photo);
                    cmd.Parameters.AddWithValue("id", Id);
                    cmd.ExecuteNonQuery();

                }                                                              
                conn.Close();

            }}
                                                                                                                                                                                                                 



        private void ButtonReg_Clicked(object sender, EventArgs e)
        {
            // cargaPerfilMySql();
            if (string.IsNullOrEmpty(userName.Text) || string.IsNullOrEmpty(password.Text)
                || string.IsNullOrEmpty(Apellido.Text)
                || string.IsNullOrEmpty(Nombre.Text)
                || string.IsNullOrEmpty(Email.Text)
                || string.IsNullOrEmpty(Telefono.Text)
                || string.IsNullOrEmpty(password.Text)
                || string.IsNullOrEmpty(userName.Text))
                DisplayAlert("Alerta", "No puede haber camos en blanco!", "OK");

            else
                cargaPerfilSql();
        }
    }
}