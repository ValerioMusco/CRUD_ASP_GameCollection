using PremiereAppASP.Models;
using System.Data;

namespace PremiereAppASP.Services
{
    public class GameService
    {

        private List<Games> Games { get; set; }

        public GameService()
        {
            Games = new(){
                new( "lol", "un jeu de merde", "moba", DateTime.Parse("2001-1-1"), 0),
                new( "cod", "un deuxième jeu de merde", "fps" ,DateTime.Parse("2002-2-2"), 1),
                new( "Tarkov", "Best game", "fps",DateTime.Parse("2003-3-3"), 2),
                new( "Smite", "Inconnu", "moba",DateTime.Parse("2004-4-4"), 3),
                new( "WoW", "un jeu Cool", "rpg",DateTime.Parse("2005-5-5"), 4)
            };
        }

        public List<Games> GetGames()
        {
            return Games;
        }

        public Games GetById(int id)
        {
            return Games.FirstOrDefault(g => g.Id == id) ?? new();
        }

        public void CreateGame(int count, Games g)
        {

            Games.Add(new Games(
                g.Name,
                g.Description ?? "",
                g.Genre,
                g.ReleaseDate,
                count
            ));
        }

        public void DeleteGame(int id)
        {

            Games.Remove(GetById(id));
        }

        public void UpdateGame(Games newGame)
        {

            Games gameToUpdate = Games.FirstOrDefault( g => g.Id == newGame.Id ) ?? new();
            gameToUpdate.Name = newGame.Name;
            gameToUpdate.Description = newGame.Description;
            gameToUpdate.Genre = newGame.Genre;
            gameToUpdate.ReleaseDate = newGame.ReleaseDate;
        }
    }
}
