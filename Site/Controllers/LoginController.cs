using Infra;
using Infra.Context;
using Infra.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Site.Controllers
{
    [Route("/login/{action=Index}")]
    public class LoginController : Controller
    {
        private Context _context;

        public LoginController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "admin");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(string nome, string senha)
        {
            var usuario = new Usuario();

            usuario = _context.Usuario.AsNoTracking().FirstOrDefault(x => x.Nome.Equals(nome) && x.Senha.Equals(senha) && x.Ativo);

            if (usuario != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString())
                };

                var claimidentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authproperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimidentity), authproperties);

                return RedirectToAction("Index", "admin");
            }

            ViewBag.Error = "Usuário ou senha inválido";

            return View();
        }


        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
