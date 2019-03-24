using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystem
{
    public class Cars
    {
        public int ID
        {
            get;
            set;
        }
        public string Number
        {
            get;
            set;

        }
        public int FK_User
        {
            get;
            set;
        }
        public static List<Cars> GetCars(int userId)
        {
            string sql = "SELECT * FROM [Cars] Where [FK_User]=" + userId;
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.City_ParkingConnectionString))
            {
                var adapter = new SqlDataAdapter(sql, con);
                adapter.Fill(dataTable);
            }
            return dataTable.AsEnumerable().Select(el => new Cars() { ID = Convert.ToInt32(el["ID"]), Number = Convert.ToString(el["Number"]), FK_User = Convert.ToInt32(el["FK_User"]) }).ToList();
        }
        public override string ToString()
        {
            return Number;
        }
    }
}
