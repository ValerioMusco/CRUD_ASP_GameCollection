using PremiereAppASP.Models;
using System.Data;
using System.Data.SqlClient;

namespace PremiereAppASP.Services {
    public class GameDbService : GenericService<Games, int>, IGameDbService {

        public GameDbService( IDbConnection dbConnection ) : base( dbConnection, "Games", "Id" ) {
        }

        protected override Games Convert( IDataRecord dataRecord ) {

            return new Games {

                Id = (int)dataRecord["Id"],
                Name = (string)dataRecord["Title"],
                Description = dataRecord["Description"] == DBNull.Value ? null : (string)dataRecord["Description"],
                Genre = (string)dataRecord["Genre"],
                ReleaseDate = dataRecord["ReleaseDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)dataRecord["ReleaseDate"]
            };
        }

        public bool Create( Games game ) {

            using( IDbCommand dbCommand = _connection.CreateCommand() ) {

                dbCommand.CommandText = "INSERT INTO Games VALUES " +
                                        "(@GameName, @GameDescription, @GameGenre, @GameReleaseDate)";

                GenerateParameter( dbCommand, "GameName", game.Name );
                GenerateParameter( dbCommand, "GameDescription", game.Description );
                GenerateParameter( dbCommand, "GameGenre", game.Genre );
                GenerateParameter( dbCommand, "GameReleaseDate", game.ReleaseDate );

                CheckOpenConnection( _connection );
                _connection.Open();
                return dbCommand.ExecuteNonQuery() == 1;
            }
        }

        public bool Update( Games newGame ) {
            
            using( IDbCommand dbCommand = _connection.CreateCommand()) {
                
                dbCommand.CommandText = $"UPDATE Games SET " +
                                        $"Title = @newTitle, " +
                                        $"Description = @newDescription, " +
                                        $"Genre = @newGenre, " +
                                        $"ReleaseDate = @newReleaseDate " +
                                        $"WHERE Id = @gameId";

                GenerateParameter( dbCommand, "newTitle", newGame.Name );
                GenerateParameter( dbCommand, "newDescription", newGame.Description );
                GenerateParameter( dbCommand, "newGenre", newGame.Genre );
                GenerateParameter( dbCommand, "newReleaseDate", newGame.ReleaseDate );
                GenerateParameter( dbCommand, "gameId", newGame.Id );

                CheckOpenConnection( _connection );
                _connection.Open();
                return dbCommand.ExecuteNonQuery() == 1;
            }
        }
    }
}
