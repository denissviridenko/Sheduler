﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Model;
using Scheduler.Services;
using Scheduler.Repository;



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

            if (result != null && result.Value != null) 
            {
               
                return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value);
            }
            else
            {
                return BadRequest();
            }
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
                return BadRequest("Student group data is null or ID mismatch.");
            }

            var existingGroup = await _groupRepository.GetGroupById(id);
            if (existingGroup.Value == null)
            {
                return NotFound("Student group not found.");
            }

            var result = await _groupRepository.UpdateGroupById(id, studentGroup);

            if (result == null)
            {
                return BadRequest("Failed to update student group.");
            }

            return Ok(result.Value);
        }


        [HttpDelete("{groupId}")]
        public async Task<ActionResult<StudentGroup>> Delete(int groupId)
        {
            await _groupRepository.DeleteGroup(groupId);
            return Ok();
        }
    }
}
