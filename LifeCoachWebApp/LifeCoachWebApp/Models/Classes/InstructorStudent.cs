using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeCoachWebApp.Models.Classes
{
    public class InstructorStudent
    {
        public IEnumerable<Student> Ogrenciler { get; set; }
        public IEnumerable<Instructor> Egitmenler { get; set; }
    }
}