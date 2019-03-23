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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
                string sqlExpression = $"SELECT [ID],[Admin] FROM [dbo].[User] WHERE [Login]='{this.textBox1.Text}' and [Password]='{textBox2.Text}' ";
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.City_ParkingConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows) // если есть данные
                    {

                        while (reader.Read()) // построчно считываем данные
                        {
                            object id = reader.GetValue(0);
                            object admin = reader.GetValue(1);
                        Debug.WriteLine(admin);
                        if (admin.Equals(1))
                        {
                            Admin adForm = new Admin();
                            adForm.Show();

                        }
                        else
                        {
                            var userForm = new User(id);
                            userForm.Show();
                        }
                       
                    }
                    }
                    else
                    {
                        MessageBox.Show("Пользователя не существует в системе");
                    }

                    reader.Close();
                }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var reg = new Register();
            if (reg.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Пользователь успешно добавлен в базу данных");
            }
            else
            {
                MessageBox.Show("Неудалось добавить пользователя в базу данных");

            }
        }
    }
}
