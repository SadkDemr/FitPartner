using LifeCoachWebApp.Models.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace LifeCoachWebApp.Controllers
{
    public class InstructorController : Controller
    {
        // GET: Instructor
        Context c = new Context();
       

        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["InstructorMail"];
            var value = c.Instructors.FirstOrDefault(x => x.InstructorMail == mail);
            ViewBag.m = mail;
            return View(value);
        }

        [Authorize]
        public ActionResult InstructorProfile()
        {
            var mail = (string)Session["InstructorMail"];
            var value = c.Instructors.FirstOrDefault(x => x.InstructorMail == mail);
            ViewBag.m = mail;
            return View(value);
        }

        [Authorize]
        public ActionResult InstructorBring()
        {
            var mail = (string)Session["InstructorMail"];
            List<SelectListItem> value1 = (from x in c.WorkingAreas.ToList() select new SelectListItem
            {
                Text = x.WorkingAreaName,
                Value = x.WorkingAreaID.ToString()
            }).ToList();

            ViewBag.val = value1;

            var id = c.Instructors.Where(x => x.InstructorMail == mail.ToString()).Select(y => y.InstructorID).FirstOrDefault();
            var instructorprofile = c.Instructors.Find(id);
            return View(instructorprofile);

        }

        [Authorize]
        public ActionResult InstructorUpdate(Instructor instructor, HttpPostedFileBase InstructorImage)
        {
            var mail = (string)Session["InstructorMail"];
            var id = c.Instructors.Where(x => x.InstructorMail == mail.ToString()).Select(y => y.InstructorID).FirstOrDefault();
            var instructorprofile = c.Instructors.Find(instructor.InstructorID);
            if (instructorprofile.InstructorMail == instructor.InstructorMail && instructorprofile.InstructorPass == instructor.InstructorPass)
            {

                instructorprofile.InstructorName = instructor.InstructorName;
                instructorprofile.InstructorLastName = instructor.InstructorLastName;
                instructorprofile.InstructorPhoneNumber = instructor.InstructorPhoneNumber;
                instructorprofile.InstructorDateOfBirth = instructor.InstructorDateOfBirth;
                instructorprofile.InstructorAddress = instructor.InstructorAddress;
                instructorprofile.InstructorCity = instructor.InstructorCity;
                instructorprofile.InstructorExperience = instructor.InstructorExperience;
                instructorprofile.InstructorCoverLetter = instructor.InstructorCoverLetter;
                instructorprofile.InstructorAbout = instructor.InstructorAbout;
                instructorprofile.InstructorPoint = instructor.InstructorPoint;
                instructorprofile.WorkingAreas.WorkingAreaID = instructor.WorkingAreas.WorkingAreaID;

                if (ModelState.IsValid)
                {
                    if (InstructorImage != null && InstructorImage.ContentLength > 0)
                    {
                        string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                        string uzanti = Path.GetExtension(Request.Files[0].FileName);
                        string yol = "~/Image/" + dosyaadi + uzanti;
                        InstructorImage.SaveAs(Server.MapPath(yol));
                        instructorprofile.InstructorImage = "/Image/" + dosyaadi + uzanti;
                    }
                }

                c.SaveChanges();

                return RedirectToAction("InstructorProfile");
            }
            else
            {

                instructorprofile.InstructorName = instructor.InstructorName;
                instructorprofile.InstructorLastName = instructor.InstructorLastName;
                instructorprofile.InstructorPhoneNumber = instructor.InstructorPhoneNumber;
                instructorprofile.InstructorMail = instructor.InstructorMail;
                instructorprofile.InstructorDateOfBirth = instructor.InstructorDateOfBirth;
                instructorprofile.InstructorAddress = instructor.InstructorAddress;
                instructorprofile.InstructorCity = instructor.InstructorCity;
                instructorprofile.InstructorExperience = instructor.InstructorExperience;
                instructorprofile.InstructorCoverLetter = instructor.InstructorCoverLetter;
                instructorprofile.InstructorAbout = instructor.InstructorAbout;
                instructorprofile.InstructorPass = instructor.InstructorPass;
                instructorprofile.InstructorPoint = instructor.InstructorPoint;
                instructorprofile.WorkingAreas.WorkingAreaID = instructor.WorkingAreas.WorkingAreaID;


                if (ModelState.IsValid)
                {
                    if (InstructorImage != null && InstructorImage.ContentLength > 0)
                    {
                        string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                        string uzanti = Path.GetExtension(Request.Files[0].FileName);
                        string yol = "~/Image/" + dosyaadi + uzanti;
                        InstructorImage.SaveAs(Server.MapPath(yol));
                        instructorprofile.InstructorImage = "/Image/" + dosyaadi + uzanti;
                    }
                }

                c.SaveChanges();
                return RedirectToAction("InstructorLogin", "Home");
            }
        }

        [Authorize]
        public ActionResult StudentList()
        {
            var mail = (string)Session["InstructorMail"];
            var id = c.Instructors.Where(x => x.InstructorMail == mail.ToString()).Select(y => y.InstructorID).FirstOrDefault();
            var value = c.Students.Where(x => x.InstructorID == id).ToList();

            if (value == null)
            {
                return View(new List<Student>());
            }

            return View(value);
        }

        [Authorize]
        [HttpGet]
        public ActionResult StudentAdd()
        {
            var mail = (string)Session["InstructorMail"];
            var id = c.Instructors.Where(x => x.InstructorMail == mail.ToString()).Select(y => y.InstructorID).FirstOrDefault();
            ViewBag.InstructorId = id;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult StudentAdd(Student s)
        {
            try
            {
                c.Students.Add(s);
                c.SaveChanges();
                return RedirectToAction("StudentList");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw;
            }
        }

        [Authorize]
        public ActionResult StudentBring(int id)
        {
            var mail = (string)Session["InstructorMail"];
            List<SelectListItem> value1 = (from x in c.WorkingAreas.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.WorkingAreaName,
                                               Value = x.WorkingAreaID.ToString()
                                           }).ToList();

            ViewBag.val = value1;

            var studentValue = c.Students.Find(id);

            return View("StudentBring", studentValue);  
        }

        [Authorize]
        public ActionResult StudentUpdate(Student s) 
        {
            var std = c.Students.Find(s.StudentID);
            std.StudentName = s.StudentName;
            std.StudentLastName = s.StudentLastName;
            std.StudentPhoneNumber = s.StudentPhoneNumber;
            std.StudentMail = s.StudentMail;
            std.InstructorID = s.InstructorID;
            std.StudentLength = s.StudentLength;
            std.StudentWeight = s.StudentWeight;
            std.StudentFatRate = s.StudentFatRate;
            std.StudentAddress = s.StudentAddress;
            std.StudentCity = s.StudentCity;
            std.StudentDateOfBirth = s.StudentDateOfBirth;

            c.SaveChanges();
            return RedirectToAction("StudentList");
        }

        [Authorize]
        public ActionResult StudentDelete(int id) 
        {
            var student = c.Students.Find(id);
            c.Students.Remove(student);
            c.SaveChanges();
            return RedirectToAction("StudentList");
        }

        [Authorize]
        public ActionResult StudentTaskCalender(int? studentId)
        {
            if (!studentId.HasValue)
            {
                // İşlem yapmak için geçerli bir öğrenci kimliği sağlanmadıysa, bir hata mesajı döndürün veya başka bir eyleme yönlendirin
                return RedirectToAction("Hata"); // Örnek olarak, Hata adlı başka bir eyleme yönlendirin.
            }

            var yapilacaklar = c.Task.Where(t => t.Students.StudentID == studentId.Value).ToList(); // İlgili öğrenciye ait taskları çekin

            var model = new TaskViewModel
            {
                TaskList = yapilacaklar,
                Task = new Tasks() // Add an empty Tasks object
            };
            return View(model);
        }

        [Authorize]
        public JsonResult etkinlikGetir(int? studentId)
        {
            try
            {
                if (!studentId.HasValue)
                {
                    return Json(new List<Tasks>(), JsonRequestBehavior.AllowGet);
                }

                var yapilacaklar = c.Task
                    .Where(t => t.Students.StudentID == studentId.Value)
                    .Select(t => new
                    {
                        TasksID = t.TasksID,
                        title = t.title,
                        description = t.description,
                        start = t.start,
                        end = t.end,
                        color = t.color,
                        textColor = t.textColor,
                        StudentID = t.Students.StudentID
                        // Diğer özellikleri buraya ekleyin...
                    })
                    .ToList();

                return Json(yapilacaklar, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Log the exception message
                System.Diagnostics.Debug.WriteLine(ex.Message);
                // Return an HTTP 500 error
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult etkinlikSil(int id)
        {
            var task = c.Task.Find(id);
            if (task == null)
            {
                return Json(new { success = false });
            }

            c.Task.Remove(task);
            c.SaveChanges();
            return Json(new { success = true });
        }


        [Authorize]
        [HttpPost]
        public ActionResult updateTask(int id, DateTime start, DateTime? end)
        {
            var task = c.Task.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }

            task.start = start;
            task.end = end;

            c.Entry(task).State = EntityState.Modified;
            c.SaveChanges();

            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult createTask(Tasks newTask)
        {
            if (ModelState.IsValid)
            {
                newTask.start = newTask.start?.ToUniversalTime();
                newTask.end = newTask.end?.ToUniversalTime();
                c.Task.Add(newTask);
                c.SaveChanges();

                // Fetch the task after saving to ensure all properties are set correctly
                var task = c.Task.Find(newTask.TasksID);

                return Json(new
                {
                    success = true,
                    task = new
                    {
                        TasksID = task.TasksID,
                        title = task.title,
                        description = task.description,
                        start = task.start.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                        end = task.end.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                        color = task.color,
                        textColor = task.textColor,
                        StudentId = task.StudentId
                    }
                });
            }
            else
            {
                return Json(new { success = false, errorMessage = "Data is not valid" });
            }
        }





    }
}