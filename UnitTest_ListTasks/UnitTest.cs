using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLib;

namespace UnitTest_ListTasks
{
    public class UnitTest
    {
        [Test]

        public void TestAdd()
        {
            ListTasks listTasks = new ListTasks();

            ToDoTask task = new ToDoTask("Task1", "Create program", new DateTime(2020, 11, 19), new DateTime(2020, 11, 20), State.Open);

            listTasks.Add(task);

            Assert.AreEqual(1, listTasks.GetAllTasks().Count);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void TestAdd_Return_Exception()
        {
            ListTasks listTasks = new ListTasks();
            ToDoTask task=null;

            listTasks.Add(task);
        }

        [Test]

        public void TestEdit()
        {
            ListTasks listTasks = new ListTasks();
            listTasks.Add(new ToDoTask("F", "dasdas", DateTime.Now, new DateTime(2020, 11, 22), State.Open));

            ToDoTask task = listTasks.Tasks.Where(x => x.Id == 1).FirstOrDefault();

            task.Title = "Task 1";

            listTasks.Edit(task);

            Assert.AreEqual("Task 1", listTasks.Tasks.Where(x => x.Id == 1).FirstOrDefault().Title);
        }

        [Test]
        [ExpectedException(typeof(Exception))]

        public void TestEdit_Return_Exception()
        {
            ListTasks listTasks = new ListTasks();
            ToDoTask task = null;

            listTasks.Edit(task);
        }

        [Test]

        public void TestRemove()
        {
            ListTasks listTasks = new ListTasks();
            listTasks.Add(new ToDoTask("F1", "d1", DateTime.Now, new DateTime(2020, 11, 22), State.Open));
            listTasks.Add(new ToDoTask("F2", "d2", DateTime.Now, new DateTime(2020, 11, 22), State.Open));
            listTasks.Add(new ToDoTask("F3", "d3", DateTime.Now, new DateTime(2020, 11, 22), State.Open));

            ToDoTask task = listTasks.FindById(2);

            listTasks.Remove(task);

            Assert.AreEqual(2, listTasks.GetAllTasks().Count);
        }

        [Test]
        [ExpectedException(typeof(Exception))]

        public void TestRemove_Return_Exception()
        {
            ListTasks listTasks = new ListTasks();
            ToDoTask task = null;

            listTasks.Remove(task);
        }

        [Test]

        public void TestFindById()
        {
            ListTasks listTasks = new ListTasks();
            listTasks.Add(new ToDoTask("F1", "d1", DateTime.Now, new DateTime(2020, 11, 22), State.Open));
            listTasks.Add(new ToDoTask("F2", "d2", DateTime.Now, new DateTime(2020, 11, 22), State.Open));
            listTasks.Add(new ToDoTask("F3", "d3", DateTime.Now, new DateTime(2020, 11, 22), State.Open));

            ToDoTask task = listTasks.FindById(2);

            Assert.AreEqual("F2", task.Title);
        }

        [Test]

        public void TestGetAllTasks()
        {
            ListTasks listTasks = new ListTasks();
            listTasks.Add(new ToDoTask("F1", "d1", DateTime.Now, new DateTime(2020, 11, 22), State.Open));
            listTasks.Add(new ToDoTask("F2", "d2", DateTime.Now, new DateTime(2020, 11, 22), State.Open));
            listTasks.Add(new ToDoTask("F3", "d3", DateTime.Now, new DateTime(2020, 11, 22), State.Open));

            var res = listTasks.GetAllTasks();

            Assert.AreEqual(3, res.Count);
        }
    }
}
