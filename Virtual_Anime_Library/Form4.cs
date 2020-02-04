using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Virtual_Anime_Library
{
    public partial class Form4 : Form
    {
        static string ConnectionString = @"Data Source=LAPTOP-I9URQIMV\SQLEXPRESS;Initial Catalog=Anime;Integrated Security=True";
        SqlConnection conn = new SqlConnection(ConnectionString);
        SqlCommand com;
        SqlDataReader reader;

        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null || textBox1.Text == "" || textBox2.Text == null || textBox2.Text == "")
            {
                MessageBox.Show("Error");
            }
            else
            {
                conn.Open();

                com = new SqlCommand("Select count (*) from registration", conn);
                com = new SqlCommand("Select top 1 ID from registration order by ID desc", conn);

                reader = com.ExecuteReader();

                reader.Read();
                int id = Convert.ToInt32(reader[0]) + 1;
                reader.Close();

                com = new SqlCommand("INSERT INTO registration VALUES (@id, @Login ,@Password)", conn);
                com.Parameters.Add("@id", SqlDbType.Int).Value = id;
                com.Parameters.Add("@Login", SqlDbType.VarChar).Value = textBox1.Text;
                com.Parameters.Add("@Password", SqlDbType.VarChar).Value = textBox2.Text;
                com.ExecuteNonQuery();

                conn.Close();
                reader.Close();
                this.Close();
                Form2 frm2 = new Form2();
                frm2.Show();
            }
        }
    }
}
