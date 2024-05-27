using Microsoft.AspNetCore.Mvc;
using Scheduler.Model;

namespace Scheduler.Services
{
    public interface IStudentGroupProcessService
    {
        public StudentGroup CalculateParams(StudentGroup studentGroup);
        public Task<ActionResult<StudentGroup>> ProcessGroupInfo(StudentGroup studentGroup, bool isNewGroup);
    }
}
