using System.ComponentModel.DataAnnotations;

namespace Scheduler.Model
{
    public class StudentGroup
    {
        [Key]
        public int Id { get; set; }

        public string GroupName { get; set; } = "";

        public int NumberOfStudentsBudget { get; set; }

        public int NumberOfStudentsPaid { get; set; }

        public int NumberOfStreams { get; set; }

        public double NumberOfGroups { get; set; }

        public int NumberOfSubgroups { get; set; }
    }
}
