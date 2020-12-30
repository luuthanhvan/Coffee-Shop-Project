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
using QuanLyQuanCafe.DTO;
using System.Globalization;

namespace QuanLyQuanCafe
{
    public partial class Salary : Form
    {
        public Salary(Account acc)
        {
            InitializeComponent();
            loadDateTimePickerSalary();
            this.LoginAccount = acc;
            loadListSalaryByDate(LoginAccount.UserName, dtPFromDateSalary.Value, dtpToDateSalary.Value);
            Sum();
        }
        void loadDateTimePickerSalary()
        {
            DateTime today = DateTime.Now;
            dtPFromDateSalary.Value = new DateTime(today.Year, today.Month, 1);
            dtpToDateSalary.Value = dtPFromDateSalary.Value.AddMonths(1).AddDays(-1);
        }
        private Account loginAccount;


        public Account LoginAccount
        {
            get { return loginAccount; }
            set
            {
                loginAccount = value;
            }
        }
        private void btnViewSalary_Click(object sender, EventArgs e)
        {
            loadListSalaryByDate(LoginAccount.UserName, dtPFromDateSalary.Value, dtpToDateSalary.Value);
            Sum();
        }
        private void loadListSalaryByDate(string userName,DateTime checkIn, DateTime checkOut)
        {
            //dgvSalary.ClearSelection
            dgvSalary.DataSource = AccountDAO.Instance.getListSalaryByDate(userName,checkIn, checkOut);
        }
        private void Sum()
        {
            Double[] columnData = (from DataGridViewRow row in dgvSalary.Rows
                                where row.Cells[1].FormattedValue.ToString() != string.Empty
                                select Convert.ToDouble(row.Cells[3].FormattedValue)).ToArray();
            
            Double Salary = Convert.ToDouble(columnData.Sum().ToString());
            CultureInfo culture = new CultureInfo("vi_VN");
            txtSalary.Text = Salary.ToString("c", culture);
            int [] co = (from DataGridViewRow row in dgvSalary.Rows
                                   where row.Cells[1].FormattedValue.ToString() != string.Empty
                                   select Convert.ToInt32(row.Cells[2].FormattedValue)).ToArray();
            txtTime.Text = co.Sum().ToString();
     
        }

        private void btnCloseSalary_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
