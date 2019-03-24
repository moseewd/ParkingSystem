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
    public partial class CarUpdate : Form
    {
        public int ID;
        public CarUpdate()
        {
            InitializeComponent();
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text.Length != 12)
            {
                button1.Enabled = false;

            }
            else
            {
                button1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.City_ParkingConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = $"UPDATE  [dbo].[Cars] set [Number]='{maskedTextBox1.Text}' Where [ID]={ID}";
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
