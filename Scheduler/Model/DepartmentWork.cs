using System.ComponentModel.DataAnnotations;

namespace Scheduler.Model
{
    public class DepartmentWork
    {
        [Key]
        public int Id { get; set; }

        public int DiciplineId { get; set; }               

        public int GroupId { get; set; }

        public double CourseProject { get; set; }
        public double TestsReview { get; set; }
        public double Exams { get; set; }
        public double CourseWork { get; set; }
        public double InZe { get; set; }
        public double InHours { get; set; }
        public double LecturesTotal { get; set; }
        public double LabWorkTotal { get; set; }

        public double PracticeTotal { get; set; }
        public double IndividualSessions { get; set; }

        public double ModularRatingSystem { get; set; }
      
        public double Consultations { get; set; }

        public double Offsets { get; set; }

        public double EducationalPractice { get; set; }

        public double ProductionPractice { get; set; }

        public double DiplomaDesign { get; set; }

        public double GEKGAK { get; set; }

        public double PostgraduateAndMasterDegree { get; set; }

        public double TotalHours { get; set; }

        public bool BudgetFlag { get; set; }

        public string StudyYear { get; set; }    

    }
}
