using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskLib;

namespace ToDoTask
{
    public partial class Form_ShowTasks : Form
    {
        int id = -1;

        ListTasks tasks = new ListTasks();
        public Form_ShowTasks()
        {
            InitializeComponent();

            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = tasks.GetAllTasks();
            }
            catch { }

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            try
            {
                textBox_Description.Text = dataGridView1.CurrentRow.Cells["Description"].Value.ToString();
            }
            catch { }
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0) return;
            try
            {
                id = (int)dataGridView1.CurrentRow.Cells["Id"].Value;

                TodoTask task = tasks.FindById(id);
                tasks.Remove(task);

                textBox_Description.Text = string.Empty;

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = tasks.GetAllTasks();
                id = -1;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error",MessageBoxButtons.OK,MessageBoxIcon.Warning); }
        }

        private void btn_Remove_MouseEnter(object sender, EventArgs e)
        {
            Button button = sender as Button;

            button.BackColor = Color.DarkCyan;
            button.ForeColor = Color.Black;
        }

        private void btn_Remove_MouseLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;

            button.BackColor = Color.Transparent;
            button.ForeColor = Color.DarkCyan;
        }
    }
}
