using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternShipBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternShipBackend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<SpecificDays> SpecificDays { get; set; }

        public DbSet<InternShip> InternShips { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<File> Files{ get; set; }
        public DbSet<Recourse> Recourses{ get; set; }
        public DbSet<Grade> Grades { get; set; }

        public DbSet<GradingSetting> GradingSettings { get; set; }




        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeachersLogin> TeachersLogins { get; set; }

    }
}
