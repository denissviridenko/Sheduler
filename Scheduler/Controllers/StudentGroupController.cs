using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Model;
using Scheduler.Services;
using Scheduler.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scheduler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentGroupController : ControllerBase
    {
        private readonly IStudentGroupProcessService _groupProcessService;
        private readonly IStudentGroupRepository _groupRepository;

        public StudentGroupController(IStudentGroupProcessService groupProcessService, IStudentGroupRepository groupRepository)
        {
            _groupProcessService = groupProcessService;
            _groupRepository = groupRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentGroup>>> Get()
        {
            var groupsResult = await _groupRepository.GetAllGroups();
            if (groupsResult == null)
            {
                return NotFound("No student groups found.");
            }

            return Ok(groupsResult.Value);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentGroup>> Get(int id)
        {
            var groupResult = await _groupRepository.GetGroupById(id);

            if (groupResult == null || groupResult.Value == null)
            {
                return NotFound($"Group with ID {id} not found.");
            }

            return Ok(groupResult.Value);
        }

        [HttpPost]
        public async Task<ActionResult<StudentGroup>> Post(StudentGroup studentGroup)
        {
            if (studentGroup == null)
            {
                return BadRequest("Student group data is null.");
            }

            var createdGroupResult = await _groupRepository.CreateGroup(studentGroup);
            if (createdGroupResult == null || createdGroupResult.Value == null)
            {
                return BadRequest("Failed to create the student group.");
            }

            var createdGroup = createdGroupResult.Value;
            var updatedGroup = _groupProcessService.CalculateParams(createdGroup);
            var updatedGroupResult = await _groupRepository.UpdateGroup(updatedGroup);

            if (updatedGroupResult == null || updatedGroupResult.Value == null)
            {
                return BadRequest("Failed to update the student group.");
            }

            return CreatedAtAction(nameof(Get), new { id = updatedGroupResult.Value.Id }, updatedGroupResult.Value);
        }

        [HttpPut]
        public async Task<ActionResult<StudentGroup>> Put(StudentGroup studentGroup)
        {
            if (studentGroup == null)
            {
                return BadRequest("Invalid student group data.");
            }

            // Проверка существования группы по идентификатору
            var existingGroupResult = await _groupRepository.GetGroupById(studentGroup.Id);
            if (existingGroupResult == null || existingGroupResult.Value == null)
            {
                return NotFound($"Group with ID {studentGroup.Id} not found.");
            }

            // Обновление группы
            var result = await _groupProcessService.ProcessGroupInfo(studentGroup, false);
            if (result == null || result.Value == null)
            {
                return BadRequest("Failed to process the student group.");
            }

            var updatedGroup = result.Value;
            var calculatedGroup = _groupProcessService.CalculateParams(updatedGroup);
            var updatedGroupResult = await _groupRepository.UpdateGroup(calculatedGroup);

            if (updatedGroupResult == null || updatedGroupResult.Value == null)
            {
                return BadRequest("Failed to update the student group.");
            }

            return Ok(updatedGroupResult.Value);
        }

        [HttpDelete("{groupId}")]
        public async Task<ActionResult> Delete(int groupId)
        {
            var groupResult = await _groupRepository.GetGroupById(groupId);
            if (groupResult == null || groupResult.Value == null)
            {
                return NotFound($"Group with ID {groupId} not found.");
            }

            await _groupRepository.DeleteGroup(groupId);
            return NoContent();
        }
    }
}
