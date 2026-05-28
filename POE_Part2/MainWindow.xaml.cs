using Assignment_1_Part1;
using System;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace POE_Part2
{
    public partial class MainWindow : Window
    {
        private Chatbot bot = new Chatbot();

        public MainWindow()
        {
            InitializeComponent();
            bot.LoadMemory();

            PlayGreeting();

            // If name is already stored, greet with it
            string storedName = bot.Recall("name");
            if (!string.IsNullOrEmpty(storedName))
            {
                AddBotMessage($"👋 Hi {storedName}! Welcome back. You can chat with me about anything, and I’ll also share cybersecurity tips if you’d like.");
            }
            else
            {
                AddBotMessage("👋 Hi there! I’m your chatbot. You can chat with me about anything, and I’ll also share cybersecurity tips if you’d like.");
            }

            // Update memory labels immediately
            NameMemory.Text = "Name: " + (storedName ?? "(not set)");
            BirthdayMemory.Text = "Birthday: " + (bot.Recall("birthday") ?? "(not set)");
            LocationMemory.Text = "Location: " + (bot.Recall("location") ?? "(not set)");
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string input = UserInput.Text.Trim();
            if (string.IsNullOrEmpty(input))
            {
                AddBotMessage("⚠️ Please type something before sending.", Brushes.Red);
                return;
            }

            HandleUserInput(input.ToLower());
            UserInput.Clear();
        }

        private void QuickTopic_Click(object sender, RoutedEventArgs e)
        {
            string keyword = (sender as Button)?.Tag?.ToString() ?? "";
            HandleUserInput(keyword);
        }

        private void PlayGreeting_Click(object sender, RoutedEventArgs e)
        {
            PlayGreeting();
        }

        private void PlayGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("C:\\Users\\cash\\source\\repos\\POE_Part2\\POE_Part2\\POE_Part2\\Voice 1.wav");
                player.Play();
                AddBotMessage("🎧 Playing greeting audio...");
            }
            catch
            {
                AddBotMessage("⚠️ Greeting audio could not play.", Brushes.Red);
            }
        }

        private void HandleUserInput(string question)
        {
            AddUserMessage(question);

            string response;

            // Memory management
            if (question.StartsWith("my name is") || question.StartsWith("i am"))
            {
                string name = question.Replace("my name is", "").Replace("i am", "").Trim();
                bot.Remember("name", name);
                response = $"Nice to meet you, {name}! I'll remember your name.";

                // Update immediately
                NameMemory.Text = "Name: " + name;
            }
            else if (question.StartsWith("my birthday is"))
            {
                string birthday = question.Replace("my birthday is", "").Trim();
                bot.Remember("birthday", birthday);
                response = $"Got it! I'll remember your birthday as {birthday}.";
                BirthdayMemory.Text = "Birthday: " + birthday;
            }
            else if (question.StartsWith("i live in"))
            {
                string location = question.Replace("i live in", "").Trim();
                bot.Remember("location", location);
                response = $"Thanks for sharing! I'll remember that you live in {location}.";
                LocationMemory.Text = "Location: " + location;
            }
            else
            {
                // General responses with personalization
                if (question.Contains("hello") || question.Contains("hi") || question.Contains("hey"))
                {
                    string name = bot.Recall("name");
                    response = !string.IsNullOrEmpty(name) ? $"Hello {name}! How are you today?" : "Hello! How are you today?";
                }
                else if (question.Contains("who are you"))
                    response = "I’m your chatbot companion. I can chat casually or share cybersecurity awareness tips if you’d like.";
                else if (question.Contains("what can you do") || question.Contains("what can you help me with"))
                    response = "I can chat with you, detect sentiment, remember your details, and provide cybersecurity tips.";
                else
                    response = bot.GetResponse(question);
            }

            // Reference stored info later
            if (question.Contains("birthday"))
            {
                string birthday = bot.Recall("birthday");
                if (!string.IsNullOrEmpty(birthday))
                    response += $" 🎂 I remember your birthday is {birthday}. Should I remind you when it’s close?";
            }

            if (question.Contains("cybersecurity"))
            {
                string location = bot.Recall("location");
                if (!string.IsNullOrEmpty(location))
                    response += $" 🌍 Since you live in {location}, I can share local cybersecurity tips if you’d like.";
            }

            // Topic chaining
            if (response.Contains("password"))
                response += " 🔐 Would you like me to also explain Two-Factor Authentication?";
            else if (response.Contains("phishing"))
                response += " 📧 Do you want tips on spotting scam emails too?";
            else if (response.Contains("vpn"))
                response += " 🌐 Since you’re curious about VPNs, should I explain safe browsing next?";

            AddBotMessage(response);

            bot.SaveMemory();
        }

        private void ClearMemory_Click(object sender, RoutedEventArgs e)
        {
            bot.ClearMemory();
            NameMemory.Text = "Name: (not set)";
            BirthdayMemory.Text = "Birthday: (not set)";
            LocationMemory.Text = "Location: (not set)";
            AddBotMessage("🗑️ Memory cleared.");
        }

        // Regular chatbot look: user right, bot left, with timestamps
        private void AddUserMessage(string text)
        {
            ChatPanel.Children.Add(new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                Children =
                {
                    new Border
                    {
                        Background = Brushes.LightGray,
                        CornerRadius = new CornerRadius(4),
                        Padding = new Thickness(6),
                        Margin = new Thickness(2),
                        Child = new TextBlock
                        {
                            Text = text,
                            Foreground = Brushes.Black,
                            FontFamily = new FontFamily("Consolas"),
                            TextWrapping = TextWrapping.Wrap
                        }
                    },
                    new TextBlock
                    {
                        Text = DateTime.Now.ToString("HH:mm"),
                        FontSize = 10,
                        Foreground = Brushes.DarkGray,
                        HorizontalAlignment = HorizontalAlignment.Right
                    }
                }
            });
        }

        private void AddBotMessage(string text, Brush? color = null)
        {
            ChatPanel.Children.Add(new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Children =
                {
                    new Border
                    {
                        Background = Brushes.Gray,
                        CornerRadius = new CornerRadius(4),
                        Padding = new Thickness(6),
                        Margin = new Thickness(2),
                        Child = new TextBlock
                        {
                            Text = text,
                            Foreground = color ?? Brushes.White,
                            FontFamily = new FontFamily("Consolas"),
                            TextWrapping = TextWrapping.Wrap
                        }
                    },
                    new TextBlock
                    {
                        Text = DateTime.Now.ToString("HH:mm"),
                        FontSize = 10,
                        Foreground = Brushes.LightGray,
                        HorizontalAlignment = HorizontalAlignment.Left
                    }
                }
            });
        }
    }
}
