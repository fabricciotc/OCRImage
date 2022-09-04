using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IronOcr;
using Aspose.OCR;

namespace Casero.Controllers
{
    public class ImagenController : Controller
    {
        private IWebHostEnvironment _hostingEnvironment;

        public ImagenController(IWebHostEnvironment hostEnvironment)
        {
            _hostingEnvironment = hostEnvironment;
        }
        // GET: HomeController
        public ActionResult Index(string? message, string file)
        {
            ViewBag.Message = message;
            ViewBag.File = file.Substring(file.IndexOf(@"\upload")); 
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(IFormCollection collection)
        {
            var file = collection.Files.First();
            string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "upload");
                if (file.Length > 0)
                {
                    string filePath = Path.Combine(uploads, file.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    var Ocr = new IronTesseract();
                    Ocr.Language = OcrLanguage.Spanish;
                    using (var Input = new OcrInput(filePath))
                    {
                        // Input.Deskew();  // use if image not straight
                        // Input.DeNoise(); // use if image contains digital noise
                        var Result = Ocr.Read(Input);
                        var text = Result.Text;
                        return RedirectToAction(nameof(Index), new { message = text,file= filePath });

                }

            }
            try
                {
                return RedirectToAction(nameof(Index));
            }
            catch
                {
                    return View();
                }
        }

        // GET: HomeController/Edit/5
   
    }
}
