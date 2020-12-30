using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyQuanCafe.DTO;
using QuanLyQuanCafe.DAO;
using System.Data;

namespace QuanLyQuanCafe.DAO
{
      class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance
        {
            get { if (instance == null) instance = new TableDAO();
                return TableDAO.instance; }
            set { TableDAO.instance = value; }
        }

        public static int TableWidth = 100;
        public static int TableHeight = 100;
        private TableDAO() { }
        public void switchTable(int id1, int id2)
        {
            
           DataProvider.Instance.ExecuteQuery("EXEC USP_SwitchTable @idTable1 , @idTable2 ", new object[] { id1, id2 });
        }
        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();
            DataTable data = DataProvider.Instance.ExecuteQuery("EXEC USP_GetTableList");
            foreach(DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }
            return tableList;
        }
        public List<Table> getListTable()
        {
            List<Table> list = new List<Table>();
            string query = "SELECT * FROM TableFood";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                list.Add(table);
            }
            return list;
        }
       

        public List<Table> searchTableByName(string name)
        {
            List<Table> list = new List<Table>();
            string query = "SELECT * FROM TableFood WHERE name LIKE N'%" + name + "%'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                list.Add(table);
            }
            return list;
        }

        public bool insertTable(string name)
        {
            string query = string.Format("INSERT TableFood(name) VALUES (N'{0}')", name);
            int result = DataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
        }
        public bool upDateTable(int id, string name, string status)
        {
            string query = string.Format("UPDATE TableFood SET name = N'{0}', status = N'{1}' WHERE id = {2}", name, status, id);
            int result = DataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
        }
        public bool deleteTable(int idTable)
        {
            BillInfoDAO.Instance.deleteBillInfoByTable(idTable);
            string query = string.Format("DELETE TableFood WHERE id = {0}", idTable);
            int result = DataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
        }


    }
}
 