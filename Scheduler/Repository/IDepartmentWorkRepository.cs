using Microsoft.AspNetCore.Mvc;
using Scheduler.Model;

namespace Scheduler.Repository
{
    public interface IDepartmentWorkRepository
    {
        public Task<ActionResult<IEnumerable<DepartmentWork>>> GetAllDepartmentWorks();

        public Task<ActionResult<DepartmentWork>> GetDepartmentWorkById(int id);

        public Task<ActionResult<IEnumerable<DepartmentWork>>> GetAllDepartmentWorksByStadyYear(string stadyYear);

        public Task<ActionResult<DepartmentWork>> CreateDepartmentWork(DepartmentWork departmentWork);

        public Task<ActionResult<DepartmentWork>> UpdateDepartmentWork(DepartmentWork departmentWork);

        public Task<ActionResult<DepartmentWork>> DeleteDepartmentWork(int departmentWork);

        public bool CheckIfDepartmentWorkExists(DepartmentWork departmentWork);

        byte[] GenerateExcelFile(DepartmentWork departmentWork);
    }
}
