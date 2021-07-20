using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Database_Connect
{
    class Program
    {



        static string ConnectionString = "Data Source=DESKTOP-O5TE4OQ\\SQLEXPRESS;Initial Catalog = Test; Integrated Security = True";

        static void Main(string[] args)
        {
            //string firstName = "";
            //string lastName = "";
            //
            //string userInput = "";
           


            DbConnection data = new DbConnection(ConnectionString);
            Console.Write(" This is your Scaler :");
            data.ExecuteScalar("select * from customer;");
            Console.WriteLine();

            List<object[]> resultList = data.ExecuteQuery("select * from customer;");

            Console.Write("Rows Affected");
            data.ExecuteNonQuery("UPDATE Customer SET FirstName = 'Collin' WHERE CustomerId = '2004';");
      
            Console.WriteLine();

            for (int index = 0; index < resultList.Count(); index++)
            {
                object[] aryTemp = resultList[index];
                for (int count = 0; count < aryTemp.Length; count++)
                {
                    Console.Write(aryTemp[count]+ "\t");
                }
                Console.WriteLine();
            }
            Console.ReadKey();

           //Console.Write("Please enter a first name (just leave it blank for no first name): ");
           //firstName = Console.ReadLine();
           //
           //
           //
           //Console.Write("Please enter a last name: ");
           //lastName = Console.ReadLine();
           //
           //PrintDatabaseTable("Customer", firstName, lastName);
           //
           //
           //
           //Console.WriteLine();




            Console.ReadKey();

        }



        static void PrintDatabaseTable(string tableName, string firstName, string lastName)
        {
            //establish connection
            SqlConnection connection = new SqlConnection(ConnectionString);



            //build string to hold sql
            string sqlCommanString = "";
            if (firstName == "")
            {
                sqlCommanString = string.Format("SELECT * FROM {0} WHERE LastName = '{1}'", tableName, lastName);
            }
            else
            {
                sqlCommanString = string.Format("SELECT * FROM {0} WHERE FirstName = '{1}' and LastName = '{2}'", tableName, firstName, lastName);
            }




            //build sql command object instance
            SqlCommand command = new SqlCommand(sqlCommanString, connection);



            //open database connection
            connection.Open();
            //try to connect to database
            try
            {
                //execute a database reader
                SqlDataReader dataReaderResults = command.ExecuteReader();
                //get & iterate through results
                while (dataReaderResults.Read())
                {
                    //print all fields of one record
                    for (int index = 0; index < dataReaderResults.FieldCount; index++)
                    {
                        Console.Write(dataReaderResults[index] + "\t");
                    }//end for



                    Console.WriteLine();
                }//end while
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR INTERACTING WITH DATABASE");
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }



            //close connection to database
            connection.Close();
        }
    }
}