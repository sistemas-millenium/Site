using Infra.Context;
using Infra.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace Site.Controllers
{
    [Authorize]
    [Route("/admin/Estatisticas/{action=Index}")]
    public class DadosController : Controller
    {
        public Context _context;
        private IWebHostEnvironment _hostEnvironment;

        public DadosController(Context context, IWebHostEnvironment environment)
        {
            _context = context;
            _hostEnvironment = environment;
        }

        public IActionResult Index()
        {
            return View(_context.Dado.AsNoTracking().OrderBy(x => x.Ordenacao).ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Edit = false;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public IActionResult Create(Dado Dado, IFormFile? imagem)
        {
            ViewBag.Edit = false;

            Dado.Ordenacao = _context.Dado.AsNoTracking().Count();

            if (imagem != null)
            {
                var path = Directory.GetParent(_hostEnvironment.ContentRootPath).Parent.FullName;

                path = Path.Combine(path, "imagens");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var filename = Dado.DadoId.ToString() + "." + imagem.ContentType.Split("/")[1].ToString();

                path = Path.Combine(path, filename);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    imagem.CopyTo(stream);
                }

                Dado.Caminho = filename;
            }

            _context.Dado.Add(Dado);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            ViewBag.Edit = true;

            return View(nameof(Create), _context.Dado.First(x => x.DadoId == id));
        }

        [DisableRequestSizeLimit]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Dado Dado, IFormFile? imagem, bool imagemalterada)
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

                    var filename = Dado.DadoId.ToString() + "." + imagem.ContentType.Split("/")[1].ToString();

                    path = Path.Combine(path, filename);

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        imagem.CopyTo(stream);
                    }

                    Dado.Caminho = filename;
                }
            }

            _context.Dado.Update(Dado);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public JsonResult remover(Guid id)
        {
            var Dado = _context.Dado.First(x => x.DadoId == id);

            _context.Dado.Remove(Dado);

            _context.SaveChanges();

            if (!string.IsNullOrEmpty(Dado.Caminho))
            {
                var path = Directory.GetParent(_hostEnvironment.ContentRootPath).Parent.FullName;

                path = Path.Combine(path, "imagens");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                path = Path.Combine(path, Dado.Caminho);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            return new JsonResult(new { });
        }

        public IActionResult SalvarOrdenacao(List<Dado> Dados)
        {
            foreach (var p in Dados)
            {
                var Dado = _context.Dado.AsNoTracking().First(x => x.DadoId == p.DadoId);

                Dado.Ordenacao = Dados.IndexOf(p);

                _context.Dado.Update(Dado);
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
