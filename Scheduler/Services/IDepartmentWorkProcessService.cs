using Microsoft.AspNetCore.Mvc;
using Scheduler.Model;

namespace Scheduler.Services
{
    public interface IDepartmentWorkProcessService 
    {
        public DepartmentWork CalculateParams(DepartmentWork dw, StudentGroup studentGroup, Discipline discipline);
    }
}
