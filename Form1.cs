using Google.Protobuf.Reflection;
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

namespace qwerty222
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            getInfo();
        }

        public void getInfo()
        {
            string querry = ("SELECT id as 'номер', name as 'название', price as 'цена' from product");
            MySqlConnection conn = Class2.GetSqlConnection();
            MySqlDataAdapter ada = new MySqlDataAdapter(querry, conn);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                ada.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            string addcar = "insert into product(name, price) values('" + name.Text + "'," + price.Text + ");";
            MySqlConnection conn = Class2.GetSqlConnection();
            MySqlCommand cmDB = new MySqlCommand(addcar, conn);
            try
            {
                conn.Open();
                MySqlDataReader rd = cmDB.ExecuteReader();
                conn.Close();
                MessageBox.Show(" Я овощь.");
                getInfo();
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка.");
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            string update = "update product set name = '" + name.Text +"', price = " + price.Text + " where id = "+ dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString() + ";";
            MySqlConnection conn = Class2.GetSqlConnection();
            MySqlCommand cmDB = new MySqlCommand(update, conn);
            try
            {
                conn.Open();
                cmDB.ExecuteReader();
                conn.Close();
                MessageBox.Show("данные обновлены");
                getInfo();
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка.");
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            name.Text = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            price.Text = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString(); 
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }
    }
}
