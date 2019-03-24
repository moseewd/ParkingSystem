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
    public partial class Pay : Form
    {
        private int id;
        
        public Pay( int id)
        {
            this.id = id;
           
            InitializeComponent();
            listBox1.DataSource = Payment.GetPayment(id);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var newcard = new AddCard();
            newcard.ID =id;
            if (newcard.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Способ оплаты успешно пополнен");
                listBox1.DataSource = Payment.GetPayment(id);
            }
            else
            {
                MessageBox.Show("Не удалось добавить способ оплаты");

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                Payment card = (Payment)listBox1.SelectedItem;
                try
                {
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.City_ParkingConnectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand();
                        command.CommandText = $"DELETE FROM [dbo].[Payment] Where [ID]={card.ID}";
                        command.Connection = connection;
                        command.ExecuteNonQuery();
                        MessageBox.Show("Способ оплаты успешно удален из базы данных");
                        listBox1.DataSource = Payment.GetPayment(id);
                    }
                }
                catch (Exception exc)
                {
                    Debug.WriteLine(exc);
                    MessageBox.Show("Неудалось удалить способ оплаты из базы данных");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
