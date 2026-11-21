using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.Interfaces;
using MySql.Data.MySqlClient;

namespace API.Database
{
    public class ReadFurnitureData : IGetAllFurniture, IGetFurniture
    {
        public List<Furniture> GetAllFurniture()
        {
            try
            {
                ConnectionString myConnection = new ConnectionString();
                string cs = myConnection.cs; 
                using MySqlConnection con = new MySqlConnection(cs); 
                con.Open();

                string stm = "select * from furniture"; 
                using var cmd = new MySqlCommand(stm, con); 

                using MySqlDataReader rdr = cmd.ExecuteReader();

                List<Furniture> allFurniture = new List<Furniture>(); 
                while(rdr.Read()) 
                {
                    allFurniture.Add(RecordMappers.ToFurniture(rdr)); 
                }

                System.Console.WriteLine("Retrieved Successfully");
                return allFurniture;
            }
            catch(Exception ex)
            {
                System.Console.WriteLine("Retrieval Failed"  + ex.Message);
                return null;
            }
        }

        public Furniture GetFurniture(int id)
        {
            try
            {
                ConnectionString myConnection = new ConnectionString();
                string cs = myConnection.cs; 
                using MySqlConnection con = new MySqlConnection(cs); 
                con.Open();

                string stm = "select * from furniture where furniture_id = @id"; 
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

                return RecordMappers.ToFurniture(rdr);
            }
            catch
            {
                System.Console.WriteLine("Retrieval Failed");
                return null;
            }
        }

       
    }
}
