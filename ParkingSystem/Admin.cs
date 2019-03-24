using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

        private void button1_Click(object sender, EventArgs e)
        {
            var reg = new AddNewAdmin();
            if (reg.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Администратор успешно добавлен в базу данных");
               userBindingSource.DataSource= userTableAdapter.GetData();
            }
            else
            {
                MessageBox.Show("Неудалось добавить администратора в базу данных");

            }
        }

        private void advancedDataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void advancedDataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (advancedDataGridView2.SelectedRows.Count > 0)
            {
               
                var Det = new UserDetail((int)advancedDataGridView2.Rows[advancedDataGridView2.SelectedRows[0].Index].Cells[0].Value);
                
                Det.ShowDialog();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {

            SaveFileDialog file = new SaveFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(Path.GetFullPath(file.FileName));
                
                    var table = (DataSet)userBindingSource.DataSource;

                for (int i = 0; i < table.Tables[0].Columns.Count; i++)
                {
                    writer.Write(table.Tables[0].Columns[i].ColumnName + "\t");
                }
                writer.WriteLine();
                for (int i = 0; i < table.Tables[0].Columns.Count; i++)
                {
                    for (int j = 0; j < table.Tables[0].Rows.Count; j++)
                    {
                        writer.Write(table.Tables[0].Rows[j][i].ToString() + "\t");
                        Debug.WriteLine(table.Tables[0].Rows[j][i].ToString());
                    }
                    writer.WriteLine();
                }
                writer.Close();
                MessageBox.Show("Сохранение завершено");
            }
                
                   
            }
            
        
    }
}
