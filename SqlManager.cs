using System;
using System.Text.Json;
using Microsoft.Data.Sqlite;
namespace finalSzczygielski
{
    public class SqlManager
    {
        //Responsible for handling stuff with sql
        private readonly string connectionString = "Data Source=questions.db";

        public void InitializeDatabase()
        {
            using var conn = new SqliteConnection(connectionString); //connection
            conn.Open(); //open the connection

            string createTableQuery = @"
            CREATE TABLE IF NOT EXISTS Questions (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                text TEXT NOT NULL,
                correct_answer TEXT NOT NULL,
                topic TEXT,
                difficulty INTEGER,
                available BOOLEAN DEFAULT 1
            );";

            //This will execute only if the table does not yet exist
            using var cmd = new SqliteCommand(createTableQuery, conn);
            cmd.ExecuteNonQuery(); //Run, dont return results
        }

        public Question GetRandomQuestion()
        {
            using var conn = new SqliteConnection(connectionString);
            conn.Open();

            string query = @"
            SELECT id, text, correct_answer, topic, difficulty, available
            FROM Questions
            WHERE available = 1
            ORDER BY RANDOM()
            LIMIT 1;
            ";

            using var cmd = new SqliteCommand(query, conn);
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                int id = reader.GetInt32(0);
                string text = reader.GetString(1);
                string correctAnswer = reader.GetString(2);
                string topic = reader.IsDBNull(3) ? null : reader.GetString(3);
                int difficulty = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                bool available = reader.GetBoolean(5);

                return new Question(id, text, correctAnswer, topic, difficulty, available);
            }

            throw new Exception("Out of questions"); // No available question found
        }

        public void LoadQuestionsFromJson(string filePath)
        {
            string json = File.ReadAllText(filePath);
            var questions = JsonSerializer.Deserialize<List<Question>>(json);

            using var conn = new SqliteConnection(connectionString);
            conn.Open();

            foreach (var q in questions)
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                INSERT OR IGNORE INTO Questions (id, text, correct_answer, topic, difficulty, available)
                VALUES ($id, $text, $correctAnswer, $topic, $difficulty, $available);
                ";

                cmd.Parameters.AddWithValue("$id", q.id);
                cmd.Parameters.AddWithValue("$text", q.text);
                cmd.Parameters.AddWithValue("$correctAnswer", q.correctAnswer);
                cmd.Parameters.AddWithValue("$topic", q.topic ?? "");
                cmd.Parameters.AddWithValue("$difficulty", q.difficulty);
                cmd.Parameters.AddWithValue("$available", q.available);

                cmd.ExecuteNonQuery();
            }
        }

        public void SetAvailabilityFlag(Question q, bool isAvailable)
        {
            using var conn = new SqliteConnection(connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
            UPDATE Questions
            SET available = $available
            WHERE id = $id;
            ";

            cmd.Parameters.AddWithValue("$available", isAvailable ? 1 : 0); // SQLite uses 0/1 for booleans
            cmd.Parameters.AddWithValue("$id", q.id);

            cmd.ExecuteNonQuery();
        }

        public void SetAllQuestionsAvailable()
        {
            using var conn = new SqliteConnection(connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE Questions SET available = 1;";

            cmd.ExecuteNonQuery();
        }
    }
}

