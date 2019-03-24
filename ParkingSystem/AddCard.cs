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
    public partial class AddCard : Form
    {
        public int ID;
        public AddCard()
        {
            InitializeComponent();
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text.Length != 14)
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
                    command.CommandText = $"INSERT INTO [dbo].[Payment] ([Card],[FK_User]) Values('{maskedTextBox1.Text}',{ID})";
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
