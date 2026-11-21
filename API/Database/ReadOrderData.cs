using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.Interfaces;
using MySql.Data.MySqlClient;

namespace API.Database
{
    public class ReadOrderData : IGetAllOrders, IGetOrder
    {
        public List<OrderForm> GetAllOrders()
        {
            try
            {
                ConnectionString myConnection = new ConnectionString();
                string cs = myConnection.cs; 
                using MySqlConnection con = new MySqlConnection(cs); 
                con.Open();

                string stm = "select * from orderform"; 
                using var cmd = new MySqlCommand(stm, con); 

                using MySqlDataReader rdr = cmd.ExecuteReader();

                List<OrderForm> allOrders = new List<OrderForm>(); 
                while(rdr.Read()) 
                {
                    allOrders.Add(RecordMappers.ToOrderForm(rdr)); 
                }

                System.Console.WriteLine("Retrieved Successfully");
                return allOrders;
            }
            catch(Exception ex)
            {
                System.Console.WriteLine("Retrieval Failed"  + ex.Message);
                return null;
            }
        }

        public OrderForm GetOrder(int id)
        {
            try
            {
                ConnectionString myConnection = new ConnectionString();
                string cs = myConnection.cs; 
                using MySqlConnection con = new MySqlConnection(cs); 
                con.Open();

                string stm = "select * from orderform where order_id = @id"; 
                using var cmd = new MySqlCommand(stm, con); 

                cmd.Parameters.AddWithValue("@id",id); 
                cmd.Prepare(); 

                using MySqlDataReader rdr = cmd.ExecuteReader();

                if (!rdr.Read())
                {
                    System.Console.WriteLine("No matching record found");
                    return null;
                }


                System.Console.WriteLine("Retrieved Successfully");

                return RecordMappers.ToOrderForm(rdr);
            }
            catch
            {
                System.Console.WriteLine("Retrieval Failed");
                return null;
            }
        }
    }
}