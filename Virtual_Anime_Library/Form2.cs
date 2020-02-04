using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Virtual_Anime_Library
{
    public partial class Form2 : Form
    {
        static string ConnectionString = @"Data Source=LAPTOP-I9URQIMV\SQLEXPRESS;Initial Catalog=Anime;Integrated Security=True";
        SqlConnection conn = new SqlConnection(ConnectionString);
        SqlCommand com;
        SqlDataReader reader;

        public Form2()
        {
            InitializeComponent();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 frm1 = new Form1();
            frm1.Show();
        }

        private void button1_Click(object sender, EventArgs e)      //  Вход в систему
        {
            conn.Open();

            com = new SqlCommand("Select * from registration where Login = @Login and Password = @Password", conn);
            com.Parameters.Add("@Login", SqlDbType.VarChar).Value = textBox1.Text;
            com.Parameters.Add("@Password", SqlDbType.VarChar).Value = textBox2.Text;

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
            else
            {
                MessageBox.Show("Неверно введён логин или пароль\nПовторите попытку");
                textBox1.Clear();
                textBox2.Clear();
                reader.Close();
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)      // Регистрация
        {
            Form4 frm4 = new Form4();
            frm4.Show();
            this.Hide();
        }
    }
}
