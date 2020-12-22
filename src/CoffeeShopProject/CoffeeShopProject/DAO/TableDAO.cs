using CoffeeShopProject.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopProject.DAO{
    public class TableDAO{
        private static TableDAO instance;
        public static int tableWidth = 80;
        public static int tableHeigh = 80;
        public static TableDAO Instance { 
            get {
                if (instance == null)
                    instance = new TableDAO();

                return instance;
            }
            set => instance = value;
        }
        private TableDAO() { }
        public List<Table> loadTableList(){
            List<Table> listOfTables = new List<Table>();

            string query = "EXEC dbo.USP_GetTableList";
            DataTable data = DataProvider.Instance.executeQuery(query);

            // convert DataTable to List<Table>
            foreach(DataRow item in data.Rows){
                Table table = new Table(item);
                listOfTables.Add(table);
            }

            return listOfTables;
         }
    }
}
