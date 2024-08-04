using Microsoft.AspNetCore.Mvc;
using University.Models;
using University.Data;
using ClosedXML.Excel;

namespace University.Controllers
{
    public class ExcelController : Controller
    {
        private readonly UniversityContext _context;

        public ExcelController (UniversityContext context)
        {
            _context = context;
        }

        public FileResult ExportExcel (string FileName, IEnumerable<Student> Students)
        {
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> ImportExcel (IFormFile excel)
        {
            var workbook = new XLWorkbook(excel.OpenReadStream());

            var sheet = workbook.Worksheet(1);

            var firstRow = sheet.FirstRowUsed().RangeAddress.FirstAddress.RowNumber;
            var lastRow = sheet.LastRowUsed().RangeAddress.FirstAddress.RowNumber;

            var students = new List<Student>();

            for (int i = firstRow + 1; i <= lastRow; i++)
            {
                var row = sheet.Row(i);

                var student = new Student
                {
                    Name = row.Cell(2).Value.ToString(),
                    LastName = row.Cell(3).Value.ToString(),
                    Email = row.Cell(4).Value.ToString(),
                    Phone = row.Cell(5).Value.ToString()
                };

                students.Add(student);
            }

            await _context.AddRangeAsync(students);
            await _context.SaveChangesAsync();


            return RedirectToAction("Db", "Home");
        }
    }
}