using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;


namespace StaffInformationSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void btnInsert_Click(object sender, EventArgs e)
        {

            Regex rex = new Regex(@"[a-zA-Z]+@[a-zA-Z]+.com$");
            Regex rex1 = new Regex(@"^(09)[0-9]{9}$");
            bool isValid = rex.IsMatch(txtEmail.Text);
            bool isValid1 = rex1.IsMatch(txtPhno.Text);
            dateOfBirth.Format = DateTimePickerFormat.Custom;
            dateOfBirth.CustomFormat = "MM-dd-yyyy";
            string gender = "";

            
    


            if (!(isValid))
            {
                MessageBox.Show("Email is invalid");
            }

            if (!(isValid1))
            {
                MessageBox.Show("Phone is invalid");
            }

            if (radioMale.Checked)
            {
                gender = "Male";
            }
            else if (radioFemale.Checked)
            {
                gender = "Female";
            }


            if (isValid && isValid1 && txtName.Text != "" && gender != "" && txtAddress.Text != "" && dateOfBirth.Text != "")
            {


                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\User\OneDrive - University of Computer Studies (Maubin)\Desktop\MUB1274\StaffInformationSystem\StaffInformationSystem\Staff.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();
                try
                {
                    string str = @"Insert into Staff (name, email, address, gender, dob, phno) values ('" + txtName.Text + "', '" + txtEmail.Text + "', '" + txtAddress.Text + "', '" + gender + "', '" + dateOfBirth.Text + "', '" + txtPhno.Text + "' ) ";
                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Insert Sucessfully");

                    clear();
                    Show();
                 
                    
                }
                catch (SqlException error)
                {
                    MessageBox.Show(error.Message);
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
                finally
                {
                    con.Close();
                  
                }

            }
            else
            {
                MessageBox.Show("Please check your data");
            }


        }

        public void clear()
        {
            int id = 1;
            SqlConnection con1 = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\User\OneDrive - University of Computer Studies (Maubin)\Desktop\MUB1274\StaffInformationSystem\StaffInformationSystem\Staff.mdf;Integrated Security=True;Connect Timeout=30");

            con1.Open();
            string str1 = @"select * from Staff";
            SqlCommand cmd1 = new SqlCommand(str1, con1);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                id = Convert.ToInt32(dr1[0]);
                id += 1;
                txtStaff_id.Text = id.ToString();

            }

            txtName.Text = "";
            txtEmail.Text = "";
            radioMale.Checked = false;
            radioFemale.Checked = false;
            txtPhno.Text = "";
            txtAddress.Text = "";
            dateOfBirth.Text = "";
            con1.Close();
            
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtStaff_id.Text == "")
            {
                MessageBox.Show("Invalid Employee_Id");
            }
            else
            {

                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\User\OneDrive - University of Computer Studies (Maubin)\Desktop\MUB1274\StaffInformationSystem\StaffInformationSystem\Staff.mdf;Integrated Security=True;Connect Timeout=30");

                con.Open();
                try
                {
                    string gender = "";

                    if (radioMale.Checked)
                    {
                        gender = "Male";
                    }
                    else if (radioFemale.Checked)
                    {
                        gender = "Female";
                    }

                    String upd = "update Staff set name = '" + txtName.Text + "',email = '" + txtEmail.Text + "', gender = '" + gender + "' ,phno = '" + txtPhno.Text + "',address = '" + txtAddress.Text + "' where staff_Id='" + txtStaff_id.Text + "'";
                    SqlCommand cmd = new SqlCommand(upd, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Update Sucessfully");

                    clear();
                    Show();
                }


                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                con.Close();
            }
        }

        public void Show()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\User\OneDrive - University of Computer Studies (Maubin)\Desktop\MUB1274\StaffInformationSystem\StaffInformationSystem\Staff.mdf;Integrated Security=True;Connect Timeout=30");

            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Staff", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            int id = 1;
            txtStaff_id.Text = id.ToString();
            SqlConnection con1 = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\User\OneDrive - University of Computer Studies (Maubin)\Desktop\MUB1274\StaffInformationSystem\StaffInformationSystem\Staff.mdf;Integrated Security=True;Connect Timeout=30");

            con1.Open();
            string str1 = @"select * from Staff";
            SqlCommand cmd1 = new SqlCommand(str1, con1);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                id = Convert.ToInt32(dr1[0]);
                id += 1;
                txtStaff_id.Text = id.ToString();

            }
            Show();
        }



        private void txtPhno_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtPhno.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txtPhno.Text = txtPhno.Text.Remove(txtPhno.Text.Length - 1);
            }
            if (txtPhno.Text.Length > 11)
            {
                MessageBox.Show("no more than 11 numbers");
                txtPhno.Text = txtPhno.Text.Remove(txtPhno.Text.Length - 1); ;
                txtPhno.Focus();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
                {
                    dataGridView1.CurrentRow.Selected = true;
                    txtStaff_id.Text = dataGridView1.Rows[e.RowIndex].Cells["staff_Id"].Value.ToString();
                    txtName.Text = dataGridView1.Rows[e.RowIndex].Cells["name"].Value.ToString();
                    txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells["email"].Value.ToString();
                    dateOfBirth.Text = dataGridView1.Rows[e.RowIndex].Cells["dob"].Value.ToString();
                    txtPhno.Text = dataGridView1.Rows[e.RowIndex].Cells["phno"].Value.ToString();
                    txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells["address"].Value.ToString();

                    if (dataGridView1.Rows[e.RowIndex].Cells["gender"].Value.ToString() == "Male")
                    {
                        radioMale.Checked = true;
                    }
                    else if (dataGridView1.Rows[e.RowIndex].Cells["gender"].Value.ToString() == "Female")
                    {
                        radioFemale.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtStaff_id.Text == "")
            {
                MessageBox.Show("Invalid Employee_Id");
            }
            else
            {

                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\User\OneDrive - University of Computer Studies (Maubin)\Desktop\MUB1274\StaffInformationSystem\StaffInformationSystem\Staff.mdf;Integrated Security=True;Connect Timeout=30");

                con.Open();
                try
                {
                   
                    String upd = "delete from Staff  where staff_Id='" + txtStaff_id.Text + "'";
                    SqlCommand cmd = new SqlCommand(upd, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Delete Sucessfully");

                    clear();
                    Show();
                }


                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                con.Close();
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        

       
    }
}




