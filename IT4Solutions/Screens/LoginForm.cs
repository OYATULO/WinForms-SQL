using IT4Solutions.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using IT4Solutions.Screens;

namespace IT4Solutions
{
    public partial class LoginForm : MetroFramework.Forms.MetroForm
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                using (SqlConnection  con= new SqlConnection(ApplicationSettings.ConnectionStrgin()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_login_Verifications",con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@username", UserName.Text.Trim());
                        cmd.Parameters.AddWithValue("@password", Password.Text.Trim());

                        con.Open();
                        SqlDataReader rd = cmd.ExecuteReader();
                        if (rd.Read())
                        {
                            this.Hide();
                            DashBoardForms dsb = new DashBoardForms();
                            dsb.Show();
                        }
                        else
                        {
                            MessageBox.Show("Errors");
                        }
                    }
                }
            }

        }

        private bool IsValid()
        {
            if (UserName.Text.Trim()==string.Empty)
            {
                return false;
            }
            if (Password.Text.Trim()==string.Empty)
            {
                return false;
            }
            return true;
        }
    }
}
