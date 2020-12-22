using CoffeeShopProject.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopProject.DAO{
    public class CategoryDAO{
        private static CategoryDAO instance;
        public static CategoryDAO Instance { 
            get {
                if (instance == null)
                    instance = new CategoryDAO();

                return instance;
            } 
            set => instance = value; 
        }
        private CategoryDAO() { }
        public List<Category> getListOfCategories(){
            List<Category> listOfCategories = new List<Category>();
            
            string query = "SELECT * FROM FoodCategory";
            DataTable data = DataProvider.Instance.executeQuery(query);

            foreach(DataRow item in data.Rows){
                Category category = new Category(item);
                listOfCategories.Add(category);
            }

            return listOfCategories;
        }

    }
}
