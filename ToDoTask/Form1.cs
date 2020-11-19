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
    public partial class Form1 : Form
    {
        ListTasks tasks = new ListTasks();
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_AddNew_Click(object sender, EventArgs e)
        {
            TodoTask task = new TodoTask();

            Form_Add_Edit add = new Form_Add_Edit(task, TypeF.Add);
            if (add.ShowDialog() == DialogResult.OK)
            {
                tasks.Add(task);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = tasks.GetAllTasks();
            }
        }
    }
}
