using LifeCoachWebApp.Models.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LifeCoachWebApp.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        Context c = new Context();

        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["StudentMail"];
            var value = c.Students.FirstOrDefault(x => x.StudentMail == mail);
            ViewBag.m = mail;
            return View(value);
        }

        [Authorize]
        public ActionResult StudentProfile()
        {
            var mail = (string)Session["StudentMail"];
            var value = c.Students.FirstOrDefault(x => x.StudentMail == mail);
            ViewBag.m = mail;
            return View(value);
        }

        [Authorize]
        public ActionResult StudentBring()
        {
            var mail = (string)Session["StudentMail"];

            var id = c.Students.Where(x => x.StudentMail == mail.ToString()).Select(y => y.StudentID).FirstOrDefault();
            var studentprofile = c.Students.Find(id);
            return View(studentprofile);

        }

        [Authorize]
        public ActionResult StudentUpdate(Student student)
        {
            var mail = (string)Session["StudentMail"];
            var id = c.Students.Where(x => x.StudentMail == mail.ToString()).Select(y => y.InstructorID).FirstOrDefault();
            var studentprofile = c.Students.Find(student.StudentID);
            if (studentprofile.StudentMail == student.StudentMail && studentprofile.StudentPass == student.StudentPass)
            {

                studentprofile.StudentLastName = student.StudentName;
                studentprofile.StudentLastName = student.StudentLastName;
                studentprofile.StudentPhoneNumber = student.StudentPhoneNumber;
                studentprofile.StudentDateOfBirth = student.StudentDateOfBirth;
                studentprofile.StudentAddress = student.StudentAddress;
                studentprofile.StudentCity = student.StudentCity;
                studentprofile.StudentLength = student.StudentLength;
                studentprofile.StudentWeight = student.StudentWeight;
                studentprofile.StudentFatRate = student.StudentFatRate;

                c.SaveChanges();

                return RedirectToAction("StudentProfile");
            }
            else
            {

                studentprofile.StudentLastName = student.StudentName;
                studentprofile.StudentLastName = student.StudentLastName;
                studentprofile.StudentPhoneNumber = student.StudentPhoneNumber;
                studentprofile.StudentDateOfBirth = student.StudentDateOfBirth;
                studentprofile.StudentAddress = student.StudentAddress;
                studentprofile.StudentCity = student.StudentCity;
                studentprofile.StudentLength = student.StudentLength;
                studentprofile.StudentWeight = student.StudentWeight;
                studentprofile.StudentFatRate = student.StudentFatRate;
                studentprofile.StudentPass = student.StudentPass;
                studentprofile.StudentMail = student.StudentMail;


                c.SaveChanges();
                return RedirectToAction("StudentLogin", "Home");
            }
        }

        [Authorize]
        public ActionResult StudentTaskCalenderPage()
        {


            return View();
        }


        [Authorize]
        public ActionResult etkinlikGetir()
        {
            var mail = (string)Session["StudentMail"];
            var id = c.Students.Where(x => x.StudentMail == mail.ToString()).Select(y => y.StudentID).FirstOrDefault();
            var value = c.Task.Where(x => x.StudentId == id).ToList();

            var events = value.Select(values => new
            {
                title = values.title, // Etkinlik adı (TaskName'ini kullandım, sizin modelinizdeki ilgili alanla değiştirin)
                description = values.description,
                start = string.Format("{0:yyyy-MM-ddTHH:mm:ss}", values.start), // Etkinliğin başlangıç tarihi (ISO 8601 format)
                end = string.Format("{0:yyyy-MM-ddTHH:mm:ss}", values.end),
                color = values.color,
                textColor = values.textColor,
                StudentID = values.Students.StudentID
                // Etkinliğin bitiş tarihi (ISO 8601 format)
            }).ToList();


            return Json(events, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CompleteTask(int id)
        {
            // Fetch the task with the given id
            var task = c.Task.Find(id);
            if (task == null)
            {
                return Json(new { message = "Task not found" });
            }

            // Change color to red
            task.color = "red";

            c.Entry(task).State = EntityState.Modified;
            c.SaveChanges();

            return Json(new { message = "Task marked as completed" });
        }


    }
}