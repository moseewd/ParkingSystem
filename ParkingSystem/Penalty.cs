using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystem
{
   public  class Penalty
    {
        public int ID
        {
            get;
            set;

        }
        public int Sum
        {
            get;
            set;

        }
        public int FK_User
        {
            get;
            set;
        }

        public static List< Penalty> GetPenalty(int userId)
        {
            string sql = "SELECT * FROM [Penalty] Where [FK_User]=" + userId;
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.City_ParkingConnectionString))
            {
                var adapter = new SqlDataAdapter(sql, con);
                adapter.Fill(dataTable);
            }
            return dataTable.AsEnumerable().Select(el => new Penalty() { ID = Convert.ToInt32(el["ID"]), Sum = Convert.ToInt32(Convert.ToString(el["Sum"])), FK_User = Convert.ToInt32(el["FK_User"]) }).ToList();
        }
        public override string ToString()
        {
            return Convert.ToString(Sum);
        }
    }
}
