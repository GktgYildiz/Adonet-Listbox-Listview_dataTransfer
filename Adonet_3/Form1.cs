using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Adonet_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Server=DESKTOP-MUMOMKH;database=Northwind;Trusted_Connection=True;");
        private void Form1_Load(object sender, EventArgs e)
        {
            SqlDataAdapter dap = new SqlDataAdapter("Select CategoryId,CategoryName from Categories", conn);

            DataTable dt = new DataTable();
            dap.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "CategoryName";
            comboBox1.ValueMember = "CategoryId";

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dap = new SqlDataAdapter("Select ProductId,ProductName from Products where CategoryId=@id", conn);
            dap.SelectCommand.Parameters.AddWithValue("@id", comboBox1.SelectedValue);

            DataSet ds = new DataSet();
            dap.Fill(ds);
            listBox1.DataSource = ds.Tables[0];
            listBox1.DisplayMember = "ProductName";
            listBox1.ValueMember = "ProductId";

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                MessageBox.Show(listBox1.SelectedValue.ToString()); 
            }
        }
    }
}
