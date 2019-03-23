using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ParkingSystem
{
    public partial class User : Form
    {
        private Userclass user;
        public User(object id)
        {
            user = new Userclass();
            InitializeComponent();
            string sqlExpression = $"SELECT [ID],[Full_Name],[Balance],[Debt],[Drive_License],[Password],[Login],[Pasport],[Admin] FROM [dbo].[User] WHERE [ID]='{id}' ";
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.City_ParkingConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    while (reader.Read()) // построчно считываем данные
                    {
                        for (int i = 0; i < user.GetType().GetProperties().Length-1; i++)
                        {
                            if(reader.GetValue(i) is DBNull)
                            {

                            }
                            else
                            user.GetType().GetProperties()[i].SetValue(user, reader.GetValue(i));
                        }

                    }
                }
            }
            textBox5.Text = user.Full_Name;
            textBox4.Text = user.Pasport;
            textBox3.Text = user.Drive_License;
            textBox1.Text = Convert.ToString(user.Balance);
            textBox2.Text = Convert.ToString(user.Debt);
            advancedDataGridView1.DataSource = user.carsList;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
