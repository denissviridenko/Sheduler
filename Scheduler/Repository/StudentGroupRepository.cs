using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Repository
{
    public class StudentGroupRepository : IStudentGroupRepository
    {
        private ApplicationContext db;

        public StudentGroupRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<ActionResult<IEnumerable<StudentGroup>>> GetAllGroups()
        {
            return await db.StudentGroups.ToListAsync();
        }
        public async Task<ActionResult<StudentGroup>> GetGroupById(int id)
        {
            var group = await db.Set<StudentGroup>().FindAsync(id);
            return group;
        }


        /* z public async Task<ActionResult<StudentGroup>> GetGroupById(int id)
          {
              StudentGroup studentGroup = await db.StudentGroups.FirstOrDefaultAsync(x => x.Id == id);
              return new ObjectResult(studentGroup);
          }*/

        public async Task<ActionResult<StudentGroup>> CreateGroup(StudentGroup studentGroup)
        {
            db.StudentGroups.Add(studentGroup);
            await db.SaveChangesAsync();
            return studentGroup;
        }

        public async Task<ActionResult<StudentGroup>> UpdateGroup(StudentGroup studentGroup)
        {
            db.StudentGroups.Update(studentGroup);
            await db.SaveChangesAsync();
            return studentGroup;
        }
        public async Task<ActionResult<StudentGroup>> DeleteGroup(int groupId)
        {
            StudentGroup studentGroup = db.StudentGroups.FirstOrDefault(x => x.Id == groupId);
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
            if (!db.StudentGroups.Any(x => x.GroupName == studentGroup.GroupName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool GroupExists(int id)
        {
            return db.StudentGroups.Any(e => e.Id == id);
        }
    }
}
