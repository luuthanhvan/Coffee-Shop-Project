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
using System.Threading;

namespace QuanLyQuanCafe
{
    public partial class frmTableManager : Form
    {
        
        public frmTableManager(Account acc) 
        {
            InitializeComponent();
            this.LoginAccount = acc;
            loadTable();
            loadCategory();
            loadComboboxTable(cbbSwitchTable);
        }

        #region Method
        private Account loginAccount;


        public Account LoginAccount
        {
            get { return loginAccount; }
            set
            {
                loginAccount = value;
                changeAccount(loginAccount.Type);
            }
        }
        void changeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            thôngTinCáNhânToolStripMenuItem.Text += "(" + LoginAccount.DisplayName + ")";
        }
        void loadComboboxTable(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.LoadTableList();
            cb.DisplayMember = "Name";
        }
        void loadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.getListCategory();
            cbbCategory.DataSource = listCategory;
            cbbCategory.DisplayMember = "Name";
        }
        void loadFoodListByCategoryID(int id)
        {
            List<Food> listFood = FoodDAO.Instance.getFoodByCategoryID(id);
            cbbFood.DataSource = listFood;
            cbbFood.DisplayMember = "Name";
        }
        void loadTable()
        {
            flpTable.Controls.Clear();
            List<Table> tableList = TableDAO.Instance.LoadTableList();
            foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight};

                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += btn_Click;
                btn.Tag = item;
                if (item.Status == "Trống")
                {

                    btn.Image = Image.FromFile(@"D:\QuanLyQuanCafe\logo.jpg");
                }
                else
                {
                    btn.BackColor = Color.LightPink;
                }

                this.flpTable.Controls.Add(btn);
            }
        }
        void showBill(int id)
        {
            lsvBill.Items.Clear();
            List<QuanLyQuanCafe.DTO.Menu> listBillInfo = MenuDAO.Isntance.getListMenuByTable(id);
            float totalPrice = 0;
            foreach (QuanLyQuanCafe.DTO.Menu item in listBillInfo)
            {
                ListViewItem lviItem = new ListViewItem(item.FoodName.ToString());
                lviItem.SubItems.Add(item.Count.ToString());
                lviItem.SubItems.Add(item.Price.ToString());
                lviItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lsvBill.Items.Add(lviItem);
            }
            CultureInfo culture = new CultureInfo("vi_VN");
           // Thread.CurrentThread.CurrentCulture = culture;
            txtTotalPrice.Text = totalPrice.ToString("c",culture);
        }
        
        #endregion

        #region Events

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAccountProfile f = new frmAccountProfile(loginAccount );
            f.ShowDialog();
        }
       

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdmin f = new frmAdmin();
            f.InsetFood += f_InsertFood;
            f.DeleteFood += f_DeleteFood;
            f.UpdateFood += f_UpdateFood; /*
            f.UpdateCategory += f_UpdateCategory;
            f.InsetCategory += f_InsertCategory;
            f.DeleteCategory += f_deleteCategofy;*/
            //this.Visible = false;
            f.ShowDialog();
        }
        void f_UpdateFood(object sender, EventArgs e)
        {
            loadFoodListByCategoryID((cbbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                showBill((lsvBill.Tag as Table).ID);

        }

        void f_InsertFood(object sender, EventArgs e)
        {
            loadFoodListByCategoryID((cbbCategory.SelectedItem as Category).ID);
            if(lsvBill.Tag != null)
                showBill((lsvBill.Tag as Table).ID);

        }
        void f_DeleteFood(object sender, EventArgs e)
        {
            loadFoodListByCategoryID((cbbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
                showBill((lsvBill.Tag as Table).ID);
            loadTable();

        }
        private void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag    as Table).ID;
            lsvBill.Tag = (sender as Button).Tag;
            showBill(tableID);
        }
       
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            if (table == null)
            {
                MessageBox.Show("Bạn cần phải chọn bàn trước khi thêm", "Thông báo");
                return;
            }
            int idBill = BillDAO.Instance.getUncheckBillIDByTableID(table.ID);
            int foodID = (cbbFood.SelectedItem as Food).Id;
            int count = (int)nudFoodCount.Value;
            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.getMaxIDBill(), foodID, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, foodID, count);
            
            }
            showBill(table.ID);
            loadTable();

        }


        private void cbbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
           int id = 0;
            ComboBox cb = sender as ComboBox;
            if(cb.SelectedItem == null)
                return; 
            Category selected = cb.SelectedItem as Category;
                id = selected.ID;
            loadFoodListByCategoryID(id);
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            int discount = (int ) this.nudDiscount.Value;

            int idBill = BillDAO.Instance.getUncheckBillIDByTableID(table.ID);

            string txtTotal = txtTotalPrice.Text.Replace(".", "");
            double totalPrice = Convert.ToDouble(txtTotal.Split(',')[0]);
            
            double discountPrice = totalPrice/100*discount;
            double finalTotalPrice = totalPrice - discountPrice;
            if (idBill != -1)
            {
                if (MessageBox.Show(string.Format("Bạn muốn thanh toán hoá đơn bàn {0} \nTổng tiền: {1} Giảm giá: {2} Phải trả: {3} " 
                    , table.Name,totalPrice,discountPrice,finalTotalPrice), "Thông báo", MessageBoxButtons.OKCancel) 
                    == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.checkOut(idBill,discount,(float)finalTotalPrice);
                    showBill(table.ID);
                    loadTable();
                }
            }
            nudDiscount.Value = 0;
        }
        #endregion

        private void btnSwitchTable_Click_1(object sender, EventArgs e)
        {
            int id1 = (lsvBill.Tag as Table).ID;

            int id2 = (cbbSwitchTable.SelectedItem as Table).ID;
            if (MessageBox.Show(string.Format("Bạn có thật sự muốn chuyển bàn {0} qua {1}", (lsvBill.Tag as Table).Name, (cbbSwitchTable.SelectedItem as Table).Name), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                TableDAO.Instance.switchTable(id1, id2);

                loadTable();
            }
        }

        private void bànTrốngToolStripMenuItem_Click(object sender, EventArgs e)
        {
           flpTable.Controls.Clear();
            List<Table> tableList = TableDAO.Instance.LoadTableList();
            foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight};

                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += btn_Click;
                btn.Tag = item;
                if (item.Status == "Trống")
                {
                    this.flpTable.Controls.Add(btn);
                    btn.Image = Image.FromFile(@"D:\QuanLyQuanCafe\logo.jpg");
                }

            }
        }

        private void bànCóNgườiToolStripMenuItem_Click(object sender, EventArgs e)
        {

            flpTable.Controls.Clear();
            List<Table> tableList = TableDAO.Instance.LoadTableList();
            foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight };

                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += btn_Click;
                btn.Tag = item;
                if (item.Status == "Có người")
                {
                    this.flpTable.Controls.Add(btn);
                    btn.BackColor = Color.LightPink;
                }

            }
        }

        private void hướngDẫnSửDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help f = new Help("huongdansudung");
            f.ShowDialog();
        }

        private void thôngTinPhầnMềmToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Help f = new Help("thongtinphanmem");
            f.ShowDialog();
        }

        private void checkInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountDAO.Instance.checkIn(LoginAccount.UserName);
            MessageBox.Show("Check In thành công", "Thông báo");
        }

        private void checkOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountDAO.Instance.checkOut(LoginAccount.UserName);
            MessageBox.Show("Check out thành công", "Thông báo");

        }

        private void frmTableManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            AccountDAO.Instance.checkOut(LoginAccount.UserName);
            MessageBox.Show("Bạn đã check out", "Thông báo");
        }

        private void lươngToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Account loginAccount = AccountDAO.Instance.getAccountByUserName(LoginAccount.UserName);
            Salary f = new Salary(loginAccount);
            f.ShowDialog();
        }
    }
}
