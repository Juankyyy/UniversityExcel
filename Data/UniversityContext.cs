using Microsoft.EntityFrameworkCore;
using University.Models;

namespace University.Context
{
    public class UniversityContext : DbContext
    {
        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options) {}

        public DbSet<Career> Careers { get; set; }
        public DbSet<Inscription> Inscriptions { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Universities> Universities { get; set; }
    }
}