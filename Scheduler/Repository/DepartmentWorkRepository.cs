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
                worksheet.Cell(1, 4).Value = "CourseWork";
                worksheet.Cell(1, 5).Value = "InZe";
                worksheet.Cell(1, 6).Value = "InHours";
                worksheet.Cell(1, 7).Value = "LecturesTotal";
                worksheet.Cell(1, 8).Value = "LabWorkTotal";
                worksheet.Cell(1, 9).Value = "PracticeTotal";
                worksheet.Cell(1, 10).Value = "IndividualSessions";
                worksheet.Cell(1, 11).Value = "ModularRatingSystem";
                worksheet.Cell(1, 12).Value = "Consultations";
                worksheet.Cell(1, 13).Value = "Offsets";
                worksheet.Cell(1, 14).Value = "EducationalPractice";
                worksheet.Cell(1, 15).Value = "ProductionPractice";
                worksheet.Cell(1, 16).Value = "DiplomaDesign";
                worksheet.Cell(1, 17).Value = "GEKGAK";
                worksheet.Cell(1, 18).Value = "PostgraduateAndMasterDegree";
                worksheet.Cell(1, 19).Value = "TotalHours";

                worksheet.Cell(2, 1).Value = departmentWork.CourseProject;
                worksheet.Cell(2, 2).Value = departmentWork.TestsReview;
                worksheet.Cell(2, 3).Value = departmentWork.Exams;
                worksheet.Cell(2, 4).Value = departmentWork.CourseWork;
                worksheet.Cell(2, 5).Value = departmentWork.InZe;
                worksheet.Cell(2, 6).Value = departmentWork.InHours;
                worksheet.Cell(2, 7).Value = departmentWork.LecturesTotal;
                worksheet.Cell(2, 8).Value = departmentWork.LabWorkTotal;
                worksheet.Cell(2, 9).Value = departmentWork.PracticeTotal;
                worksheet.Cell(2, 10).Value = departmentWork.IndividualSessions;
                worksheet.Cell(2, 11).Value = departmentWork.ModularRatingSystem;
                worksheet.Cell(2, 12).Value = departmentWork.Consultations;
                worksheet.Cell(2, 13).Value = departmentWork.Offsets;
                worksheet.Cell(2, 14).Value = departmentWork.EducationalPractice;
                worksheet.Cell(2, 15).Value = departmentWork.ProductionPractice;
                worksheet.Cell(2, 16).Value = departmentWork.DiplomaDesign;
                worksheet.Cell(2, 17).Value = departmentWork.GEKGAK;
                worksheet.Cell(2, 18).Value = departmentWork.PostgraduateAndMasterDegree;
                worksheet.Cell(2, 19).Value = departmentWork.TotalHours;

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

    }
}
