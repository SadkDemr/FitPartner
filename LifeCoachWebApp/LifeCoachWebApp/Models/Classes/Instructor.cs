using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using DataAnnotationsExtensions;

namespace LifeCoachWebApp.Models.Classes
{
    public class Instructor
    {
        [Key]
        public int InstructorID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string InstructorName { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string InstructorLastName { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(15)]
        public string InstructorPhoneNumber { get; set; }

        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Email is not valid")]
        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string InstructorMail { get; set; }


        [Display(Name = "Doğum Tarihi")]
        public DateTime? InstructorDateOfBirth { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(300)]
        public string InstructorAddress { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(40)]
        public string InstructorCity{ get; set; }

        public int? InstructorExperience { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string InstructorCoverLetter { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(1000)]
        public string InstructorAbout { get; set; }
        public string InstructorImage { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string InstructorPass { get; set; }
        public int? InstructorPoint { get; set; }
        public int WorkingAreaID { get; set; }
        public virtual WorkingArea WorkingAreas { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}