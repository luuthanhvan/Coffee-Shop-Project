using CoffeeShopProject.DAO;
using CoffeeShopProject.DTO;
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
            loadTable();
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

            List<CoffeeShopProject.DTO.Menu> listBillDetails = MenuDAO.Instance.getListMenuByTable(id);

            foreach(CoffeeShopProject.DTO.Menu item in listBillDetails){
                ListViewItem listViewItem = new ListViewItem(item.FoodName.ToString());
                listViewItem.SubItems.Add(item.Count.ToString());
                listViewItem.SubItems.Add(item.Price.ToString());
                listViewItem.SubItems.Add(item.TotalPrice.ToString());

                this.lvBill.Items.Add(listViewItem);
            }
        }
        #endregion

        #region Events
        private void Button_Click(object sender, EventArgs e){
            int tableId = ((sender as Button).Tag as Table).ID;
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
        #endregion
    }
}
