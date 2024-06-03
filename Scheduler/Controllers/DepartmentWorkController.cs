using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
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
        private byte[] fileContent;

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

            // Добавление новой записи в базу данных
            db.DepartmentWorks.Add(dw);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = dw.Id }, dw);
        }
        [HttpPost("ExportToExcel/{id}")]
        /*  public async Task<IActionResult> ExportToExcel(int id)
          {
              try
              {
                  // Получаем файл Excel с данными по идентификатору
                //  var fileContent = await _departmentWorkProcessService.ExportToExcel(id);

                  // Отправляем файл Excel в ответе
                  return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DepartmentWork.xlsx");
              }
              catch (Exception ex)
              {
                  // В случае ошибки возвращаем код состояния 500 и сообщение об ошибке
                  return StatusCode(500, ex.Message);
              }
          }*/
        [HttpPost("ExportToExcel/{id}")]
        public async Task<IActionResult> ExportToExcel(int id)
        {
            try
            {
                // Создаем пустой файл Excel
                byte[] emptyFile = GenerateEmptyExcelFile();

                // Отправляем пустой файл Excel в ответе
                return File(emptyFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EmptyDepartmentWork.xlsx");
            }
            catch (Exception ex)
            {
                // В случае ошибки возвращаем код состояния 500 и сообщение об ошибке
                return StatusCode(500, ex.Message);
            }
        }

        // Метод для генерации пустого файла Excel
        private byte[] GenerateEmptyExcelFile()
        {
            using (var package = new ExcelPackage())
            {
                // Создаем новый лист в файле Excel
                var worksheet = package.Workbook.Worksheets.Add("DepartmentWork");

                // Сохраняем пустой файл Excel в массив байтов
                return package.GetAsByteArray();
            }
        }
        [HttpPut]
        public async Task<ActionResult<DepartmentWork>> Put(DepartmentWork dw)
        {
            if (dw == null || dw.Id == 0)
            {
                return BadRequest("DepartmentWork ID is required for update.");
            }

            if (!db.DepartmentWorks.Any(x => x.Id == dw.Id))
            {
                return NotFound("DepartmentWork not found.");
            }

            // Обновление существующей записи в базе данных
            db.Entry(dw).State = EntityState.Modified;
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