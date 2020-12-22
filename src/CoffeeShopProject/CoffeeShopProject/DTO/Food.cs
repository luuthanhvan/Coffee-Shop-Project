﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopProject.DTO{
    public class Food{
        private int iD;
        private string name;
        private int categoryId;
        private float price;
        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        public int CategoryId { get => categoryId; set => categoryId = value; }
        public float Price { get => price; set => price = value; }
        public Food(int id, string name, int categoryId, float price){
            this.ID = id;
            this.Name = name;
            this.CategoryId = categoryId;
            this.Price = price;
        }
        public Food(DataRow row){
            this.ID = (int)row["id"];
            this.Name = row["name"].ToString();
            this.categoryId = (int)row["idCategory"];
            this.price = (float)Convert.ToDouble(row["price"].ToString());
        }
    }
}
