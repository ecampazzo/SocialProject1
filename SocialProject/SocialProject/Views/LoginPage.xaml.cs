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

using SocialProject.Models;
using SocialProject.Views;
using SocialProject.ViewModels;

namespace SocialProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        ItemsViewModel viewModel;
        public LoginPage()
        {
            InitializeComponent();
            Inicializar();

            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = viewModel = new ItemsViewModel();
        }

        private void Inicializar()
        {
            loginButton.Clicked += ButtonText_Clicked;
            buttonReg.Clicked += ButtonReg_Clicked;

        }

        private void ButtonReg_Clicked(object sender, EventArgs e)
        {
            
            ((NavigationPage)this.Parent).PushAsync(new RegistroPage());
        }


        private void ButtonText_Clicked(object sender, EventArgs e)
        {
            //string error; //Variable que va contener el error
            /*Crear una instancia de la clase MySQLconn y le añadimos los argumentos de 
            nuestros Edit Text que son el usuario y contraseña.
            */
            //MySqlConn conn = new MySqlConn(userName.Text, password.Text);
            //var db = new AppDb();
            var conString = "host=192.168.0.112;port=3306;user id=appUser;password=appUser;database=socialproject;Allow User Variables=true;";
            MySqlDataReader reader;
            using (var conn = new MySqlConnection(conString))
            {
                conn.Open();
                string usuario = userName.Text;
                string pass = password.Text;
                // Insert some data
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    // cmd.CommandText = "INSERT INTO usuarios (username,password) VALUES (@user,@pass)";
                    cmd.CommandText = "SELECT password FROM usuarios WHERE username = @user";
                    cmd.Parameters.AddWithValue("user", usuario);
                    //cmd.Parameters.AddWithValue("pass", pass);

                    string passReturn = "";

                    //cmd.ExecuteNonQuery();
                    reader = cmd.ExecuteReader();
                    while(reader.Read()) { 
                         passReturn = Convert.ToString(reader["password"]);}
                    reader.Close();

                        //userName.Text = passReturn;
                        if (passReturn==pass) userName.Text = "ok";
                        else
                          userName.Text = "no pass";
                    


                }

                //campoTexto.Text = "Conexion Exitosa!";

                //Ahora mandamos a traer nuestro metodo para realizar nuestra conexion
                /*
                if (conn.TryConnection(out error))
                {
                    //Si no hay error va a mostrar un mensaje.
                    campoTexto.Text = "Conexion Exitosa!";
                }
                else
                {
                    //Si hay error manda un mensaje junto con el error
                    campoTexto.Text = error;
                }*/
            }
        }
    }
}