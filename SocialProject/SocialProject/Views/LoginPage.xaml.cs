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
using System.Data;
using System.Data.SqlClient;
using SocialProject.Models;
using SocialProject.Views;
using SocialProject.ViewModels;




namespace SocialProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public int idUser;
        public LoginPage()
        {
            InitializeComponent();
            Inicializar();

            
        }

        private void Inicializar()
        {   NavigationPage.SetHasNavigationBar(this, false);
            // BindingContext = viewModel = new LoginPageViewModel();

            loginButton.Clicked += ButtonText_Clicked;
            buttonReg.Clicked += ButtonReg_Clicked;
            fotoPerfil.Source = "defaultUser1.png";
          


        }
        /*
        private void Boton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Alert", "You have been alerted", "OK");
        }*/

        private void ButtonReg_Clicked(object sender, EventArgs e)
        {
            
            ((NavigationPage)this.Parent).PushAsync(new RegistroPage());
        }


        private void ButtonText_Clicked(object sender, EventArgs e)
        {
            //loginMySql();
           if (string.IsNullOrEmpty(userName.Text)|| string.IsNullOrEmpty(password.Text))
                DisplayAlert("Alerta", "No puede haber camos en blanco!", "OK");
           else
            loginSql();
        }


        private void loginMySql() { 
            
            var conString = "host=192.168.0.112;port=3306;user id=appUser;password=appUser;database=socialproject;Allow User Variables=true;";
            MySqlDataReader reader;
            using (var conn = new MySqlConnection(conString))
            {
                conn.Open();
                string usuario = userName.Text;
                string pass = password.Text;
                string fotoFile=" ";
                string nombre = " ";
                // Insert some data
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    // cmd.CommandText = "INSERT INTO usuarios (username,password) VALUES (@user,@pass)";
                    cmd.CommandText = "SELECT password,photo,nombre FROM usuarios WHERE username = @user";
                    cmd.Parameters.AddWithValue("user", usuario);
                    //cmd.Parameters.AddWithValue("pass", pass);

                    string passReturn = "";

                    //cmd.ExecuteNonQuery();
                    reader = cmd.ExecuteReader();
                    while(reader.Read()) { 
                         passReturn = Convert.ToString(reader["password"]);
                        fotoFile = Convert.ToString(reader["photo"]);
                        nombre = Convert.ToString(reader["nombre"]);
                        idUser = Convert.ToInt32(reader["idUsuario"]);
                    }
                    reader.Close();

                    string foto = fotoFile.Trim('"');

                    //userName.Text = passReturn;

                    if (passReturn == pass)
                    {
                        fotoPerfil.Source = "https://socialproject.azurewebsites.net/" + foto;
                        password.IsVisible = false;
                        userName.IsVisible = false;

                        buttonReg.IsVisible = false;
                        loginButton.IsVisible = false;
                        Saludo.Text = "Hola " + nombre + "!";
                    }


                    //userName.Text = "ok";
                    else
                        userName.Text = "no pass";
                    

                }

            }
        }
        private void loginSql()
        {

            var conString = "data source=socialproject.database.windows.net;user id=appuser;password=Pass1234Word;database=socialproject;Connect Timeout=60";
            //var conString = "data source=192.168.0.112;user id=appuser;password=Pass1234Word;database=socialproject;Connect Timeout=60";

            //;Allow User Variables=tru @"data source=10.82.96.2;initial catalog=Empresa;user id=sa;password=tupassword;



            using (SqlConnection conn = new SqlConnection(conString))
            {   

                conn.Open();
                string usuario = userName.Text;
                string pass = password.Text;
                string fotoFile = " ";
                string nombre = " ";
                // Insert some data


             //  if (userName.Text == "") 

              //   DisplayAlert("Atención", "No puede haber campos vacios!", "OK");
        

                SqlDataReader reader;
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT password,photo,nombre FROM usuarios WHERE username = @user";
                    cmd.Parameters.AddWithValue("user", usuario);


                    string passReturn = "";

                    //cmd.ExecuteNonQuery();

       
                        reader = cmd.ExecuteReader();



                        while (reader.Read())
                        {
                            passReturn = Convert.ToString(reader["password"]);
                            fotoFile = Convert.ToString(reader["photo"]);
                            nombre = Convert.ToString(reader["nombre"]);
                        }
                        reader.Close();
                        string foto = fotoFile.Trim('"');
                        //userName.Text = passReturn;
                        if (passReturn == pass)
                        {
                        //userName.Text = "ok";
                        fotoPerfil.Source = "https://socialproject.azurewebsites.net/" + foto;
                        //fotoPerfil.Source = "https://serverlocal/" + foto;
                        password.IsVisible = false;
                            userName.IsVisible = false;

                            buttonReg.IsVisible = false;
                            loginButton.IsVisible = false;
                            Saludo.Text = "Hola " + nombre + "!";
                        }
                        else
                            userName.Text = "no pass";


                    }
                }
 
            
        }
    }
}