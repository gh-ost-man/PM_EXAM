using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLib;

namespace UnitTest_ListTasks
{

    /// <summary>
    /// The class contains methods that check the correct  TaskLib  methods.
    /// </summary>
    public class UnitTest
    {
        [Test]

        ///Сhecks the addition of a new item
        public void TestAdd()
        {
            ListTasks listTasks = new ListTasks();

            TodoTask task = new TodoTask("Task1", "Create program", new DateTime(2020, 11, 19), new DateTime(2020, 11, 20), State.Open);

            listTasks.Add(task);

            Assert.AreEqual(1, listTasks.GetAllTasks().Count);
        }

        [Test]

        ///Сhecks the editable element
        public void TestEdit()
        {
            ListTasks listTasks = new ListTasks();
            listTasks.Add(new TodoTask("F", "dasdas", DateTime.Now, new DateTime(2020, 11, 22), State.Open));

            TodoTask task = listTasks.Tasks.Where(x => x.Id == 1).FirstOrDefault();

            task.Title = "Task 1";

            listTasks.Edit(task);

            Assert.AreEqual("Task 1", listTasks.Tasks.Where(x => x.Id == 1).FirstOrDefault().Title);
        }

        [Test]
        ///Сhecks the deletion of the item
        public void TestRemove()
        {
            ListTasks listTasks = new ListTasks();
            listTasks.Add(new TodoTask("F1", "d1", DateTime.Now, new DateTime(2020, 11, 22), State.Open));
            listTasks.Add(new TodoTask("F2", "d2", DateTime.Now, new DateTime(2020, 11, 22), State.Open));
            listTasks.Add(new TodoTask("F3", "d3", DateTime.Now, new DateTime(2020, 11, 22), State.Open));

            TodoTask task = listTasks.FindById(2);

            listTasks.Remove(task);

            Assert.AreEqual(2, listTasks.GetAllTasks().Count);
        }

        [Test]

        ///Searches item by ID
        public void TestFindById()
        {
            ListTasks listTasks = new ListTasks();
            listTasks.Add(new TodoTask("F1", "d1", DateTime.Now, new DateTime(2020, 11, 22), State.Open));
            listTasks.Add(new TodoTask("F2", "d2", DateTime.Now, new DateTime(2020, 11, 22), State.Open));
            listTasks.Add(new TodoTask("F3", "d3", DateTime.Now, new DateTime(2020, 11, 22), State.Open));

            TodoTask task = listTasks.FindById(2);

            Assert.AreEqual("F2", task.Title);
        }

        [Test]
        public void TestGetAllTasks()
        {
            ListTasks listTasks = new ListTasks();
            listTasks.Add(new TodoTask("F1", "d1", DateTime.Now, new DateTime(2020, 11, 22), State.Open));
            listTasks.Add(new TodoTask("F2", "d2", DateTime.Now, new DateTime(2020, 11, 22), State.Open));
            listTasks.Add(new TodoTask("F3", "d3", DateTime.Now, new DateTime(2020, 11, 22), State.Open));

            var res = listTasks.GetAllTasks();

            Assert.AreEqual(3, res.Count);
        }
    }
}
