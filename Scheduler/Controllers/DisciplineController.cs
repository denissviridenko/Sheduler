using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Model;
using Scheduler.Repository;

namespace Scheduler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplineController : ControllerBase
    {
        IDisciplineRepository _disciplineRepository;

        public DisciplineController(IDisciplineRepository disciplineRepository)
        {
            _disciplineRepository = disciplineRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Discipline>>> Get()
        {
            return await _disciplineRepository.GetAllDisciplines();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Discipline>> Get(int id)
        {
            var discipline = await _disciplineRepository.GetDisciplineById(id);
            return discipline;
        }

        [HttpPost]
        public async Task<ActionResult<Discipline>> Post(Discipline discipline)
        {
            if (discipline == null)
            {
                return BadRequest();
            }
            await _disciplineRepository.CreateDiscipline(discipline);
            return Ok(discipline);
        }

        [HttpPut]
        public async Task<ActionResult<Discipline>> Put(Discipline discipline)
        {
            if (discipline == null)
            {
                return BadRequest();
            }
            
            await _disciplineRepository.UpdateDiscipline(discipline);
            
            return Ok(discipline);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Discipline>> Put(int id, Discipline discipline)
        {
            if (discipline == null)
            {
                return BadRequest("Discipline data is null.");
            }

            if (id != discipline.Id)
            {
                return BadRequest("ID in the URL does not match ID in the discipline data.");
            }

            var existingDisciplineResult = await _disciplineRepository.GetDisciplineById(id);
            if (existingDisciplineResult.Result is NotFoundResult)
            {
                return NotFound("Discipline not found.");
            }

            var result = await _disciplineRepository.UpdateDisciplineById(id, discipline);
            return result.Result;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Discipline>> Delete(int id)
        {
            var discipline = await _disciplineRepository.DeleteDiscipline(id);
            return Ok(discipline);
        }
    }
}
