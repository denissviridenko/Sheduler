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
                return BadRequest("DepartmentWork cannot be null.");
            }

            var result = await _departmentWorkProcessService.ProcessGroupInfo(dw, null, null, true);

            // Добавление новой записи в базу данных
            db.DepartmentWorks.Add(dw);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = dw.Id }, dw);
        }

        [HttpPut]
        public async Task<ActionResult<DepartmentWork>> Put(DepartmentWork dw)
        {
            if (dw == null)
            {
                return BadRequest("DepartmentWork cannot be null.");
            }

            if (!db.DepartmentWorks.Any(x => x.Id == dw.Id))
            {
                return NotFound("DepartmentWork not found.");
            }

            var result = await _departmentWorkProcessService.ProcessGroupInfo(dw, null, null, false);

            // Обновление существующей записи в базе данных
            db.DepartmentWorks.Update(dw);
            await db.SaveChangesAsync();

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