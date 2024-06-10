using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sube2.HelloMvc.Models;
using System.Net;

namespace Sube2.HelloMvc.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            using (var ctx = new OkulDbContext())
            {
                var lst = ctx.Ogrenciler.ToList();
                return View(lst);
            }
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(Ogrenci ogr)
        {
            if (ogr != null)
            {
                ;
                using (var ctx = new OkulDbContext())
                {
                    ctx.Ogrenciler.Add(ogr);
                    ctx.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult EditStudent(int id)
        {
            using (var ctx = new OkulDbContext())
            {
                var ogr = ctx.Ogrenciler.Find(id);
                return View(ogr);
            }
        }

        [HttpPost]
        public IActionResult EditStudent(Ogrenci ogr)
        {
            if (ogr != null)
            {
                using (var ctx = new OkulDbContext())
                {
                    ctx.Entry(ogr).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteStudent(int id)
        {
            using (var ctx = new OkulDbContext())
            {
                ctx.Ogrenciler.Remove(ctx.Ogrenciler.Find(id));
                ctx.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult AssignCourse(int? id)
        {
            using (var ctx = new OkulDbContext()) {
                Ogrenci student = ctx.Ogrenciler.Include(s => s.Dersler).FirstOrDefault(s => s.Ogrenciid == id);
                ViewBag.CourseId = new SelectList(ctx.Dersler, "Dersid", "Dersad");
                return View(student);
            }
 
        }
        public ActionResult AssignCourse(int id, int courseId)
        {
            using (var ctx = new OkulDbContext()) {
                Ogrenci student = ctx.Ogrenciler.Include(s => s.Dersler).FirstOrDefault(s => s.Ogrenciid == id);

                Ders course = ctx.Dersler.Find(courseId);


                student.Dersler.Add(course);
                ctx.SaveChanges();

                return RedirectToAction("Details", new { id = student.Ogrenciid });
            }
               
        }
        public ActionResult Courses(int? id)
        {
            using (var ctx = new OkulDbContext()) {
                Ogrenci student = ctx.Ogrenciler.Include(s => s.Dersler).FirstOrDefault(s => s.Ogrenciid == id);

                return View(student);
            }
                
        }
    }
}
