using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LifeCoachWebApp.Models.Classes
{
    public class Context: DbContext
    {
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Tasks> Task { get; set; }
        public DbSet<WorkingArea> WorkingAreas { get; set; }

    }
}