using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace Electricals_PointOfSale.Models
{
   public class DataBaseHandler
    {
        private static readonly DataBaseHandler Instance = new DataBaseHandler();
        private static string conString = "Data Source = Database/ElectricalsStoreDatabase.db; Version = 3;";
        private SQLiteConnection connection = new SQLiteConnection(conString);
        private SQLiteCommand command;

       
        //private SQLiteDataAdapter sqlDataAdapter;
        //private DataSet sqlDataSet;
        public DataTable sqlDataTable;
        private int rowCount;
        public int numRows
        {
            get
            {
                return rowCount;
            }
            set
            {
                rowCount = value;
            }
        }
        
      
        public string successMessage;
        public int recordsCount;
       
                
        public void insertQuery(string query , params object[] param)
        {
            try
            {
                command = new SQLiteCommand(query, connection);
                for (int i = 0; i < param.Length; i++)
                {
                    command.Parameters.AddWithValue("@p" + i, param[i]);
                }
                connection.Open();
                int count = command.ExecuteNonQuery();
                connection.Close();
                if (count == 1)
                {
                    successMessage = "Success";
                }
                else { successMessage = "Failed"; }
            }
            catch (Exception ex)
            {

                handleError(ex);
            }


        }
        public void selectQuery(string query)
        {
            try
            {

            
                DataTable DT;
                using (SQLiteDataAdapter sqlDa = new SQLiteDataAdapter(query, connection))
                {
                    SQLiteCommandBuilder cb = new SQLiteCommandBuilder(sqlDa);
                    
                    DT = new DataTable();
                    sqlDa.Fill(DT);
                    numRows = DT.Rows.Count;
                    //sqlDataTable.Clear();
                    sqlDataTable = DT;
                    //DT.Clear();
                }
               


            }
            catch (Exception ex)
            {
                handleError(ex);
            }
            

        }
   

        public void updateQuery(string query)
        {
            try
            {
                command = new SQLiteCommand(query, connection);
                connection.Open();
                int count = command.ExecuteNonQuery();
                connection.Close();
                if (count > 0)
                {
                    successMessage = "Success";
                }
                else successMessage = "Failed";
            }
            catch (Exception e)
            {
                handleError(e);

            }


        }

        public void createTable(string query)
        {
            try
            {
                command = new SQLiteCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Open();
            }
            catch (Exception ex)
            {
                handleError(ex);
              
            }
           
        }

        public void deleteQuery(string query)
        {
            command = new SQLiteCommand(query, connection);
            connection.Open();
            int count = command.ExecuteNonQuery();
            connection.Close();
        }

        private void handleError(Exception error)
        {
           // string errorMessage = "There was an Error in your Request please Rectify and TRY AGAIN";
            successMessage = error.Message;
        }
    }
}
