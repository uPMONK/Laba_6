using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public class ToDo
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public ToDo(string title, string description)
        {
            this.Title = title;
            this.Description = description;

        }
        
    }
}
