using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Model;
using Scheduler.Repository;

namespace Scheduler.Services
{
    public class StudentGroupProcessService : IStudentGroupProcessService
    {
        IStudentGroupRepository _groupRepository;

        public StudentGroupProcessService(IStudentGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<ActionResult<StudentGroup>> ProcessGroupInfo(StudentGroup studentGroup, bool isNewGroup)
        {
            var sg = _groupRepository.CheckIfGroupExists(studentGroup);

            var sgpc = new StudentGroupProcessService(_groupRepository);

            studentGroup = sgpc.CalculateParams(studentGroup);

            if (isNewGroup)
            {                
                if (!sg)
                {
                    return await _groupRepository.CreateGroup(studentGroup);
                }
                else
                {
                    return null;//группа с данным именем уже существет
                }
            }
            else
            {                
                if (!sg)
                {
                    return null;//невозможно обновить группу, группа не существует
                }
                else
                {
                    return await _groupRepository.UpdateGroup(studentGroup);
                }
            }
        }

        public StudentGroup CalculateParams(StudentGroup studentGroup)
        {
            var totalStudents = studentGroup.NumberOfStudentsBudget + studentGroup.NumberOfStudentsPaid;//узнать что будет когда больше 30 человек набрали

            if (totalStudents <= 15)
            {
                studentGroup.NumberOfStreams = 1;
                studentGroup.NumberOfGroups = 0.5;
                studentGroup.NumberOfSubgroups = 1;
            }
            else if (totalStudents >= 15 && totalStudents <= 30)
            {
                studentGroup.NumberOfStreams = 1;
                studentGroup.NumberOfGroups = 1;
                studentGroup.NumberOfSubgroups = 2;
            }
            
            return studentGroup;
        }
    }
}
