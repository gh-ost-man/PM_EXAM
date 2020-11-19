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

        int id = -1;
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
                try
                {
                    tasks.Add(task);
                    btn_ShowAll_Click(sender, e);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message,"Error");}
            }
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            TodoTask task = new TodoTask();

            Form_Add_Edit edit = new Form_Add_Edit(task, TypeF.Edit);
            if (edit.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    tasks.Edit(task);
                    btn_ShowAll_Click(sender, e);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message,"Error");}
            }
        }

        private void btn_ShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = tasks.GetAllTasks();
            }
            catch { }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tasks.GetAllTasks();
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (id == -1) return;
            try
            {
                TodoTask task = tasks.FindById(id);
                tasks.Remove(task);

                textBox_Description.Text = string.Empty;
                btn_ShowAll_Click(sender, e);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message,"Error");}
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0) return;

            try
            {
                id = (int)dataGridView1.CurrentRow.Cells["Id"].Value;
                textBox_Description.Text= dataGridView1.CurrentRow.Cells["Description"].Value.ToString();
            }
            catch { }
        }
    }
}
