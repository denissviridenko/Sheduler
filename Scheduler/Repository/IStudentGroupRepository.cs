using Microsoft.AspNetCore.Mvc;
using Scheduler.Model;

namespace Scheduler.Repository
{
    public interface IStudentGroupRepository
    {
        public Task<IEnumerable<StudentGroup>> GetAllGroups();

        public Task<StudentGroup> GetGroupById(int id);

        public Task<StudentGroup> CreateGroup(StudentGroup studentGroup);

        public Task<StudentGroup> UpdateGroup(StudentGroup studentGroup);
     
        public Task<StudentGroup> DeleteGroup(int groupId);

        public bool CheckIfGroupExists(StudentGroup studentGroup);
    }
}
