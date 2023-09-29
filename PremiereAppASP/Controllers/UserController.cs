using BC = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Mvc;
using PremiereAppASP.Models.Mappers;
using PremiereAppASP.Services;
using PremiereAppASP.Models;
using Newtonsoft.Json;
using PremiereAppASP.Tools;

namespace PremiereAppASP.Controllers {
    public class UserController : Controller {

        private readonly IUserService _userService;
        private readonly SessionManager _sessionManager;

        public UserController( IUserService userService, SessionManager sessionManager){
            _userService = userService;
            _sessionManager = sessionManager;
        }

        public IActionResult Index() {

            return View();
        }

        public IActionResult Register() {
            return View();
        }

        [HttpPost]
        public IActionResult Register( UserFormRegister newUser ) {

            ViewData["UserCreated"] = _userService.Create( newUser );
            return RedirectToAction( "Index" );
        }

        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserFormLogging logging) {


            try {

                if( BC.Verify( logging.Password, _userService.GetPassword( logging ) ) ) {

                    User connectedUser = _userService.Login(logging.Username);
                    _sessionManager.ConnecterUser = connectedUser;
                    return RedirectToAction( "Index", "Game" );
                }
                else
                    logging.ErrorMessage = "Identifiant incorrect";
            }
            catch(Exception ex) {

                logging.ErrorMessage = "Identifiant introuvable";
            }
            
            return View(logging);

        }

        public IActionResult Logout() {

            _sessionManager.Logout();
            return RedirectToAction( "Index", "Game" );
        }
    }
}
