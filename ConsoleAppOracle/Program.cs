using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppOracle
{
    class Program
    {
        static void Main(string[] args)
        {
            //Demo: Basic ODP.NET Core application to connect, query, and return
            // results from an OracleDataReader to a console

            //Create a connection to Oracle			
            string conString = "User Id=hr;Password=oracle;" +

            //How to connect to an Oracle DB without SQL*Net configuration file
            //  also known as tnsnames.ora.
            "Data Source=localhost:1522/xe;";

            //How to connect to an Oracle DB with a DB alias.
            //Uncomment below and comment above.
            //"Data Source=<service name alias>;";

            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        //Use the command to display employee names from 
                        // the EMPLOYEES table
                        cmd.CommandText = "select first_name from employees where department_id = :id";

                        // Assign id to the department number 50 
                        OracleParameter id = new OracleParameter("id", 50);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine("Employee First Name: " + reader.GetString(0));
                        }

                        Console.WriteLine();
                        Console.WriteLine("Press 'Enter' to continue");

                        reader.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.ReadLine();
                }
            }
        }
    }
}
