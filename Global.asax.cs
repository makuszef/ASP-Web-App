using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI.WebControls;

namespace Lab1
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            
            //Application["uzytkownicy"] = new List<Uzytkownik>();
            
        }

        protected void Session_Start(object sender, EventArgs e)
        {

            //uzytkownicy.Add(new Uzytkownik(""));
            /*
            List<Uzytkownik> list = (List<Uzytkownik>)Application["uzytkownicy"];
            Application["liczbaUzytkownikow"] = list.Count;
            */
            
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            string aktualnyUser = (string)Session["aktualnyUzytkownik"];
            /*
            List<Uzytkownik> uzytkownicy = (List<Uzytkownik>)Application["uzytkownicy"];
            Uzytkownik user = null;
            if (Session["aktualnyUzytkownik"] != null)
            {
                
                aktualnyUser = (string)Session["aktualnyUzytkownik"];
                foreach (Uzytkownik uzytkownik in uzytkownicy)
                {
                    if (uzytkownik.Nazwa.Equals(aktualnyUser))
                    {
                        user = uzytkownik;
                    }
                }
                if (user != null)
                    uzytkownicy.Remove(user);
                Application["uzytkownicy"] = uzytkownicy;
            }
            */
            if (aktualnyUser == null)
            {
                return;
            }
            string connString = WebConfigurationManager.ConnectionStrings["BazaASP"].ConnectionString;
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string Updatequery = "Update Uzytkownicy Set SessionID=@SessionID, Aktywny=@Aktywny Where Nazwa=@Nazwa";
            SqlCommand sqlCommand = new SqlCommand(Updatequery, connection);
            sqlCommand.Parameters.AddWithValue("@Nazwa", aktualnyUser);
            sqlCommand.Parameters.AddWithValue("@Aktywny", "False");
            sqlCommand.Parameters.AddWithValue("@SessionID", "0");

            sqlCommand.ExecuteNonQuery();
            connection.Close();
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}