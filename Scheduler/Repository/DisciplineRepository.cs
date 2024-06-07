using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Model;

namespace Scheduler.Repository
{
    public class DisciplineRepository : IDisciplineRepository
    {
        private ApplicationContext db;
        public DisciplineRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<ActionResult<IEnumerable<Discipline>>> GetAllDisciplines()
        {
            return await db.Disciplines.ToListAsync();
        }

        public async Task<ActionResult<Discipline>> GetDisciplineById(int id)
        {
            Discipline discipline = await db.Disciplines.FirstOrDefaultAsync(x => x.Id == id);
            return discipline;
        }

        public async Task<ActionResult<Discipline>> CreateDiscipline(Discipline discipline)
        {

            db.Disciplines.Add(discipline);
            await db.SaveChangesAsync();
            return discipline;
        }

        public async Task<ActionResult<Discipline>> UpdateDiscipline(Discipline discipline)
        {
            db.Disciplines.Update(discipline);
            await db.SaveChangesAsync();
            return discipline;
        }
        public async Task UpdateDisciplineById(int id, Discipline discipline)
        {
            if (id != discipline.Id)
            {
                throw new ArgumentException("ID in the URL does not match ID in the discipline data.");
            }

            var existingDiscipline = await db.Disciplines.FindAsync(id);
            if (existingDiscipline == null)
            {
                throw new KeyNotFoundException("Discipline not found.");
            }

            existingDiscipline.DiciplineName = discipline.DiciplineName;
            existingDiscipline.LecturesAccodingToPlan = discipline.LecturesAccodingToPlan;
            existingDiscipline.LabWorkAccordingToPlan = discipline.LabWorkAccordingToPlan;
            existingDiscipline.PracticeAccordingToPlan = discipline.PracticeAccordingToPlan;
            existingDiscipline.MpcAccordingToPlan = discipline.MpcAccordingToPlan;
            existingDiscipline.AkrAccordingToPlan = discipline.AkrAccordingToPlan;
            existingDiscipline.CourseProjectAccordingToPlan = discipline.CourseProjectAccordingToPlan;
            existingDiscipline.СonsultationsAccordingToPlan = discipline.СonsultationsAccordingToPlan;
            existingDiscipline.ReviewsAccordingToPlan = discipline.ReviewsAccordingToPlan;
            existingDiscipline.TestsAccordingToPlan = discipline.TestsAccordingToPlan;
            existingDiscipline.ExamsAccordingToPlan = discipline.ExamsAccordingToPlan;
            existingDiscipline.EducationalPracticeAccordingToPlan = discipline.EducationalPracticeAccordingToPlan;
            existingDiscipline.ProductionPracticeAccordingToPlan = discipline.ProductionPracticeAccordingToPlan;
            existingDiscipline.GraduationProjectAccordingToPlan = discipline.GraduationProjectAccordingToPlan;
            existingDiscipline.GEKGAKAccordingToPlan = discipline.GEKGAKAccordingToPlan;
            existingDiscipline.PostgraduateAndMasterDegreeAccordingToPlan = discipline.PostgraduateAndMasterDegreeAccordingToPlan;
            existingDiscipline.AllHoursAccordingToPlan = discipline.AllHoursAccordingToPlan;
            existingDiscipline.Lectures = discipline.Lectures;
            existingDiscipline.LabWork = discipline.LabWork;
            existingDiscipline.Practice = discipline.Practice;
            existingDiscipline.Mpc = discipline.Mpc;
            existingDiscipline.Akr = discipline.Akr;
            existingDiscipline.CourseProject = discipline.CourseProject;
            existingDiscipline.Сonsultations = discipline.Сonsultations;
            existingDiscipline.Reviews = discipline.Reviews;
            existingDiscipline.Tests = discipline.Tests;
            existingDiscipline.Exams = discipline.Exams;
            existingDiscipline.EducationalPractice = discipline.EducationalPractice;
            existingDiscipline.ProductionPractice = discipline.ProductionPractice;
            existingDiscipline.GraduationProject = discipline.GraduationProject;
            existingDiscipline.GEKGAK = discipline.GEKGAK;
            existingDiscipline.PostgraduateAndMasterDegree = discipline.PostgraduateAndMasterDegree;
            existingDiscipline.AllHours = discipline.AllHours;
            existingDiscipline.ExamFlag = discipline.ExamFlag;
            existingDiscipline.OffsetFlag = discipline.OffsetFlag;
            existingDiscipline.CourseProjectFlag = discipline.CourseProjectFlag;
            existingDiscipline.DiplomaFlag = discipline.DiplomaFlag;
            existingDiscipline.GEKGAKFlag = discipline.GEKGAKFlag;
            existingDiscipline.EducationalPracticeFlag = discipline.EducationalPracticeFlag;
            existingDiscipline.ProductionPracticeFlag = discipline.ProductionPracticeFlag;
            existingDiscipline.PostgraduateAndMasterDegreeFlag = discipline.PostgraduateAndMasterDegreeFlag;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DisciplineExists(id))
                {
                    throw new KeyNotFoundException("Discipline not found.");
                }
                else
                {
                    throw;
                }
            }
        }






        private bool DisciplineExists(int id)
        {
            return db.Disciplines.Any(e => e.Id == id);
        }


        public async Task<ActionResult<Discipline>> DeleteDiscipline(int disciplineId)
        {
            Discipline discipline = db.Disciplines.FirstOrDefault(x => x.Id == disciplineId);
            if (discipline == null)
            {
                return null;
            }
            db.Disciplines.Remove(discipline);
            await db.SaveChangesAsync();
            return discipline;
        }

        public bool CheckIfDisciplineExists(Discipline discipline)
        {
            if (!db.Disciplines.Any(x => x.DiciplineName == discipline.DiciplineName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
