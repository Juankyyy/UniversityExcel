using Microsoft.AspNetCore.Mvc;
using University.Models;

namespace University.Controllers
{
    public class ExcelController : Controller
    {
        public FileResult ExportExcel (string FileName, IEnumerable<Student> Students)
        {
            return null;
        }
    }
}