using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParkingSystem
{
    public partial class ParkingDetailForm : Form
    {
        int id = 0;
        public ParkingDetailForm()
        {
            InitializeComponent();
        }
        public ParkingDetailForm( int idPark)
            {
            InitializeComponent();
            idPark = id;
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.City_ParkingConnectionString))
            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT Status FROM Place WHERE FK_Parking= " + id, con))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
                for (int i=0; i<dt.Rows.Count;i++)
                {
                    listView1.Items.Add("Парковочное место № "+i + " "+ (((int)dt.Rows[i][0])==0?"Занято":"Свободно"));
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ParkingDetailForm_Load(object sender, EventArgs e)
        {

        }
    }
}
