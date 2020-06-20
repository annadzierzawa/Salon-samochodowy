using MySql.Data.MySqlClient;


namespace Salon_samochodowy.DAL
{
    public class DBConnection
    {
        private readonly MySqlConnectionStringBuilder connStringBuilder = new MySqlConnectionStringBuilder();

        private static DBConnection instance = null;
        public static DBConnection Instance => instance ?? (instance = new DBConnection());

        public MySqlConnection Connection => new MySqlConnection(connStringBuilder.ToString());

        private DBConnection()
        {
            connStringBuilder.Server = Properties.Settings.Default.host;
            connStringBuilder.UserID = Properties.Settings.Default.user;
            connStringBuilder.Password = Properties.Settings.Default.password;
            connStringBuilder.Database = Properties.Settings.Default.database;
            connStringBuilder.Port = Properties.Settings.Default.port;
        }
    }
}