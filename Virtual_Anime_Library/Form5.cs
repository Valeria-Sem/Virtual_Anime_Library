using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Virtual_Anime_Library
{
    public partial class Form5 : Form
    {
        static string ConnectionString = @"Data Source=LAPTOP-I9URQIMV\SQLEXPRESS;Initial Catalog=Anime;Integrated Security=True";
        SqlConnection conn = new SqlConnection(ConnectionString);
        SqlCommand com;
        SqlDataReader reader;

        string CommandText = "Select * from anime";
        static DataSet ds = new DataSet();
        static DataTable dt = new DataTable();
        static SqlDataAdapter dataAdapter;

        public Form5()
        {
            InitializeComponent();
            dataAdapter = new SqlDataAdapter(CommandText, ConnectionString);
            dataAdapter.Fill(ds, "anime");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();

            com = new SqlCommand("Select count (*) from anime", conn);
            com = new SqlCommand("Select top 1 [ID аниме] from anime order by [ID аниме] desc", conn);

            reader = com.ExecuteReader();
            reader.Read();
            int id = Convert.ToInt32(reader[0]) + 1;
            reader.Close();

            com = new SqlCommand("INSERT INTO anime VALUES (@id, @Name , @Genres, @Dates, @Studios, @Ages, @Ratings, @Season, @Popularity)", conn);
            com.Parameters.Add("@id", SqlDbType.Int).Value = id;
            com.Parameters.Add("@Name", SqlDbType.NVarChar).Value = textBox1.Text;
            com.Parameters.Add("@Genres", SqlDbType.NVarChar).Value = textBox2.Text;
            com.Parameters.Add("@Dates", SqlDbType.NVarChar).Value = textBox3.Text;
            com.Parameters.Add("@Studios", SqlDbType.NVarChar).Value = textBox4.Text;
            com.Parameters.Add("@Ages", SqlDbType.NVarChar).Value = textBox5.Text;
            com.Parameters.Add("@Ratings", SqlDbType.NVarChar).Value = textBox6.Text;
            com.Parameters.Add("@Season", SqlDbType.NVarChar).Value = textBox7.Text;
            com.Parameters.Add("@Popularity", SqlDbType.NVarChar).Value = textBox8.Text;
            com.ExecuteNonQuery();

            com = new SqlCommand("Select * from anime", conn);
            dataAdapter.SelectCommand = com;
            ds.Reset();
            dataAdapter.Fill(ds, "anime");

            conn.Close();
            reader.Close();
            Close();
            Form3 frm3 = new Form3();
            frm3.Show();
        }
    }
}
