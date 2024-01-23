using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab1
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string nazwaDropDown = "";
                if (Session["Zalogowanie"] != null)
                {
                    nazwaDropDown = DropDownList1.SelectedValue;
                }
                Uzytkownik aktualnyUser = getCurrentUser();
                Session["wybranyUserDropDownList"] = nazwaDropDown;
                // || aktualnyUser.Nazwa.Equals(nazwaDropDown)
                if (aktualnyUser != null)
                {
                    //dodaj nazwe user w label
                    LabelUser.Text = aktualnyUser.Nazwa;
                    WypelnijDetailsView1(aktualnyUser.Nazwa);
                }
                string wybranyChanged = (string)Session["wybranyChanged"];
                if (wybranyChanged != null)
                {
                    //Uzytkownik nowyUser = GetUzytkownik(wybranyChanged);
                    WypelnijDetailsView1(wybranyChanged);
                }
                /*
                if ((List<Uzytkownik>)Application["uzytkownicy"] != null)
                {

                    //wypelnijDropDownList
                    List<Uzytkownik> list = (List<Uzytkownik>)Application["uzytkownicy"];
                    if (list.Count > 0)
                    {
                        WypelnijDropDownList1();
                        if (nazwaDropDown != null)
                        {
                            Uzytkownik wybranyUzytkownik = GetUzytkownik(nazwaDropDown);
                            //WypelnijDetailsView1(wybranyUzytkownik);
                        }

                    }
                }
                */
                /*
                if (Session["Zalogowanie"] != null)
                {
                    UpdatePanel1.Visible = false;
                    UpdatePanel2.Visible = true;
                }
                */
                if (ViewState["zmiana"] != null)
                {
                    pole1.Controls.Remove(Image1);
                }
                if ((string)ViewState["zmiana"] == "pole1")
                {
                    //pole1.Controls.Remove(Image1);
                    pole2.Controls.Add(Image1);

                }
                else if ((string)ViewState["zmiana"] == "pole2")
                {
                    //pole2.Controls.Remove(Image1);
                    pole3.Controls.Add(Image1);
                }
                else if ((string)ViewState["zmiana"] == "pole3")
                {
                    //pole3.Controls.Remove(Image1);
                    pole4.Controls.Add(Image1);
                }
                else if ((string)ViewState["zmiana"] == "pole4")
                {
                    //pole4.Controls.Remove(Image1);
                    pole5.Controls.Add(Image1);
                }
                else if ((string)ViewState["zmiana"] == "pole5")
                {
                    //pole5.Controls.Remove(Image1);
                    pole1.Controls.Add(Image1);
                    if (Session["obraz"] != null)
                    {
                        Image1.ImageUrl = (String)Session["obraz1"];
                        Image1.Style["height"] = (String)Session["obraz2"];
                        Image1.Style["width"] = (String)Session["obraz3"];
                    }

                }
                //DodajIluUzytkownikowDoDetailsView();

            } else
            {
                BazaDanych();
            } 
        }

        protected void ButtonPoprzedni_Click(object sender, EventArgs e)
        {
            ZerowanieCzesciSesji();
            Label4.Text = "0 s";
            Label2.Text = "0 s";
            if (Image1.ImageUrl.Equals("Rysunki/cyberpunk.jpg"))
            {
                Image1.ImageUrl = "Rysunki/witcher.jpg";
                ButtonKolejny.Enabled = true;
            }
            else if (Image1.ImageUrl.Equals("Rysunki/witcher.jpg"))
            {
                Image1.ImageUrl = "Rysunki/metro.jpg";
                ButtonKolejny.Enabled = true;
            }
            else if (Image1.ImageUrl.Equals("Rysunki/metro.jpg"))
            {
                Image1.ImageUrl = "Rysunki/ryze.jpg";
                ButtonPoprzedni.Enabled = false;
            }
            if (IsPostBack)
                ZwiekszLiczbeZmian("LiczbaZmianRysunku");
            WypelnijDetailsView1v2();

        }
        protected void ButtonKolejny_Click(object sender, EventArgs e)
        {
            
            ZerowanieCzesciSesji();
            Label4.Text = "0 s";
            Label2.Text = "0 s";
            if (Image1.ImageUrl.Equals("Rysunki/ryze.jpg"))
            {
                Image1.ImageUrl = "Rysunki/metro.jpg";
                ButtonPoprzedni.Enabled = true;
            }
            else if (Image1.ImageUrl.Equals("Rysunki/metro.jpg"))
            {
                Image1.ImageUrl = "Rysunki/witcher.jpg";
                ButtonPoprzedni.Enabled = true;
            }
            else if (Image1.ImageUrl.Equals("Rysunki/witcher.jpg"))
            {
                Image1.ImageUrl = "Rysunki/cyberpunk.jpg";
                ButtonKolejny.Enabled = false;
            }
            if (IsPostBack)
                ZwiekszLiczbeZmian("LiczbaZmianRysunku");
            WypelnijDetailsView1v2();
        }
        protected void ZerowanieCzesciSesji()
        {
            Session["czas1"] = null;
            Session["czas2"] = null;
            Session["czas3"] = null;
            Session["czas4"] = null;
            Session["czas5"] = null;
            Session["obraz1"] = null;
            Session["obraz2"] = null;
            Session["obraz3"] = null;
            Session["obraz"] = null;
        }
        protected void WypelnijDropDownList1()
        {
            WypelnijDropDownListzBazy();
            /*
            DropDownList1.Items.Clear();
            Uzytkownik pierwszy = null;
            Uzytkownik uzytkownik1 = getCurrentUser();
            if ((List<Uzytkownik>)Application["uzytkownicy"] != null)
            {
                //wypelnijDropDownList

                List<Uzytkownik> list = (List<Uzytkownik>)Application["uzytkownicy"];
                if (list.Count > 0)
                {
                    foreach(Uzytkownik uzytkownik in list)
                    {
                        if (Session["wybranyChanged"] != null && uzytkownik.Nazwa.Equals((string)Session["wybranyChanged"]))
                        {
                            if (pierwszy == null)
                                pierwszy = uzytkownik;
                            else
                                DropDownList1.Items.Add(new ListItem(uzytkownik.Nazwa));
                        }
                        else if (uzytkownik1 != null && uzytkownik.Nazwa.Equals(uzytkownik1.Nazwa))
                        {
                            if (pierwszy == null)
                                pierwszy = uzytkownik;
                            else
                                DropDownList1.Items.Add(new ListItem(uzytkownik.Nazwa));
                        }
                        else
                        {
                            DropDownList1.Items.Add(new ListItem(uzytkownik.Nazwa));
                        }
                    }
                }
                if (pierwszy != null)
                {
                    DropDownList1.Items.Insert(0, new ListItem(pierwszy.Nazwa));
                }

            }
            */
        }
        protected void WypelnijDetailsView1(string NazwaUsera)
        {
            sqldata1.SelectParameters["Nazwa"].DefaultValue = NazwaUsera;
        }
        protected void WypelnijDetailsView1(Uzytkownik user)
        {
            //List<Uzytkownik> list = (List<Uzytkownik>)Application["uzytkownicy"];
            sqldata1.SelectParameters["Nazwa"].DefaultValue = user.Nazwa;
            
            /*
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Nazwa", typeof(string));
            dataTable.Columns.Add("Data uruchomienia", typeof(string));
            dataTable.Columns.Add("Godzina uruchomienia", typeof(string));
            dataTable.Columns.Add("Ilu uzytkownikow", typeof(int));
            dataTable.Columns.Add("Ile zmian rysunku", typeof(int));
            dataTable.Columns.Add("Ile zmian polozenia", typeof(int));
            dataTable.Columns.Add("Ile zmian rozmiaru", typeof(int));
            dataTable.Columns.Add("Czas Maksymalny", typeof(string));
            dataTable.Columns.Add("Czas Minimalny", typeof(string));

            DataRow row = dataTable.NewRow();
            if (user != null)
            {
                row["Nazwa"] = user.Nazwa;
                row["Data uruchomienia"] = user.DataUruchomienia.Day.ToString() + "." + user.DataUruchomienia.Month.ToString() + "." + user.DataUruchomienia.Year.ToString();
                row["Godzina uruchomienia"] = user.DataUruchomienia.Hour.ToString() + ":" + user.DataUruchomienia.Minute.ToString();
                row["Ilu uzytkownikow"] = list.Count;
                row["Ile zmian rysunku"] = user.LiczbaZmianRysunku;
                row["Ile zmian polozenia"] = user.LiczbaZmianPolozeniaRysunku;
                row["Ile zmian rozmiaru"] = user.LiczbaZmianRozmiaru;
                row["Czas Maksymalny"] = user.CzasMaksymanlny.TotalSeconds.ToString() + " s";
                row["Czas Minimalny"] = user.CzasMinimalny.TotalSeconds.ToString() + " s";
            }

            dataTable.Rows.Add(row);
            DetailsView1.DataSource = dataTable;
            DetailsView1.DataBind();
            */
        }
        /*
        protected Uzytkownik GetUzytkownik(string szukanyUser)
        {
            Uzytkownik user = null;
            List<Uzytkownik> list = (List<Uzytkownik>)Application["uzytkownicy"];
            if (list.Count > 0)
            {
                foreach (Uzytkownik uzytkownik in list)
                {
                    try
                    {
                        if (uzytkownik != null && uzytkownik.Nazwa.ToLower().Equals(szukanyUser.ToLower()))
                        {
                            user = uzytkownik;
                        }
                    }
                    catch (NullReferenceException exeption) { }
                }
            }
            return user;
        }
        */
        protected Uzytkownik getCurrentUser()
        {
            string NazwaUsera = (string)Session["aktualnyUzytkownik"];
            if (NazwaUsera == null)
            {
                return null;
            }
            string connString = WebConfigurationManager.ConnectionStrings["BazaASP"].ConnectionString;
            SqlConnection connection = new SqlConnection(connString);
            string query = "Select * From Uzytkownicy Where Nazwa=@Nazwa";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.AddWithValue("@Nazwa", NazwaUsera);
            connection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            string Nazwa;
            Uzytkownik aktualnyUser = new Uzytkownik();
            while (reader.Read())
            {
                aktualnyUser.Nazwa = NazwaUsera;
                aktualnyUser.GodzinaUruchomienia = reader["GodzinaUruchomienia"].ToString();
                aktualnyUser.DataUruchomieniaString = reader["DataUruchomienia"].ToString();
                aktualnyUser.LiczbaZmianRozmiaru = (int)reader["LiczbaZmianRozmiaru"];
                aktualnyUser.LiczbaZmianPolozeniaRysunku = (int)reader["LiczbaZmianPolozeniaRysunku"];
                aktualnyUser.LiczbaZmianRysunku = (int)reader["LiczbaZmianRysunku"];
                aktualnyUser.CzasMaksymanlny = TimeSpan.Parse(reader["CzasMaksymanlny"].ToString());
                aktualnyUser.CzasMinimalny = TimeSpan.Parse(reader["CzasMinimalny"].ToString());
            }
            connection.Close();
            /*
            List<Uzytkownik> list = (List<Uzytkownik>)Application["uzytkownicy"];
            if (Session["aktualnyUzytkownik"] == null)
            {
                return null;
            }
            string aktualnyUser = (string)Session["aktualnyUzytkownik"];
            Uzytkownik user = null;
            foreach (Uzytkownik uzytkownik in list)
            {
                //DropDownList1.Items.Add(new ListItem(uzytkownik.Nazwa));
                if (uzytkownik.Nazwa.ToLower().Equals(aktualnyUser.ToLower()))
                {
                    user = uzytkownik;
                }
            }
            */
            return aktualnyUser;
        }
        protected void ZwiekszLiczbeZmian(string opcja)
        {
            //zmien czasy uruchomienia jesli user w bazie
            string NazwaUsera = (string)Session["aktualnyUzytkownik"];
            string connString = WebConfigurationManager.ConnectionStrings["BazaASP"].ConnectionString;
            SqlConnection connection = new SqlConnection(connString);
            string query = "";
            if (opcja.Equals("LiczbaZmianRysunku"))
            {
                query = "Update Uzytkownicy Set LiczbaZmianRysunku = LiczbaZmianRysunku + 1 Where Nazwa=@Nazwa";
            }
            else if (opcja.Equals("LiczbaZmianPolozeniaRysunku"))
            {
                query = "Update Uzytkownicy Set LiczbaZmianPolozeniaRysunku = LiczbaZmianPolozeniaRysunku + 1 Where Nazwa=@Nazwa";
            }
            else if (opcja.Equals("LiczbaZmianRozmiaru"))
            {
                query = "Update Uzytkownicy Set LiczbaZmianRozmiaru = LiczbaZmianRozmiaru + 1 Where Nazwa=@Nazwa";
            }
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.AddWithValue("@Nazwa", NazwaUsera);
            connection.Open();
            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch(SqlException e)
            {

            }
            connection.Close();
            DetailsView1.DataBind();
            /*
            Uzytkownik user = getCurrentUser();
            if (user != null)
            {
                if (opcja.Equals("LiczbaZmianRysunku"))
                {
                    user.LiczbaZmianRysunku += 1;
                }
                else if (opcja.Equals("LiczbaZmianPolozeniaRysunku"))
                {
                    user.LiczbaZmianPolozeniaRysunku += 1;
                }
                else if (opcja.Equals("LiczbaZmianRozmiaru"))
                {
                    user.LiczbaZmianRozmiaru += 1;
                }
                BazaDanychUpdate();
            }
            */
        }
        protected void ZmianaCzasowUzytkownika(TimeSpan timeSpan)
        {
            Uzytkownik uzytkownik = getCurrentUser();
            if (uzytkownik == null)
                return;
            if (uzytkownik.CzasMaksymanlny < timeSpan)
            {
                uzytkownik.CzasMaksymanlny = timeSpan;
            }
            DateTime dateTime = DateTime.Now;
            DateTime dateTime1 = DateTime.Now.AddYears(1);
            if (uzytkownik.CzasMinimalny == dateTime - dateTime)
            {
                uzytkownik.CzasMinimalny = dateTime1 - dateTime;
            }
            if (uzytkownik.CzasMinimalny > timeSpan)
            {
                uzytkownik.CzasMinimalny = timeSpan;
            }
            ZmianaCzasowUzytkownikaBazaDanych(uzytkownik.CzasMaksymanlny, uzytkownik.CzasMinimalny);
        }
        protected void ZmianaCzasowUzytkownikaBazaDanych(TimeSpan CzasMaksymanlny, TimeSpan CzasMinimalny)
        {
            string NazwaUsera = (string)Session["aktualnyUzytkownik"];
            string connString = WebConfigurationManager.ConnectionStrings["BazaASP"].ConnectionString;
            SqlConnection connection = new SqlConnection(connString);
            string query = "Update Uzytkownicy Set CzasMaksymanlny = @CzasMaksymanlny, CzasMinimalny = @CzasMinimalny Where Nazwa=@Nazwa";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.AddWithValue("@Nazwa", NazwaUsera);
            sqlCommand.Parameters.AddWithValue("@CzasMaksymanlny", CzasMaksymanlny);
            sqlCommand.Parameters.AddWithValue("@CzasMinimalny", CzasMinimalny);
            connection.Open();
            sqlCommand.ExecuteNonQuery();
            connection.Close();
        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            ZerowanieCzesciSesji();
            Label4.Text = "0 s";
            Label2.Text = "0 s";
            if (RadioButtonList1.SelectedValue.Equals("Maly rozmiar rysunku"))
            {
                //Image1.CssClass = "maly";
                Image1.Style["height"] = "50px";
                Image1.Style["width"] = "50px";
            }
            else if (RadioButtonList1.SelectedValue.Equals("Sredni rozmiar rysunku"))
            {
                //Image1.CssClass = "sredni";
                Image1.Style["height"] = "75px";
                Image1.Style["width"] = "75px";
            }
            else if (RadioButtonList1.SelectedValue.Equals("Duzy rozmiar rysunku"))
            {
                //Image1.CssClass = "duzy";
                Image1.Style["height"] = "100px";
                Image1.Style["width"] = "100px";
            }
            if(IsPostBack)
                ZwiekszLiczbeZmian("LiczbaZmianRozmiaru");
            WypelnijDetailsView1v2();
            DetailsView1.DataBind();
        }
        protected void Image1_Click(object sender, ImageClickEventArgs e)
        {
            
            if (IsPostBack)
            {
                ZwiekszLiczbeZmian("LiczbaZmianPolozeniaRysunku");
            }
            
            if (pole1.Controls.Contains(Image1))
            {
                
                ViewState["zmiana"] = "pole1";
                pole2.Controls.Add(Image1);
                pole1.Controls.Remove(Image1);
                if (Session["obraz"] != null)
                {
                    Image1.ImageUrl = (String)Session["obraz1"];
                    Image1.Style["height"] = (String)Session["obraz2"];
                    Image1.Style["width"] = (String)Session["obraz3"];
                }
                ZerowanieCzesciSesji();
                Session["czas1"] = DateTime.Now;
            }
            else if (pole2.Controls.Contains(Image1))
            {
                Session["czas2"] = DateTime.Now;
                ViewState["zmiana"] = "pole2";
                pole3.Controls.Add(Image1);
                pole2.Controls.Remove(Image1);
            }
            else if (pole3.Controls.Contains(Image1))
            {
                Session["czas3"] = DateTime.Now;
                ViewState["zmiana"] = "pole3";
                pole3.Controls.Remove(Image1);
                pole4.Controls.Add(Image1);
            }
            else if (pole4.Controls.Contains(Image1))
            {
                Session["czas4"] = DateTime.Now;
                ViewState["zmiana"] = "pole4";
                pole4.Controls.Remove(Image1);
                pole5.Controls.Add(Image1);
            }
            else if (pole5.Controls.Contains(Image1))
            {
                Session["czas5"] = DateTime.Now;
                ViewState["zmiana"] = "pole5";
                pole5.Controls.Remove(Image1);
                pole1.Controls.Add(Image1);
                Session["obraz"] = Image1;
                Session["obraz1"] = Image1.ImageUrl;
                Session["obraz2"] = Image1.Style["height"];
                Session["obraz3"] = Image1.Style["width"];

            }
            
            List<DateTime> list = new List<DateTime>();
            if (Session["czas1"] != null)
            {
                list.Add((DateTime)Session["czas1"]);
            }
            if (Session["czas2"] != null)
            {
                list.Add((DateTime)Session["czas2"]);
            }
            if (Session["czas3"] != null)
            {
                list.Add((DateTime)Session["czas3"]);
            }
            if (Session["czas4"] != null)
            {
                list.Add((DateTime)Session["czas4"]);
            }
            if (Session["czas5"] != null)
            {
                list.Add((DateTime)Session["czas5"]);
            }
            if (list.Count > 0)
            {
                List<TimeSpan> timeSpans = list.Skip(1).Select((x, i) => x - list[i]).ToList();
                List<double> doubles = timeSpans.Select(x => x.TotalMilliseconds).ToList();
                if (doubles.Count > 0)
                {
                    TimeSpan timeSpan1 = timeSpans[doubles.IndexOf(doubles.Max())];
                    TimeSpan timeSpan2 = timeSpans[doubles.IndexOf(doubles.Min())];
                    ZmianaCzasowUzytkownika(timeSpan1);
                    ZmianaCzasowUzytkownika(timeSpan2);
                    Label4.Text = timeSpan1.TotalSeconds.ToString() + " s";
                    Label2.Text = timeSpan2.TotalSeconds.ToString() + " s";
                }
            }
            WypelnijDetailsView1v2();
            DetailsView1.DataBind();
        }
        protected void WypelnijDetailsView1v2()
        {
            Uzytkownik aktualnyUser = getCurrentUser();
            string wybranyChanged = (string)Session["wybranyChanged"];
            if (wybranyChanged != null)
            {
                //Uzytkownik wybranyUser = GetUzytkownik(wybranyChanged);
                WypelnijDetailsView1(wybranyChanged);
            }
            else if (aktualnyUser != null)
            {
                WypelnijDetailsView1(aktualnyUser.Nazwa);
            }
        }
        //dodawanie usera
        protected void ButtonZatwierdz_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)  //nie przesylaj strony jesli jest taki sam uzytkownik
            {
                ;
            }
            else
            {
                //List<Uzytkownik> uzytkownicy = (List<Uzytkownik>)Application["uzytkownicy"];
                Uzytkownik nowyUzytkownik = new Uzytkownik(TextBox1.Text, DateTime.Now);
                nowyUzytkownik.LiczbaZmianRozmiaru = 0;
                nowyUzytkownik.LiczbaZmianRysunku = 0;
                nowyUzytkownik.LiczbaZmianPolozeniaRysunku = 0;
                DateTime dateTime = DateTime.Now;
                DateTime dateTime1 = dateTime.AddYears(1);
                nowyUzytkownik.CzasMaksymanlny = dateTime - dateTime;
                nowyUzytkownik.CzasMinimalny = nowyUzytkownik.CzasMaksymanlny;
                DodajUzytkownikaDoBazy(nowyUzytkownik);
                //WypelnijDetailsView1(nowyUzytkownik);
                /*
                bool flaga = false;
                foreach (Uzytkownik uzytkownik in uzytkownicy)
                {
                    try
                    {
                        if (uzytkownik.Nazwa.Equals(nowyUzytkownik.Nazwa))
                        {
                            flaga = true;
                        }
                    }
                    catch (NullReferenceException exeption) { }
                }
                */
                Session["aktualnyUzytkownik"] = TextBox1.Text;
                Session["Zalogowanie"] = true;
                
                UpdatePanel1.Visible = false;
                UpdatePanel2.Visible = true;
                //uzytkownicy.Add(nowyUzytkownik);
                WypelnijDropDownList1();
                Uzytkownik aktualnyUser = getCurrentUser();
                LabelUser.Text = TextBox1.Text;
                WypelnijDetailsView1(TextBox1.Text);

            }

        }
        protected void SprawdzSesjezBazy(string nazwaUzytkownika)
        {
            string connString = WebConfigurationManager.ConnectionStrings["BazaASP"].ConnectionString;
            SqlConnection connection = new SqlConnection(connString);
            //sprawdz sessionID
            string queryIleSesji = "Select count(*) From Uzytkownicy Where SessionID=@SessionID AND Aktywny='True' AND Nazwa=@Nazwa";
            SqlCommand sqlCountCommand = new SqlCommand(queryIleSesji, connection);
            sqlCountCommand.Parameters.AddWithValue("@SessionID", Session.SessionID);
            sqlCountCommand.Parameters.AddWithValue("@Nazwa", nazwaUzytkownika);
            connection.Open();
            object wynik1 = sqlCountCommand.ExecuteScalar();
            connection.Close();
            string IleoTakiejSamejSesji = wynik1.ToString();
            if (!IleoTakiejSamejSesji.Equals("0"))
            {
                CustomValidator1.ErrorMessage = "Nie mozna podpiac sie pod istniejaca sesje";
                CustomValidator1.IsValid = false;
            }
        }
        protected void BazaDanych()
        {
            
            string connString = WebConfigurationManager.ConnectionStrings["BazaASP"].ConnectionString;
            SqlConnection connection = new SqlConnection(connString);
            string mySelect1 = "Select * From Uzytkownicy";
            SqlCommand DbCommand = new SqlCommand(mySelect1, connection);
            try
            {
                connection.Open();
                SqlDataReader DbDataReader = DbCommand.ExecuteReader();
                DbDataReader.Close();
                connection.Close();
            }
            catch (SqlException ex) { Response.Write("Nie ma polaczenia z baza"); Session["NieMaBazy"] = "True"; }
        }
        protected void BazaDanychUpdate()   
        {
            Uzytkownik uzytkownik = getCurrentUser();
            string connString = WebConfigurationManager.ConnectionStrings["BazaASP"].ConnectionString;
            SqlConnection connection = new SqlConnection(connString);
            string query = "Update Uzytkownicy Set LiczbaZmianRysunku = @LiczbaZmianRysunku, LiczbaZmianPolozeniaRysunku = @LiczbaZmianPolozeniaRysunku, LiczbaZmianRozmiaru = @LiczbaZmianRozmiaru, CzasMaksymanlny = @CzasMaksymanlny, CzasMinimalny = @CzasMinimalny Where Nazwa=@Nazwa";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.AddWithValue("@Nazwa", uzytkownik.Nazwa);
            sqlCommand.Parameters.AddWithValue("@LiczbaZmianRysunku", uzytkownik.LiczbaZmianRysunku);
            sqlCommand.Parameters.AddWithValue("@LiczbaZmianPolozeniaRysunku", uzytkownik.LiczbaZmianPolozeniaRysunku);
            sqlCommand.Parameters.AddWithValue("@LiczbaZmianRozmiaru", uzytkownik.LiczbaZmianRozmiaru);
            sqlCommand.Parameters.AddWithValue("@CzasMaksymanlny", uzytkownik.CzasMaksymanlny);
            sqlCommand.Parameters.AddWithValue("@CzasMinimalny", uzytkownik.CzasMinimalny);
            connection.Open();
            sqlCommand.ExecuteNonQuery();

            connection.Close();
        }
        protected void DodajUzytkownikaDoBazy(Uzytkownik uzytkownik)
        {
            string connString = WebConfigurationManager.ConnectionStrings["BazaASP"].ConnectionString;
            SqlConnection connection = new SqlConnection(connString);
            string query = "Insert Into Uzytkownicy (Nazwa, DataUruchomienia, GodzinaUruchomienia, LiczbaZmianRysunku, LiczbaZmianPolozeniaRysunku, LiczbaZmianRozmiaru, CzasMaksymanlny, CzasMinimalny, Aktywny, SessionID) Values (@Nazwa, @DataUruchomienia, @GodzinaUruchomienia, @LiczbaZmianRysunku, @LiczbaZmianPolozeniaRysunku, @LiczbaZmianRozmiaru, @CzasMaksymanlny, @CzasMinimalny, @Aktywny, @SessionID)";
            string querySelect = "Select count(*) From Uzytkownicy Where Nazwa=@Nazwa";
            SqlCommand sqlSelectCommand = new SqlCommand(querySelect, connection);
            sqlSelectCommand.Parameters.AddWithValue("@Nazwa", uzytkownik.Nazwa);
            
            /*
            SqlDataReader reader = sqlSelectCommand.ExecuteReader();
            string Nazwa;
            //sprawdz czy nie ma uzytkownika o takiej samej nazwie
            while (reader.Read())
            {
                Nazwa = reader.GetString(reader.GetOrdinal("Nazwa"));
            }
            */
            connection.Open();
            //sprawdz czy uzytkownik jest w bazie
            object wynik = sqlSelectCommand.ExecuteScalar();
            string iluUzytkownikow = wynik.ToString();
            //nie ma takiego usera dodaj do bazy
            if (iluUzytkownikow.Equals("0"))
            {
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("@Nazwa", uzytkownik.Nazwa);
                sqlCommand.Parameters.AddWithValue("@GodzinaUruchomienia", uzytkownik.DataUruchomienia.Hour.ToString() + ":" + uzytkownik.DataUruchomienia.Minute.ToString());
                sqlCommand.Parameters.AddWithValue("@DataUruchomienia", uzytkownik.DataUruchomienia.Day.ToString() + "." + uzytkownik.DataUruchomienia.Month.ToString() + "." + uzytkownik.DataUruchomienia.Year.ToString());
                sqlCommand.Parameters.AddWithValue("@LiczbaZmianRysunku", uzytkownik.LiczbaZmianRysunku);
                sqlCommand.Parameters.AddWithValue("@LiczbaZmianPolozeniaRysunku", uzytkownik.LiczbaZmianPolozeniaRysunku);
                sqlCommand.Parameters.AddWithValue("@LiczbaZmianRozmiaru", uzytkownik.LiczbaZmianRozmiaru);
                sqlCommand.Parameters.AddWithValue("@CzasMaksymanlny", uzytkownik.CzasMaksymanlny);
                sqlCommand.Parameters.AddWithValue("@CzasMinimalny", uzytkownik.CzasMinimalny);
                sqlCommand.Parameters.AddWithValue("@Aktywny", "True");
                sqlCommand.Parameters.AddWithValue("@SessionID", Session.SessionID);

                sqlCommand.ExecuteNonQuery();
            }
            else
            {

                //zmien czasy uruchomienia jesli user w bazie
                string Updatequery = "Update Uzytkownicy Set GodzinaUruchomienia=@GodzinaUruchomienia, DataUruchomienia=@DataUruchomienia, SessionID=@SessionID, Aktywny=@Aktywny Where Nazwa=@Nazwa";
                SqlCommand sqlCommand = new SqlCommand(Updatequery, connection);
                sqlCommand.Parameters.AddWithValue("@Nazwa", uzytkownik.Nazwa);
                sqlCommand.Parameters.AddWithValue("@GodzinaUruchomienia", uzytkownik.DataUruchomienia.Hour.ToString() + ":" + uzytkownik.DataUruchomienia.Minute.ToString());
                sqlCommand.Parameters.AddWithValue("@DataUruchomienia", uzytkownik.DataUruchomienia.Day.ToString() + "." + uzytkownik.DataUruchomienia.Month.ToString() + "." + uzytkownik.DataUruchomienia.Year.ToString());
                sqlCommand.Parameters.AddWithValue("@Aktywny", "True");
                sqlCommand.Parameters.AddWithValue("@SessionID", Session.SessionID);
                sqlCommand.ExecuteNonQuery();
            }
            
            
            connection.Close();
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string nazwaDropDown = (string)Session["wybranyUserDropDownList"];
            Session["wybranyChanged"] = nazwaDropDown;
            //Uzytkownik uzytkownik = GetUzytkownik(nazwaDropDown);
            if (nazwaDropDown != null)
            {
                WypelnijDetailsView1(nazwaDropDown);
            }
            WypelnijDropDownList1();
        }
        protected void WypelnijDropDownListzBazy()
        {
            string connString = WebConfigurationManager.ConnectionStrings["BazaASP"].ConnectionString;
            SqlConnection connection = new SqlConnection(connString);
            string querySelect = "Select Nazwa From Uzytkownicy Where Aktywny='True'";
            SqlCommand sqlSelectCommand = new SqlCommand(querySelect, connection);
            connection.Open();
            
            SqlDataReader reader = sqlSelectCommand.ExecuteReader();
            string Nazwa;
            string nazwaDropDown = (string)Session["wybranyUserDropDownList"];
            if (nazwaDropDown == null)
            {
                nazwaDropDown = "";
            }
            //wyczysc dropdownlist
            DropDownList1.Items.Clear();
            while (reader.Read())
            {
                Nazwa = reader.GetString(reader.GetOrdinal("Nazwa"));
                if (!Nazwa.Equals(nazwaDropDown))
                {
                    DropDownList1.Items.Add(new ListItem(Nazwa));
                }
            }
            if (nazwaDropDown != "")
            {
                DropDownList1.Items.Insert(0, new ListItem(nazwaDropDown));
            }
            //do modyfikacji
            /*
            Uzytkownik pierwszy = null;
            Uzytkownik uzytkownik1 = getCurrentUser();
            if ((List<Uzytkownik>)Application["uzytkownicy"] != null)
            {
                //wypelnijDropDownList

                List<Uzytkownik> list = (List<Uzytkownik>)Application["uzytkownicy"];
                if (list.Count > 0)
                {
                    foreach (Uzytkownik uzytkownik in list)
                    {
                        if (Session["wybranyChanged"] != null && uzytkownik.Nazwa.Equals((string)Session["wybranyChanged"]))
                        {
                            if (pierwszy == null)
                                pierwszy = uzytkownik;
                            else
                                DropDownList1.Items.Add(new ListItem(uzytkownik.Nazwa));
                        }
                        else if (uzytkownik1 != null && uzytkownik.Nazwa.Equals(uzytkownik1.Nazwa))
                        {
                            if (pierwszy == null)
                                pierwszy = uzytkownik;
                            else
                                DropDownList1.Items.Add(new ListItem(uzytkownik.Nazwa));
                        }
                        else
                        {
                            DropDownList1.Items.Add(new ListItem(uzytkownik.Nazwa));
                        }
                    }
                }
                if (pierwszy != null)
                {
                    DropDownList1.Items.Insert(0, new ListItem(pierwszy.Nazwa));
                }

            }
            */


            connection.Close() ;
        }
        protected void DodajIluUzytkownikowDoDetailsView()
        {
            string connString = WebConfigurationManager.ConnectionStrings["BazaASP"].ConnectionString;
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            string queryIleSesji = "Select count(*) From Uzytkownicy Where Aktywny='True'";
            SqlCommand sqlCommand = new SqlCommand(queryIleSesji, connection);
            object wynik = sqlCommand.ExecuteScalar();
            string iluUserow = wynik.ToString(); 
            connection.Close();
            
            DetailsView1.DataBind();
        }
        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string brakBazy = (string)Session["NieMaBazy"];
            if (brakBazy != null)
            {
                CustomValidator1.ErrorMessage = "Nie ma polaczenia z baza";
                args.IsValid = false;
                return;
            }
            string nazwaUzytkownika = TextBox1.Text;
            string connString = WebConfigurationManager.ConnectionStrings["BazaASP"].ConnectionString;
            SqlConnection connection = new SqlConnection(connString);
            //sprawdz sessionID
            string queryIleSesji = "Select count(*) From Uzytkownicy Where (Aktywny='True' OR SessionID=@SessionID) AND Nazwa=@Nazwa";
            SqlCommand sqlCountCommand = new SqlCommand(queryIleSesji, connection);
            sqlCountCommand.Parameters.AddWithValue("@SessionID", Session.SessionID);
            sqlCountCommand.Parameters.AddWithValue("@Nazwa", nazwaUzytkownika);
            connection.Open() ;
            object wynik1 = sqlCountCommand.ExecuteScalar();
            connection.Close() ;
            string IleoTakiejSamejSesji = wynik1.ToString();
            if (!IleoTakiejSamejSesji.Equals("0"))
            {
                CustomValidator1.ErrorMessage = "Nie mozna podpiac sie pod istniejaca sesje";
                args.IsValid = false;
                return;
            }

            /*
            List<Uzytkownik> list = (List<Uzytkownik>)Application["uzytkownicy"];
            if (list.Count > 0)
            {
                foreach (Uzytkownik uzytkownik in list)
                {
                    try
                    {
                        if (uzytkownik != null && uzytkownik.Nazwa.Equals(nazwaUzytkownika))
                        {
                            //znaleziono user o takiej samej nazwie
                            args.IsValid = false;
                            return;
                        }
                    }
                    catch (NullReferenceException exeption) { }
                }
            }
            */
            args.IsValid = true;
            
        }

        protected void ZerujButton_Click(object sender, EventArgs e)
        {
            Uzytkownik uzytkownik = getCurrentUser();
            if ( uzytkownik == null)
            {
                return;
            }
            string connString = WebConfigurationManager.ConnectionStrings["BazaASP"].ConnectionString;
            SqlConnection connection = new SqlConnection(connString);
            string query = "Update Uzytkownicy Set LiczbaZmianRysunku = @LiczbaZmianRysunku, LiczbaZmianPolozeniaRysunku = @LiczbaZmianPolozeniaRysunku, LiczbaZmianRozmiaru = @LiczbaZmianRozmiaru, CzasMaksymanlny = @CzasMaksymanlny, CzasMinimalny = @CzasMinimalny Where Nazwa=@Nazwa";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.AddWithValue("@Nazwa", uzytkownik.Nazwa);
            sqlCommand.Parameters.AddWithValue("@LiczbaZmianRysunku", 0);
            sqlCommand.Parameters.AddWithValue("@LiczbaZmianPolozeniaRysunku", 0);
            sqlCommand.Parameters.AddWithValue("@LiczbaZmianRozmiaru", 0);
            sqlCommand.Parameters.AddWithValue("@CzasMaksymanlny", 0);
            sqlCommand.Parameters.AddWithValue("@CzasMinimalny", 0);
            connection.Open();
            sqlCommand.ExecuteNonQuery();

            connection.Close();
            DetailsView1.DataBind();
        }
        protected void DivParentOnMouseOut(object sender, EventArgs e)
        {
            string script = "alert('This is an alert message.');";
            ClientScript.RegisterStartupScript(this.GetType(), "Alert", script, true);
            Session.Abandon();
        }
        
        protected void Timer1_Tick(object sender, EventArgs e)
        {

            WypelnijDropDownList1();
            //WypelnijDetailsView1();
            /*
            Uzytkownik aktualnyUser = getCurrentUser();
            // || aktualnyUser.Nazwa.Equals(nazwaDropDown)
            if (aktualnyUser != null)
            {
                //dodaj nazwe user w label
                LabelUser.Text = aktualnyUser.Nazwa;
                WypelnijDetailsView1(aktualnyUser.Nazwa);
            }
            string wybranyChanged = (string)Session["wybranyChanged"];
            if (wybranyChanged != null)
            {
                //Uzytkownik nowyUser = GetUzytkownik(wybranyChanged);
                WypelnijDetailsView1(wybranyChanged);
            }
            */
            DetailsView1.DataBind();
            NestedUpdatePanel.Update();
        }
    }
}