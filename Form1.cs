using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Database_Management_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=LPSHARIF;Initial Catalog=Customer;Integrated Security=True");

        public int customerID;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getCustomerRecord();
        }

        private void getCustomerRecord()
        {
            SqlCommand cmd = new SqlCommand("select * from Customer", con);
            DataTable dt = new DataTable();
            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            CustomerTable.DataSource = dt;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (isVaild())
            {
                SqlCommand cmd = new SqlCommand("insert into Customer values (@firstName, @lastName,@age,@city)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@firstName", fstnamebox.Text);
                cmd.Parameters.AddWithValue("@lastName", lstnamebox.Text);
                cmd.Parameters.AddWithValue("@age", agebox.Text);
                cmd.Parameters.AddWithValue("@city", citybox.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New Customer Added Successfully!", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);

                getCustomerRecord();
                refreshBoxs();
            }
        }

        private bool isVaild()
        {
            if (fstnamebox.Text == string.Empty)
            {
                MessageBox.Show("Customer's First Name is required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (lstnamebox.Text == string.Empty)
            {
                MessageBox.Show("Customer's Last Name is required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (agebox.Text == string.Empty)
            {
                MessageBox.Show("Age is required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (citybox.Text == string.Empty)
            {
                MessageBox.Show("City name is required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            refreshBoxs();
        }

        private void refreshBoxs()
        {
            customerID = 0;
            fstnamebox.Clear();
            lstnamebox.Clear();
            agebox.Clear();
            citybox.Clear();
            idbox.Clear();
        }

        private void CustomerTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            customerID = Convert.ToInt32(CustomerTable.SelectedRows[0].Cells[0].Value);
            idbox.Text = CustomerTable.SelectedRows[0].Cells[0].Value.ToString();
            fstnamebox.Text = CustomerTable.SelectedRows[0].Cells[1].Value.ToString();
            lstnamebox.Text = CustomerTable.SelectedRows[0].Cells[2].Value.ToString();
            agebox.Text = CustomerTable.SelectedRows[0].Cells[3].Value.ToString();
            citybox.Text = CustomerTable.SelectedRows[0].Cells[4].Value.ToString();
        }

        private bool executeable()
        {
            if (customerID == Convert.ToInt32(idbox.Text))
            {
                return true;
            }
            return false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (customerID > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE Customer SET firstName = @firstName, lastName = @lastName,age = @age,city = @city WHERE customerId = @customerId", con);
                cmd.CommandType = CommandType.Text;
                
                cmd.Parameters.AddWithValue("@firstName", fstnamebox.Text);
                cmd.Parameters.AddWithValue("@lastName", lstnamebox.Text);
                cmd.Parameters.AddWithValue("@age", agebox.Text);
                cmd.Parameters.AddWithValue("@city", citybox.Text);
                cmd.Parameters.AddWithValue("@customerId", this.customerID);

                bool ok = executeable();

                if (ok)
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Customer's Details Updated Successfully!", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    getCustomerRecord();
                    refreshBoxs();
                }
                else
                {
                    MessageBox.Show("Can't Change Customer ID :(", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else
            {
                MessageBox.Show("Please Select A Customer!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (customerID > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Customer WHERE customerId = @customerId", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@customerId", this.customerID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Customer's Record Deleted Successfully!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                getCustomerRecord();
                refreshBoxs();
            }
            else
            {
                MessageBox.Show("Please Select A Customer!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
