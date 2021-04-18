namespace DatabaseApp.Database
{
    public abstract class CoreSql
    {
        protected string server;
        protected string user;
        protected string database;
        protected string port;
        protected string password;

        public CoreSql() {
            this.user = null;
            this.server = null;
            this.database = null;
            this.port = null;
            this.password = null;
        }

        public CoreSql(string server, string user, string password, string database, string port)
        {
            this.user = user;
            this.server = server;
            this.password = password;
            this.database = database;
            this.port = port;
        }
    }
}
