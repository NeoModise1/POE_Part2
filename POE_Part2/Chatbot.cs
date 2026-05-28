using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Assignment_1_Part1
{
    public class Chatbot
    {
        private Dictionary<string, string> memory = new Dictionary<string, string>();

        public string GetResponse(string question)
        {
            if (string.IsNullOrWhiteSpace(question)) return "Input cannot be empty.";

            // Sentiment detection
            if (question.Contains("worried") || question.Contains("stressed"))
                return "😟 I understand how you feel. Let’s take it step by step.";
            if (question.Contains("happy") || question.Contains("excited"))
                return "😊 That’s great to hear! Keep it up!";
            if (question.Contains("sad") || question.Contains("upset"))
                return "😢 I’m sorry you feel that way. Talking it through might help.";
            if (question.Contains("angry"))
                return "😡 I see you’re upset. Let’s focus on solutions.";
            if (question.Contains("confused"))
                return "🤔 I’ll try to clarify that for you.";
            if (question.Contains("curious"))
                return "🔍 Curiosity is great! Let’s explore that topic.";

            // Memory recall
            if (question.Contains("what is my name"))
                return "Your name is " + Recall("name");
            if (question.Contains("when is my birthday"))
                return "Your birthday is " + Recall("birthday");

            if (question.StartsWith("my birthday is"))
            {
                string birthday = question.Substring(14).Trim();
                Remember("birthday", birthday);
                return $"Got it! I’ll remember your birthday is {birthday}.";
            }

            // Keyword tips
            CybersecurityTips tips = new CybersecurityTips();
            string tip = tips.GetTip(question);
            return tip;
        }

        public void Remember(string key, string value)
        {
            memory[key] = value;
        }

        public string Recall(string key)
        {
            return memory.ContainsKey(key) ? memory[key] : "(not set)";
        }

        // Persistence
        public void SaveMemory()
        {
            string json = JsonSerializer.Serialize(memory);
            File.WriteAllText("memory.json", json);
        }

        public void LoadMemory()
        {
            if (File.Exists("memory.json"))
            {
                string json = File.ReadAllText("memory.json");
                memory = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            }
        }

        public void ClearMemory()
        {
            memory.Clear();
            if (File.Exists("memory.json"))
                File.Delete("memory.json");
        }
    }
}
