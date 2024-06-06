using Microsoft.AspNetCore.Mvc;
using Scheduler.Model;
using Scheduler.Repository;
using System;

namespace Scheduler.Services
{
    public class DepartmentWorkProcessService : IDepartmentWorkProcessService
    {
        IDepartmentWorkRepository _departmentWorkRepository;
        IDisciplineRepository _disciplineRepository;
        IStudentGroupRepository _studentGroupRepository;

        public DepartmentWorkProcessService(IDepartmentWorkRepository departmentWorkRepository, IDisciplineRepository disciplineRepository, IStudentGroupRepository studentGroupRepository)
        {
            _departmentWorkRepository = departmentWorkRepository;
            _disciplineRepository = disciplineRepository;
            _studentGroupRepository = studentGroupRepository;
        }



        public DepartmentWork CalculateParams(DepartmentWork dw, StudentGroup studentGroup, Discipline discipline)
        {
            dw.LecturesTotal = studentGroup.NumberOfStreams * discipline.LecturesAccodingToPlan;
            dw.LabWorkTotal = studentGroup.NumberOfSubgroups * discipline.LabWorkAccordingToPlan;
            dw.PracticeTotal = studentGroup.NumberOfSubgroups * discipline.PracticeAccordingToPlan;

            dw.ModularRatingSystem = studentGroup.NumberOfStudentsBudget * 0.1;
            dw.Consultations = Math.Round(studentGroup.NumberOfGroups * 2, 2);
            dw.Exams = studentGroup.NumberOfStudentsBudget * 0.5;
            dw.Offsets = studentGroup.NumberOfStudentsBudget * 0.35;
            dw.CourseProject = studentGroup.NumberOfStudentsBudget * 4;
            dw.EducationalPractice = Math.Round(studentGroup.NumberOfGroups * 2, 0) * 36;//проверить 
            dw.ProductionPractice = studentGroup.NumberOfStudentsBudget * 0.5 * 3;//проверить
            dw.DiplomaDesign = studentGroup.NumberOfStudentsBudget * 18;
            dw.GEKGAK = studentGroup.NumberOfStudentsBudget * 1;
            dw.TotalHours = dw.ModularRatingSystem + dw.IndividualSessions + dw.CourseProject + dw.Consultations + dw.TestsReview + dw.Offsets + dw.Exams + dw.EducationalPractice
                + dw.ProductionPractice + dw.DiplomaDesign + dw.GEKGAK + dw.PostgraduateAndMasterDegree + dw.LecturesTotal + dw.LabWorkTotal + dw.PracticeTotal;

            return dw;
        }

        public DepartmentWork CalculateTotals(string semestrYear)
        {
            var totals = _departmentWorkRepository.GetAllDepartmentWorksByStadyYear(semestrYear);
            // Здесь должен быть ваш код для расчета итогов
            return null;
        }
    }
}
