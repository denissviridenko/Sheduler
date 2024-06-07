using Scheduler.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scheduler.Repository
{
    public interface IStudentGroupRepository
    {
        Task<IEnumerable<StudentGroup>> GetAllGroups();
        Task<StudentGroup> GetGroupById(int id);
        Task<StudentGroup> CreateGroup(StudentGroup studentGroup);
        Task<StudentGroup> UpdateGroup(StudentGroup studentGroup);
        Task<StudentGroup> DeleteGroup(int groupId);
        bool CheckIfGroupExists(StudentGroup studentGroup);
    }
}
