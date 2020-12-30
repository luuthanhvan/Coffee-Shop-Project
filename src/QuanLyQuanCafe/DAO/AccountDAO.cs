using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyQuanCafe.DTO;
using System.Security.Cryptography;

namespace QuanLyQuanCafe.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get {
                if (instance == null) instance = new AccountDAO();
                return AccountDAO.instance;
            }
            private set { AccountDAO.instance = value; }
        }
        private AccountDAO() { }

        public bool Login(string userName, string passWord)
        {
            
            string query = "EXEC USP_Login @userName , @passWord";

            DataTable result = DataProvider.Instance.ExecuteQuery(query,new object[]{userName,passWord});

            return result.Rows.Count > 0;
        }
        public DataTable getListAccount()
        {
            string query = " SELECT UserName, Display, type FROM Account";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public bool  updateAccount(string userName, string displayNamem, string pass, string newPass)
        {
            int data = DataProvider.Instance.ExcuteNoneQuery( "exec USP_UpdateAccount @userName , @displayName , @password , @newPassword ",new object[]{userName,displayNamem,pass,newPass});
            return data>0;
        }
        public Account getAccountByUserName(string userName)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM account WHERE userName = '" + userName+"'");
            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }

            return null;
        }
        public bool insertAccount(string username, string display , int type)
        {
            string query = string.Format("INSERT Account(Username,Display,Type,Password) VALUES (N'{0}',N'{1}',{2},'1234')", username, display, type);
            int result = DataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
        }
        public bool editAccount(string userName, string display, int type)
        {
            string query = string.Format("UPDATE Account SET username = N'{0}', display = N'{1}' , type ={2} WHERE username = N'{3}'", userName, display, type, userName);
            int result = DataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
        }
        public bool deleteAccount(string userName)
        {
            string query = string.Format("DELETE Account WHERE UserName = N'{0}'", userName);
            int result = DataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
        }
        public bool resetPassword(string username)
        {
            string query = string.Format("Update Account SET password = N'0' WHERE USerName = N'{0}'",username );
            int result = DataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
        }

        public void checkIn(string username)
        {
            DataProvider.Instance.ExcuteNoneQuery("EXEC USP_InsertSalaryStaffCheckIn " + username);
        }
        public void checkOut(string username)
        {
            DataProvider.Instance.ExcuteNoneQuery("EXEC USP_InsertSalaryStaffCheckOut " + username);
        }
        public DataTable getListSalaryByDate(string userName, DateTime checkIn, DateTime checkOut)
        {
            return DataProvider.Instance.ExecuteQuery(" exec USP_getListSalaryByDate @userName ,  @checkIn  , @checkOut ", new object[] {userName , checkIn, checkOut });
        }
    }
}
