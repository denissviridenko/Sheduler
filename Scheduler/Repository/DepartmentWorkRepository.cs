using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Model;

namespace Scheduler.Repository
{
    public class DepartmentWorkRepository : IDepartmentWorkRepository
    {
        private ApplicationContext db;
        public DepartmentWorkRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<ActionResult<IEnumerable<DepartmentWork>>> GetAllDepartmentWorks()
        {
            return await db.DepartmentWorks.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<DepartmentWork>>> GetAllDepartmentWorksByStadyYear(string stadyYear)
        {
            return await db.DepartmentWorks.Where(x => x.StudyYear == stadyYear).ToListAsync();
        }

        public async Task<ActionResult<DepartmentWork>> GetDepartmentWorkById(int id)
        {
            DepartmentWork departmentWork = await db.DepartmentWorks.FirstOrDefaultAsync(x => x.Id == id);
            return new ObjectResult(departmentWork);
        }

        public async Task<ActionResult<DepartmentWork>> CreateDepartmentWork(DepartmentWork departmentWork)
        {

            db.DepartmentWorks.Add(departmentWork);
            await db.SaveChangesAsync();
            return departmentWork;
        }

        public async Task<ActionResult<DepartmentWork>> UpdateDepartmentWork(DepartmentWork departmentWork)
        {
            db.DepartmentWorks.Update(departmentWork);
            await db.SaveChangesAsync();
            return departmentWork;
        }

        public async Task<ActionResult<DepartmentWork>> DeleteDepartmentWork(int departmentWorkId)
        {
            DepartmentWork departmentWork = db.DepartmentWorks.FirstOrDefault(x => x.Id == departmentWorkId);
            if (departmentWork == null)
            {
                return null;
            }
            db.DepartmentWorks.Remove(departmentWork);
            await db.SaveChangesAsync();
            return departmentWork;
        }

        public bool CheckIfDepartmentWorkExists(DepartmentWork departmentWork)
        {
            if (!db.DepartmentWorks.Any(x => x.Id == departmentWork.Id))
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
