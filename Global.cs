using Microsoft.Data.Sqlite;


static class Global
{
    public static SqliteConnection connection = new SqliteConnection("Data Source=production.db");
}
