using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FinalTest.Models
{
    public class DatabaseContext:DbContext
    {
      public  DatabaseContext() : base("FinalDb")
        { }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Addmission> Addmissions { get; set; }
    }
}