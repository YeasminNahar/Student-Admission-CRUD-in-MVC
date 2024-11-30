using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalTest.Models.Vm
{
    public class StudentVM
    {
        public StudentVM()
        {
            this.Addmissions = new List<Addmission>();
        }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
        public bool IsAdmitted { get; set; }
        public string Picture { get; set; }
        public HttpPostedFileBase PictureFile { get; set; }
        public IList<Addmission> Addmissions { get; set; }
    }
}