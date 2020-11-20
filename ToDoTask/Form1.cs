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


        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();

            activeForm = childForm;

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChieldForm.Controls.Add(childForm);
            panelChieldForm.Tag = childForm;
            panelChieldForm.BringToFront();
            childForm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tasks.GetAllTasks();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btn_ShowAll_Click(object sender, EventArgs e)
        {
            openChildForm(new Form_ShowTasks());
        }

        private void btn_AddNew_Click(object sender, EventArgs e)
        {
            TodoTask task = null;
            openChildForm(new Form_Add_Edit(task, TypeF.Add));
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            TodoTask task = null;
            openChildForm(new Form_Add_Edit(task, TypeF.Edit));
        }
    }
}
