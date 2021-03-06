﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyQuanCafe.DTO;
using System.Data;

namespace QuanLyQuanCafe.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Isntance
        {
            get { if(instance == null) instance = new MenuDAO();
                return MenuDAO.instance; }
            set { MenuDAO.instance = value; }
        }
        private MenuDAO() { }

        public List<Menu> getListMenuByTable(int id)
        {
            List<Menu> listMenu = new List<Menu>();
            string query = "SELECT f.name,bi.count,f.price,f.price*bi.count AS totalPrice FROM BillInfo AS bi, Bill AS b, Food AS f WHERE b.status=0 AND bi.idBill = b.id AND bi.idFood = f.id AND b.idTable=" + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Menu menu = new Menu(item);
                listMenu.Add(menu);
            }
            return listMenu;
        }
    }
}
