using Microsoft.EntityFrameworkCore;
using Scheduler.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Repository
{
    public class StudentGroupRepository : IStudentGroupRepository
    {
        private readonly ApplicationContext db;

        public StudentGroupRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<StudentGroup>> GetAllGroups()
        {
            return await db.StudentGroups.ToListAsync();
        }

        public async Task<StudentGroup> GetGroupById(int id)
        {
            return await db.StudentGroups.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<StudentGroup> CreateGroup(StudentGroup studentGroup)
        {
            db.StudentGroups.Add(studentGroup);
            await db.SaveChangesAsync();
            return studentGroup;
        }

        public async Task<StudentGroup> UpdateGroup(StudentGroup studentGroup)
        {
            db.StudentGroups.Update(studentGroup);
            await db.SaveChangesAsync();
            return studentGroup;
        }

        public async Task<StudentGroup> DeleteGroup(int groupId)
        {
            var studentGroup = await db.StudentGroups.FirstOrDefaultAsync(x => x.Id == groupId);
            if (studentGroup == null)
            {
                return null;
            }
            db.StudentGroups.Remove(studentGroup);
            await db.SaveChangesAsync();
            return studentGroup;
        }

        public bool CheckIfGroupExists(StudentGroup studentGroup)
        {
            return db.StudentGroups.Any(x => x.GroupName == studentGroup.GroupName);
        }

        private bool GroupExists(int id)
        {
            return db.StudentGroups.Any(e => e.Id == id);
        }
    }
}
