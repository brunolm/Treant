using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Treant.Domain;

namespace Treant.MockViewModels
{
    public class BoardViewModel
    {
        public BoardViewModel()
        {
            var taskItems02 = new ObservableCollection<TaskItem>(new List<TaskItem>
            {
                new TaskItem { Name = "Task 02" },
            });

            var taskItems01 = new ObservableCollection<TaskItem>(new List<TaskItem>
            {
                new TaskItem { Name = "Task 01", TaskItems = taskItems02 },
            });

            CurrentBoard = new Board { Name = "C#" };

            TaskItems = taskItems01;
        }

        public Board CurrentBoard { get; set; }

        public ObservableCollection<TaskItem> TaskItems { get; private set; }
    }
}
