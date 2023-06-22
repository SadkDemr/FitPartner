using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeCoachWebApp.Models.Classes
{
    public class TaskViewModel
    {
        public Tasks Task { get; set; }
        public List<Tasks> TaskList { get; set; }
    }
}