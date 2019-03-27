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
        int sum;
        int bal;
        int bal1;
        int deb;
        int bd;
        int db;
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
                        for (int i = 0; i < user.GetType().GetProperties().Length-2; i++)
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
            textBox6.Text = Convert.ToString(user.Balance);
            textBox2.Text = Convert.ToString(user.Debt);
            advancedDataGridView1.DataSource = user.carsList;
            string sql = "SELECT SUM([Sum]) FROM [dbo].[Penalty] WHERE FK_User = "+id;
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.City_ParkingConnectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sql, con);
                object count = command.ExecuteScalar();
                if (Convert.ToString(count) != "")
                {
                    sum += Convert.ToInt32(count);
                }
            }
            string sql3 = "SELECT [Balance] FROM [dbo].[User] WHERE ID= " + id;
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.City_ParkingConnectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sql3, con);
                object count = command.ExecuteScalar();
                if (Convert.ToString(count) != "")
                {
                    bal += Convert.ToInt32(count);
                }
            }

            string sql1 = "SELECT [Debt] FROM [dbo].[User] WHERE ID= "+id;
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.City_ParkingConnectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sql1, con);
                object count = command.ExecuteScalar();
                if (Convert.ToString(count) != "")
                {
                    sum += Convert.ToInt32(count);
                }
               

            }
            textBox2.Text = Convert.ToString(sum);
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.City_ParkingConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "UPDATE [User] SET [Debt]=" + sum + "WHERE ID= " + id;
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                    MessageBox.Show("Данные о задолженности обновлены");

                }
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
                MessageBox.Show("Не удалось обновить данные о задолженности");
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.City_ParkingConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "DELETE FROM [Penalty] WHERE FK_User= " + id;
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                    MessageBox.Show("Данные о назначенных вам штрафах внесены в задолженность и удалены");

                }
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
                MessageBox.Show("Не удалось удалить ваши штрафы");
            }
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
            button7.Enabled = false;
            button1.Enabled = false;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                bal += Convert.ToInt32(maskedTextBox1.Text);
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.City_ParkingConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "UPDATE [User] SET [Balance]=" + bal + "WHERE ID= " + user.ID;
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                    MessageBox.Show("Вы успешно пополнили баланс");
                  
                }
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
                MessageBox.Show("Неудалось пополнить баланс");
            }

            string sql = "SELECT ([Balance]) FROM [dbo].[User] WHERE ID = " + user.ID;
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.City_ParkingConnectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sql, con);
                object count = command.ExecuteScalar();
                if (Convert.ToString(count) != "")
                {
                    textBox6.Text = Convert.ToString(count);
                }
            }

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void advancedDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void advancedDataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text.Length != 5)
            {

                button7.Enabled = false;

            }
            else
            {
                button7.Enabled = true;
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bal1 = Convert.ToInt32(maskedTextBox2.Text);

            string sql1 = "SELECT ([Debt]) FROM [dbo].[User] WHERE ID = " + user.ID;
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.City_ParkingConnectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sql1, con);
                object count = command.ExecuteScalar();
                if (Convert.ToString(count) != "")
                {
                    deb = Convert.ToInt32(count);
                }
            }

            try
            {
                Convert.ToUInt32(deb - bal1);
                db = deb - bal1;//задолженность-пополнение

                string sql5 = "SELECT ([Balance]) FROM [dbo].[User] WHERE ID = " + user.ID;
                using (SqlConnection con = new SqlConnection(Properties.Settings.Default.City_ParkingConnectionString))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(sql5, con);
                    object count = command.ExecuteScalar();
                    if (Convert.ToString(count) != "")
                    {
                       bd = Convert.ToInt32(count)-deb;
                    }
                }//Остаток баланса

                
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.City_ParkingConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "UPDATE [User] SET [Balance]=" + bd + "WHERE ID= " + user.ID;
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }

                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.City_ParkingConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "UPDATE [User] SET [Debt]=" + db + "WHERE ID= " + user.ID;
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }


                string sql3 = "SELECT ([Balance]) FROM [dbo].[User] WHERE ID = " + user.ID;
                using (SqlConnection con = new SqlConnection(Properties.Settings.Default.City_ParkingConnectionString))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(sql3, con);
                    object count = command.ExecuteScalar();
                    if (Convert.ToString(count) != "")
                    {
                        textBox6.Text = Convert.ToString(count);
                    }
                }


                string sql4 = "SELECT ([Debt]) FROM [dbo].[User] WHERE ID = " + user.ID;
                using (SqlConnection con = new SqlConnection(Properties.Settings.Default.City_ParkingConnectionString))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(sql4, con);
                    object count = command.ExecuteScalar();
                    if (Convert.ToString(count) != "")
                    {
                        textBox2.Text = Convert.ToString(count);
                    }
                }

                MessageBox.Show("Оплата прошла успешно, данные о балансе и задолженности обновлены");
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
                MessageBox.Show("Вам не хватает средств");
            }
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (maskedTextBox2.Text.Length != 5)
            {

                button1.Enabled = false;

            }
            else
            {
                button1.Enabled = true;
            }
        }
    }
}
