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
using IT4Solutions.General;

namespace IT4Solutions
{
    public partial class Change_password : MetroFramework.Forms.MetroForm
    {
        public Change_password()
        {
            InitializeComponent();
            using (SqlConnection con  = new SqlConnection(ApplicationSettings.ConnectionStrgin()))
            {
                using (SqlCommand cmd = new SqlCommand("select_UserAdmin", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        UserName.Text = dr["UserName"].ToString();
                        Password.Text = dr["Password"].ToString();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            using (SqlConnection con = new SqlConnection(ApplicationSettings.ConnectionStrgin()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_change_login", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", UserName.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", Password.Text.Trim());

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                       
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        con.Close();
                    }
                        
                }
            }
        }
    }
}
