using Microsoft.AspNetCore.Mvc;

namespace WebApplication1
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UploadPhoto()
        {
            if (Request?.Form?.Files == null)
            {
                return new JsonResult(string.Empty);
            }

            if (!Request.Form.Files.Any())
            {
                return new JsonResult(string.Empty);
            }


            var file = Request.Form.Files[0] as FormFile;

            if (file == null)
            {
                return new JsonResult(string.Empty);
            }

            var directoryPath = System.IO.Path.Combine(_webHostEnvironment.ContentRootPath, "uploads");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (var fs = new FileStream(System.IO.Path.Combine(directoryPath, file.FileName), FileMode.OpenOrCreate))
            {
                 file.CopyTo(fs);
            }


            return new JsonResult(true);
        }
    }
}
