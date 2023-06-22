using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeCoachWebApp.Models.Classes
{
    public class InstrWA
    {
        public IEnumerable<Instructor> Egitmenler { get; set; }
        public IEnumerable<WorkingArea> Alanlar { get; set; }
    }
}