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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Server=DESKTOP-MUMOMKH;database=Northwind;Trusted_Connection=True;");
        SqlDataAdapter dap;

        private void Form3_Load(object sender, EventArgs e)
        {
            dap = new SqlDataAdapter("select CustomerId,CompanyName from Customers", conn);


            DataSet ds = new DataSet();
            dap.Fill(ds);

            listBox1.DataSource = ds.Tables[0];
            listBox1.DisplayMember = "CompanyName";
            listBox1.ValueMember = "CustomerId";


        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {


            Idtxt.Text = listBox1.SelectedValue.ToString();
            //companytxt.Text = listBox1.GetItemText(listBox1.SelectedItem);
            DataRowView drv = (DataRowView)listBox1.SelectedItem;
            companytxt.Text = drv["CompanyName"].ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SqlDataAdapter dap = new SqlDataAdapter("Update Customers Set CompanyName = @name where CustomerId=@id", conn);

            SqlCommand cmd = new SqlCommand("Update Customers Set CompanyName = @name where CustomerId=@id", conn);

            dap.UpdateCommand = cmd;

            dap.UpdateCommand.Parameters.AddWithValue("@name", companytxt.Text);
            dap.UpdateCommand.Parameters.AddWithValue("@id", Idtxt.Text);
            conn.Open();
            dap.UpdateCommand.ExecuteNonQuery();
            MessageBox.Show("işlem başarılı");
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            dap = new SqlDataAdapter("select CustomerId,CompanyName from Customers", conn);


            DataSet ds = new DataSet();
            dap.Fill(ds);

            listBox2.DataSource = ds.Tables[0];
            listBox2.DisplayMember = "CompanyName";
            listBox2.ValueMember = "CustomerId";

            listView1.Columns.Add("Id");
            listView1.Columns.Add("Name");

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = item["CustomerId"].ToString();
                lvi.SubItems.Add(item["CompanyName"].ToString());

            }


            //SqlCommand cmd = new SqlCommand("select CustomerId,CompanyName from Customers", conn);
            //conn.Open();
            //SqlDataReader dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //    ListViewItem lst = new ListViewItem();
            //lst.Text = (string)dr["CustomerId"];
            //    lst.SubItems.Add((string)dr["CompanyName"]);

            //}
            //conn.Close();

        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Text=  listBox2.SelectedValue.ToString();
            DataRowView drv = (DataRowView)listBox2.SelectedItem;
            lvi.SubItems.Add(drv["CompanyName"].ToString());
            listView1.Items.Add(lvi);
            listView1.Columns[1].Width = 150;

        }
    }
}
