using CoffeeShopProject.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopProject.DAO{
    public class BillDetailsDAO{
        private static BillDetailsDAO instance;
        public static BillDetailsDAO Instance { 
            get {
                if (instance == null)
                    instance = new BillDetailsDAO();

                return instance;
            }
            set => instance = value; 
        }
        private BillDetailsDAO() { }
        public List<BillDetails> getListOfBillDetails(int id){
            List<BillDetails> listOfBillDetails = new List<BillDetails>();

            string query = "SELECT * FROM dbo.BillDetails WHERE idBill = " + id +"";
            DataTable data = DataProvider.Instance.executeQuery(query);

            foreach(DataRow item in data.Rows){
                BillDetails billDetails = new BillDetails(item);
                listOfBillDetails.Add(billDetails);
            }

            return listOfBillDetails;
        }
        public void insertBillDetails(int billId, int foodId, int count){
            string query = "EXEC USP_InsertBillDetails @idBill , @idFood , @count";
            DataProvider.Instance.executeNonQuery(query, new object[] { billId, foodId, count});
        }
    }
}
