using Infra.Context;
using Infra.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace Site.Controllers
{
    [Authorize]
    [Route("/admin/MiniCarrossels/{action=Index}")]
    public class MiniCarrouselController : Controller
    {
        public Context _context;
        private IWebHostEnvironment _hostEnvironment;

        public MiniCarrouselController(Context context, IWebHostEnvironment environment)
        {
            _context = context;
            _hostEnvironment = environment;
        }

        public IActionResult Index()
        {
            return View(_context.MiniCarrossel.AsNoTracking().OrderBy(x => x.Ordenacao).ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Edit = false;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public IActionResult Create(MiniCarrossel MiniCarrossel, IFormFile? imagem)
        {
            ViewBag.Edit = false;

            MiniCarrossel.Ordenacao = _context.MiniCarrossel.AsNoTracking().Count();

            if (imagem != null)
            {
                var path = Directory.GetParent(_hostEnvironment.ContentRootPath).Parent.FullName;

                path = Path.Combine(path, "imagens");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var filename = MiniCarrossel.MiniCarrosselId.ToString() + "." + imagem.ContentType.Split("/")[1].ToString();

                path = Path.Combine(path, filename);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    imagem.CopyTo(stream);
                }

                MiniCarrossel.Caminho = filename;
            }

            _context.MiniCarrossel.Add(MiniCarrossel);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            ViewBag.Edit = true;

            return View(nameof(Create), _context.MiniCarrossel.First(x => x.MiniCarrosselId == id));
        }

        [DisableRequestSizeLimit]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(MiniCarrossel MiniCarrossel, IFormFile? imagem, bool imagemalterada)
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

                    var filename = MiniCarrossel.MiniCarrosselId.ToString() + "." + imagem.ContentType.Split("/")[1].ToString();

                    path = Path.Combine(path, filename);

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        imagem.CopyTo(stream);
                    }

                    MiniCarrossel.Caminho = filename;
                }
            }

            _context.MiniCarrossel.Update(MiniCarrossel);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public JsonResult remover(Guid id)
        {
            var MiniCarrossel = _context.MiniCarrossel.First(x => x.MiniCarrosselId == id);

            _context.MiniCarrossel.Remove(MiniCarrossel);

            _context.SaveChanges();

            if (!string.IsNullOrEmpty(MiniCarrossel.Caminho))
            {
                var path = Directory.GetParent(_hostEnvironment.ContentRootPath).Parent.FullName;

                path = Path.Combine(path, "imagens");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                path = Path.Combine(path, MiniCarrossel.Caminho);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            return new JsonResult(new { });
        }

        public IActionResult SalvarOrdenacao(List<MiniCarrossel> MiniCarrossels)
        {
            foreach (var p in MiniCarrossels)
            {
                var MiniCarrossel = _context.MiniCarrossel.AsNoTracking().First(x => x.MiniCarrosselId == p.MiniCarrosselId);

                MiniCarrossel.Ordenacao = MiniCarrossels.IndexOf(p);

                _context.MiniCarrossel.Update(MiniCarrossel);
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
