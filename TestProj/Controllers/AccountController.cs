using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Security.Claims;
using Microsoft.Owin.Security;
using TestProj.ViewModels;
using TestProj.Models;
using System.Threading.Tasks;
using TestProj.DAL;

namespace IdentityUoWApp.Controllers
{
    public class AccountController : Controller
    {
        EFContext db = new EFContext();

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await db.ApplicationUsers.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                
                //User user = await db.Users.FirstOrDefaultAsync(u => u.Login == model.Email && u.Password == model.Password);
                //string role = user is AdminUser ? "Admin" : user is EndUser ? "EndUser" : null;

                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    var claim = Claiming(user);

                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Companies");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Companies");
        }

        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { Email = model.Email, Password = model.Password, RoleId = 1 }; //possible problems with Role
                var result = await Task.Run(() => db.ApplicationUsers.Add(user));
                var claim = Claiming(user);
                AuthenticationManager.SignOut();
                AuthenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = false
                }, claim);
                return RedirectToAction("Index", "Companies");
            }

            return View(model);
        }

        ClaimsIdentity Claiming(ApplicationUser user)
        {
            ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String));
            claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email, ClaimValueTypes.String));
            claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "OWIN Provider", ClaimValueTypes.String));
            if (user.Role != null)
                claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name, ClaimValueTypes.String));
            return claim;
        }
    }
}