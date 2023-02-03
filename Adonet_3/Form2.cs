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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Server=DESKTOP-MUMOMKH;database=Northwind;Trusted_Connection=True;");

        private void Form2_Load(object sender, EventArgs e)
        {
            SqlDataAdapter dap = new SqlDataAdapter("select CustomerId,CompanyName from Customers; select ShipperId, CompanyName from Shippers", conn);

            //dap.SelectCommand 
            //dap.InsertCommand = new SqlCommand("Insert....", conn);
            //dap.UpdateCommand
            //dap.DeleteCommand
            DataSet ds = new DataSet();
            dap.Fill(ds);
            ds.Tables[0].TableName = "Customerss";
            ds.Tables[1].TableName = "Shipperss";

            listBox1.DataSource = ds.Tables[0];
            listBox1.DisplayMember = "CompanyName";
            listBox1.ValueMember = "CustomerId";

            comboBox1.DataSource = ds.Tables["Shipperss"];
            comboBox1.DisplayMember = "CompanyName";
            comboBox1.ValueMember = "ShipperId";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dap = new SqlDataAdapter("select (CONVERT(NVARCHAR(20),OrderId) +' >>  '+ CONVERT(NVARCHAR(11),OrderDate)) as ERP4 from Orders where CustomerId=@cid and ShipVia=@sid ", conn);
            dap.SelectCommand.Parameters.AddWithValue("@cid", listBox1.SelectedValue);
            dap.SelectCommand.Parameters.AddWithValue("@sid", comboBox1.SelectedValue);

            DataTable dt = new DataTable();
            dap.Fill(dt);
            listBox2.DataSource = dt;
            //listBox2.DisplayMember = "OrderDate";
            listBox2.DisplayMember = "ERP4";
          
        }
    }
}
