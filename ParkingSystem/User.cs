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
            var paid = new Pay(user.ID);
            if (paid.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Способ оплаты добавен");
                textBox7.Text = paid.listBox1.SelectedItem.ToString();
          }
            else
            {
                MessageBox.Show("Неудалось добавить способ оплаты");

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var reg = new AddCar();
            reg.ID = user.ID;
            if (reg.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Автомобиль успешно добавлен в базу данных");
                advancedDataGridView1.DataSource = user.carsList;
            }
            else
            {
                MessageBox.Show("Неудалось добавить автомобиль в базу данных");

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (advancedDataGridView1.SelectedRows.Count <= 0)
            {
                return;
            }
           Cars car = user.carsList[advancedDataGridView1.SelectedRows[0].Index];
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.City_ParkingConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = $"DELETE FROM [dbo].[Cars] Where [ID]={car.ID}";
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                    MessageBox.Show("Автомобиль успешно удален из базы данных");
                    advancedDataGridView1.DataSource = user.carsList;
                }
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
                MessageBox.Show("Неудалось удалить автомобиль из базы данных");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (advancedDataGridView1.SelectedRows.Count <= 0)
            {
                return;
            }
            Cars car = user.carsList[advancedDataGridView1.SelectedRows[0].Index];
            var reg = new CarUpdate();
            reg.ID = car.ID;
            if (reg.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Информация об автомобиле успешно обновлена");
                advancedDataGridView1.DataSource = user.carsList;
            }
            else
            {
                MessageBox.Show("Неудалось обновить информацию об автомобиле");

            }
        }

        private void User_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Convert.ToInt32(textBox8.Text) > 0) && Convert.ToInt32(textBox8.Text) < 10000)
                {
                    textBox6.Text =textBox8.Text + Convert.ToInt32(textBox6.Text);
                }
            }
            
            catch
            {
                MessageBox.Show("Введите целое число от 1 до 10000");
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
