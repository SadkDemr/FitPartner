using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LifeCoachWebApp.Models.Classes
{
    public class Tasks
    {
        [Key]
        public int TasksID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string title { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(1000)]
        public string description { get; set; }

        [Display(Name = "Başlama Tarihi")]
        public DateTime? start { get; set; }

        [Display(Name = "Bitirme Tarihi")]
        public DateTime? end { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(10)]
        public string color { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(10)]
        public string textColor { get; set; }

        public int StudentId { get; set; }

        public virtual Student Students { get; set; }
    }
}