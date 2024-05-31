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

            if (discipline.ExamFlag)
            {
                dw.ModularRatingSystem = studentGroup.NumberOfStudentsBudget * 0.1;
                dw.Consultations = Math.Round(studentGroup.NumberOfGroups * 2, 2);
                dw.Exams = studentGroup.NumberOfStudentsBudget * 0.5;
            }
            if (discipline.OffsetFlag)
            {
                dw.ModularRatingSystem = studentGroup.NumberOfStudentsBudget * 0.1;
                dw.Offsets = studentGroup.NumberOfStudentsBudget * 0.35;
            }
            if (discipline.CourseProjectFlag)
            {
                dw.CourseProject = studentGroup.NumberOfStudentsBudget * 4;
            }
            if (discipline.EducationalPracticeFlag)
            {
                dw.EducationalPractice = Math.Round(studentGroup.NumberOfGroups * 2, 0) * 36;//проверить 
            }
            if (discipline.ProductionPracticeFlag)
            {
                dw.ProductionPractice = studentGroup.NumberOfStudentsBudget * 0.5 * 3;//проверить
            }
            if (discipline.DiplomaFlag && discipline.DiciplineName != "Рецензирование ДП" && discipline.DiciplineName != "Нормоконтроль ДП" && discipline.DiciplineName != "Утверждение ДП")
            {
                dw.DiplomaDesign = studentGroup.NumberOfStudentsBudget * 18;
            }
            if (discipline.DiplomaFlag && discipline.DiciplineName == "Рецензирование ДП")
            {
                dw.DiplomaDesign = studentGroup.NumberOfStudentsBudget * 3;
            }
            if (discipline.DiplomaFlag && discipline.DiciplineName == "Нормоконтроль ДП")
            {
                dw.DiplomaDesign = studentGroup.NumberOfStudentsBudget * 0.5;
            }
            if (discipline.DiplomaFlag && discipline.DiciplineName == "Утверждение ДП")
            {
                dw.DiplomaDesign = studentGroup.NumberOfStudentsBudget * 0.5;
            }
            if (discipline.GEKGAKFlag && discipline.DiciplineName == "Председатель ГЭК")
            {
                dw.GEKGAK = studentGroup.NumberOfStudentsBudget * 1;
            }
            if (discipline.GEKGAKFlag && discipline.DiciplineName == "Члены ГЭК")
            {
                dw.GEKGAK = studentGroup.NumberOfStudentsBudget * 2;
            }

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
