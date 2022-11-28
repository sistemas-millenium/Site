using Infra.Context;
using Infra.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace Site.Controllers
{
    [Authorize]
    [Route("/admin/Perguntas/{action=Index}")]
    public class PerguntasController : Controller
    {
        public Context _context;
        private IWebHostEnvironment _hostEnvironment;

        public PerguntasController(Context context, IWebHostEnvironment environment)
        {
            _context = context;
            _hostEnvironment = environment;
        }

        public IActionResult Index()
        {
            return View(_context.Pergunta.AsNoTracking().OrderBy(x => x.Ordenacao).ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Edit = false;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
        public IActionResult Create(Pergunta Pergunta, IFormFile? imagem)
        {
            ViewBag.Edit = false;

            if (Pergunta.Link == null)
                Pergunta.Link = "";

            if (Pergunta.NomeLink == null)
                Pergunta.NomeLink = "";

            _context.Pergunta.Add(Pergunta);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            ViewBag.Edit = true;

            return View(nameof(Create), _context.Pergunta.First(x => x.PerguntaId == id));
        }

        [DisableRequestSizeLimit]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Pergunta Pergunta, IFormFile? imagem, bool imagemalterada)
        {
            if (Pergunta.Link == null)
                Pergunta.Link = "";

            if (Pergunta.NomeLink == null)
                Pergunta.NomeLink = "";

            _context.Pergunta.Update(Pergunta);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public JsonResult remover(Guid id)
        {
            var Pergunta = _context.Pergunta.First(x => x.PerguntaId == id);

            _context.Pergunta.Remove(Pergunta);

            _context.SaveChanges();


            return new JsonResult(new { });
        }


        public IActionResult SalvarOrdenacao(List<Pergunta> Perguntas)
        {
            foreach (var p in Perguntas)
            {
                var pergnta = _context.Pergunta.AsNoTracking().First(x => x.PerguntaId == p.PerguntaId);

                pergnta.Ordenacao = Perguntas.IndexOf(p);

                _context.Pergunta.Update(pergnta);
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
