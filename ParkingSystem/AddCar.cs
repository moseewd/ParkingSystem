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
    public partial class AddCar : Form
    {
        public int ID;
        public AddCar()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.City_ParkingConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = $"INSERT INTO [dbo].[Cars] ([Number],[FK_User]) Values('{maskedTextBox1.Text}',{ID})";
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

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text.Length != 12)
            {
                button2.Enabled = false;

            }
            else
            {
                button2.Enabled = true;
            }
        }

        private void AddCar_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
        }
    }
}
