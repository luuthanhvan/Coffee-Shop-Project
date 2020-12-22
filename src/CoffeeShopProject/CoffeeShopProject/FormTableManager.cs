using CoffeeShopProject.DAO;
using CoffeeShopProject.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeShopProject{
    public partial class FormTableManager : Form{
        public FormTableManager(){
            InitializeComponent();
            loadTable();
            loadCategory();
        }

        #region Methods
        void loadTable(){
            List<Table> listTable = TableDAO.Instance.loadTableList();
            foreach(Table table in listTable){
                Button button = new Button() { Width = TableDAO.tableWidth, Height = TableDAO.tableHeigh };
                
                // add table name for buttons
                button.Text = table.Name + Environment.NewLine + table.Status;
                
                // add event for buttons
                button.Click += Button_Click;
                button.Tag = table;

                switch (table.Status){
                    case "Trống":
                        button.BackColor = Color.LightGreen;
                        break;
                    default:
                        button.BackColor = Color.LightPink;
                        break;
                }

                // add buttons to flow layout panel Table
                this.flpTable.Controls.Add(button);
            }
        }
        void showBill(int id){
            this.lvBill.Items.Clear();
            float totalPrice = 0;
            // get list menu from table id
            List<CoffeeShopProject.DTO.Menu> listBillDetails = MenuDAO.Instance.getListMenuByTable(id);

            foreach(CoffeeShopProject.DTO.Menu item in listBillDetails){
                ListViewItem listViewItem = new ListViewItem(item.FoodName.ToString());
                listViewItem.SubItems.Add(item.Count.ToString());
                listViewItem.SubItems.Add(item.Price.ToString());
                listViewItem.SubItems.Add(item.TotalPrice.ToString());

                totalPrice += item.TotalPrice;

                this.lvBill.Items.Add(listViewItem);
            }

            CultureInfo culture = new CultureInfo("vi-VN");

            this.txtTotalPrice.Text = totalPrice.ToString("c", culture);
        }
        void loadCategory() {
            List<Category> listOfCategories = CategoryDAO.Instance.getListOfCategories();
            this.cbFoodCategory.DataSource = listOfCategories;
            this.cbFoodCategory.DisplayMember = "Name";
        }
        void loadFoodListByCategoryId(int id) {
            List<Food> listOfFoods = FoodDAO.Instance.getFoodByCategoryId(id);
            this.cbFood.DataSource = listOfFoods;
            this.cbFood.DisplayMember = "Name";
        }
        #endregion

        #region Events
        private void Button_Click(object sender, EventArgs e){
            // get table id from tag of button
            int tableId = ((sender as Button).Tag as Table).ID;
            this.lvBill.Tag = (sender as Button).Tag;
            showBill(tableId);
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
        private void cbFoodCategory_SelectedIndexChanged(object sender, EventArgs e){
            int id = 0;
            ComboBox cb = sender as ComboBox;
            
            if (cb.SelectedItem == null)
                return;

            Category selected = cb.SelectedItem as Category;
            id = selected.ID;

            loadFoodListByCategoryId(id);
        }
        private void btnAddFood_Click(object sender, EventArgs e){
            Table table = this.lvBill.Tag as Table;
            int idBill = BillDAO.Instance.getUncheckBillIdByTableId(table.ID);
            int foodId = (this.cbFood.SelectedItem as Food).ID;
            int count = (int)this.nmFoodCount.Value;

            // bill didn't exist
            if(idBill == -1){
                BillDAO.Instance.insertBill(table.ID);
                BillDetailsDAO.Instance.insertBillDetails(BillDAO.Instance.getMaxIdBill(), foodId, count);
            }
            // bill existed
            else{
                BillDetailsDAO.Instance.insertBillDetails(idBill, foodId, count);
            }
            showBill(table.ID);
        }
        #endregion
    }
}
