using PremiereAppASP.Models;
using System.Data;
using System.Data.SqlClient;

namespace PremiereAppASP.Services {
    public class GameDbService {

        private readonly string _connectionString;
        private readonly List<Games> _games;

        public GameDbService() {
            _games = new List<Games>();
            _connectionString = "Data Source=DESKTOP-5858J1U;Initial Catalog=GameDB;Integrated Security=True;";
        }

        public void CreateGame(Games game) {

            using( IDbConnection dbConnection = new SqlConnection( _connectionString ) ) {

                using( IDbCommand dbCommand = dbConnection.CreateCommand() ) {

                    dbCommand.CommandText = "INSERT INTO Games VALUES " +
                                            "(@GameName, @GameDescription, @GameGenre, @GameReleaseDate)";

                    GenerateParameter(dbCommand, "GameName", game.Name);
                    GenerateParameter(dbCommand, "GameDescription", game.Description);
                    GenerateParameter(dbCommand, "GameGenre", game.Genre);
                    GenerateParameter(dbCommand, "GameReleaseDate", game.ReleaseDate);

                    dbConnection.Open();
                    dbCommand.ExecuteNonQuery();
                    dbConnection.Close();
                }
            }
        }

        public Games GetById(int id) {

            using( IDbConnection dbConnection = new SqlConnection( _connectionString ) ) {

                using( IDbCommand dbCommand = dbConnection.CreateCommand() ) {

                    dbCommand.CommandText = "SELECT * FROM Games WHERE Id = @id";

                    GenerateParameter( dbCommand, "id", id );

                    dbConnection.Open();
                    IDataReader reader = dbCommand.ExecuteReader();

                    if( reader.Read() )
                        return Convert( reader );
                    else
                        return new();
                }
            }
        }

        public List<Games> GetGames() {

            using( IDbConnection dbConnection = new SqlConnection(_connectionString)) {

                using( IDbCommand dbCommand = dbConnection.CreateCommand() ) {

                    dbCommand.CommandText = "SELECT * FROM Games";

                    dbConnection.Open();
                    IDataReader reader = dbCommand.ExecuteReader();

                    while ( reader.Read()) {

                        _games.Add( Convert( reader ) );
                    }
                    dbConnection.Close();
                }
            }

            return _games;
        }

        public void UpdateGame(Games newGame ) {

            using( IDbConnection dbConnection = new SqlConnection( _connectionString ) ) {

                using( IDbCommand dbCommand = dbConnection.CreateCommand() ) {

                    dbCommand.CommandText = "UPDATE Games SET " +
                                            "Title = @newName, " +
                                            "Description = @newDescription, " +
                                            "Genre = @newGenre, " +
                                            "ReleaseDate = @newReleaseDate " +
                                            "WHERE Id = @id";

                    GenerateParameter(dbCommand, "newName", newGame.Name );
                    GenerateParameter(dbCommand, "newDescription", newGame.Description );
                    GenerateParameter(dbCommand, "newGenre", newGame.Genre );
                    GenerateParameter(dbCommand, "newReleaseDate", newGame.ReleaseDate );
                    GenerateParameter(dbCommand, "id", newGame.Id );
                    
                    dbConnection.Open();
                    dbCommand.ExecuteNonQuery();
                    dbConnection.Close();
                }
            }
        }

        public void DeleteGame(int id ) {

            using( IDbConnection dbConnection = new SqlConnection( _connectionString ) ) {

                using( IDbCommand dbCommand = dbConnection.CreateCommand() ) {

                    dbCommand.CommandText = "DELETE FROM Games WHERE Id = @id";

                    GenerateParameter(dbCommand, "id", id );
                    dbConnection.Open();
                    dbCommand.ExecuteNonQuery();
                    dbConnection.Close();
                }
            }
        }

        public Games Convert(IDataRecord dataRecord) {

            return new Games {

                Id = (int)dataRecord["Id"],
                Name = (string)dataRecord["Title"],
                Description = dataRecord["Description"] == DBNull.Value ? null : (string)dataRecord["Description"],
                Genre = (string)dataRecord["Genre"],
                ReleaseDate = dataRecord["ReleaseDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)dataRecord["ReleaseDate"]
            };
        }
        public void GenerateParameter( IDbCommand dbCommand, string parameterName, object? value ) {

            IDataParameter parameter = dbCommand.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value ?? DBNull.Value;
            dbCommand.Parameters.Add( parameter );
        }
    }
}
