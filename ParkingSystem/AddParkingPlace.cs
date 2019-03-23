using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParkingSystem
{
    public partial class AddParkingPlace : Form
    {
        public AddParkingPlace()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.City_ParkingConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = $"INSERT INTO [dbo].[Parkings_1] ([Cost]) Values('{textBox2.Text}'); INSERT INTO [dbo].[Place] (FK_Parking) VALUES((Select Max(ID) FROM [dbo].[Parkings_1])) ";
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                    this.DialogResult = DialogResult.OK;

                }
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
                this.DialogResult = DialogResult.Cancel;
            }

        }
    }
}
