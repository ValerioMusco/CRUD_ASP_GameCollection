using Microsoft.AspNetCore.Mvc;
using PremiereAppASP.Models;
using PremiereAppASP.Services;
using System.Diagnostics;

public class GameController : Controller {

    //private readonly ILogger<GameController> _logger;

    private readonly GameDbService _gameDbService;

    public GameController( GameDbService gameDbService ) {
        _gameDbService = gameDbService;
    }


    //public GameController( ILogger<GameController> logger ) {
    //    _logger = logger;
    //}

    public IActionResult Index() {
        return View( _gameDbService.GetGames() );
    }

    public IActionResult Details( int id ) {
        return View( _gameDbService.GetById( id ) );
    }

    public IActionResult Create() {
        return View();
    }

    [HttpPost]
    public IActionResult Create( Games g ) {

        if( g != null ) {

            _gameDbService.CreateGame(g);
        }
        return RedirectToAction( "Index" );
    }

    public IActionResult Delete( int? id ) {

        if( id != null ) {

            _gameDbService.DeleteGame( (int)id );
        }
        return RedirectToAction( "Index" );
    }

    public IActionResult Edit( int id ) {

        return View( _gameDbService.GetById( id ) );
    }

    [HttpPost]
    public IActionResult Edit( Games g ) {

        if( g != null ) {

            _gameDbService.UpdateGame( g );
        }

        return RedirectToAction( "Index" );
    }

    [ResponseCache( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true )]
    public IActionResult Error() {
        return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
    }
}