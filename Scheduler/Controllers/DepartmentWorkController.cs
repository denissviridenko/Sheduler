using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scheduler.Model;
using Scheduler.Repository;
using Scheduler.Services;

namespace Scheduler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentWorkController : ControllerBase
    {
        private ApplicationContext db;
        IDepartmentWorkProcessService _departmentWorkProcessService;
        IDepartmentWorkRepository _departmentWorkRepository;
        IStudentGroupRepository _groupRepository;
        IDisciplineRepository _disciplineRepository;

        public DepartmentWorkController(ApplicationContext context, IDepartmentWorkRepository departmentWorkRepository, IDepartmentWorkProcessService departmentWorkProcessService, IStudentGroupRepository groupRepository, IDisciplineRepository disciplineRepository)
        {
            db = context;
            _departmentWorkProcessService = departmentWorkProcessService;
            _departmentWorkRepository = departmentWorkRepository;
            _groupRepository = groupRepository;
            _disciplineRepository = disciplineRepository;
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
        [HttpGet("{id}/excel")]
        public async Task<IActionResult> ExportDataToExcel(int id)
        {
            try
            {
                // Получаем информацию о департаментной работе по идентификатору
                DepartmentWork departmentWork = await db.DepartmentWorks.FirstOrDefaultAsync(x => x.Id == id);
                if (departmentWork == null)
                {
                    return NotFound();
                }

                // Генерируем файл Excel на основе данных о департаментной работе
                byte[] fileContent = _departmentWorkRepository.GenerateExcelFile(departmentWork);

                // Возвращаем файл Excel в ответе
                return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DepartmentWork.xlsx");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<DepartmentWork>> Post(DepartmentWork dw)
        {
            if (dw == null)
            {
                return BadRequest("DepartmentWork cannot be null.");
            }

            // Добавление новой записи в базу данных
            db.DepartmentWorks.Add(dw);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = dw.Id }, dw);
        }
      /*  public class DepartmentWorkUpdateModel
        {
            public DepartmentWork DepartmentWork { get; set; }
            public int DisciplineID { get; set; }
            public int StudentGroupID { get; set; }

        }*/
        

      /*  [HttpPut]
        public async Task<ActionResult<DepartmentWork>> Put([FromBody] DepartmentWorkUpdateModel updateModel)
        {
            if (updateModel.DepartmentWork == null || updateModel.DepartmentWork.Id == 0)
            {
                return BadRequest("DepartmentWork ID is required for update.");
            }

            if (!db.DepartmentWorks.Any(x => x.Id == updateModel.DepartmentWork.Id))
            {
                return NotFound("DepartmentWork not found.");
            }


            StudentGroup group = await _groupRepository.GetGroupById(updateModel.StudentGroupID);
            if (group == null)
            {
                return new NotFoundResult();
            }
            Discipline disciplines = await _disciplineRepository.GetDisciplineById(updateModel.DisciplineID);

            // Пересчет параметров
            var updatedDepartmentWork = _departmentWorkProcessService.CalculateParams(updateModel.DepartmentWork,  group, disciplines);

            // Обновление существующей записи в базе данных
            db.Entry(updatedDepartmentWork).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok(updatedDepartmentWork);
        }*/

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