using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParkingSystem
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "city_ParkingDataSet1.User". При необходимости она может быть перемещена или удалена.
            this.userTableAdapter.Fill(this.city_ParkingDataSet1.User);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "city_ParkingDataSet.v_Parking". При необходимости она может быть перемещена или удалена.
            this.v_ParkingTableAdapter.Fill(this.city_ParkingDataSet.v_Parking);

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var addPark = new AddParkingPlace();
            if (addPark.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Новая парковочная зона успешно добавлена в базу данных");
            }
            else
            {
                MessageBox.Show("Неудалось добавить новую парковочную зону в базу данных");

            }
        }
    }
}
