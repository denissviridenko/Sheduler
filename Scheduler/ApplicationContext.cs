using Microsoft.EntityFrameworkCore;
using Scheduler.Model;

namespace Scheduler
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Discipline> Disciplines { get; set; } = null!;

        public DbSet<DepartmentWork> DepartmentWorks { get; set; } = null!;

        public DbSet<StudentGroup> StudentGroups { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                    new User { Id = 1, Name = "Tom", Age = 37 },
                    new User { Id = 2, Name = "Bob", Age = 41 },
                    new User { Id = 3, Name = "Sam", Age = 24 }
            );

            modelBuilder.Entity<Discipline>().HasData(
                new Discipline { Id = 1, DiciplineName = "Б1О11 Информатика, 1 сем (экзамен) (БИОР_231)", LecturesAccodingToPlan = 16, LabWorkAccordingToPlan = 34, PracticeAccordingToPlan = 0, ExamFlag = true },
                new Discipline { Id = 2, DiciplineName = "Б1О7 Информатика, 1 сем (экзамен) (ЭЭP_231)", LecturesAccodingToPlan = 16, LabWorkAccordingToPlan = 34, PracticeAccordingToPlan = 0, ExamFlag = true },
                new Discipline { Id = 3, DiciplineName = "Б1О8 Информатика, 1 сем (экзамен) (СПР_231)", LecturesAccodingToPlan = 16, LabWorkAccordingToPlan = 34, PracticeAccordingToPlan = 0, ExamFlag = true, OffsetFlag = true, CourseProjectFlag = true, DiplomaFlag = true, GEKGAKFlag = true, EducationalPracticeFlag = true, ProductionPracticeFlag = true, PostgraduateAndMasterDegreeFlag = true }
                );
        }
    }
}
