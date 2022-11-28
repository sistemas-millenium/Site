using Infra.Context;
using Infra.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace Site.Controllers
{
    [Authorize]
    [Route("/admin/Projetos/{action=Index}")]
    public class ProjetosController : Controller
    {
        public Context _context;
        private IWebHostEnvironment _hostEnvironment;

        public ProjetosController(Context context, IWebHostEnvironment environment)
        {
            _context = context;
            _hostEnvironment = environment;
        }

        public IActionResult Index()
        {
            ViewBag.Titulo = _context.TituloProjeto.FirstOrDefault()?.Nome;

            return View(_context.Projeto.AsNoTracking().OrderBy(x => x.Ordenacao).ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Edit = false;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public IActionResult Create(Projeto Projeto, IFormFile? imagem)
        {
            ViewBag.Edit = false;

            Projeto.Ordenacao = _context.Projeto.AsNoTracking().Count();

            if (imagem != null)
            {
                var path = Directory.GetParent(_hostEnvironment.ContentRootPath).Parent.FullName;

                path = Path.Combine(path, "imagens");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var filename = Projeto.ProjetoId.ToString() + "." + imagem.ContentType.Split("/")[1].ToString();

                path = Path.Combine(path, filename);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    imagem.CopyTo(stream);
                }

                Projeto.Caminho = filename;
            }

            _context.Projeto.Add(Projeto);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            ViewBag.Edit = true;

            return View(nameof(Create), _context.Projeto.First(x => x.ProjetoId == id));
        }

        [DisableRequestSizeLimit]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Projeto Projeto, IFormFile? imagem, bool imagemalterada)
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

                    var filename = Projeto.ProjetoId.ToString() + "." + imagem.ContentType.Split("/")[1].ToString();

                    path = Path.Combine(path, filename);

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        imagem.CopyTo(stream);
                    }

                    Projeto.Caminho = filename;
                }
            }

            _context.Projeto.Update(Projeto);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public JsonResult remover(Guid id)
        {
            var Projeto = _context.Projeto.First(x => x.ProjetoId == id);

            _context.Projeto.Remove(Projeto);

            _context.SaveChanges();

            var path = Directory.GetParent(_hostEnvironment.ContentRootPath).Parent.FullName;
            path = Path.Combine(path, "imagens");

            if (!string.IsNullOrEmpty(Projeto.Caminho))
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (System.IO.File.Exists(Path.Combine(path, Projeto.Caminho)))
                {
                    System.IO.File.Delete(Path.Combine(path, Projeto.Caminho));
                }
            }

            var pathimg = Path.Combine(path, "carrossel-" + Projeto.ProjetoId.ToString());

            Directory.Delete(pathimg, true);

            return new JsonResult(new { });
        }

        public IActionResult SalvarOrdenacao(List<Projeto> Projetos)
        {
            foreach (var p in Projetos)
            {
                var Projeto = _context.Projeto.AsNoTracking().First(x => x.ProjetoId == p.ProjetoId);

                Projeto.Ordenacao = Projetos.IndexOf(p);

                _context.Projeto.Update(Projeto);
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult SalvarTitulo(string nome)
        {
            var titulo = _context.TituloProjeto.FirstOrDefault();

            if (titulo != null)
            {
                titulo.Nome = nome;

                _context.TituloProjeto.Update(titulo);
            }
            else
            {
                var titulonovo = new TituloProjeto()
                {
                    TituloProjetoId = Guid.NewGuid(),
                    Nome = nome
                };

                _context.TituloProjeto.Add(titulonovo);
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        //public JsonResult ObterImagens(Guid projetoid)
        //{

        //}
    }
}
