using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using static System.Windows.Forms.AxHost;

namespace EMP
{
    public partial class Form1 : Form
    {
        static string s = "server=DESKTOP-VSA;database=Windows_DB;integrated security=true";
        SqlConnection con = new SqlConnection(s);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            autoincreament();
            data();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        public void autoincreament()
        {
            int r;
            con.Open();
            SqlCommand cmd = new SqlCommand("Select MAX(RN) from Employee", con);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                string d = dr[0].ToString();
                if (d == "")
                {
                    textBox1.Text = "1";
                }
                else
                {
                    r = Convert.ToInt16(d[0].ToString());
                    r = r + 1;
                    textBox1.Text = r.ToString();
                }
            }
            dr.Close();
            con.Close();
        }

        public void cleardata()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.Text="";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }
        public void data()
        {
            SqlDataAdapter adp = new SqlDataAdapter("Select * from Employee order by RN",  con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // insert store procedure
                string gender;
                if (radioButton1.Checked == true)
                    gender = "Male";
                else if (radioButton2.Checked == true)
                    gender = "Female";
                else gender = "other";

                string query = "insert_data";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RN", textBox1.Text);
                cmd.Parameters.AddWithValue("@Name", textBox2.Text);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Designation", comboBox1.Text);
                cmd.Parameters.AddWithValue("@DOB", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@Salary", textBox3.Text);
                DialogResult res = MessageBox.Show("Do you want to add new employee", "Added Employee", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                label9.Text = "Error :" + ex.Message;
            }
            finally
            {
                con.Close();
            }
            cleardata();
            autoincreament();
            data();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // update store procedure
            string gender;
            if (radioButton1.Checked == true)
                gender = "Male";
            else if (radioButton2.Checked == true)
                gender = "Female";
            else gender = "other";

            string query = "update_data";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p1", textBox1.Text);
            cmd.Parameters.AddWithValue("@p2", textBox2.Text);
            cmd.Parameters.AddWithValue("@p3", gender);
            cmd.Parameters.AddWithValue("@p4", comboBox1.Text);
            cmd.Parameters.AddWithValue("@p5", dateTimePicker1.Text);
            cmd.Parameters.AddWithValue("@p6", textBox3.Text);
            DialogResult res = MessageBox.Show("Do you want to Update employee", "updated Employee", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                cmd.ExecuteNonQuery();
            }
            con.Close();
            cleardata();
            autoincreament();
            data();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // delete store procedure

            string query = "delete_data";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p1", textBox1.Text);
            DialogResult res = MessageBox.Show("Do you want to Delete employee", "Deleted Employee", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                cmd.ExecuteNonQuery();
            }
            con.Close();
            cleardata();
            autoincreament();
            data();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // search
            SqlDataAdapter adp = new SqlDataAdapter("select * from Employee where RN=" + textBox4.Text + "", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Record Not Found");
            }
            else
            {
                textBox1.Text = dt.Rows[0][0].ToString();
                textBox2.Text = dt.Rows[0][1].ToString();
                if (dt.Rows[0][2].ToString() == "male")
                    radioButton1.Checked = true;
                else
                    radioButton2.Checked = true;
                comboBox1.Text = dt.Rows[0][3].ToString();
                dateTimePicker1.Text = dt.Rows[0][4].ToString();
                textBox3.Text = dt.Rows[0][5].ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        // search max salary
        private void button7_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adp = new SqlDataAdapter(" select * from Employee order by Salary desc", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Show();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }


        // for setting roll number to user value
        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox4.Text;
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        // For Showing Gender data in DatagridView
        private void button8_Click(object sender, EventArgs e)
        {
            string gender;
            if (radioButton1.Checked == true)
                gender = "Male";
            else if (radioButton2.Checked == true)
                gender = "Female";
            else gender = "other";

            SqlDataAdapter adp = new SqlDataAdapter("select * from Employee where Gender = '"+gender+"' ", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Show();
        }

        
        // Refreshing the Data View
        private void button9_Click(object sender, EventArgs e)
        {
            data();
            cleardata();
            autoincreament();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = selectedRow.Cells["RN"].Value.ToString();
                textBox2.Text = selectedRow.Cells["Name"].Value.ToString();
                string Gender = selectedRow.Cells["Gender"].Value.ToString();
                if (Gender == "Male")
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
                comboBox1.Text = selectedRow.Cells["Designation"].Value.ToString();
                dateTimePicker1.Text = selectedRow.Cells["DOB"].Value.ToString();
                textBox3.Text = selectedRow.Cells["Salary"].Value.ToString();
            }
        }
    }
}
