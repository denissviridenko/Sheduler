using System.ComponentModel.DataAnnotations;

namespace Scheduler.Model
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string SecondName { get; set; }

        public double Rate { get; set; }


    }
}
