using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;


namespace Database_Connect
{

    class DbConnection
    {
       
        //TODO COMPLETE METHODS
        //TODO TEST METHODS
        //TODO DESIGN AN ErrorOccured() METHOD THAT RETURNS TRUE IF AN ERROR HAS OCCOURED SINCE THE LAST TIME THE FUNCTION WAS RAN.
        

        

        private string _connectionString;
        private SqlConnection _connection;
        private List<string> _errorList = new List<string>();

        public DbConnection(string newConnString)
        {
            _connectionString = newConnString;
            _connection = new SqlConnection(_connectionString);
        }//end constructor

        private bool BuildConnection()
        {
            try
            {

                _connection.Open();
            }
            catch (Exception ex)
            {
                //log error
                return false;
            }//end try

            return true;
        }//end method
        private bool CloseConnection()
        {
            try
            {
                _connection.Close();
            }
            catch (Exception ex)
            {
                _errorList.Add($"ERROR {_errorList.Count + 1} -->  " + ex.Message);
                return false;
            }//end try

            return true;
        }//end method

        public List<object[]> ExecuteQuery(string queryString)
        {
            //BUILD SQL COMMAND OBJECT INSTANCE
            SqlCommand command = new SqlCommand(queryString, _connection);
            List<object[]> resultList = new List<object[]>();
            //OPEN DATABASE CONNECTION
            if (BuildConnection() == false)
            {
            

                return resultList; //empty list of array
            }//end if

            //TRY TO CONNECT TO DB
            try
            {
                //EXEUCTE A DABATASE READER
                SqlDataReader dataReaderResults = command.ExecuteReader();

               
                //GET & ITERATE THROUGH RESULTS
                while (dataReaderResults.Read())
                {
                    //add those results to the list
                    for (int index = 0; index < dataReaderResults.FieldCount; index++)
                    {
                        object[] aryTemp = new object[6];
                        for (int count = 0; count < 6; count++)
                        {
                            aryTemp[count] = dataReaderResults[index];
                            index++;
                        }
                        resultList.Add(aryTemp);
                       
                    }//end for

                }//end while

            }
            catch (Exception ex)
            {
                _errorList.Add($"ERROR {_errorList.Count + 1} -->  " + ex.Message);
            }//end try

            CloseConnection();

            return resultList; //List of results

        }//end function
    
        public void ExecuteScalar(string queryString)
        {
            //OPEN DATABASE CONNECTION
            if (BuildConnection() == false)
            {
                Console.Write("error connection failed");
            }//end if


            try
            {
                SqlCommand command = new SqlCommand(queryString, _connection);
                object dataReaderResults = (object)command.ExecuteScalar();
                Console.Write(dataReaderResults);
            }
            catch (Exception ex)
            {
                _errorList.Add($"ERROR {_errorList.Count + 1} -->  " + ex.Message);
            }//end try

            CloseConnection();

           
        }//end function

        public void ExecuteNonQuery(string queryString)
        {
            int dataReaderResults;
            if (BuildConnection() == false)
            {
                Console.Write("error connection failed");
            }//end if


            try
            {
                SqlCommand command = new SqlCommand(queryString, _connection);
                dataReaderResults = command.ExecuteNonQuery();
                Console.Write(dataReaderResults);
            }
            catch (Exception ex)
            {
                _errorList.Add($"ERROR {_errorList.Count + 1} -->  " + ex.Message);
            }//end try

            CloseConnection();

            

        }//end function


         public string GetErrors()
          {
            string errors = "";
            for (int index = 0; index < _errorList.Count; index++)
            {
                errors = string.Format(_errorList[index] + "\n");
            }
            return errors;
            //return a string showing all errors
        }
    
    
         public string ClearErrors()
         {
            _errorList.Clear();
            return "errors have been cleared";
            //clear error list
        }
    
      public string LogErrors(string filepath)
     {
            StreamWriter file = new StreamWriter(filepath);
            for (int index = 0; index < _errorList.Count; index++)
            {
                file.WriteLine(_errorList[index] + "\n");
            }
            return "errors have been logged";

            file.Close();
            //write errors to a text file
        }




    }
}
