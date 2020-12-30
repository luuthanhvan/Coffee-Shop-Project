using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get
            {
                if (instance == null) instance = new BillDAO();
                return BillDAO.instance;
            }
            set { BillDAO.instance = value; }
        }
        private BillDAO() { }

        public int getUncheckBillIDByTableID(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM Bill WHERE idTable =" + id + "AND status = 0");
            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.Id;
            }
            return -1;
        }
        public void checkOut(int id, int discount,float totalPrice)
        {
            string query = "UPDATE Bill SET dateCheckOut = GETDATE(), status = 1, discount = " + discount +", totalPrice = "+totalPrice+" WHERE id = " + id;
            DataProvider.Instance.ExcuteNoneQuery(query);
        }

        public void InsertBill(int id)
        {

              DataProvider.Instance.ExcuteNoneQuery("exec USP_InsertBill @idTable", new object[] { id });

            //DataProvider.Instance.ExcuteNoneQuery("exec USP_InsertBill @idTable = ", new object[] { 1 });
        }
        public DataTable getListBillByDate(DateTime checkIn, DateTime checkOut)
        { 
           return DataProvider.Instance.ExecuteQuery(" exec USP_getListBillByDate @checkIn  , @checkOut ", new object[] { checkIn, checkOut });
        }

        public int getNumBillByDate(DateTime checkIn, DateTime checkOut)
        {
            //return (int)DataProvider.Instance.ExcuteScalar(" exec USP_getNumBillByDate @checkIn  , @checkOut ", new object[] { checkIn, checkOut });
               return (int)DataProvider.Instance.ExcuteScalar(" exec USP_getNumBillByDate '"+ checkIn + "', '"+ checkOut + "'" );
        }
        public DataTable getListBillByDatePage(DateTime checkIn, DateTime checkOut, int page)
        {
            return DataProvider.Instance.ExecuteQuery(" exec USP_getListBillByDatePage @checkIn  , @checkOut , @page ", new object[] { checkIn, checkOut, page });
        }
        public int getMaxIDBill()
        {
            try
            {
                return (int)DataProvider.Instance.ExcuteScalar("SELECT MAX(id) FROM Bill");
            }
            catch
            {
                return 1;
            }
        }
    }
}
