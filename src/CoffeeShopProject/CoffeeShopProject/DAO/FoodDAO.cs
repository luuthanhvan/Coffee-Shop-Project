using CoffeeShopProject.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopProject.DAO{
    public class FoodDAO{
        private static FoodDAO instance;
        public static FoodDAO Instance { 
            get {
                if (instance == null)
                    instance = new FoodDAO();

                return instance;
            } 
            set => instance = value; 
        }
        private FoodDAO() { }
        public List<Food> getFoodByCategoryId(int id){
            List<Food> listOfFoods = new List<Food>();

            string query = "SELECT * FROM Food WHERE idCategory = "+ id +"";
            DataTable data = DataProvider.Instance.executeQuery(query);

            foreach(DataRow item in data.Rows){
                Food food = new Food(item);
                listOfFoods.Add(food);
            }

            return listOfFoods;
        }
    }
}
