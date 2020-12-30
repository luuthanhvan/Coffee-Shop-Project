using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyQuanCafe.DTO;
using System.Data;
namespace QuanLyQuanCafe.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance
        {
            get { if (instance == null) instance = new FoodDAO();
                return FoodDAO.instance; }
            set { FoodDAO.instance = value; }
        }
        private FoodDAO() { }
        public List<Food> getFoodByCategoryID(int id)
        {
            List<Food> list = new List<Food>();
            string query = "SELECT * FROM Food WHERE idCategory = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {               
                Food food = new Food(item);
                list.Add(food);
            }
            return list;
        }
        public List<Food> searchFoodByName(string name)
        {
            List<Food> list = new List<Food>();
            string query = "SELECT * FROM Food WHERE name LIKE N'%"+ name +"%'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }
            return list;
        }
        public List<Food> getListFood()
        {
            List<Food> list = new List<Food>();
            string query = "SELECT id,idCategory,name,price  FROM Food ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }
            return list;
        }
        public bool insertFood(string name, int id, float price)
        {
            string query = string.Format("INSERT Food(name,idCategory,price) VALUES (N'{0}',{1},{2})",name,id,price);
            int result = DataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
        }
        public bool upDateFood(int idFood, string name, int id, float price)
        {
            string query = string.Format("UPDATE Food SET name = N'{0}', idCategory = {1} , price ={2} WHERE id = {3}", name, id, price,idFood);
            int result = DataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
        }
        public bool deleteFood(int idFood)
        {
            BillInfoDAO.Instance.deleteBillInfoByFood(idFood);
            string query = string.Format("DELETE Food WHERE id = {0}", idFood);
            int result = DataProvider.Instance.ExcuteNoneQuery(query);
            return result > 0;
        }

        public void deleteFoodByCategory(int id)
        {
            DataProvider.Instance.ExecuteQuery("DELETE Food WHERE idCategory = " + id);

        }
    }
}
