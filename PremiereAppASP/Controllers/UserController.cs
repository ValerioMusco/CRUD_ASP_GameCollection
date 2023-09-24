using Microsoft.AspNetCore.Mvc;
using PremiereAppASP.Models.Mappers;
using PremiereAppASP.Services;

namespace PremiereAppASP.Controllers {
    public class UserController : Controller {

        private readonly IUserService _userService;

        public UserController(IUserService userService){
            _userService = userService;
        }

        public IActionResult Index() {

            bool created = TempData["UserCreated"] as bool? ?? false;
            return View(created);
        }

        public IActionResult Register() {
            return View();
        }

        [HttpPost]
        public IActionResult Register( UserFormRegister newUser ) {

            TempData["UserCreated"] = _userService.Create( newUser );
            return RedirectToAction( "Index" );
        }

        public IActionResult Login() {
            return View();
        }
    }
}
