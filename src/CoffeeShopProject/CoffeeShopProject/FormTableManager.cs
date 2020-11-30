using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeShopProject{
    public partial class FormTableManager : Form{
        public FormTableManager(){
            InitializeComponent();
        }
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e){
            this.Close();
        }

        private void personalInfoToolStripMenuItem_Click(object sender, EventArgs e){
            FormAccountProfile f = new FormAccountProfile();
            f.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e){
            FormAdmin f = new FormAdmin();
            f.ShowDialog();
        }
    }
}
