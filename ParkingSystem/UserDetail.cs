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
    public partial class UserDetail : Form
    {
        Userclass userclass;
        

        public UserDetail(int user)
        {
            InitializeComponent();

            userclass = new Userclass();
            advancedDataGridView1.DataSource = userclass.penaltylist;
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
                        for (int i = 0; i < userclass.GetType().GetProperties().Length - 2; i++)
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
            textBox2.Text = Convert.ToString(userclass.Login);
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
            
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.City_ParkingConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = $"INSERT INTO [dbo].[Penalty] ([Sum],[FK_User]) Values('{maskedTextBox1.Text}',{userclass.ID})";
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                    MessageBox.Show("Штраф назначен");
                    advancedDataGridView1.DataSource = userclass.penaltylist;
                }
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
                MessageBox.Show("Не удалось назначить штраф");
            }
        }

        private void UserDetail_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            advancedDataGridView1.DataSource = userclass.penaltylist;
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text.Length != 5)
            {
                
                button1.Enabled = false;

            }
            else
            {
                button1.Enabled = true;
            }
        }

        private void advancedDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
