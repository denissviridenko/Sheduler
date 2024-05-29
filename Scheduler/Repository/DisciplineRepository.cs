using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Model;

namespace Scheduler.Repository
{
    public class DisciplineRepository : IDisciplineRepository
    {
        private ApplicationContext db;
        public DisciplineRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<ActionResult<IEnumerable<Discipline>>> GetAllDisciplines()
        {
            return await db.Disciplines.ToListAsync();
        }

        public async Task<ActionResult<Discipline>> GetDisciplineById(int id)
        {
            Discipline discipline = await db.Disciplines.FirstOrDefaultAsync(x => x.Id == id);
            return new ObjectResult(discipline);
        }

        public async Task<ActionResult<Discipline>> CreateDiscipline(Discipline discipline)
        {

            db.Disciplines.Add(discipline);
            await db.SaveChangesAsync();
            return discipline;
        }

        public async Task<ActionResult<Discipline>> UpdateDiscipline(Discipline discipline)
        {
            db.Disciplines.Update(discipline);
            await db.SaveChangesAsync();
            return discipline;
        }
        public async Task<ActionResult<Discipline>> UpdateDisciplineById(int id, Discipline discipline)
        {
            if (id != discipline.Id)
            {
                return new BadRequestResult();
            }

            var existingDiscipline = await db.Disciplines.FindAsync(id);
            if (existingDiscipline == null)
            {
                return new NotFoundResult();
            }

            db.Entry(discipline).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DisciplineExists(id))
                {
                    return new NotFoundResult();
                }
                else
                {
                    throw;
                }
            }

            return new OkObjectResult(discipline);
        }



        private bool DisciplineExists(int id)
        {
            return db.Disciplines.Any(e => e.Id == id);
        }


        public async Task<ActionResult<Discipline>> DeleteDiscipline(int disciplineId)
        {
            Discipline discipline = db.Disciplines.FirstOrDefault(x => x.Id == disciplineId);
            if (discipline == null)
            {
                return null;
            }
            db.Disciplines.Remove(discipline);
            await db.SaveChangesAsync();
            return discipline;
        }

        public bool CheckIfDisciplineExists(Discipline discipline)
        {
            if (!db.Disciplines.Any(x => x.DiciplineName == discipline.DiciplineName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
