using Infra.Context;
using Infra.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Site.Extensions;

namespace Site.Controllers
{
    [Route("{action=Index}")]
    public class HomeController : Controller
    {
        public Context _context;
        private IWebHostEnvironment _hostEnvironment;

        public HomeController(Context context, IWebHostEnvironment environment)
        {
            _context = context;
            _hostEnvironment = environment;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None)]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<FileResult> ObterImagem(string filename)
        {
            var path = Directory.GetParent(_hostEnvironment.ContentRootPath).Parent.FullName;

            path = Path.Combine(path, "imagens", filename);

            var provider = new FileExtensionContentTypeProvider();

            string contentType;
            if (!provider.TryGetContentType(filename, out contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(path);

            return File(bytes, contentType);
        }

        public JsonResult ObterImagensProjeto(Guid projetoid)
        {
            List<ImagemProjeto> imagens = _context.ImagemProjeto.Where(x => x.ProjetoId == projetoid).ToList();

            return Json(new { html = ControllerExtensions.RenderViewAsync(this, "_imagensprojeto", imagens, true).Result });
        }     
    }
}