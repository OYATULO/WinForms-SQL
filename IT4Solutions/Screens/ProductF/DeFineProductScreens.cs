using IT4Solutions.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IT4Solutions.Screens.ProductF
{
    public partial class DeFineProductScreens : MetroFramework.Forms.MetroForm
    {
        public DeFineProductScreens()
        {
            InitializeComponent();
        }
        public bool IsUpdate { get; set; }

        private void DeFineProductScreens_Load(object sender, EventArgs e)
        {
            if (!IsUpdate)
            {

            }
            LoadAllSizeInDataGridView();
            LoadIntoCombox();
        }

        private void LoadIntoCombox()
        {
            CategoryCombox1.DataSource = GetComboxData(2);
            SuplierCombox.DataSource = GetComboxData(2);
        }

        private DataTable GetComboxData(int ListTypeID)
        {
            DataTable dt = new DataTable();

            using ( SqlConnection con =new SqlConnection(ApplicationSettings.ConnectionStrgin()) )
            {
                using (SqlCommand cmd = new SqlCommand("usp_listTypeData_LoadtoCombox", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ListTypeID", ListTypeID);
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    dt.Load(reader);

                }
            }


            return dt;
        }

        private void LoadAllSizeInDataGridView()
        {
            SizedDataGridView.DataSource = GetSizeData();
            SizedDataGridView.Columns[0].Visible = false;
        }

        private DataTable GetSizeData()
        {
            DataTable dtSizes = new DataTable() ;
            using (SqlConnection  con = new SqlConnection(ApplicationSettings.ConnectionStrgin()))
            {
                using (SqlCommand  cmd= new SqlCommand("usp_loadAllSizes", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ListTypeID", 1);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    dtSizes.Load(reader);
                }
            }
            dtSizes.Columns["Select"].ReadOnly = false;
            return dtSizes;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void ClearControls()
        {
            ProductnameTextBox.Clear();
            PurchaisTextbox.Clear();
            SuplierCombox.SelectedIndex = -1;
            CategoryCombox1.SelectedIndex = -1;
            SalecPriceTextBox.Clear();
            foreach (DataGridViewRow rows in SizedDataGridView.Rows)
            {
                rows.Cells["Select"].Value = 0;
            }
            ProductnameTextBox.Focus();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                using (SqlConnection con = new SqlConnection(ApplicationSettings.ConnectionStrgin()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_insertProduction", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", ProductnameTextBox.Text);
                        cmd.Parameters.AddWithValue("@Category", CategoryCombox1.SelectedValue);
                        cmd.Parameters.AddWithValue("@SuppliID", SuplierCombox.SelectedValue);
                        cmd.Parameters.AddWithValue("@PurchasePrice", PurchaisTextbox.Text);
                        cmd.Parameters.AddWithValue("@SalelPrice", SalecPriceTextBox.Text);
                        con.Open();
                        int id = Convert.ToInt32(cmd.ExecuteScalar());
                        MessageBox.Show(id.ToString());
                    }
                }

            }
        }

        private bool IsValid()
        {
            if (ProductnameTextBox.Text.Trim()==string.Empty)
            {
                MessageBox.Show("valid name ");
                ProductnameTextBox.Focus();
                return false;
            }
            if (PurchaisTextbox.Text.Trim()==string.Empty)
            {
                MessageBox.Show("valid name ");
                PurchaisTextbox.Focus();
                return false;
            }
            return true;
        }
    }
}
