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
namespace QuanLyQuanCafe
{
    public partial class frmAdmin : Form
    {
        BindingSource foodList = new BindingSource();
        BindingSource accountList = new BindingSource();
        BindingSource categoryList = new BindingSource();
        BindingSource tableList = new BindingSource();
        public frmAdmin()
        {
            InitializeComponent();
            loadDateTimePickerBill();

            dgvBill.DataSource = BillDAO.Instance.getListBillByDatePage(dtPFromDate.Value, dtpToDate.Value, Convert.ToInt32(txtPageBill.Text));
            
            loadListFood();
            dgvFood.DataSource = foodList;
            addFoodBinding();

            loadListCategory();
            dgvCategory.DataSource = categoryList;
            addCategoryBinding();
            loadCategoryIntoCombobox(cbbFoodCategory);

            loadListTable();
            dgvTable.DataSource = tableList;
            addTableBinding();
            //loadTableIntoCombobox(cbbTableStatus);

            dgvAccount.DataSource = accountList;
            addAccountBinding();
            loadAccount();
        }
        #region methods
        void addAccount(string userName, string displayname, int type)
        {
            if (AccountDAO.Instance.insertAccount(userName, displayname, type))
            {
                MessageBox.Show("Thêm tài khoản thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại", "Thông báo");
            }
            loadAccount();
        }

        void editAccount(string userName, string displayname, int type)
        {
            if (AccountDAO.Instance.editAccount(userName, displayname, type))
            {
                MessageBox.Show("Sửa tài khoản thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Sửa tài khoản thất bại", "Thông báo");
            }
            loadAccount();
        }

        void deleteAccount(string userName)
        {
            if (AccountDAO.Instance.deleteAccount(userName))
            {
                MessageBox.Show("Xoá tài khoản thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Xoá tài khoản thất bại", "Thông báo");
            }
            loadAccount();
        }

        void resetPass(string userName)
        {
            if (AccountDAO.Instance.resetPassword(userName))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại", "Thông báo");
            }
        }
        void addAccountBinding()
        {
            txtUsername.DataBindings.Add(new Binding("Text", dgvAccount.DataSource, "UserName",true, DataSourceUpdateMode.Never));
            txtDisplayName.DataBindings.Add(new Binding("Text", dgvAccount.DataSource, "Display",true,DataSourceUpdateMode.Never));
            nudAccountType.DataBindings.Add(new Binding("Value", dgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }
        void loadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.getListAccount();
        }
        List<Food> searchFoodByName(string name)
        {
            List<Food> listFood = new List<Food>();
            listFood = FoodDAO.Instance.searchFoodByName(name);
            return listFood;
        }

        List<Category> searchCategoryByName(string name)
        {
            List<Category> listCategory = new List<Category>();
            listCategory = CategoryDAO.Instance.searchCategoryByName(name);
            return listCategory;
        }
        List<Table> searchTableByName(string name)
        {
            List<Table> listTable = new List<Table>();
            listTable = TableDAO.Instance.searchTableByName(name);
            return listTable;
        }
        private void btnSearchCategory_Click(object sender, EventArgs e)
        {

            categoryList.DataSource = searchCategoryByName(txtSearchCategory.Text);
        }

        void addFoodBinding()
        {   
            txtFoodName.DataBindings.Add(new Binding("Text", dgvFood.DataSource, "Name",true, DataSourceUpdateMode.Never));
            txtFoodId.DataBindings.Add(new Binding("Text", dgvFood.DataSource, "Id", true, DataSourceUpdateMode.Never));
            nudPrice.DataBindings.Add(new Binding("Value", dgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never)); 
            cbbFoodCategory.DataBindings.Add(new Binding("SelectedValue", dgvFood.DataSource, "CategoryID", true, DataSourceUpdateMode.Never));
        }
        void addTableBinding()
        {
            txtTableName.DataBindings.Add(new Binding("Text", dgvTable.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txtTabeID.DataBindings.Add(new Binding("Text", dgvTable.DataSource, "Id", true, DataSourceUpdateMode.Never));
            txtStatus.DataBindings.Add(new Binding("Text", dgvTable.DataSource, "Status", true, DataSourceUpdateMode.Never));
        }
        void addCategoryBinding()
        {
            txtNameCategory.DataBindings.Add(new Binding("Text", dgvCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txtCategoryID.DataBindings.Add(new Binding("Text", dgvCategory.DataSource, "Id", true, DataSourceUpdateMode.Never));
        }
        void loadCategoryIntoCombobox(ComboBox cbb)
        {
            cbb.DataSource = CategoryDAO.Instance.getListCategory();
            cbb.DisplayMember = "Name";
        }

        void loadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.getListFood();
        }

        void loadListTable()
        {
            tableList.DataSource = TableDAO.Instance.getListTable();
        }
        void loadListCategory()
        {
            categoryList.DataSource = CategoryDAO.Instance.getListCategory();
        }
        void loadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtPFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpToDate.Value = dtPFromDate.Value.AddMonths(1).AddDays(-1);
        }
        private void loadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dgvBill.DataSource = BillDAO.Instance.getListBillByDate(checkIn, checkOut);
        }

        #endregion
        
        #region events
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            loadListBillByDate(dtPFromDate.Value, dtpToDate.Value);
        }

        #endregion

        private void btnViewFood_Click(object sender, EventArgs e)
        {
            loadListFood();
        }

        private void btnViewCategory_Click(object sender, EventArgs e)
        {
            loadListCategory();
        }
        
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txtFoodName.Text;
            int categoryID = (cbbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nudPrice.Value;
            if (FoodDAO.Instance.insertFood(name, categoryID, price))
            {
                MessageBox.Show("Thêm món thành công","Thông báo");
                loadListFood();
                if (insertFood != null)
                    insertFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Thêm món thất bại", "Thông báo");
            }
        }

        private void btnAddCategory_Click_1(object sender, EventArgs e)
        {
            string name = txtNameCategory.Text;
            if (CategoryDAO.Instance.insertCategory(name))
            {
                MessageBox.Show("Thêm danh mục thành công", "Thông báo");
                loadListCategory();
                if (insertCategory != null)
                    insertCategory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Thêm danh mục thất bại", "Thông báo");
            }
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtFoodId.Text);
            string name = txtFoodName.Text;
            int categoryID = (cbbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nudPrice.Value;
            if (FoodDAO.Instance.upDateFood(id,name, categoryID, price))
            {
                MessageBox.Show("Sửa món thành công", "Thông báo");
                loadListFood();
                if (updateFood != null)
                    updateFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Sửa món thất bại", "Thông báo");
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {   
            int id = Convert.ToInt32(txtFoodId.Text);
            if (FoodDAO.Instance.deleteFood(id))
            {
                MessageBox.Show("Xoá món thành công", "Thông báo");
                loadListFood();
                if (deleteFood != null)
                    deleteFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Xóa món thất bại", "Thông báo");
            }

        }

        private event EventHandler insertFood;
        public event EventHandler InsetFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }
        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove{ deleteFood -= value; }
        }
        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }


        private event EventHandler insertCategory;
        public event EventHandler InsetCategory
        {
            add { insertCategory += value; }
            remove { insertCategory -= value; }
        }
        private event EventHandler deleteCategory;
        public event EventHandler DeleteCategory
        {
            add { deleteCategory += value; }
            remove { deleteCategory -= value; }
        }
        private event EventHandler updateCategory;
        public event EventHandler UpdateCategory
        {
            add { updateCategory += value; }
            remove { updateCategory -= value; }
        }


        private event EventHandler insertTable;
        public event EventHandler InsetTable
        {
            add { insertTable += value; }
            remove { insertTable -= value; }
        }
        private event EventHandler deleteTable;
        public event EventHandler DeleteTable
        {
            add { deleteTable += value; }
            remove { deleteTable -= value; }
        }
        private event EventHandler updateTable;
        public event EventHandler UpdateTable
        {
            add { updateTable += value; }
            remove { updateTable -= value; }
        }
        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            foodList.DataSource =  searchFoodByName(txtSearchNameFood.Text);
        }

        private void btnViewCount_Click(object sender, EventArgs e)
        {
            loadAccount();
        }

        private void btnAddcount_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string displayName = txtDisplayName.Text;
            int type = (int)nudAccountType.Value;
            addAccount(username, displayName, type);
        }

        private void btnDeleteCount_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            deleteAccount(username);

        }

        private void btnEditCount_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string displayName = txtDisplayName.Text;
            int type = (int)nudAccountType.Value;
            editAccount(username, displayName, type);

        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            resetPass(username);
        }

        private void txtPageBill_TextChanged(object sender, EventArgs e)
        {
            dgvBill.DataSource = BillDAO.Instance.getListBillByDatePage(dtPFromDate.Value, dtpToDate.Value, Convert.ToInt32(txtPageBill.Text));
        }

        private void btnFirstBill_Click(object sender, EventArgs e)
        {
            txtPageBill.Text = "1";
        }

        private void btnLassBill_Click(object sender, EventArgs e)
        {
            int sumRecord = BillDAO.Instance.getNumBillByDate( dtPFromDate.Value, dtpToDate.Value);
            int lastPage = sumRecord / 10;
            if (sumRecord % 10 != 0)
                lastPage ++;
            txtPageBill.Text = "" + lastPage;
        }

        private void btnBillPrivous_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txtPageBill.Text);

            if (page > 1)
                page--;

            txtPageBill.Text = page.ToString();

        }

        private void btnBillNext_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txtPageBill.Text);
            int sumRecord = BillDAO.Instance.getNumBillByDate(dtPFromDate.Value, dtpToDate.Value);

            if (page < sumRecord)
                page++;

            txtPageBill.Text = page.ToString();
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtCategoryID.Text);
            string name = txtNameCategory.Text;
            if (CategoryDAO.Instance.upDateFood(id, name))
            {
                MessageBox.Show("Sửa danh mục thành công", "Thông báo");
                loadListCategory();
                if (updateCategory != null)
                    updateCategory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Sửa danh mục thất bại", "Thông báo");
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtCategoryID.Text);
            if (CategoryDAO.Instance.deleteCategory(id))
            {
                MessageBox.Show("Xoá danh mục thành công", "Thông báo");
                loadListCategory();
                if (deleteCategory != null)
                    deleteCategory(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Xoá danh mục thất bại", "Thông báo");
            }
        }

        private void btnSearchTable_Click_1(object sender, EventArgs e)
        {

           tableList.DataSource = searchTableByName(txtSearchTable.Text);
        }

        private void btnViewTable_Click(object sender, EventArgs e)
        {

            loadListTable();
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = txtTableName.Text;
            if(TableDAO.Instance.insertTable(name))
            {
                MessageBox.Show("Thêm bàn mới thành công","Thông báo");
                loadListTable();
                if (insertTable != null)
                    insertTable(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Thêm bàn thất bại", "Thông báo");
            }
        
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtTabeID.Text);
            if (TableDAO.Instance.deleteTable(id))
            {
                MessageBox.Show("Xoá bàn thành công", "Thông báo");
                loadListTable();
                if (deleteTable != null)
                    deleteTable(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Xoá bàn thất bại", "Thông báo");
            }

        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtTabeID.Text);
            string name = txtTableName.Text;
            string status = txtStatus.Text;
            
           if (TableDAO.Instance.upDateTable(id,name,status))
            {
                MessageBox.Show("Sửa bàn thành công", "Thông báo");
                loadListTable();
                if (updateTable != null)
                    updateTable(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Sửa bàn thất bại", "Thông báo");
            }
        }

        public void btnReturn_Click(object sender, EventArgs e)
        { 
            
            this.Close();
        }
    }
}
