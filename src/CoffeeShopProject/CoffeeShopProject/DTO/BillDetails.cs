using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopProject.DTO{
    public class BillDetails{
        private int iD;
        private int billId;
        private int foodId;
        private int count;

        public int ID { get => iD; set => iD = value; }
        public int BillId { get => billId; set => billId = value; }
        public int FoodId { get => foodId; set => foodId = value; }
        public int Count { get => count; set => count = value; }

        public BillDetails(int id, int billId, int foodId, int count){
            this.ID = id;
            this.BillId = billId;
            this.FoodId = foodId;
            this.Count = count;
        }

        public BillDetails(DataRow row){
            this.ID = (int)row["id"];
            this.BillId = (int)row["idBill"];
            this.FoodId = (int)row["idFood"];
            this.Count = (int)row["count"];
        }
    }
}
