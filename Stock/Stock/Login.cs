﻿using System;
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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        //clear button
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Clear();
            textBox1.Focus();
        }

        //Login button 
        private void button2_Click(object sender, EventArgs e)
        {
            //TO-DO: check user and pass
            SqlConnection con = new SqlConnection(@"Data Source=.\AYASQL;Initial Catalog=stock;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT *
                FROM[dbo].[Login] WHERE UserName = '"+textBox1.Text+"' and Password = '"+textBox2.Text +"'",con);
            DataTable dt = new DataTable();

            sda.Fill(dt);

            if (dt.Rows.Count == 1)
            {

                this.Hide();
                StockMain main = new StockMain();
                main.Show();
            }
            else
            {
                MessageBox.Show("Invalid User name and Password !!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                button1_Click(sender, e);

            }

           
        }
    }
}
