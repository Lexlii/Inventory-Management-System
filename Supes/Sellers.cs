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

namespace Supes
{
    public partial class Sellers : Form
    {
        public Sellers()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\pc\Documents\Martdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void Sellers_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void AttDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Attid.Text = AttDGV.SelectedRows[0].Cells[0].Value.ToString();
            AttName.Text = AttDGV.SelectedRows[0].Cells[1].Value.ToString();
            AttAge.Text = AttDGV.SelectedRows[0].Cells[2].Value.ToString();
            AttPhone.Text = AttDGV.SelectedRows[0].Cells[3].Value.ToString();
            AttPass.Text = AttDGV.SelectedRows[0].Cells[4].Value.ToString();
            
        }

        private void populate()
        {
            Con.Open();
            string query = "select * from AttendantTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            AttDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string query = "insert into AttendantTbl values(" + Attid.Text + ", '" + AttName.Text + "', '" + AttAge.Text + "', '" + AttPhone + "', '" + AttPass + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Attendant Added Successfully");
                Con.Close();
                populate();
                Attid.Text = "";
                AttName.Text = "";
                AttAge.Text = "";
                AttPhone.Text = "";
                AttPass.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Attid.Text == "" || AttName.Text == "" || AttAge.Text == ""  || AttPhone.Text == "" || AttPass.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    Con.Open();
                    string query = "update AttendantTbl set AttName='" + AttName.Text + "', AttAge=" + AttAge.Text + ", AttPhone='" + AttPhone.Text + "', AttPass='" + AttPass.Text + "' where Attid=" + Attid.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Attendant successfully updated");
                    Con.Close();
                    populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Attid.Text == "")
                {
                    MessageBox.Show("Select the attendant to remove");
                }
                else
                {
                    Con.Open();
                    string query = "delete from AttendantTbl where Attid=" + Attid.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Attendant removed successfully");
                    Con.Close();
                    populate();
                    Attid.Text = "";
                    AttName.Text = "";
                    AttAge.Text = "";
                    AttPhone.Text = "";
                    AttPass.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
