using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Site.Controllers
{
    [Authorize]
    [Route("/admin/usuarios/{action=Index}")]
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()

        {
            return View();
        }
    }
}
