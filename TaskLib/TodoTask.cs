using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace TaskLib
{
    /// <summary>
    /// Contains the basic properties of the task.
    /// </summary>

    public class TodoTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDT { get; set; }
        public DateTime EndDT { get; set; }
        public State state { get; set; }

        public TodoTask() { }

        public TodoTask(string title, string description, DateTime startDT, DateTime endDT, State state)
        {
            this.Title = title;
            this.Description = description;
            this.StartDT = startDT;
            this.EndDT = endDT;
            this.state = state;
        }
    }

    /// <summary>
    /// Contains task states
    /// </summary>
    public enum State
    {
        Open = 1, InProgress = 2, Close = 3
    }


    /// <summary>
    /// Contains the list tasks.
    /// </summary>

    public class ListTasks
    {
        public List<TodoTask> Tasks = new List<TodoTask>();

        XmlSerializer xs = new XmlSerializer(typeof(List<TodoTask>),new XmlRootAttribute("Tasks"));
        XDocument doc;

        static int id = -1;

        /// <summary>
        ///  The method adds a new task.
        /// </summary>
        /// <param name="task">New task</param>
        public void Add(TodoTask task)
        {
           
            if (task == null) throw new Exception("item was null"); ///If task == null throw Exception(task was null)

            if (Tasks.Count == 0) id = 1;
            else id = Tasks[Tasks.Count - 1].Id + 1;

            task.Id = id;
            Tasks.Add(task);
            id++;


            if (!File.Exists("Tasks.xml"))
            {
                using (FileStream fs = new FileStream("Tasks.xml", FileMode.OpenOrCreate))
                {
                    xs.Serialize(fs, Tasks);
                }
            }
            else
            {
                using (FileStream fs = new FileStream("Tasks.xml", FileMode.Truncate))
                {
                    xs.Serialize(fs, Tasks);
                }
            }
        }
        /// <summary>
        /// The method edits task
        /// </summary>
        /// <param name="task">Сhanged task.</param>
        public void Edit(TodoTask task)
        {
            if (task == null) throw new Exception("item was null"); ///If task == null throw Exception(task was null)
            int index = -1;

            for (int i = 0; i < Tasks.Count; i++)
                if (task.Id == Tasks[i].Id) index = i;

            Tasks[index] = task;

            using (FileStream fs = new FileStream("Tasks.xml", FileMode.Truncate))
            {
                xs.Serialize(fs, Tasks);
            }
        }

        /// <summary>
        /// The method removes task
        /// </summary>
        /// <param name="task">For remove</param>

        public void Remove(TodoTask task)
        {
            if (task == null) throw new Exception("item was null");///If task == null throw Exception(task was null)

            doc = XDocument.Load("Tasks.xml");
            doc.Element("Tasks").Elements("TodoTask").Where(x => x.Element("Id").Value == task.Id.ToString()).Remove();

            doc.Save("Tasks.xml");
        }

        /// <summary>
        ///The method find task by id.
        /// </summary>
        /// <param name="_id">Task ID</param>
        /// <returns>Task by id</returns>
        public TodoTask FindById(int _id)
        {
            return Tasks.Where(x => x.Id == _id).FirstOrDefault();
        }

        /// <summary>
        /// The method returns entire to-do list
        /// </summary>
        /// <returns>The entire to-do list.</returns>

        public List<TodoTask> GetAllTasks()
        {
            if (File.Exists("Tasks.xml"))
            {
                using (FileStream fs = new FileStream("Tasks.xml", FileMode.OpenOrCreate))
                {
                    Tasks = (List<TodoTask>)xs.Deserialize(fs);
                }
            }

            return Tasks;
        }
    }

}
