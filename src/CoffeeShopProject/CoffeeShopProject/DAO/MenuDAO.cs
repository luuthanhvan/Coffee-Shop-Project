using CoffeeShopProject.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopProject.DAO{
    public class MenuDAO{
        private static MenuDAO instance;

        public static MenuDAO Instance { 
            get{
                if (instance == null)
                    instance = new MenuDAO();

                return instance;
            } 
            set => instance = value; 
        }

        private MenuDAO() { }

        public List<Menu> getListMenuByTable(int id){
            List<Menu> listMenu = new List<Menu>();

            string query = "SELECT dbo.Food.name, dbo.BillDetails.count, dbo.Food.price, dbo.Food.price*dbo.BillDetails.count as totalPrice FROM dbo.Bill, dbo.BillDetails, dbo.Food WHERE dbo.Bill.id = dbo.BillDetails.idBill AND dbo.BillDetails.idFood = dbo.Food.id AND dbo.Bill.status = 0 AND dbo.Bill.idTable = " + id +"";

            DataTable data = DataProvider.Instance.executeQuery(query);

            foreach(DataRow item in data.Rows){
                Menu menu = new Menu(item);
                listMenu.Add(menu);
            }

            return listMenu;
        }
    }
}
