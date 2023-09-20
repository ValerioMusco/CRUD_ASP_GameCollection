using PremiereAppASP.Models;
using System.Data;
using System.Data.SqlClient;

namespace PremiereAppASP.Services {
    public class GameDbService : IGameDbService {

        private readonly List<Games> _games;
        private readonly IDbConnection _connection;

        //public GameDbService(IConfiguration config) {
        //    _games = new();
        //    _connection = new SqlConnection( config.GetConnectionString( "default" ) );
        //}

        public GameDbService(IDbConnection dbConnection)
        {
            _connection = dbConnection;
            _games = new();
        }

        protected Games Convert( IDataRecord dataRecord ) {

            return new Games {

                Id = (int)dataRecord["Id"],
                Name = (string)dataRecord["Title"],
                Description = dataRecord["Description"] == DBNull.Value ? null : (string)dataRecord["Description"],
                Genre = (string)dataRecord["Genre"],
                ReleaseDate = dataRecord["ReleaseDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)dataRecord["ReleaseDate"]
            };
        }

        protected void GenerateParameter( IDbCommand dbCommand, string parameterName, object? value ) {

            IDataParameter parameter = dbCommand.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value ?? DBNull.Value;
            dbCommand.Parameters.Add( parameter );
        }

        public void CreateGame( Games game ) {

            using( IDbCommand dbCommand = _connection.CreateCommand() ) {

                dbCommand.CommandText = "INSERT INTO Games VALUES " +
                                        "(@GameName, @GameDescription, @GameGenre, @GameReleaseDate)";

                GenerateParameter( dbCommand, "GameName", game.Name );
                GenerateParameter( dbCommand, "GameDescription", game.Description );
                GenerateParameter( dbCommand, "GameGenre", game.Genre );
                GenerateParameter( dbCommand, "GameReleaseDate", game.ReleaseDate );

                _connection.Open();
                dbCommand.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public Games GetById( int id ) {

            using( IDbCommand dbCommand = _connection.CreateCommand() ) {

                dbCommand.CommandText = "SELECT * FROM Games WHERE Id = @id";

                GenerateParameter( dbCommand, "id", id );

                _connection.Open();
                IDataReader reader = dbCommand.ExecuteReader();

                if( reader.Read() )
                    return Convert( reader );
                else
                    return new();
            }

        }

        public List<Games> GetGames() {

            using( IDbCommand dbCommand = _connection.CreateCommand() ) {

                dbCommand.CommandText = "SELECT * FROM Games";

                _connection.Open();
                IDataReader reader = dbCommand.ExecuteReader();

                while( reader.Read() )
                    _games.Add( Convert( reader ) );

                _connection.Close();
            }

            return _games;
        }

        public void UpdateGame( Games newGame ) {

            using( IDbCommand dbCommand = _connection.CreateCommand() ) {

                dbCommand.CommandText = "UPDATE Games SET " +
                                        "Title = @newName, " +
                                        "Description = @newDescription, " +
                                        "Genre = @newGenre, " +
                                        "ReleaseDate = @newReleaseDate " +
                                        "WHERE Id = @id";

                GenerateParameter( dbCommand, "newName", newGame.Name );
                GenerateParameter( dbCommand, "newDescription", newGame.Description );
                GenerateParameter( dbCommand, "newGenre", newGame.Genre );
                GenerateParameter( dbCommand, "newReleaseDate", newGame.ReleaseDate );
                GenerateParameter( dbCommand, "id", newGame.Id );

                _connection.Open();
                dbCommand.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public void DeleteGame( int id ) {

            using( IDbCommand dbCommand = _connection.CreateCommand() ) {

                dbCommand.CommandText = "DELETE FROM Games WHERE Id = @id";

                GenerateParameter( dbCommand, "id", id );
                _connection.Open();
                dbCommand.ExecuteNonQuery();
                _connection.Close();
            }
        }
    }
}
