using Microsoft.AspNetCore.Mvc;
using iTextSharp.text.pdf;
using University.Models;
using University.Data;
using iTextSharp.text;
using Microsoft.EntityFrameworkCore;


namespace University.Controllers
{
    public class PdfController : Controller
    {
        private readonly UniversityContext _context;
        public PdfController(UniversityContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ExportPDF(int id)
        {
            // CREATE PDF DOCUMENT
            MemoryStream workStream = new MemoryStream();
            Document document = new Document();

            PdfWriter writer = PdfWriter.GetInstance(document, workStream);
            writer.CloseStream = false;

            document.Open();

            // PDF CONTENT
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", "favi.png");

            // Añadir la imagen al documento
            if (System.IO.File.Exists(imagePath))
            {
                Image img = Image.GetInstance(imagePath);
                img.ScaleToFit(20f, 20f);
                img.Alignment = Element.ALIGN_LEFT;
                document.Add(img);
                document.Add(new Paragraph(" "));
            }
            else
            {
                document.Add(new Paragraph("--------------- La imagen no se encontró D: ---------------"));
            }

            StudentList(document);

            document.NewPage();

            StudentDetails(document, id);

            document.Close();
            
            // CONFIG PDF RESULT
            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return File(workStream, "application/pdf", "Documentico.pdf");
        }

        private void StudentList(Document document)
        {
            var students = _context.Students.ToList();

            // TABLE
            PdfPTable table = new PdfPTable(5);
            table.WidthPercentage = 100;
            table.SpacingAfter = 50;

            // TH
            // table.AddCell("ID");
            // table.AddCell("Name");
            // table.AddCell("Last Name");
            // table.AddCell("Email");
            // table.AddCell("Phone");

            // TH con estilos
            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20, Font.NORMAL, BaseColor.BLACK);
            var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, Font.NORMAL, BaseColor.WHITE);

            Paragraph title = new Paragraph("ESTUDIANTES", titleFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 20
            };
            document.Add(title);

            PdfPCell cell = new PdfPCell(new Phrase("ID", boldFont))
            {
                BackgroundColor = BaseColor.BLUE,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5
            };
            table.AddCell(cell);
            
            cell = new PdfPCell(new Phrase("Name", boldFont))
            {
                BackgroundColor = BaseColor.BLUE,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5
            };
            table.AddCell(cell);
            
            cell = new PdfPCell(new Phrase("Last Name", boldFont))
            {
                BackgroundColor = BaseColor.BLUE,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5
            };
            table.AddCell(cell);
            
            cell = new PdfPCell(new Phrase("Email", boldFont))
            {
                BackgroundColor = BaseColor.BLUE,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5
            };
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Phone", boldFont))
            {
                BackgroundColor = BaseColor.BLUE,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5
            };
            table.AddCell(cell);

            // Añadir los datos de los usuarios
            foreach (var student in students)
            {
                table.AddCell(student.Id.ToString());
                table.AddCell(student.Name);
                table.AddCell(student.LastName);
                table.AddCell(student.Email);
                table.AddCell(student.Phone);
            }

            document.Add(table);
        }

        private void StudentDetails(Document document, int id)
        {
            try
            {
                var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20, Font.NORMAL, BaseColor.BLACK);
                var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, Font.NORMAL, BaseColor.WHITE);
                var errorFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, Font.NORMAL, BaseColor.RED);
                
                var student = _context.Students.Find(id);

                if (student == null)
                {
                    document.Add(new Paragraph("ERROR: No se encontró el estudiante"));
                }
                else
                {
                    var inscription = _context.Inscriptions.Include(u => u.University).Include(c => c.Career).Where(i => i.StudentId == student.Id);
                    
                    // CAREERS
                    var careerIds = inscription.Select(i => i.CareerId).Distinct();
                    if (careerIds.Count() == 0)
                    {
                        document.Add(new Paragraph("ERROR: No se encontraron carreras relacionadas al estudiante", errorFont));
                    }

                    var career = _context.Careers.Where(c => careerIds.Contains(c.Id));

                    // SUBJECTS
                    var subjects = _context.Subjects.Include(t => t.Teacher).Include(s => s.Semester).Where(s => careerIds.Contains(s.CareerId));

                    // Info Personal
                    document.Add(new Paragraph("Info Personal", titleFont));

                    document.Add(new Paragraph("ID: " + student.Id));
                    document.Add(new Paragraph("Nombres: " + student.Name));
                    document.Add(new Paragraph("Apellidos: " + student.LastName));
                    document.Add(new Paragraph("Email: " + student.Email));
                    document.Add(new Paragraph("Celular: " + student.Phone));
                    document.Add(new Paragraph(" ", titleFont));

                    // Inscripciones
                    document.Add(new Paragraph("Inscripciones", titleFont));

                    document.Add(new Paragraph("Estado de la inscripción: " + inscription.First().Status));
                    document.Add(new Paragraph("Universidad: " + inscription.First().University.Name));
                    document.Add(new Paragraph("Carrera: " + inscription.First().Career.Name));
                    document.Add(new Paragraph(" ", titleFont));

                    // Carrera
                    document.Add(new Paragraph(career.First().Name, titleFont));

                    // Materias
                    document.Add(new Paragraph("Materias", boldFont));

                    foreach (var subject in subjects)
                    {
                        document.Add(new Paragraph(subject.Name, boldFont));
                        
                        document.Add(new Paragraph("Profesor: " + subject.Teacher.Name));
                        document.Add(new Paragraph($"Semestre: {subject.Semester.Year} - {subject.Semester.SemesterNumber}"));
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}