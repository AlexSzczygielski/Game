using System;
namespace finalSzczygielski
{
    public class Question
    {
        public int id { get; protected set; }
        public string text { get; protected set; }
        public string correctAnswer { get; protected set; }
        public string topic { get; protected set; }
        public int difficulty { get; protected set; }
        public bool available { get; protected set; }

        public Question(int id, string text, string correctAnswer, string topic, int difficulty, bool available)
        {
            this.id = id;
            this.text = text;
            this.correctAnswer = correctAnswer;
            this.topic = topic;
            this.difficulty = difficulty;
            this.available = available;
        }

        public override string ToString()
        {
            return $"Question {id} from {topic}, {text}";
        }
    }
}

