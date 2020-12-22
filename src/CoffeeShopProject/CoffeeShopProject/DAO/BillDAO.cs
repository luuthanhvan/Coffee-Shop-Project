using CoffeeShopProject.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopProject.DAO{
    public class BillDAO{
        private static BillDAO instance;
        
        public static BillDAO Instance { 
            get{
                if (instance == null)
                    instance = new BillDAO();

                return instance;
            } 
            set => instance = value; 
        }
        
        private BillDAO() { }
        
        public int getUncheckBillIdByTableId(int id){
            string query = "SELECT * FROM dbo.Bill WHERE idTable = " + id + " AND status = 0";
            DataTable data = DataProvider.Instance.executeQuery(query);
            if(data.Rows.Count > 0){
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }
            else{
                return -1;
            }
        }
    }
}
