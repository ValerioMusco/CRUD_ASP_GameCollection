using BC = BCrypt.Net.BCrypt;
using PremiereAppASP.Enums;
using PremiereAppASP.Models;
using PremiereAppASP.Models.Mappers;
using System.Data;

namespace PremiereAppASP.Services {
    public class UserService : GenericService<User, int>, IUserService {

        public UserService( IDbConnection connection) : base( connection, "Users", "Id") {
        }

        protected override User Convert( IDataRecord dataRecord ) {
            throw new NotImplementedException();
        }

        public bool Create( UserFormRegister userFormRegister ) {

            using( IDbCommand dbCommand = _connection.CreateCommand() ) {

                string hashedPassword = BC.HashPassword(userFormRegister.Password, BC.GenerateSalt() );

                dbCommand.CommandText = $"INSERT INTO Users VALUES " +
                                        $"(@Username, @Password, @Mail, @Privileges)";

                GenerateParameter( dbCommand, "Username", userFormRegister.Username );
                GenerateParameter(dbCommand, "Password", hashedPassword );
                GenerateParameter(dbCommand, "Mail", userFormRegister.Email );
                GenerateParameter(dbCommand, "Privileges", Privileges.Guest.ToString() );

                CheckOpenConnection( _connection );
                _connection.Open();

                try {

                    return dbCommand.ExecuteNonQuery() == 1;
                }
                catch( Exception ex ) {

                    return false;
                }
            }
        }

        public bool Update( UserFormRegister userFormRegister, int id ) {
            throw new NotImplementedException();
        }

        IEnumerable<UserFormRegister> IGenericService<UserFormRegister, int>.GetAll() {
            throw new NotImplementedException();
        }

        UserFormRegister IGenericService<UserFormRegister, int>.GetById( int Id ) {
            throw new NotImplementedException();
        }

        public string GetPassword(UserFormLogging logging) {

            using( IDbCommand dbCommand = _connection.CreateCommand() ) {

                dbCommand.CommandText = $"SELECT Password FROM Users " +
                                        $"WHERE Username = @Username";

                GenerateParameter( dbCommand, "Username", logging.Username ); 
                
                CheckOpenConnection(_connection );
                _connection.Open();

                IDataReader reader = dbCommand.ExecuteReader();

                if( !reader.Read() )
                    throw new Exception( "Erreur" );

                return reader.GetString( 0 );
            }
        }
    }
}