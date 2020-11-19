using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TaskLib
{
    /**
   @class ToDoMask
   Contains the basic properties of the task.
   */
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

    /**
    @enum State
    Contains task states
    */
    public enum State
    {
        Open = 1, InProgress = 2, Close = 3
    }


    /**
     @class ListTasks
     Contains the list tasks.
     */
    public class ListTasks
    {
        public List<TodoTask> Tasks = new List<TodoTask>();

        XmlSerializer xs = new XmlSerializer(typeof(List<TodoTask>));

        static int id = -1;

        ///@param task new task.
        public void Add(TodoTask task)
        {
            if (task == null) throw new Exception("item was null");

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
        ///@param task changeв task.
        public void Edit(TodoTask task)
        {
            if (task == null) throw new Exception("item was null");
            int index = -1;

            for (int i = 0; i < Tasks.Count; i++)
                if (task.Id == Tasks[i].Id) index = i;

            Tasks[index] = task;

            using (FileStream fs = new FileStream("Tasks.xml", FileMode.Truncate))
            {
                xs.Serialize(fs, Tasks);
            }
        }

        ///@param task for remove
        public void Remove(TodoTask task)
        {
            if (task == null) throw new Exception("item was null");

            Tasks.Remove(task);

            using (FileStream fs = new FileStream("Tasks.xml", FileMode.Truncate))
            {
                xs.Serialize(fs, Tasks);
            }
        }

        ///@param id task ID
        ///@return task by id
        public TodoTask FindById(int _id)
        {
            return Tasks.Where(x => x.Id == _id).FirstOrDefault();
        }

        ///@return the entire to-do list.
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
