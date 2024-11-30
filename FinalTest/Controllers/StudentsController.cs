using FinalTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using FinalTest.Models.Vm;
using System.IO;

namespace FinalTest.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Students
        DatabaseContext db = new DatabaseContext();
        public ActionResult Index()
        {
            var students = db.Students.Include(a => a.Addmissions.Select(b => b.Subject)).OrderByDescending(s => s.StudentId).ToList();
            return View(students);
        }
        public ActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Create(StudentVM studentVM, int[] subjectId)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    StudentName = studentVM.StudentName,
                    Age = studentVM.Age,
                    IsAdmitted = studentVM.IsAdmitted,


                };
                HttpPostedFileBase file = studentVM.PictureFile;
                if (file != null)
                {
                    string Filepath = Path.Combine("/Images",
                        DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(Filepath));
                    student.Picture = Filepath;

                }
                foreach (var st in subjectId)
                {
                    Addmission addmission = new Addmission()
                    {
                        Student = student,
                        StudentId = student.StudentId,
                        SubjectId = st


                    };
                    db.Addmissions.Add(addmission);



                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult AddSubject(int? id)
        {

            ViewBag.subject = new SelectList(db.Subjects.ToList(), "SubjectId", "SubjectName", (id != null) ? id.ToString() : "");
            return PartialView("Add");
        
        }
        public ActionResult Edit(int? id)
        {
            var student = db.Students.Find(id);
            var d = db.Addmissions.Where(x => x.StudentId == student.StudentId).ToList();
            var newsb = new StudentVM
            { 
            StudentId=student.StudentId,
            StudentName=student.StudentName,
            Age=student.Age,
            IsAdmitted=student.IsAdmitted,
            Picture=student.Picture
            
            };
            newsb.Addmissions = d;
            return View(newsb);
        }
        [HttpPost]
        public ActionResult Edit(StudentVM studentVM, int[] subjectId)
        {
            if (ModelState.IsValid)
            {
                var student = db.Students.Find(studentVM.StudentId);
                if (student == null)
                {
                    return HttpNotFound();
                }


                student.StudentName = studentVM.StudentName;
                student.Age = studentVM.Age;
                student.IsAdmitted = studentVM.IsAdmitted;



                HttpPostedFileBase file = studentVM.PictureFile;
                if (file != null)
                {
                    string FilePath = Path.Combine("/Images",
                          DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(FilePath));
                    student.Picture = FilePath;
                }
                else
                {
                    student.Picture = student.Picture;
                }

                var rs = db.Addmissions.Where(t => t.StudentId == student.StudentId).ToList();
                foreach (var t in rs)
                {
                    db.Addmissions.Remove(t);
                }
                foreach (var stat in subjectId)
                {
                    var s = db.Subjects.FirstOrDefault(st => st.SubjectId == stat);
                    if (s != null)
                    {
                        var addmission = new Addmission()
                        {
                            Student = student,
                            SubjectId = s.SubjectId,
                            StudentId = student.StudentId,

                        };
                        db.Addmissions.Add(addmission);

                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentVM);
        }
        public ActionResult Delete(int? id)
        {
            var stdt = db.Students.FirstOrDefault(c => c.StudentId == id);
            var addmissioninfo = db.Addmissions.Where(c => c.SubjectId == id).ToList();
            db.Addmissions.RemoveRange(addmissioninfo);
            db.Students.Remove(stdt);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}
