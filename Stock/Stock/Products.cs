using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Products_Load(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 0;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            LoadData();

        }

        //Add Button
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\AYASQL;Initial Catalog=stock;Integrated Security=True");
            con.Open();
            bool status = false;

            if (comboBox2.SelectedIndex == 0)
            {
                status = true;
            }
            else {
                status = false;
            }

            //UPDATE logic
            var sqlQuery = "";
            if (ifProductExist(con, textBox1.Text))
            {
                sqlQuery = @"UPDATE [dbo].[Products]  SET [ProductName] = '" + textBox2.Text + "' ,[ProductStatus] = '" + status + "' WHERE[ProducCode] ='" + textBox1.Text + "' ";
            }
            else {
                sqlQuery = @"INSERT INTO[stock].[dbo].[Products]
        ([ProducCode]
        ,[ProductName]
        ,[ProductStatus])
            VALUES
            ('" + textBox1.Text + "', '" + textBox2.Text + "' ,'" + status + "')";
            }



            //insret logic
            SqlCommand cmd = new SqlCommand(sqlQuery, con);
            cmd.ExecuteNonQuery();
            con.Close();

            //Reading Data
           LoadData();


        }
        public void LoadData() {

            SqlConnection con = new SqlConnection(@"Data Source=.\AYASQL;Initial Catalog=stock;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [stock].[dbo].[Products]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ProducCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();


                if ((bool)item["ProductStatus"])
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Active";
                }
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Deactive";
                }
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

            if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString()== "Active")
            {
                comboBox2.SelectedIndex = 0;
            }
            else
            {
                 comboBox2.SelectedIndex = 1;
            }
         
        }
        private bool ifProductExist(SqlConnection con, string productCode) {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT 1 FROM [dbo].[Products] WHERE [ProducCode]='" + productCode + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)

                return true;
            else
                return false;
        }

        // Delete Button
        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\AYASQL;Initial Catalog=stock;Integrated Security=True");
           

            var sqlQuery = "";
            if (ifProductExist(con, textBox1.Text))
            {
                con.Open();
                sqlQuery = @"DELETE FROM [dbo].[Products] WHERE[ProducCode] ='" + textBox1.Text + "' ";
                textBox1.Text = "";
                textBox2.Text = "";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                MessageBox.Show("Record Not Exist..!!");
            }

            //Reading Data
            LoadData();

        }
    }
}
