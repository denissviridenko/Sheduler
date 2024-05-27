using Microsoft.AspNetCore.Mvc;
using Scheduler.Model;

namespace Scheduler.Services
{
    public interface IDepartmentWorkProcessService 
    {
        public Task<ActionResult<DepartmentWork>> ProcessGroupInfo(DepartmentWork departmentWork, Discipline discipline, StudentGroup studentGroup, bool isNewDepartmentWork);

        public DepartmentWork CalculateParams(DepartmentWork dw, StudentGroup studentGroup, Discipline discipline);
    }
}
