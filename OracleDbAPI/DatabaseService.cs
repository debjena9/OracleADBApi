using Oracle.ManagedDataAccess.Client;

namespace OracleDbAPI
{
    // User model
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    // Database service to handle database operations
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
           
            using (var connection = new OracleConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT NAME, AGE FROM users";

                var users = new List<User>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        users.Add(new User
                        {
                            Name = reader.GetString(0),
                            Age = reader.GetInt32(1)
                        });
                    }
                }

                return users;
            }
        }

        public async Task CreateUser(User user)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO users (NAME, AGE) VALUES (:name, :age)";
                command.Parameters.Add(new OracleParameter("name", user.Name));
                command.Parameters.Add(new OracleParameter("age", user.Age));

                await command.ExecuteNonQueryAsync();
            }
        }
    }

}
