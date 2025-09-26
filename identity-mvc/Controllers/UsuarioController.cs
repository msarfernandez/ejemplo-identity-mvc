using identity_mvc.Data;
using identity_mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace identity_mvc.Controllers
{
    [Authorize(Roles = Roles.GerenteVendedor)]
    public class UsuarioController : Controller
    {
        private readonly EjemploDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UsuarioController(EjemploDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        public async Task<IActionResult> Index()
        {
            var adminId = await _context.Roles.Where(r => r.Name == "Admin")
                .Select(r => r.Id)
                .SingleOrDefaultAsync();

            var usuarios = await _context.Users
                .Select(u => new UsuarioVM
                {
                    Id = u.Id,
                    Email = u.Email,
                    EsAdmin = _context.UserRoles.Any(ur => ur.RoleId == adminId && ur.UserId == u.Id)
                })
                .ToListAsync();

            //User >> Roles
            //Roles

            return View(usuarios);
        }
        public async Task<IActionResult>PermisoAdmin(UsuarioVM vm)
        {
            var usuario = _context.Users.Where(u => u.Id == vm.Id).SingleOrDefault();
            if(usuario == null)
                return NotFound();

            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

            if (vm.EsAdmin)
                await _userManager.RemoveFromRoleAsync(usuario, "Admin");
            else
                await _userManager.AddToRoleAsync(usuario, "Admin");


            return RedirectToAction("Index");
        }
    }
}
