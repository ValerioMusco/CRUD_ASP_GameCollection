using System.Data;
using System.Data.Common;

namespace PremiereAppASP.Services {
    public abstract class GenericService<T, U> : IGenericService<T, U> where T : class {

        protected readonly IDbConnection _connection;
        private readonly string _tableName;
        private readonly string _tableId;

        public GenericService(IDbConnection connection, string tableName, string tableId ) {
            _connection = connection;
            _tableName = tableName;
            _tableId = tableId;
        }

        protected void GenerateParameter(IDbCommand dbCommand, string parameterName, object? value) {

            IDataParameter parameter = dbCommand.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value ?? DBNull.Value;
            dbCommand.Parameters.Add( parameter );
        }

        protected static void CheckOpenConnection(IDbConnection connection) {

            if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();
        }
        protected abstract T Convert( IDataRecord dataRecord );

        public bool Delete( U Id ) {
            
            using ( IDbCommand dbCommand = _connection.CreateCommand() ) {

                dbCommand.CommandText = $"DELETE FROM {_tableName} WHERE {_tableId} = @id";

                GenerateParameter(dbCommand, "id", Id);

                CheckOpenConnection( _connection );
                _connection.Open();
                return dbCommand.ExecuteNonQuery() == 1;
            }
        }

        public IEnumerable<T> GetAll() {
            
            using ( IDbCommand dbCommand = _connection.CreateCommand() ) {

                dbCommand.CommandText = $"SELECT * FROM {_tableName}";

                CheckOpenConnection(_connection );
                _connection.Open();
                
                IDataReader reader = dbCommand.ExecuteReader();

                while( reader.Read() )
                    yield return Convert( reader );
            }
        }

        public T GetById( U Id ) {
            
            using(IDbCommand dbCommand = _connection.CreateCommand() ) {

                dbCommand.CommandText = $"SELECT * FROM {_tableName} " +
                                        $"WHERE {_tableId} = @id";
                GenerateParameter( dbCommand, "id", Id ); 

                CheckOpenConnection( _connection );
                _connection.Open();

                IDataReader reader = dbCommand.ExecuteReader();

                if( !reader.Read() )
                    throw new Exception( "Error" );

                return Convert(reader);
            }
        }
    }
}
