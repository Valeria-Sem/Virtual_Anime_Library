using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System;

namespace Virtual_Anime_Library
{
    public partial class Form6 : Form
    {
        static string ConnectionString = @"Data Source=LAPTOP-I9URQIMV\SQLEXPRESS;Initial Catalog=Anime;Integrated Security=True";
        SqlConnection conn = new SqlConnection(ConnectionString);
        SqlCommand com;
        SqlDataReader reader;

        public Form6()
        {
            InitializeComponent();
            string path = Application.StartupPath + @"\anima.html";
            webBrowser1.Navigate(path);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            conn.Open();
            SqlCommand com = new SqlCommand("Select * from registration", conn);

            reader = com.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                int id = Convert.ToInt32(reader[0]);
                reader.Close();

                if (id == 1)                                // проверка айдишника для админа
                {
                    Form3 frm3 = new Form3();
                    frm3.Show();
                    frm3.bindingNavigator1.Visible = true;
                    this.Hide();
                }
                else                                        // проверка айдишника для пользователя
                {
                    Form3 frm3 = new Form3();
                    frm3.Show();
                    this.Hide();
                }
                reader.Close();
                conn.Close();
            }
        }
    }
}
