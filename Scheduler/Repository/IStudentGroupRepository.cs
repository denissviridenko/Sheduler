using Microsoft.AspNetCore.Mvc;
using Scheduler.Model;

namespace Scheduler.Repository
{
    public interface IStudentGroupRepository
    {
        public Task<ActionResult<IEnumerable<StudentGroup>>> GetAllGroups();

        public Task<StudentGroup> GetGroupById(int id);

        public Task<ActionResult<StudentGroup>> CreateGroup(StudentGroup studentGroup);

        public Task<ActionResult<StudentGroup>> UpdateGroup(StudentGroup studentGroup);
     
        public Task<ActionResult<StudentGroup>> DeleteGroup(int groupId);

        public bool CheckIfGroupExists(StudentGroup studentGroup);
    }
}
