using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IT4Solutions.Screens.ProductF;

namespace IT4Solutions.Screens
{
    public partial class DashBoardForms : MetroFramework.Forms.MetroForm
    {
        public DashBoardForms()
        {
            InitializeComponent();
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DeFineProductScreens dpf = new DeFineProductScreens();
            dpf.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }
    }
}
