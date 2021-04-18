using MySql.Data.MySqlClient;

namespace DatabaseApp.Database
{
    class SqlConnection : CoreSql
    {

        private MySqlConnection connection;
        private static MySqlConnection instance = null;

        public SqlConnection(string server, string user, string password, string database, string port)
        {
            this.user = user;
            this.server = server;
            this.password = password;
            this.database = database;
            this.port = port;
        }

        public static MySqlConnection Instance()
        {
            if (instance == null)
                instance = new MySqlConnection();
            return instance;
        }

        public void ConnectWithDatabase()
        {
            string connString = string.Format("server={0};user={1};database={2};port={3};password={4}", server, user, database, port, password);
            connection = new MySqlConnection(connString);
        }

        public MySqlConnection GetDatabaseConnection()
        {
            return connection;
        }

        public void OpenConnection()
        {
            connection.Open();
        }

        public void CloseConnection()
        {
            connection.Close();
        }
    }
}

