using System;
using System.Data;
using System.Data.SqlClient;

namespace denemestaj
{
    public class SqlOperations
    {
        public static SqlConnection connection = new SqlConnection("Data Source=HAMZA;Initial Catalog=notlar;Integrated Security=True");


        public static void CheckConnection(SqlConnection tempConnection)
        {
            if (tempConnection.State == ConnectionState.Closed)
            {
                tempConnection.Open();
            }
            else
            {

            }
        }
    }
}