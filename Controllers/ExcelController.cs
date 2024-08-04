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

        public FileResult ExportExcel ()
        {
            var students = _context.Students.ToList();

            var fileName = "Students.xlsx";

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Students");

                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "Name";
                worksheet.Cell(1, 3).Value = "Last Name";
                worksheet.Cell(1, 4).Value = "Email";
                worksheet.Cell(1, 5).Value = "Phone";

                for (int i = 0; i < students.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = students[i].Id;
                    worksheet.Cell(i + 2, 2).Value = students[i].Name;
                    worksheet.Cell(i + 2, 3).Value = students[i].LastName;
                    worksheet.Cell(i + 2, 4).Value = students[i].Email;
                    worksheet.Cell(i + 2, 5).Value = students[i].Phone;
                }

                // Table Border
                var range = worksheet.Range(1, 1, students.Count + 1, 5);

                range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                range.Style.Border.OutsideBorderColor = XLColor.Black;
                range.Style.Border.InsideBorderColor = XLColor.Black;

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileName}");
                }
            }
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