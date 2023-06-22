using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LifeCoachWebApp.Models.Classes
{
    public class WorkingArea
    {
        [Key]
        public int WorkingAreaID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string WorkingAreaName { get; set; }
        public ICollection<Instructor> Instructors { get; set; }
    }
}