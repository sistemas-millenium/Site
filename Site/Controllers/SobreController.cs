using Infra.Context;
using Infra.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace Site.Controllers
{
    [Authorize]
    [Route("/admin/SobreNos/{action=Index}")]
    public class SobreController : Controller
    {
        public Context _context;
        private IWebHostEnvironment _hostEnvironment;

        public SobreController(Context context, IWebHostEnvironment environment)
        {
            _context = context;
            _hostEnvironment = environment;
        }

        public IActionResult Index()
        {
            return View(_context.SobreNos.AsNoTracking().ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Edit = false;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public IActionResult Create(SobreNos SobreNos, IFormFile? imagem)
        {
            ViewBag.Edit = false;

            if (imagem != null)
            {
                var path = Directory.GetParent(_hostEnvironment.ContentRootPath).Parent.FullName;

                path = Path.Combine(path, "imagens");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var filename = SobreNos.SobreNosId.ToString() + "." + imagem.ContentType.Split("/")[1].ToString();

                path = Path.Combine(path, filename);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                using (var ms = new MemoryStream())
                {
                    imagem.CopyTo(ms);

                    var fileBytes = ms.ToArray();

                    System.IO.File.WriteAllBytes(path, fileBytes);
                }

                SobreNos.Caminho = filename;
            }

            _context.SobreNos.Add(SobreNos);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            ViewBag.Edit = true;

            return View(nameof(Create), _context.SobreNos.First(x => x.SobreNosId == id));
        }

        [DisableRequestSizeLimit]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(SobreNos SobreNos, IFormFile? imagem, bool imagemalterada)
        {
            if (imagemalterada)
            {
                if (imagem != null)
                {
                    var path = Directory.GetParent(_hostEnvironment.ContentRootPath).Parent.FullName;

                    path = Path.Combine(path, "imagens");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var filename = SobreNos.SobreNosId.ToString() + "." + imagem.ContentType.Split("/")[1].ToString();

                    path = Path.Combine(path, filename);

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    using (var ms = new MemoryStream())
                    {
                        imagem.CopyTo(ms);

                        var fileBytes = ms.ToArray();

                        System.IO.File.WriteAllBytes(path, fileBytes);
                    }

                    SobreNos.Caminho = filename;
                }
            }

            _context.SobreNos.Update(SobreNos);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public JsonResult remover(Guid id)
        {
            var SobreNos = _context.SobreNos.First(x => x.SobreNosId == id);

            _context.SobreNos.Remove(SobreNos);

            _context.SaveChanges();

            if (!string.IsNullOrEmpty(SobreNos.Caminho))
            {
                var path = Directory.GetParent(_hostEnvironment.ContentRootPath).Parent.FullName;

                path = Path.Combine(path, "imagens");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                path = Path.Combine(path, SobreNos.Caminho);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            return new JsonResult(new { });
        }
    }
}
