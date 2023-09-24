using PremiereAppASP.Models;

namespace PremiereAppASP.Services {
    public interface IGameDbService : IGenericService<Games, int> {

        public bool Create( Games game );

        public bool Update( Games newGame );
    }
}