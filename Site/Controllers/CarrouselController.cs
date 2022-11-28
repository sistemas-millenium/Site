using Infra.Context;
using Infra.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace Site.Controllers
{
    [Authorize]
    [Route("/admin/carrossels/{action=Index}")]
    public class CarrouselController : Controller
    {
        public Context _context;
        private IWebHostEnvironment _hostEnvironment;

        public CarrouselController(Context context, IWebHostEnvironment environment)
        {
            _context = context;
            _hostEnvironment = environment;
        }
             
        public IActionResult Index()
        {
            return View(_context.Carrossel.AsNoTracking().OrderBy(x => x.Ordenacao).ToList());
        }
       
        public IActionResult Create()
        {
            ViewBag.Edit = false;

            return View();
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public IActionResult Create(Carrossel carrossel, IFormFile? imagem)
        {
            ViewBag.Edit = false;

            if (carrossel.Link == null)
                carrossel.Link = "";

            carrossel.Ordenacao = _context.Carrossel.AsNoTracking().Count();

            if (imagem != null)
            {
                var path = Directory.GetParent(_hostEnvironment.ContentRootPath).Parent.FullName;

                path = Path.Combine(path, "imagens");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var filename = carrossel.CarrosselId.ToString() + "." + imagem.ContentType.Split("/")[1].ToString();

                path = Path.Combine(path, filename);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    imagem.CopyTo(stream);
                }
                               
                carrossel.Caminho = filename;
            }

            _context.Carrossel.Add(carrossel);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
                
        public IActionResult Edit(Guid id)
        {
            ViewBag.Edit = true;

            return View(nameof(Create), _context.Carrossel.First(x => x.CarrosselId == id));
        }

        [DisableRequestSizeLimit]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Carrossel carrossel, IFormFile? imagem, bool imagemalterada)
        {
            if (carrossel.Link == null)
                carrossel.Link = "";

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

                    var filename = carrossel.CarrosselId.ToString() + "." + imagem.ContentType.Split("/")[1].ToString();

                    path = Path.Combine(path, filename);

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        imagem.CopyTo(stream);
                    }

                    carrossel.Caminho = filename;
                }
            }

            _context.Carrossel.Update(carrossel);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public JsonResult remover(Guid id)
        {
            var carrossel = _context.Carrossel.First(x => x.CarrosselId == id);

            _context.Carrossel.Remove(carrossel);

            _context.SaveChanges();

            if (!string.IsNullOrEmpty(carrossel.Caminho))
            {
                var path = Directory.GetParent(_hostEnvironment.ContentRootPath).Parent.FullName;

                path = Path.Combine(path, "imagens");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                path = Path.Combine(path, carrossel.Caminho);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            return new JsonResult(new { });
        }

        public IActionResult SalvarOrdenacao(List<Carrossel> carrossels)
        {
            foreach (var p in carrossels)
            {
                var carrossel = _context.Carrossel.AsNoTracking().First(x => x.CarrosselId == p.CarrosselId);

                carrossel.Ordenacao = carrossels.IndexOf(p);

                _context.Carrossel.Update(carrossel);
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
