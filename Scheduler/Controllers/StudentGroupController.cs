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
            var groups = await _groupRepository.GetAllGroups();
            return Ok(groups);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentGroup>> Get(int id)
        {
            var group = await _groupRepository.GetGroupById(id);

            if (group == null)
            {
                return NotFound();
            }

            return Ok(group);
        }

        [HttpPost]
        public async Task<ActionResult<StudentGroup>> Post(StudentGroup studentGroup)
        {
            if (studentGroup == null)
            {
                return BadRequest("Student group data is null.");
            }

            var createdGroup = await _groupRepository.CreateGroup(studentGroup);
            if (createdGroup == null)
            {
                return BadRequest("Failed to create the student group.");
            }

            var updatedGroup = _groupProcessService.CalculateParams(createdGroup);
            await _groupRepository.UpdateGroup(updatedGroup);

            return CreatedAtAction(nameof(Get), new { id = updatedGroup.Id }, updatedGroup);
        }

        [HttpPut]
        public async Task<ActionResult<StudentGroup>> Put(StudentGroup studentGroup)
        {
            if (studentGroup == null)
            {
                return BadRequest("Invalid student group data.");
            }

            var calculatedGroup = _groupProcessService.CalculateParams(studentGroup);
            var updatedGroup = await _groupRepository.UpdateGroup(calculatedGroup);

            if (updatedGroup == null)
            {
                return NotFound($"Group with ID {studentGroup.Id} not found.");
            }

            return Ok(updatedGroup);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var group = await _groupRepository.GetGroupById(id);
            if (group == null)
            {
                return NotFound();
            }

            await _groupRepository.DeleteGroup(id);
            return NoContent();
        }
    }
}
