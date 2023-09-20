using PremiereAppASP.Models;

namespace PremiereAppASP.Services {
    public interface IGameDbService {
        void CreateGame( Games game );
        void DeleteGame( int id );
        Games GetById( int id );
        List<Games> GetGames();
        void UpdateGame( Games newGame );
    }
}