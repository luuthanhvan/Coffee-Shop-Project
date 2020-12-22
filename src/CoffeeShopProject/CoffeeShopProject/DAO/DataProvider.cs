using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopProject.DAO{
    class DataProvider{
        // create an static instance -> through this instance, we can get/set data from database just only one connection
        private static DataProvider instance;
        public static DataProvider Instance{ // encapsulation instance
            get {
                if (instance == null)
                    instance = new DataProvider();
                
                return DataProvider.instance;
            }
            private set { 
                DataProvider.instance = value; 
            }
        }

        // constructor
        private DataProvider(){}

        private string connectionStr = @"Data Source=.\sqlexpress;Initial Catalog=CoffeeShop;Integrated Security=True";
        
        public DataTable executeQuery(string query, object[] parameters = null){
            DataTable data = new DataTable();
            
            using (SqlConnection connection = new SqlConnection(connectionStr)){
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                if(parameters != null){
                    string[] listParams = query.Split(' ');
                    int cnt = 0;
                    foreach(string item in listParams){
                        if (item.Contains('@')){
                            command.Parameters.AddWithValue(item, parameters[cnt]);
                            cnt++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
                connection.Close();
            }

            return data;
        }

        // return number of lines which execute successfully
        public int executeNonQuery(string query, object[] parameters = null){
            int nbLines = 0;

            using (SqlConnection connection = new SqlConnection(connectionStr)){
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                if (parameters != null){
                    string[] listParams = query.Split(' ');
                    int cnt = 0;
                    foreach (string item in listParams){
                        if (item.Contains('@')){
                            command.Parameters.AddWithValue(item, parameters[cnt]);
                            cnt++;
                        }
                    }
                }

                nbLines = command.ExecuteNonQuery();

                connection.Close();
            }

            return nbLines;
        }

        // return the quantity from SELECT COUNT
        public object executeScalar(string query, object[] parameters = null){

            object data = 0;

            using (SqlConnection connection = new SqlConnection(connectionStr)){
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                if (parameters != null){
                    string[] listParams = query.Split(' ');
                    int cnt = 0;
                    foreach (string item in listParams){
                        if (item.Contains('@')){
                            command.Parameters.AddWithValue(item, parameters[cnt]);
                            cnt++;
                        }
                    }
                }

                data = command.ExecuteScalar();

                connection.Close();
            }

            return data;
        }
    }
}
