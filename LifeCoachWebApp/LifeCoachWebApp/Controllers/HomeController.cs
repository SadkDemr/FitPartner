using LifeCoachWebApp.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LifeCoachWebApp.Controllers
{
    public class HomeController : Controller
    {

        Context c = new Context();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }


        public ActionResult Blog()
        {
            return View();
        }

        public ActionResult BlogDetay()
        {
            return View();
        }

        public ActionResult BlogDetay2()
        {
            return View();
        }

        [HttpGet]
        public ActionResult InstructorRegister()
        {
            List<SelectListItem> value1 = (from x in c.WorkingAreas.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.WorkingAreaName,
                                               Value = x.WorkingAreaID.ToString()
                                           }).ToList();

            ViewBag.val = value1;

            ViewBag.Accounts = new SelectList(c.WorkingAreas, "WorkingAreaID", "WorkingAreaName");

            return View();
        }

        [HttpPost]
        public ActionResult InstructorRegister(Instructor i)
        {
            var existingInstructor = c.Instructors.FirstOrDefault(x => x.InstructorMail == i.InstructorMail);

            if (existingInstructor != null)
            {
                ViewBag.DuplicateError = "Bu e-posta adresi zaten kayıtlı.";
                List<SelectListItem> value1 = (from x in c.WorkingAreas.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.WorkingAreaName,
                                                   Value = x.WorkingAreaID.ToString()
                                               }).ToList();

                ViewBag.val = value1;
                return View(i); // hata varsa aynı view'i yükle
            }
            else
            {
                c.Instructors.Add(i);
                c.SaveChanges();
                return RedirectToAction("Index"); // başarılı ise index sayfasına yönlendir
            }
        }

        [HttpGet]
        public ActionResult InstructorLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InstructorLogin(Instructor i, bool? rememberMe)
        {
            var value = c.Instructors.FirstOrDefault(x => x.InstructorMail == i.InstructorMail && 
            x.InstructorPass == i.InstructorPass);
            if(value != null)
            {
                // rememberMe parametresini SetAuthCookie metoduna geçirin
                FormsAuthentication.SetAuthCookie(value.InstructorMail, rememberMe ?? false);
                Session["InstructorMail"] = value.InstructorMail.ToString();
                return RedirectToAction("InstructorProfile", "Instructor" );
            }
            else
            {
                ViewBag.LoginError = "Hatalı Giriş Yaptınız Lutfen Tekrar Deneyiniz.";
                return View();
            }
        }

        [HttpGet]
        public ActionResult StudentLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StudentLogin(Student i, bool? rememberMe)
        {
            var value = c.Students.FirstOrDefault(x => x.StudentMail == i.StudentMail &&
            x.StudentPass == i.StudentPass);
            if (value != null)
            {
                FormsAuthentication.SetAuthCookie(value.StudentMail, rememberMe ?? false);
                Session["StudentMail"] = value.StudentMail.ToString();
                return RedirectToAction("StudentTaskCalenderPage", "Student");
            }
            else
            {
                ViewBag.LoginError = "Hatalı Giriş Yaptınız Lutfen Tekrar Deneyiniz.";
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult InstructorList(int? id)
        {
            InstrWA instr = new InstrWA();
            instr.Egitmenler = c.Instructors.ToList();
            instr.Alanlar = c.WorkingAreas.ToList();

            if(id == null)
            {
                instr.Egitmenler = c.Instructors.ToList();
            }
            else
            {
                instr.Egitmenler = c.Instructors.Where(x => x.WorkingAreaID == id).ToList();
            }

 
            return View(instr);
        }

        public ActionResult InstructorDetail(int id)
        {
            var value = c.Instructors.Where(x => x.InstructorID == id).ToList();
            return View(value);
        }

        public ActionResult calendar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMail(string name, string email, string message)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add("fitpartner000@gmail.com");
                mail.From = new MailAddress(email);
                mail.Subject = "New message from " + name;

                mail.Body = message;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "fitpartner000@gmail.com"; // Your SMTP server address
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential
                     ("fitpartner000@gmail.com", "fitPartner1905"); // Your email address and password
                smtp.EnableSsl = true;
                smtp.Send(mail);

                return RedirectToAction("Success");
            }
            catch (Exception ex)
            {
                // Handle the exception
                return RedirectToAction("Error");
            }
        }

    }
}