using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }
        public Help(String choose)
        {

            InitializeComponent();
            if (choose.Equals("huongdansudung"))
            {
                txtUrl.Text = url;
                panel1.Visible = true;
                panel2.Visible = false;

            }
            else
            {
                panel2.Visible = true;
                panel1.Visible = false;
            }

        }
        
        public static string url = @"https://www.youtube.com/watch?v=tu2k9ZrDlWA&list=PL33lvabfss1xnPhBJHjM0A8TEBBcGCTsf";

   
        private void s_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(url);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
