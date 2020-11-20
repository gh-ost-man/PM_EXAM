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
    public partial class Form_Add_Edit : Form
    {
        ListTasks listTasks = new ListTasks();

        TodoTask todoTask = null;
        TypeF type;

        int id = -1;

        public Form_Add_Edit()
        {
            InitializeComponent();
            comboBox1.Items.Add(State.Open);
            comboBox1.Items.Add(State.InProgress);
            comboBox1.Items.Add(State.Close);
            comboBox1.SelectedIndex = 0;

        }
        public Form_Add_Edit(TodoTask task, TypeF typeF)
        {
            InitializeComponent();

            listTasks.GetAllTasks();

            comboBox1.Items.Add(State.Open);
            comboBox1.Items.Add(State.InProgress);
            comboBox1.Items.Add(State.Close);

            comboBox1.SelectedIndex = 0;

            this.todoTask = task;
            this.type = typeF;

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listTasks.GetAllTasks();
        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox_Title.Text)) { MessageBox.Show("Enter title!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (type == TypeF.Add)
            {
                State state = State.Open;
                if (comboBox1.SelectedItem.ToString() == "Open") state = State.Open;
                if (comboBox1.SelectedItem.ToString() == "Close") state = State.Close;
                if (comboBox1.SelectedItem.ToString() == "InProgress") state = State.InProgress;
                if (dateTimePicker_Start.Value.Date < DateTime.Now.Date) { MessageBox.Show("Not a correct date!\n-> Start Date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (dateTimePicker_End.Value.Date < dateTimePicker_Start.Value.Date) { MessageBox.Show("Not a correct date!\n End Date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }


                todoTask = new TodoTask();
                todoTask.Title = textBox_Title.Text;
                todoTask.Description = $@"{textBox_Description.Text}";
                todoTask.StartDT = dateTimePicker_Start.Value.Date;
                todoTask.EndDT = dateTimePicker_End.Value.Date;
                todoTask.state = state;

                listTasks.Add(todoTask);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = listTasks.GetAllTasks();


                this.DialogResult = DialogResult.OK;
            }

            if (type == TypeF.Edit)
            {
                State state = State.Open;
                if (comboBox1.SelectedItem.ToString() == "Open") state = State.Open;
                if (comboBox1.SelectedItem.ToString() == "Close") state = State.Close;
                if (comboBox1.SelectedItem.ToString() == "InProgress") state = State.InProgress;

                todoTask = new TodoTask();

                todoTask.Id = id;
                todoTask.Title = textBox_Title.Text;
                todoTask.Description = textBox_Description.Text;
                todoTask.StartDT = dateTimePicker_Start.Value.Date;
                todoTask.EndDT = dateTimePicker_End.Value.Date;
                todoTask.state = state;

                listTasks.Edit(todoTask);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = listTasks.GetAllTasks();

                this.DialogResult = DialogResult.OK;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            if (type == TypeF.Edit)
            {
                textBox_Title.Text = dataGridView1.CurrentRow.Cells["Title"].Value.ToString();
                textBox_Description.Text = dataGridView1.CurrentRow.Cells["Description"].Value.ToString();
                dateTimePicker_Start.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["StartDT"].Value).Date;
                dateTimePicker_End.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["EndDT"].Value).Date;

                string state = dataGridView1.CurrentRow.Cells["State"].Value.ToString();

                if (state == "Open") comboBox1.SelectedIndex = 0;
                if (state == "InProgress") comboBox1.SelectedIndex = 1;
                if (state == "Close") comboBox1.SelectedIndex = 2;

                id = (int)dataGridView1.CurrentRow.Cells["Id"].Value;
                this.Text = id.ToString();

            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
