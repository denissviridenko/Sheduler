using System.ComponentModel.DataAnnotations;

namespace Scheduler.Model
{
    public class Discipline
    {
        [Key]
        public int Id { get; set; }

        public string DiciplineName { get; set; } = "";

        public double LecturesAccodingToPlan { get; set; }

        public double LabWorkAccordingToPlan { get; set; }

        public double PracticeAccordingToPlan { get; set; }

        public double MpcAccordingToPlan { get; set; }

        public double AkrAccordingToPlan { get; set; }

        public double CourseProjectAccordingToPlan { get; set; }

        public double СonsultationsAccordingToPlan { get; set; }

        public double ReviewsAccordingToPlan { get; set; }

        public double TestsAccordingToPlan { get; set; }

        public double ExamsAccordingToPlan { get; set; }

        public double EducationalPracticeAccordingToPlan { get; set; }

        public double ProductionPracticeAccordingToPlan { get; set; }

        public double GraduationProjectAccordingToPlan { get; set; }    

        public double GEKGAKAccordingToPlan { get; set; }

        public double PostgraduateAndMasterDegreeAccordingToPlan { get; set; }

        public double AllHoursAccordingToPlan { get; set; }

        public double Lectures { get; set; }

        public double LabWork { get; set; }

        public double Practice { get; set; }

        public double Mpc { get; set; }
        public double Akr { get; set; }
        public double CourseProject { get; set; }

        public double Сonsultations { get; set; }

        public double Reviews { get; set; }

        public double Tests { get; set; }

        public double Exams { get; set; }

        public double EducationalPractice { get; set; }

        public double ProductionPractice { get; set; }

        public double GraduationProject { get; set; }

        public double GEKGAK { get; set; }

        public double PostgraduateAndMasterDegree { get; set; }

        public double AllHours { get; set; }

        public bool ExamFlag { get; set; }

        public bool OffsetFlag { get; set; }
       
        public bool CourseProjectFlag { get; set; }

        public bool DiplomaFlag { get; set; }

        public bool GEKGAKFlag { get; set; }

        public bool EducationalPracticeFlag { get; set; }

        public bool ProductionPracticeFlag { get; set; }

        public bool PostgraduateAndMasterDegreeFlag { get; set; }

    }
}
