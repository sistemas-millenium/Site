using Infra.Context;
using Infra.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace Site.Controllers
{
    public class ImagensProjetoController : Controller
    {
        private Context _context;
        private IWebHostEnvironment _hostEnvironment;

        public ImagensProjetoController(Context context, IWebHostEnvironment environment)
        {
            _context = context;
            _hostEnvironment = environment;
        }

        public ActionResult Index(Guid projetoid)
        {
            ViewBag.projetoid = projetoid;

            return View(_context.ImagemProjeto.AsNoTracking().Where(x => x.ProjetoId == projetoid).OrderBy(x => x.Ordenacao).ToList());
        }

        // GET: ImagensProjetoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ImagensProjetoController/Create
        public ActionResult Create(Guid projetoid)
        {
            ViewBag.Edit = false;

            return View(new ImagemProjeto() { ProjetoId = projetoid });
        }

        // POST: ImagensProjetoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ImagemProjeto imagemProjeto, IFormFile? imagem)
        {
            ViewBag.Edit = false;

            imagemProjeto.Ordenacao = _context.ImagemProjeto.AsNoTracking().Count();           

            if (imagem != null)
            {
                var path = Directory.GetParent(_hostEnvironment.ContentRootPath).Parent.FullName;

                path = Path.Combine(path, "imagens");

                path = Path.Combine(path, "carrossel-" + imagemProjeto.ProjetoId.ToString());

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var filename = imagemProjeto.ImagemProjetoId.ToString() + "." + imagem.ContentType.Split("/")[1].ToString();

                path = Path.Combine(path, filename);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    imagem.CopyTo(stream);
                }

                imagemProjeto.Caminho = filename;
            }

            _context.ImagemProjeto.Add(imagemProjeto);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index), new { projetoid = imagemProjeto.ProjetoId });
        }

        // GET: ImagensProjetoController/Edit/5
        public ActionResult Edit(Guid id)
        {
            ViewBag.Edit = true;

            return View(nameof(Create), _context.ImagemProjeto.First(x => x.ImagemProjetoId == id));
        }

        // POST: ImagensProjetoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ImagemProjeto imagemProjeto, IFormFile? imagem, bool imagemalterada)
        {
            ViewBag.Edit = true;

            var path = Directory.GetParent(_hostEnvironment.ContentRootPath).Parent.FullName;

            path = Path.Combine(path, "imagens");

            path = Path.Combine(path, "carrossel-" + imagemProjeto.ProjetoId.ToString());

            if (imagemalterada)
            {
                if (imagem != null)
                {
                    path = Path.Combine(path, "imagens");

                    path = Path.Combine(path, "carrossel-" + imagemProjeto.ProjetoId.ToString());

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var filename = imagemProjeto.ImagemProjetoId.ToString() + "." + imagem.ContentType.Split("/")[1].ToString();

                    path = Path.Combine(path, filename);

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        imagem.CopyTo(stream);
                    }

                    imagemProjeto.Caminho = filename;
                }
            }

            _context.ImagemProjeto.Update(imagemProjeto);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index), new { projetoid = imagemProjeto.ProjetoId });
        }

        public IActionResult SalvarOrdenacao(List<ImagemProjeto> imagens, Guid projetoid)
        {
            foreach (var p in imagens)
            {
                var imagem = _context.ImagemProjeto.AsNoTracking().First(x => x.ImagemProjetoId == p.ImagemProjetoId);

                imagem.Ordenacao = imagens.IndexOf(p);

                _context.ImagemProjeto.Update(imagem);
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index), new { projetoid = projetoid });
        }

        public async Task<FileResult> ObterImagem(string filename, Guid projetoid)
        {
            var path = Directory.GetParent(_hostEnvironment.ContentRootPath).Parent.FullName;

            path = Path.Combine(path, "imagens");

            path = Path.Combine(path, "carrossel-" + projetoid.ToString(), filename);

            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(filename, out contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(path);

            return File(bytes, contentType);
        }

        public JsonResult remover(Guid id)
        {
            var imagem = _context.ImagemProjeto.First(x => x.ImagemProjetoId == id);

            _context.ImagemProjeto.Remove(imagem);

            _context.SaveChanges();

            if (!string.IsNullOrEmpty(imagem.Caminho))
            {
                var path = Directory.GetParent(_hostEnvironment.ContentRootPath).Parent.FullName;

                path = Path.Combine(path, "imagens");

                path = Path.Combine(path, "carrossel-" + imagem.ProjetoId.ToString(), imagem.Caminho);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            return new JsonResult(new { });
        }
    }
}
