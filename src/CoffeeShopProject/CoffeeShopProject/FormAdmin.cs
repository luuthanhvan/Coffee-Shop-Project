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
    public partial class FormAdmin : Form{
        public FormAdmin(){
            InitializeComponent();
            loadAccountList();
        }

        void loadAccountList(){
            string query = "EXEC dbo.USP_GetAccountByUserName @userName";

            this.dtgvAccount.DataSource = DataProvider.Instance.executeQuery(query, new object[] {"ltvan"});
        }
    }
}
