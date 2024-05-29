using Microsoft.AspNetCore.Mvc;
using Scheduler.Model;
using Scheduler.Repository;

[Route("api/[controller]")]
[ApiController]
public class DisciplineController : ControllerBase
{
    private readonly IDisciplineRepository _disciplineRepository;

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
        return CreatedAtAction(nameof(Get), new { id = discipline.Id }, discipline);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Discipline>> Put(int id, Discipline discipline)
    {
        if (discipline == null || id != discipline.Id)
        {
            return BadRequest();
        }

        var existingDiscipline = await _disciplineRepository.GetDisciplineById(id);
        if (existingDiscipline == null)
        {
            return NotFound();
        }

        var result = await _disciplineRepository.UpdateDisciplineById(id, discipline);

        if (result == null)
        {
            return BadRequest();
        }

        return Ok(result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Discipline>> Delete(int id)
    {
        var discipline = await _disciplineRepository.DeleteDiscipline(id);
        return Ok(discipline);
    }
}
