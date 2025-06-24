using System;
using System.Text.Json;
using Microsoft.Data.Sqlite;

namespace finalSzczygielski
{
    public class SqlTextManager
    {
        private readonly string connectionString = "Data Source=texts.db";

        public void InitializeDatabase()
        {
            using var conn = new SqliteConnection(connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS TextResources (
                key TEXT PRIMARY KEY,
                content TEXT NOT NULL
            );
        ";
            cmd.ExecuteNonQuery();
        }

        public void LoadTextsFromJson(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var texts = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

            using var conn = new SqliteConnection(connectionString);
            conn.Open();

            foreach (var kvp in texts)
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                INSERT OR REPLACE INTO TextResources (key, content)
                VALUES ($key, $content);
            ";
                cmd.Parameters.AddWithValue("$key", kvp.Key);
                cmd.Parameters.AddWithValue("$content", kvp.Value);
                cmd.ExecuteNonQuery();
            }
        }

        public string GetText(string key)
        {
            using var conn = new SqliteConnection(connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT content FROM TextResources WHERE key = $key";
            cmd.Parameters.AddWithValue("$key", key);

            var result = cmd.ExecuteScalar();
            return result?.ToString() ?? $"[Missing Text: {key}]";
        }
    }
}

