using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AddTaxonomyToCountsSetGroups_GitHub
{
    public partial class createConnectionString : Form
    {
        string workingString = "";

        public createConnectionString()
        {
            InitializeComponent();

            string severname = Environment.MachineName;
            lblCon1.Text = lblCon1.Text.Replace("<computer name>", severname);
            lblCon2.Text = lblCon2.Text.Replace("<computer name>", severname);
        }

        private void button1_Click(object sender, EventArgs e)
        {           
            Close();
        }

        private void creatConnectionString_Load(object sender, EventArgs e)
        {

        }

        private void lblCon1_Click(object sender, EventArgs e)
        {
            txtConnectionString.Text = lblCon1.Text;
        }

        private void lblCon2_Click(object sender, EventArgs e)
        {
            txtConnectionString.Text = lblCon2.Text;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string conString = txtConnectionString.Text;

            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(conString);
            if (con.State != ConnectionState.Open)
            {
                try
                {
                    con.Open();
                    con.Close();
                    workingString = txtConnectionString.Text;
                    MessageBox.Show("Connected to the database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    workingString = "";
                    MessageBox.Show("Could not connect to the database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public string getConnectionString
        {
            get { return workingString; }
        }
    }
}
