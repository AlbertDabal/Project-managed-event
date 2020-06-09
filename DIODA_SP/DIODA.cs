using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;



namespace DIODA_SP
{
    //https://www.highlite.com/en/products/cables-connectors/powerdistribution/240v-extensioncords.html

    //SMD1 wymiary 0,5m x 0,5m  //sygnal 15 kostek
    //SMD2 wymiary 0,5m x 0,5m 
    //SMD3 wymiary 0,5m x 0,5m 
    //SMD4 wymiary 1m x 0,5m

    //**************************************************************

    

    //POBOR PRADU
    //>>SMD1<<
    //Średnie: 320W / m² Max : 740W / m² https://ledsee.pl/ekran-led-391-zew-standard
    //>>SMD2<<
    //Średnie: 200W/m² Max: 600W/m² https://ledsee.pl/ekran-led-481-standard
    //>>SMD3<<
    //Średnie: 140W / m² Max : 420W / m² https://ledsee.pl/ekran-led-p6
    //>>SMD4<<
    //Średnie: 400W / m² Max : 1200W / m² https://ledsee.pl/ekran-led-p6



    public partial class DIODA : Form
    {


        public DIODA()
        {
            InitializeComponent();
            
        }

        private void MENU_Load(object sender, EventArgs e)
        {

        }
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<   MENU GŁÓWNE button1,button2,button3,button4,button5   >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        private void button1_Click(object sender, EventArgs e)
        {
            NOWY_EVENT.Visible = true;

            NazwaEvent.Text = "";
            szerokosc.Text = "";
            wysokosc.Text = "";

            SMD1.Checked = false;
            SMD2.Checked = false;
            SMD3.Checked = false;
        }

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<   WYSWIETLENIE ZAWARTOSCI MAGAZYNU  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        private void button2_Click(object sender, EventArgs e)
        {
            MAGAZYN.Visible = true;

            
            string myConnectionString = "server=localhost;user=root;database=sport_media";

            var pol = new MySqlConnection(myConnectionString);



            pol.Open();

            string zapytanieSQL = "SELECT * FROM mag1";

            MySqlCommand komenda = new MySqlCommand(zapytanieSQL, pol);
            MySqlDataAdapter czytnik = new MySqlDataAdapter("SELECT * FROM mag1", pol);

            DataTable dtbl = new DataTable();
            czytnik.Fill(dtbl);

            tabela_dioda.DataSource = dtbl;

            pol.Close();


        }

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<  ZNAJDZ PRZEDMIOT  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        private void button3_Click(object sender, EventArgs e)
        {
            ZNAJDZ_PRZEDMIOT.Visible = true;
        }
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<   WYSWIETLENIE AKTUALNYCH EVENTOW  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        private void button4_Click(object sender, EventArgs e)
        {
           

            AKTAULNE_EVENTY.Visible = true;

           

            string myConnectionString = "server=localhost;user=root;database=sport_media";

            var pol = new MySqlConnection(myConnectionString);

            

            pol.Open();

            string zapytanieSQL = "SHOW TABLES";

            MySqlCommand komenda = new MySqlCommand(zapytanieSQL, pol);
            MySqlDataReader czytnik = komenda.ExecuteReader();

            listBoxAktualne.Items.Clear();

            if (czytnik.HasRows)
            {
                



                while (czytnik.Read())
                {
                    if (czytnik[0].ToString() == "mag1")
                    {

                    }
                    else
                    {
                        listBoxAktualne.Items.Add(czytnik[0].ToString());
                    }
                  
                }
                czytnik.Close();
            }
            else
            {
                MessageBox.Show("Nie Dziala");
            }


            pol.Close();
        }

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<  EVENTY DO USUNIECIA  >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        private void button5_Click(object sender, EventArgs e)
        {
            string myConnectionString = "server=localhost;user=root;database=sport_media";

            var pol = new MySqlConnection(myConnectionString);



            pol.Open();

            string zapytanieSQL = "SHOW TABLES";

            MySqlCommand komenda = new MySqlCommand(zapytanieSQL, pol);
            MySqlDataReader czytnik = komenda.ExecuteReader();

            listBoxUsun.Items.Clear();

            if (czytnik.HasRows)
            {
               



                while (czytnik.Read())
                {
                    if (czytnik[0].ToString() == "mag1")
                    {

                    }
                    else
                    {
                        listBoxUsun.Items.Add(czytnik[0].ToString());
                    }

                }
                czytnik.Close();
            }
            else
            {
                MessageBox.Show("Nie Dziala");
            }


            pol.Close();














            USUN_EVENT.Visible = true;
        }
        
        
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<KONIEC MENU>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<  PANEL NOWY EVENT Z WYNIK.DIODA >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public void button6_Click(object sender, EventArgs e)
        {

            WYNIK_DIODA myNewForm = new WYNIK_DIODA();

            myNewForm.Visible = true;

            NOWY_EVENT.Visible = false;


            dioda_led.dioda = NazwaEvent.Text;



            myNewForm.nazwaevent.Text = dioda_led.dioda;



            Diody.dioda[] smd = DaneDiody();

            dioda_led.szerokosc = double.Parse(szerokosc.Text.ToString());


            dioda_led.wysokosc = double.Parse(wysokosc.Text.ToString());

            string rodzaj_diody="1";

            int potrzeba = 0;

            double pole = 0;

            double maxpobor = 0;

            double smallpole = 0;

            string myConnectionString = "server=localhost;user=root;database=sport_media";


            var pol = new MySqlConnection(myConnectionString);


            pol.Open();


            string zapytanieSQL1 = "CREATE TABLE " + dioda_led.dioda + "(id int(100) NOT NULL,nazwa varchar(100),ilosc int(100),PRIMARY KEY(id))";
            MySqlCommand komenda = new MySqlCommand(zapytanieSQL1, pol);
            komenda.Prepare();
            komenda.ExecuteNonQuery();

            pol.Close();

            double kabinet = 0;


            myNewForm.SMDilosc.Text = "";
            myNewForm.pobor.Text = "";
            myNewForm.pobor.Text += Environment.NewLine;

            if (SMD1.Checked)
            {


                pole = dioda_led.szerokosc * dioda_led.wysokosc;



                rodzaj_diody = SMD1.Text;



                dioda_led.szerokosc = dioda_led.szerokosc / smd[1].szerokosc;
                dioda_led.wysokosc = dioda_led.wysokosc / smd[1].wysokosc;


                dioda_led.ilosckabinetow = dioda_led.szerokosc * dioda_led.wysokosc;


                kabinet = dioda_led.ilosckabinetow;


                myNewForm.SMDilosc.Text += "KABINET SMD 1" + Environment.NewLine + "ILOSC: " + dioda_led.ilosckabinetow.ToString();

                //POBÓR DIODY SMD 1

                myNewForm.pobor.Text += "POBOR DIODY SMD 1";

                maxpobor = Pobor(myNewForm, pole, smd[1].pobor, smd[1].maxpobor);

            }
            else if (SMD2.Checked)
            {

                pole = dioda_led.szerokosc * dioda_led.wysokosc;

                //POBÓR DIODY SMD 2
                myNewForm.pobor.Text += "POBOR DIODY SMD 2";

                maxpobor = Pobor(myNewForm, pole, smd[2].pobor, smd[2].maxpobor);

                rodzaj_diody = SMD2.Text;

                dioda_led.szerokosc = dioda_led.szerokosc / smd[2].szerokosc;
                dioda_led.wysokosc = dioda_led.wysokosc / smd[2].wysokosc;


                dioda_led.ilosckabinetow = dioda_led.szerokosc * dioda_led.wysokosc;

                kabinet = dioda_led.ilosckabinetow;

                myNewForm.SMDilosc.Text += "KABINET SMD 2" + Environment.NewLine + "ILOSC: " + dioda_led.ilosckabinetow.ToString();








            }
            else if (SMD3.Checked)
            {
                potrzeba = WymiarDiody(dioda_led.wysokosc, smd[4].wysokosc);

                dioda_led.szerokosc = dioda_led.szerokosc / smd[3].szerokosc;

                pole = dioda_led.szerokosc * dioda_led.wysokosc;




                if (potrzeba == 1)
                {


                    kabinet += dioda_led.szerokosc;


                    //POBÓR DIODY SMD 3

                    smallpole = dioda_led.szerokosc * 0.5;

                    //POBÓR DIODY SMD 3

                    myNewForm.pobor.Text += "POBOR DIODY SMD 3";

                    maxpobor = Pobor(myNewForm, smallpole, smd[3].pobor, smd[3].maxpobor);












                    dioda_led.wysokosc = dioda_led.wysokosc - smd[3].wysokosc;

                    dioda_led.wysokosc = dioda_led.wysokosc / smd[4].wysokosc;

                    myNewForm.SMDilosc.Text += "KABINET SMD 3" + Environment.NewLine + "ILOSC: " + dioda_led.szerokosc.ToString()+ Environment.NewLine;

                    

                    string zapytanie3 = "INSERT INTO " + dioda_led.dioda + " (id,nazwa,ilosc) VALUES ('2','SMD 3','" + dioda_led.szerokosc.ToString() + "');";

                    NowyElement(myNewForm, zapytanie3);




                }

                rodzaj_diody = "SMD 4";



                pole = pole - smallpole;

                myNewForm.pobor.Text += "POBOR DIODY SMD 4";

                maxpobor = Pobor(myNewForm, smallpole, smd[4].pobor, smd[4].maxpobor);








                dioda_led.ilosckabinetow = dioda_led.szerokosc * dioda_led.wysokosc;


                kabinet += dioda_led.ilosckabinetow;

                myNewForm.SMDilosc.Text += "KABINET SMD 4" + Environment.NewLine + "ILOSC: " + dioda_led.ilosckabinetow.ToString();


            }


            string zapytanie2 = "INSERT INTO " + dioda_led.dioda + " (id,nazwa,ilosc) VALUES ('1','" + rodzaj_diody + "','" + dioda_led.ilosckabinetow.ToString() + "');";

            NowyElement(myNewForm, zapytanie2);

            pol.Close();





            int pion = 0;
            double kwpobor = maxpobor / 1000;
            do
            {
                pion++;
                kwpobor = kwpobor - 21;

            } while (kwpobor > 21);


            myNewForm.pobor.Text += Environment.NewLine;
            

            myNewForm.pobor.Text += Environment.NewLine;



            int ilosc_powercon = 0;
            double powercon = maxpobor / 3680;

            do
            {
                ilosc_powercon++;
                powercon--;
            } while (powercon > 1);

           

            double pc = ((kabinet * 2) - ilosc_powercon) - 1;

            int rj45 = 0;



            //jedna rj45 moze byc na 15 kabinetow

            for (int t = 0; t < kabinet; t = t + 15)
            {
                rj45++;
            }

            double etercon;
            etercon = ((kabinet * 2) - rj45) - 1;

            Zapytania(myNewForm, pion, ilosc_powercon, pc, rj45, etercon);


            int kabinet_rys = 0;

            


            if (SMD1.Checked || SMD2.Checked)
            {
                int columnCount = Convert.ToInt32(dioda_led.szerokosc);
                int rowCount = Convert.ToInt32(dioda_led.wysokosc);

                //Clear out the existing controls, we are generating a new table layout
                myNewForm.tableLayoutPanel1.Controls.Clear();

                //Clear out the existing row and column styles
                myNewForm.tableLayoutPanel1.ColumnStyles.Clear();
                myNewForm.tableLayoutPanel1.RowStyles.Clear();

                //Now we will generate the table, setting up the row and column counts first
                myNewForm.tableLayoutPanel1.ColumnCount = columnCount;
                myNewForm.tableLayoutPanel1.RowCount = rowCount;

                myNewForm.tableLayoutPanel1.AutoScroll = true;

                for (int x = 0; x < columnCount; x++)
                {

                    //First add a column
                    myNewForm.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

                    for (int y = 0; y < rowCount; y++)
                    {
                        kabinet_rys++;
                        //Next, add a row.  Only do this when once, when creating the first column


                        myNewForm.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));




                        //Create the control, in this case we will add a button
                        Label cmd = new Label();
                        cmd.Size = new System.Drawing.Size(100, 100);
                        cmd.Font = new Font("Arial", 20, FontStyle.Bold);
                        cmd.Margin = new Padding(10, 10, 10, 10);
                        cmd.BackColor = Color.Blue;
                        cmd.Text = kabinet_rys.ToString();         //Finally, add the control to the correct location in the table
                        myNewForm.tableLayoutPanel1.Controls.Add(cmd);


                    }
                }




            }
            else
            {
                kabinet_rys = 0;

                int rowCount1 = Convert.ToInt32(dioda_led.wysokosc);

                

                int columnCount1 = Convert.ToInt32(dioda_led.szerokosc);
                //Clear out the existing controls, we are generating a new table layout
                myNewForm.tableLayoutPanel1.Controls.Clear();

                //Clear out the existing row and column styles
                myNewForm.tableLayoutPanel1.ColumnStyles.Clear();
                myNewForm.tableLayoutPanel1.RowStyles.Clear();

                //Now we will generate the table, setting up the row and column counts first
                myNewForm.tableLayoutPanel1.ColumnCount = columnCount1;
                myNewForm.tableLayoutPanel1.RowCount = rowCount1;
                myNewForm.tableLayoutPanel1.AutoScroll = true;

                if (potrzeba == 1)
                {


                   
                    

                    


                        //First add a column
                        myNewForm.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

                        for (int y = 0; y < columnCount1; y++)
                        {
                            kabinet_rys++;
                            //Next, add a row.  Only do this when once, when creating the first column


                            myNewForm.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));




                            //Create the control, in this case we will add a button
                            Label small = new Label();
                            small.Size = new System.Drawing.Size(100, 100);
                        small.Font = new Font("Arial", 20, FontStyle.Bold);
                            small.Margin = new Padding(10, 10, 10, 10);
                            small.BackColor = Color.Red;
                            small.Text = kabinet_rys.ToString();         //Finally, add the control to the correct location in the table
                            myNewForm.tableLayoutPanel1.Controls.Add(small);
                            
                        }
                    

                }


               
               

               

                for (int x = 0; x < columnCount1; x++)
                {

                    //First add a column
                    myNewForm.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

                    for (int y = 0; y < rowCount1; y++)
                    {
                        kabinet_rys++;
                        //Next, add a row.  Only do this when once, when creating the first column


                        myNewForm.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));




                        //Create the control, in this case we will add a button
                        Label cmd = new Label();
                        cmd.Size = new System.Drawing.Size(100, 200);
                        cmd.Font = new Font("Arial", 20, FontStyle.Bold);
                        cmd.Margin = new Padding(10, 10, 10, 10);
                        cmd.BackColor = Color.Blue;
                        cmd.Text = kabinet_rys.ToString();         //Finally, add the control to the correct location in the table
                        myNewForm.tableLayoutPanel1.Controls.Add(cmd);

                    }

                }








                NazwaEvent.Text = "";
                szerokosc.Text = "";
                wysokosc.Text = "";

                SMD1.Checked = false;
                SMD2.Checked = false;
                SMD3.Checked = false;





            }

























        }
        //<<<<<<<<<<<< DANE DIODY!!!!!!!!!!!!!!!!!>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        private static Diody.dioda[] DaneDiody()
        {
            Diody.dioda[] smd = new Diody.dioda[5];

            smd[1].id_dioda = 1;
            smd[1].nazwa = "SMD1";
            smd[1].szerokosc = 0.5;
            smd[1].wysokosc = 0.5;
            smd[1].pobor = 320;
            smd[1].maxpobor = 740;

            smd[2].id_dioda = 2;
            smd[2].nazwa = "SMD2";
            smd[2].szerokosc = 0.5;
            smd[2].wysokosc = 0.5;
            smd[2].pobor = 200;
            smd[2].maxpobor = 600;

            smd[3].id_dioda = 3;
            smd[3].nazwa = "SMD3";
            smd[3].szerokosc = 0.5;
            smd[3].wysokosc = 0.5;
            smd[3].pobor = 140;
            smd[3].maxpobor = 420;

            smd[4].id_dioda = 4;
            smd[4].nazwa = "SMD4";
            smd[4].szerokosc = 0.5;
            smd[4].wysokosc = 1;
            smd[4].pobor = 400;
            smd[4].maxpobor = 1200;
            return smd;
        }

        private static void Zapytania(WYNIK_DIODA myNewForm, int pion, int ilosc_powercon, double pc, int rj45, double etercon)
        {
            string zapytanie4 = "INSERT INTO " + dioda_led.dioda + " (id,nazwa,ilosc) VALUES ('3','Pion','" + pion + "');";

            NowyElement(myNewForm, zapytanie4);

            string zapytanie5 = "INSERT INTO " + dioda_led.dioda + " (id,nazwa,ilosc) VALUES ('4','powercon 230V','" + ilosc_powercon + "');";

            NowyElement(myNewForm, zapytanie5);

            string zapytanie6 = "INSERT INTO " + dioda_led.dioda + " (id,nazwa,ilosc) VALUES ('5','powercon','" + pc + "');";

            NowyElement(myNewForm, zapytanie6);

            string zapytanie7 = "INSERT INTO " + dioda_led.dioda + " (id,nazwa,ilosc) VALUES ('6','etercon','" + etercon + "');";

            NowyElement(myNewForm, zapytanie7);

            string zapytanie8 = "INSERT INTO " + dioda_led.dioda + " (id,nazwa,ilosc) VALUES ('7','rozdzielnia','" + pion + "');";

            NowyElement(myNewForm, zapytanie8);

            string zapytanie9 = "INSERT INTO " + dioda_led.dioda + " (id,nazwa,ilosc) VALUES ('8','rj45','" + rj45 + "');";

            NowyElement(myNewForm, zapytanie9);
        }

        private static void NowyElement(WYNIK_DIODA myNewForm,string zapytanie)
        {
            string myConnectionString = "server=localhost;user=root;database=sport_media";


            var polocznie = new MySqlConnection(myConnectionString);


            polocznie.Open();


            MySqlCommand komenda = new MySqlCommand(zapytanie, polocznie);
            komenda.Prepare();
            komenda.ExecuteNonQuery();

            polocznie.Close();
        }

        private static double Pobor(WYNIK_DIODA myNewForm, double pole, double pobor, double maxpobor)
        {
            


            pobor = pole * pobor;


            maxpobor = pole * maxpobor;

           

            myNewForm.pobor.Text += Environment.NewLine;

            myNewForm.pobor.Text += "SREDNI POBOR TO: " + pobor.ToString() + "V";

            myNewForm.pobor.Text += Environment.NewLine;

            myNewForm.pobor.Text += "MAX POBOR TO: " + maxpobor.ToString() + "V";

            myNewForm.pobor.Text += Environment.NewLine;

            myNewForm.pobor.Text += Environment.NewLine;

            return maxpobor;
        }


        

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MAGAZYN_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            MAGAZYN.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            USUN_EVENT.Visible = false;
        }

        private void USUN_EVENT_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            AKTAULNE_EVENTY.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ZNAJDZ_PRZEDMIOT.Visible = false;

            szukaj_box.Text = "";
        }

        private void ZNAJDZ_PRZEDMIOT_Paint(object sender, PaintEventArgs e)
        {

        }

        //DODAJ EVENT
        

        private void NazwaEvent_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void szerokosc_TextChanged(object sender, EventArgs e)
        {

        }

        private void wysokosc_TextChanged(object sender, EventArgs e)
        {

        }

        public static int WymiarDiody(double wymiar,double wysokosc)
        {
            for (int g = 1; g < wymiar; g++)
            {

                wysokosc++;
            }
           
            int a = 0;
            if (wysokosc > wymiar)
            {
                a = 1;
            }
            return a;

        }

        private void tabela_dioda_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

       
        //SZUKANIE ELEMENTU NA FIRMIE
        private void button11_Click(object sender, EventArgs e)
        {
            string myConnectionString = "server=localhost;user=root;database=sport_media";

            var pol = new MySqlConnection(myConnectionString);

            string nazwa_mag = szukaj_box.Text;

            pol.Open();

            string zapytanieSQL = "SELECT ilosc FROM mag1 WHERE nazwa='"+nazwa_mag+"'";

            MySqlCommand komenda = new MySqlCommand(zapytanieSQL, pol);
            MySqlDataReader czytnik = komenda.ExecuteReader();

            if (czytnik.HasRows)
            {
                MessageBox.Show("Znaleziono");

                while (czytnik.Read())
                {

                    wynik_mag.Text = "ILOSC ELEMENTU:" + czytnik[0].ToString();


                }


              


            }







            pol.Close();
            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void szukaj_box_TextChanged(object sender, EventArgs e)
        {

        }

        private void wynik_mag_Click(object sender, EventArgs e)
        {

        }

        

        private void listBoxAktualne_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Aktaulny_wybierz_Click(object sender, EventArgs e)
        {
            string ewent = listBoxAktualne.SelectedItem.ToString();


            string myConnectionString = "server=localhost;user=root;database=sport_media";

            var pol = new MySqlConnection(myConnectionString);



            pol.Open();

            string zapytanieSQL = "SELECT * FROM "+ewent;

          

            MySqlCommand komenda = new MySqlCommand(zapytanieSQL, pol);
            MySqlDataAdapter czytnik = new MySqlDataAdapter("SELECT * FROM "+ewent, pol);

            DataTable dtbl = new DataTable();
            czytnik.Fill(dtbl);

            dany_event.DataSource = dtbl;


            pol.Close();
        




















        AKTUALNE_EVENTY_TABLE.Visible = true;
        AKTAULNE_EVENTY.Visible = false;
        }

        private void AKTUALNE_EVENTY_TABLE_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dany_event_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void usun_Click(object sender, EventArgs e)
        {
            string myConnectionString = "server=localhost;user=root;database=sport_media";

            var pol = new MySqlConnection(myConnectionString);

            string usun = listBoxUsun.SelectedItem.ToString();

            pol.Open();

            string zapytanieSQL = "DROP TABLE "+usun;

            MySqlCommand komenda = new MySqlCommand(zapytanieSQL, pol);
            komenda.Prepare();
            komenda.ExecuteNonQuery();

            pol.Close();



            MessageBox.Show("Usunieto tabele");

            USUN_EVENT.Visible = false;
        }

        private void listBoxUsun_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dIODAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DIODA_INFO.Visible = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DIODA_INFO.Visible = false;
        }

        private void aUTORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DIODA_AUTOR.Visible = true;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void eVENTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MENU.BackgroundImage = DIODA_SP.Properties.Resources.party;
        }

        private void zIELONEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MENU.BackgroundImage = DIODA_SP.Properties.Resources.nowa;
        }

        private void button13_Click(object sender, EventArgs e)
        {

            label11.ForeColor = Color.Blue;

        }

        private void button13_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                label11.ForeColor = Color.Red;
            }
        }

        private void MENU_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            AKTUALNE_EVENTY_TABLE.Visible = false;
        }
    }

    public class dioda_led
    {
        public static string dioda="";
        public static double szerokosc;
        public static double wysokosc;
        public static double ilosckabinetow;
        
    }
}

public class Diody
{
    public struct dioda
    {
        public int id_dioda;
        public string nazwa;
        public double szerokosc;
        public double wysokosc;
        public double pobor;    //pobor prądu średni
        public double maxpobor;  //pobor prądu maxymalny 

    };
}






