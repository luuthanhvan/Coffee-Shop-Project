using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyQuanCafe.DAO;

namespace QuanLyQuanCafe
{
    public partial class frmAccountProfile : Form
    {
        #region Method
        private Account loginAccount;


        public Account LoginAccount
        {
            get { return loginAccount; }
            set
            {
                loginAccount = value;
                changeAccount(loginAccount);
            }
        }
        void changeAccount(Account acc)
        {
            txtUsername.Text = LoginAccount.UserName;
            txtDisplayName.Text = LoginAccount.DisplayName;
        }
        void updateAccount()
        {
            string displayName = txtDisplayName.Text;
            string password = txtPassword.Text;
            string newpass = txtNewPass.Text;
            string reenterPass = txtEnterPass.Text;
            string username = txtUsername.Text;
            if (!newpass.Equals(reenterPass))
            {
                MessageBox.Show( "Mật khẩu không khớp","Thông báo");
            }
            else
            {
                if (AccountDAO.Instance.updateAccount(username, displayName, password, newpass))
                {
                    MessageBox.Show("Cập nhật thành công","Thông báo");
                }else{
                    MessageBox.Show("Mật khẩu không chính xác","Thông báo");
                }
            }
        }
        #endregion
        
        #region event
        public frmAccountProfile(Account acc)
        {
            InitializeComponent();
            LoginAccount = acc;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            updateAccount();
        }
        #endregion 
    }
}
