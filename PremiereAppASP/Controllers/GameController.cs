using Microsoft.AspNetCore.Mvc;
using PremiereAppASP.Models;
using PremiereAppASP.Models.Services;
using System.Diagnostics;

public class GameController : Controller {

    //private readonly ILogger<GameController> _logger;

    private GameService _gameService;

    public GameController( GameService gameService ) {
        _gameService = gameService;
    }


    //public GameController( ILogger<GameController> logger ) {
    //    _logger = logger;
    //}

    public IActionResult Index() {
        return View( _gameService.GetGames() );
    }

    public IActionResult Details( int id ) {
        return View( _gameService.GetById( id ) );
    }

    public IActionResult Create() {
        return View( new Games() );
    }

    [HttpPost]
    public IActionResult Create( Games g ) {

        if( g != null ) {

            _gameService.CreateGame( _gameService.GetGames().Max( g => g.Id ) + 1, g );
        }
        return RedirectToAction( "Index" );
    }

    public IActionResult Delete( int? id ) {

        if( id != null ) {

            _gameService.DeleteGame( (int)id );
        }
        return RedirectToAction( "Index" );
    }

    public IActionResult Edit() {

        return View( new Games() );
    }

    [HttpPost]
    public IActionResult Edit( Games g ) {

        if( g != null ) {

            _gameService.UpdateGame( g );
        }

        return RedirectToAction( "Index" );
    }

    [ResponseCache( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true )]
    public IActionResult Error() {
        return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
    }
}