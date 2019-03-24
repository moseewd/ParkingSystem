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
    public partial class UserDetail : Form
    {
        Userclass userclass;
        

        public UserDetail(int user)
        {
            InitializeComponent();
            userclass = new Userclass();
            string sqlExpression = $"SELECT [ID],[Full_Name],[Balance],[Debt],[Drive_License],[Password],[Login],[Pasport],[Admin] FROM [dbo].[User] WHERE [ID]='{user}' ";
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.City_ParkingConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    while (reader.Read()) // построчно считываем данные
                    {
                        for (int i = 0; i < userclass.GetType().GetProperties().Length - 1; i++)
                        {
                            if (reader.GetValue(i) is DBNull)
                            {

                            }
                            else
                                userclass.GetType().GetProperties()[i].SetValue(userclass, reader.GetValue(i));
                        }

                    }
                }
            }
            textBox5.Text = userclass.Full_Name;
            textBox4.Text = userclass.Pasport;
            textBox3.Text = userclass.Drive_License;
            textBox1.Text = Convert.ToString(userclass.Balance);
            textBox2.Text = Convert.ToString(userclass.Debt);
            listBox1.DataSource = userclass.carsList;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void UserDetail_Load(object sender, EventArgs e)
        {

        }
    }
}
