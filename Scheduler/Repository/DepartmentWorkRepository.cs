using ClosedXML.Excel;
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
        public byte[] GenerateExcelFile(DepartmentWork departmentWork)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("DepartmentWork");

                worksheet.Cell(1, 1).Value = "CourseProject";
                worksheet.Cell(1, 2).Value = "TestsReview";
                worksheet.Cell(1, 3).Value = "Exams";

                worksheet.Cell(2, 1).Value = departmentWork.CourseProject;
                worksheet.Cell(2, 2).Value = departmentWork.TestsReview;
                worksheet.Cell(2, 3).Value = departmentWork.Exams;

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}
