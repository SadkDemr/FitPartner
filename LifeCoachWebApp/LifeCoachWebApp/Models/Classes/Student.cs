using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace LifeCoachWebApp.Models.Classes
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }

        [Column(TypeName ="Varchar")]
        [StringLength(30)]
        public string StudentName { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string StudentLastName { get; set; }

        [Display(Name = "Date of Birthday")]
        public DateTime? StudentDateOfBirth { get; set; }

        public string StudentPhoneNumber { get; set; }

        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Email is not valid")]
        public string StudentMail { get; set; }
        public int? StudentLength { get; set; }
        public int? StudentWeight { get; set; }
        public int? StudentFatRate { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(300)]
        public string StudentAddress { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(40)]
        public string StudentCity{ get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string StudentPass{ get; set; }
        public int InstructorID { get; set; }

        public virtual Instructor Instructors { get; set;}

        public ICollection<Tasks> Task { get; set; }
    }
}