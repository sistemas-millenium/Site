using Infra.Context;
using Infra.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Site.Controllers
{
    [Authorize]
    [Route("/admin/menus/{action=Index}")]
    public class MenuController : Controller
    {
        public Context _context;
        private IWebHostEnvironment _hostEnvironment;

        public MenuController(Context context, IWebHostEnvironment environment)
        {
            _context = context;
            _hostEnvironment = environment;
        }

        public IActionResult Index()
        {
            return View(_context.Menu.AsNoTracking().OrderBy(x => x.Ordenacao).ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Edit = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Menu menu)
        {
            ViewBag.Edit = false;
            _context.Menu.Add(menu);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            ViewBag.Edit = true;

            return View(nameof(Create), _context.Menu.First(x => x.MenuId == id));
        }


        [DisableRequestSizeLimit]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Menu menu, IFormFile? imagem, bool imagemalterada)
        {
            _context.Menu.Update(menu);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public JsonResult remover(Guid id)
        {
            var menu = _context.Menu.First(x => x.MenuId == id);

            _context.Menu.Remove(menu);

            _context.SaveChanges();

            return new JsonResult(new { });
        }

        public IActionResult SalvarOrdenacao(List<Menu> menus)
        {
            foreach (var p in menus)
            {
                var Menu = _context.Menu.AsNoTracking().First(x => x.MenuId == p.MenuId);

                Menu.Ordenacao = menus.IndexOf(p);

                _context.Menu.Update(Menu);
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
