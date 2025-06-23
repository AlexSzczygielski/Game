using System;
using System.Data.SQLite;
namespace finalSzczygielski
{
    public static class SqlManager
    {
        private static readonly string connectionString = "Data Source=questions.db;Version=3;";

        //public static void InitializeDatabase()
        //{
        //    using var conn = new SQLiteConnection(connectionString);
        //    conn.Open();

        //    string createTableQuery = @"
        //    CREATE TABLE IF NOT EXISTS Questions (
        //        id INTEGER PRIMARY KEY AUTOINCREMENT,
        //        text TEXT NOT NULL,
        //        correct_answer TEXT NOT NULL,
        //        options TEXT,
        //        topic TEXT,
        //        difficulty INTEGER,
        //        available BOOLEAN DEFAULT 1
        //    );";

        //    using var cmd = new SQLiteCommand(createTableQuery, conn);
        //    cmd.ExecuteNonQuery();
        //}
    }
}

