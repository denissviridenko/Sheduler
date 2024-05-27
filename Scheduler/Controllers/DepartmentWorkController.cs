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
        private ApplicationContext db;
        IDepartmentWorkProcessService _departmentWorkProcessService;
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
            DepartmentWork departmentWork = await db.DepartmentWorks.FirstOrDefaultAsync(x => x.Id == id);
            if (departmentWork == null)
                return NotFound();
            return new ObjectResult(departmentWork);
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentWork>> Post(DepartmentWork dw)
        {
            if (dw == null)
            {
                return BadRequest();
            }            

            Discipline discipline = await db.Disciplines.FirstOrDefaultAsync(x => x.Id == dw.DiciplineId);
            if (discipline == null)
                return NotFound();
            StudentGroup studentGroup = await db.StudentGroups.FirstOrDefaultAsync(x => x.Id == dw.GroupId);
            if (studentGroup == null)
                return NotFound();
            var result = await _departmentWorkProcessService.ProcessGroupInfo(dw, discipline, studentGroup, true);


            //db.DepartmentWorks.Add(dw);
            //await db.SaveChangesAsync();
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<DepartmentWork>> Put(DepartmentWork dw)
        {
            if (dw == null)
            {
                return BadRequest();
            }
            if (!db.DepartmentWorks.Any(x => x.Id == dw.Id))
            {
                return NotFound();
            }

            Discipline discipline = await db.Disciplines.FirstOrDefaultAsync(x => x.Id == dw.DiciplineId);
            if (discipline == null)
                return NotFound();
            StudentGroup studentGroup = await db.StudentGroups.FirstOrDefaultAsync(x => x.Id == dw.GroupId);
            if (studentGroup == null)
                return NotFound();
            var result = await _departmentWorkProcessService.ProcessGroupInfo(dw, discipline, studentGroup, true);

            return Ok(dw);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DepartmentWork>> Delete(int id)
        {
            DepartmentWork departmentWork = db.DepartmentWorks.FirstOrDefault(x => x.Id == id);
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
