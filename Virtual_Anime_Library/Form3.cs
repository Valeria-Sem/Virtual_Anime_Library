using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Virtual_Anime_Library
{
    public partial class Form3 : Form
    {
        bool click = true;   // мой любимый костыль_чтобы фильтр не зацикливался 
      
        static string ConnectionString = @"Data Source=LAPTOP-I9URQIMV\SQLEXPRESS;Initial Catalog=Anime;Integrated Security=True";
        SqlConnection conn = new SqlConnection(ConnectionString);
        SqlCommand com;
        SqlDataReader reader;

        string CommandText = "Select * from anime";
        static DataSet ds = new DataSet();
        static DataTable dt = new DataTable();
        static SqlDataAdapter dataAdapter;
        BindingSource bs;

        public Form3()
        {
            InitializeComponent();
            dataAdapter = new SqlDataAdapter(CommandText, ConnectionString);
            dataAdapter.Fill(ds, "anime");
            dataGridView1.DataSource = ds.Tables["anime"].DefaultView;

            bs = new BindingSource();
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dt);
            bs.DataSource = dt;
            dataGridView1.DataSource = bs;
            bindingNavigator1.BindingSource = bs;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            Form2 frm2 = new Form2();
            frm2.Show();
        }

        private void фильтрToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (click)
            {
                ///////////////////////////// Фильтр по жанрам ///////////////////////////////////////////////////////
                conn.Open();
                com = new SqlCommand("Select * from genre ", conn);

                reader = com.ExecuteReader();

                List<string[]> dataGanre = new List<string[]>();

                while (reader.Read())
                {
                    dataGanre.Add(new string[1]);

                    dataGanre[dataGanre.Count - 1][0] = reader[0].ToString();
                }
                reader.Close();
                conn.Close();

                foreach (string[] generList in dataGanre)        // generList - переменная цикла, которая перебирает записи из жанров
                {
                    поЖанрамToolStripMenuItem.DropDownItems.Add(String.Join("", generList));
                }

                ///////////////////////////// Фильтр по дате ///////////////////////////////////////////////////////

                conn.Open();
                com = new SqlCommand("Select * from date ", conn);

                reader = com.ExecuteReader();
                List<string[]> dataDate = new List<string[]>();

                while (reader.Read())
                {
                    dataDate.Add(new string[1]);

                    dataDate[dataDate.Count - 1][0] = reader[0].ToString();
                }

                reader.Close();
                conn.Close();

                foreach (string[] dateList in dataDate)
                {
                    поГодуВыпускаToolStripMenuItem.DropDownItems.Add(String.Join("", dateList));
                }

                ///////////////////////////// Фильтр по студиям ///////////////////////////////////////////////////////

                conn.Open();
                com = new SqlCommand("Select * from studio ", conn);
                dataAdapter.SelectCommand = com;

                reader = com.ExecuteReader();
                List<string[]> dataStudio = new List<string[]>();

                while (reader.Read())
                {
                    dataStudio.Add(new string[1]);

                    dataStudio[dataStudio.Count - 1][0] = reader[0].ToString();
                }

                reader.Close();
                conn.Close();

                foreach (string[] studioList in dataStudio)
                {
                    поСтудииToolStripMenuItem.DropDownItems.Add(String.Join("", studioList));
                }

                ///////////////////////////// Фильтр по возрастным ограничениям ///////////////////////////////////////////////////////

                conn.Open();
                com = new SqlCommand("Select * from age ", conn);

                reader = com.ExecuteReader();
                List<string[]> dataAge = new List<string[]>();

                while (reader.Read())
                {
                    dataAge.Add(new string[1]);

                    dataAge[dataAge.Count - 1][0] = reader[0].ToString();
                }

                reader.Close();
                conn.Close();

                foreach (string[] ageList in dataAge)
                {
                    поВозрастнымОграничениямToolStripMenuItem.DropDownItems.Add(String.Join("", ageList));
                }

                ///////////////////////////// Фильтр по рейтингу ///////////////////////////////////////////////////////

                conn.Open();
                com = new SqlCommand("Select * from rating ", conn);

                reader = com.ExecuteReader();
                List<string[]> dataRating = new List<string[]>();

                while (reader.Read())
                {
                    dataRating.Add(new string[1]);

                    dataRating[dataRating.Count - 1][0] = reader[0].ToString();
                }

                reader.Close();
                conn.Close();

                foreach (string[] ratingList in dataRating)
                {
                    поРейтингуToolStripMenuItem.DropDownItems.Add(String.Join("", ratingList));
                }

                ///////////////////////////// Фильтр по сезону ///////////////////////////////////////////////////////

                conn.Open();
                com = new SqlCommand("Select * from seasons ", conn);

                reader = com.ExecuteReader();
                List<string[]> dataSeasons = new List<string[]>();

                while (reader.Read())
                {
                    dataSeasons.Add(new string[1]);

                    dataSeasons[dataSeasons.Count - 1][0] = reader[0].ToString();
                }

                reader.Close();
                conn.Close();

                foreach (string[] seasonsList in dataSeasons)
                {
                    поСезонамToolStripMenuItem.DropDownItems.Add(String.Join("", seasonsList));
                }

                ///////////////////////////// Фильтр по популярности ///////////////////////////////////////////////////////

                conn.Open();
                com = new SqlCommand("Select * from popularity ", conn);

                reader = com.ExecuteReader();
                List<string[]> dataPopularity = new List<string[]>();

                while (reader.Read())
                {
                    dataPopularity.Add(new string[1]);

                    dataPopularity[dataPopularity.Count - 1][0] = reader[0].ToString();
                }

                reader.Close();
                conn.Close();

                foreach (string[] popularityList in dataPopularity)
                {
                    поПопулярностиToolStripMenuItem.DropDownItems.Add(String.Join("", popularityList));
                }
            }

            click = false;
        }

        private void поЖанрамToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            conn.Open();

            var item = e.ClickedItem;

            com = new SqlCommand("Select * from anime where Жанр = @item", conn);
            com.Parameters.Add("@item", SqlDbType.NVarChar).Value = item.Text;
          
            dataAdapter.SelectCommand = com;
            ds.Reset();
            dataAdapter.Fill(ds, "anime");
            dataGridView1.DataSource = ds.Tables["anime"].DefaultView;

            if (ds.Tables["anime"].Rows.Count == 0)
            {
                (поЖанрамToolStripMenuItem as ToolStripDropDownItem).HideDropDown();
                MessageBox.Show("Аниме данного жанра нету в базе");
            }

            conn.Close();
        }

        private void поГодуВыпускаToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            conn.Open();

            var item = e.ClickedItem;

            com = new SqlCommand("Select * from anime where [Год выпуска] = @item", conn);
            com.Parameters.Add("@item", SqlDbType.NVarChar).Value = item.Text;

            dataAdapter.SelectCommand = com;
            ds.Reset();
            dataAdapter.Fill(ds, "anime");
            dataGridView1.DataSource = ds.Tables["anime"].DefaultView;

            if (ds.Tables["anime"].Rows.Count == 0)
            {
                (поГодуВыпускаToolStripMenuItem as ToolStripDropDownItem).HideDropDown();
                MessageBox.Show("Аниме данного года нету в базе");
            }

            conn.Close();
        }

        private void поСтудииToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            conn.Open();

            var item = e.ClickedItem;

            com = new SqlCommand("Select * from anime where Студия = @item", conn);
            com.Parameters.Add("@item", SqlDbType.NVarChar).Value = item.Text;

            dataAdapter.SelectCommand = com;
            ds.Reset();
            dataAdapter.Fill(ds, "anime");
            dataGridView1.DataSource = ds.Tables["anime"].DefaultView;

            if (ds.Tables["anime"].Rows.Count == 0)
            {
                (поСтудииToolStripMenuItem as ToolStripDropDownItem).HideDropDown();
                MessageBox.Show("Аниме данной студии нету в базе");
            }

            conn.Close();
        }

        private void поВозрастнымОграничениямToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            conn.Open();

            var item = e.ClickedItem;

            com = new SqlCommand("Select * from anime where [Возрастные ограничения] = @item", conn);
            com.Parameters.Add("@item", SqlDbType.NVarChar).Value = item.Text;

            dataAdapter.SelectCommand = com;
            ds.Reset();
            dataAdapter.Fill(ds, "anime");
            dataGridView1.DataSource = ds.Tables["anime"].DefaultView;

            if (ds.Tables["anime"].Rows.Count == 0)
            {
                (поВозрастнымОграничениямToolStripMenuItem as ToolStripDropDownItem).HideDropDown();
                MessageBox.Show("Аниме данного возрастного ограничения нету в базе");
            }

            conn.Close();
        }

        private void поРейтингуToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            conn.Open();

            var item = e.ClickedItem;

            com = new SqlCommand("Select * from anime where Рейтинг = @item", conn);
            com.Parameters.Add("@item", SqlDbType.NVarChar).Value = item.Text;

            dataAdapter.SelectCommand = com;
            ds.Reset();
            dataAdapter.Fill(ds, "anime");
            dataGridView1.DataSource = ds.Tables["anime"].DefaultView;

            if (ds.Tables["anime"].Rows.Count == 0)
            {
                (поРейтингуToolStripMenuItem as ToolStripDropDownItem).HideDropDown();
                MessageBox.Show("Аниме данного рейтинга нету в базе");
            }

            conn.Close();
        }

        private void поСезонамToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            conn.Open();

            var item = e.ClickedItem;

            com = new SqlCommand("Select * from anime where Сезоны = @item", conn);
            com.Parameters.Add("@item", SqlDbType.NVarChar).Value = item.Text;

            dataAdapter.SelectCommand = com;
            ds.Reset();
            dataAdapter.Fill(ds, "anime");
            dataGridView1.DataSource = ds.Tables["anime"].DefaultView;

            if (ds.Tables["anime"].Rows.Count == 0)
            {
                (поСезонамToolStripMenuItem as ToolStripDropDownItem).HideDropDown();
                MessageBox.Show("Аниме данного сезона нету в базе");
            }

            conn.Close();
        }

        private void поПопулярностиToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            conn.Open();

            var item = e.ClickedItem;

            com = new SqlCommand("Select * from anime where Популярность = @item", conn);
            com.Parameters.Add("@item", SqlDbType.NVarChar).Value = item.Text;

            dataAdapter.SelectCommand = com;
            ds.Reset();
            dataAdapter.Fill(ds, "anime");
            dataGridView1.DataSource = ds.Tables["anime"].DefaultView;

            if (ds.Tables["anime"].Rows.Count == 0)
            {
                (поПопулярностиToolStripMenuItem as ToolStripDropDownItem).HideDropDown();
                MessageBox.Show("Аниме данной популярности нету в базе");
            }

            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            com = new SqlCommand("Select * from anime", conn);
            dataAdapter.SelectCommand = com;
            ds.Reset();
            dataAdapter.Fill(ds, "anime");
            dataGridView1.DataSource = ds.Tables["anime"].DefaultView;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            Form5 frm5 = new Form5();
            frm5.Show();
            Hide();
        }

        private void Form3_Activated(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            conn.Open();
            com = new SqlCommand("Delete from anime where [ID аниме] ="+ l1.Text + "", conn);
            com.ExecuteNonQuery();

            com = new SqlCommand("Select * from anime", conn);
            dataAdapter.SelectCommand = com;
            ds.Reset();
            dataAdapter.Fill(ds, "anime");
            dataGridView1.DataSource = ds.Tables["anime"].DefaultView;

            conn.Close();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                DisplayInfo(row);
            }
        }

        private void DisplayInfo(DataGridViewRow row)               
        {
            l1.Text = row.Cells["ID аниме"].Value.ToString();
        }

        private void сохранитьВФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(@"d:KOLLEGE\\anime\\Anime.doc", FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fs);

            try
            {
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    for (int i = 0; i < dataGridView1.Rows[j].Cells.Count; i++)
                    {
                        streamWriter.Write("{ " + dataGridView1.Rows[j].Cells[i].Value + " }");
                    }

                    streamWriter.WriteLine();
                }

                streamWriter.Close();
                fs.Close();

                MessageBox.Show("Файл успешно сохранен");
            }
            catch
            {
                MessageBox.Show("Ошибка при сохранении файла!");
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7 frm7 = new Form7();
            frm7.Show();
            Hide();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 frm6 = new Form6();
            frm6.Show();
            this.Hide();
        }
    }
}
