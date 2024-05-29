using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Model;
using Scheduler.Services;
using Scheduler.Repository;
using System.Text.RegularExpressions;



namespace Scheduler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentGroupController : ControllerBase
    {
        IStudentGroupProcessService _groupProcessService;
        IStudentGroupRepository _groupRepository;
        public StudentGroupController(IStudentGroupProcessService groupProcessService, IStudentGroupRepository groupRepository)
        {
            _groupProcessService = groupProcessService;
            _groupRepository = groupRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentGroup>>> Get()
        {
            return await _groupRepository.GetAllGroups();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentGroup>> Get(int id)
        {
            return await _groupRepository.GetGroupById(id);
        }

        [HttpPost]
        public async Task<ActionResult<StudentGroup>> Post(StudentGroup studentGroup)
        {
            if (studentGroup == null)
            {
                return BadRequest();
            }
            var result = await _groupProcessService.ProcessGroupInfo(studentGroup, true);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<StudentGroup>> Put(StudentGroup studentGroup)
        {
            if (studentGroup == null)
            {
                return BadRequest();
            }
            var result = await _groupProcessService.ProcessGroupInfo(studentGroup, false);

            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<StudentGroup>> Put(int id, StudentGroup studentGroup)
        {
            if (studentGroup == null || id != studentGroup.Id)
            {
                return BadRequest();
            }

            var existingGroup = await _groupRepository.GetGroupById(id);
            if (existingGroup == null)
            {
                return NotFound();
            }

            await _groupRepository.UpdateGroup(studentGroup);

            return Ok(studentGroup);
        }


        [HttpDelete("{groupId}")]
        public async Task<ActionResult<StudentGroup>> Delete(int groupId)
        {
            await _groupRepository.DeleteGroup(groupId);
            return Ok();
        }
    }
}
