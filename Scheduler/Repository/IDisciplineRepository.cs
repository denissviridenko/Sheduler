using Microsoft.AspNetCore.Mvc;
using Scheduler.Model;

namespace Scheduler.Repository
{
    public interface IDisciplineRepository
    {
        public Task<ActionResult<IEnumerable<Discipline>>> GetAllDisciplines();

        public Task<ActionResult<Discipline>> GetDisciplineById(int id);

        public Task<ActionResult<Discipline>> CreateDiscipline(Discipline discipline);

        public Task<ActionResult<Discipline>> UpdateDiscipline(Discipline discipline);

        public Task<ActionResult<Discipline>> DeleteDiscipline(int disciplineId);

        public bool CheckIfDisciplineExists(Discipline discipline);
    }
}
