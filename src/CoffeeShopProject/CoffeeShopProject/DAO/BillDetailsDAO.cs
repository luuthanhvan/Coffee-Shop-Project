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

        public List<BillDetails> getListBillDetails(int id){
            List<BillDetails> listBillDetails = new List<BillDetails>();

            string query = "SELECT * FROM dbo.BillDetails WHERE idBill = " + id +"";
            DataTable data = DataProvider.Instance.executeQuery(query);

            foreach(DataRow item in data.Rows){
                BillDetails billDetails = new BillDetails(item);
                listBillDetails.Add(billDetails);
            }

            return listBillDetails;
        }
    }
}
