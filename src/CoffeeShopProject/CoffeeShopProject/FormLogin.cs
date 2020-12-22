using CoffeeShopProject.DAO;
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
    public partial class FormLogin : Form{
        public FormLogin(){
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, EventArgs e){
            string username = this.txtUsername.Text;
            string password = this.txtPassword.Text;

            if(login(username, password)){
                FormTableManager f = new FormTableManager();
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else{
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu!");
            }
        }
        bool login(string username, string password){
            return AccountDAO.Instance.login(username, password);
        }
        private void btnExit_Click(object sender, EventArgs e){
            Application.Exit();
        }
        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e){
            if (MessageBox.Show("Bạn có thực sự muốn thoát chương trình?", "Thông báo", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes ){
                e.Cancel = true;
            }
        }
    }
}
