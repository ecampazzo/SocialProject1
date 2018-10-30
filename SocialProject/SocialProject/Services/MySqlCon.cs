using System;
using System.Collections.Generic;
using System.Text;

using MySql.Data.MySqlClient;
//using MySql.Data.MySqlClient;
using MySqlConnector;


    public class MySqlConn
    {
    string usr;
    string pass;
    
    public MySqlConn(string user, string password)
    {
        usr = user;
        pass = password;
    }
    //MySqlConnectionStringBuilder Builder = new MySqlConnectionStringBuilder();
    /*
    public bool TryConnection(out string error)
    {
     
       // Builder.Server = "192.168.0.112";
       // Builder.Port = 3306;
      //  Builder.Database = "socialproject";
       // Builder.UserID ="appUser"; 
       // Builder.Password = "appUser";
       // Builder.CharacterSet = "utf8";

        try
        {
            //MySqlConnection ms = new MySqlConnection(Builder.ToString());
             MySqlConnection ms = new MySqlConnection("Server=192.168.0.112;Port=3306;database=socialproject;Uid=appUser;Password=appUser;charset=utf8");
        
            ms.Open();
               error = "";
            return true;
        }catch (Exception ex)
        {
            error = ex.ToString();
            return false;
        }


    }
    */
    public class AppDb : IDisposable
    {
        public readonly MySqlConnection Connection;

        public AppDb()
        {
            Connection = new MySqlConnection("host=192.168.0.112;port=3306;user id=appUser;password=appUser;database=socialproject;");
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }



}

