using BC = BCrypt.Net.BCrypt;
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

            return View();
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

        [HttpPost]
        public IActionResult Login(UserFormLogging logging) {


            try {

                if( BC.Verify( logging.Password, _userService.GetPassword( logging ) ) ) {

                    TempData["Logged"] = true;
                    return RedirectToAction( "Index" );
                }
                else
                    logging.ErrorMessage = "Identifiant incorrect";
            }
            catch(Exception ex) {

                logging.ErrorMessage = "Identifiant introuvable";
            }
            
            return View(logging);

        }
    }
}
