using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TiemCamDo.DB_Layer
{
    class DBMain
    {
        private static DBMain instance;
        public static DBMain Instance
        {
            get {if (instance == null) instance = new DBMain(); return DBMain.instance; }
            private set { DBMain.instance = value; }
        }

        private DBMain() { }

        string ConnStr = @"Data Source=DESKTOP-UML28IP\SQLEXPRESS;" +
        "Initial Catalog=CamDo;" +
        "Integrated Security=True";
       public DataTable MyExecuteQuery(string query)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);

                connection.Close();
            }
            return data;
        }

        public int MyExecuteNonQuery(string query)
        {
            int data = 0;

            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                data = command.ExecuteNonQuery();

                connection.Close();
            }
            return data;
        }
    }
}
