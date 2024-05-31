using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Model;
using Scheduler.Services;

namespace Scheduler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentWorkController : ControllerBase
    {
        private readonly ApplicationContext db;
        private readonly IDepartmentWorkProcessService _departmentWorkProcessService;

        public DepartmentWorkController(ApplicationContext context, IDepartmentWorkProcessService departmentWorkProcessService)
        {
            db = context;
            _departmentWorkProcessService = departmentWorkProcessService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentWork>>> Get()
        {
            return await db.DepartmentWorks.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentWork>> Get(int id)
        {
            var departmentWork = await db.DepartmentWorks.FirstOrDefaultAsync(x => x.Id == id);
            if (departmentWork == null)
                return NotFound();

            return new ObjectResult(departmentWork);
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentWork>> Post(DepartmentWork dw)
        {
            if (dw == null)
            {
                return BadRequest("DepartmentWork cannot be null.");
            }

            var discipline = await db.Disciplines.FirstOrDefaultAsync(x => x.Id == dw.DiciplineId);
            if (discipline == null)
                return NotFound("Discipline not found.");

            var studentGroup = await db.StudentGroups.FirstOrDefaultAsync(x => x.Id == dw.GroupId);
            if (studentGroup == null)
                return NotFound("StudentGroup not found.");

            var result = await _departmentWorkProcessService.ProcessGroupInfo(dw, discipline, studentGroup, true);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<DepartmentWork>> Put(DepartmentWork dw)
        {
            if (dw == null)
            {
                return BadRequest("DepartmentWork cannot be null.");
            }

            var existingDepartmentWork = await db.DepartmentWorks.FindAsync(dw.Id);
            if (existingDepartmentWork == null)
            {
                return NotFound("DepartmentWork not found.");
            }

            var discipline = await db.Disciplines.FirstOrDefaultAsync(x => x.Id == dw.DiciplineId);
            if (discipline == null)
                return NotFound("Discipline not found.");

            var studentGroup = await db.StudentGroups.FirstOrDefaultAsync(x => x.Id == dw.GroupId);
            if (studentGroup == null)
                return NotFound("StudentGroup not found.");

            var result = await _departmentWorkProcessService.ProcessGroupInfo(dw, discipline, studentGroup, false);

            db.Entry(existingDepartmentWork).CurrentValues.SetValues(dw);
            await db.SaveChangesAsync();

            return Ok(existingDepartmentWork);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DepartmentWork>> Delete(int id)
        {
            var departmentWork = await db.DepartmentWorks.FirstOrDefaultAsync(x => x.Id == id);
            if (departmentWork == null)
            {
                return NotFound();
            }

            db.DepartmentWorks.Remove(departmentWork);
            await db.SaveChangesAsync();
            return Ok(departmentWork);
        }
    }
}
