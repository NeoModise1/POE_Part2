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
        private string pendingQuestion = "";
        private string pendingInfo = "";

        public MainWindow()
        {
            InitializeComponent();
            bot.LoadMemory();

            PlayGreeting();

            // Always start sequential prompts
            if (string.IsNullOrEmpty(bot.Recall("name")))
            {
                AddBotMessage("👋 Hi there! What is your name?");
                pendingInfo = "name";
            }
            else if (string.IsNullOrEmpty(bot.Recall("birthday")))
            {
                AddBotMessage("🎂 Could you tell me your birthday?");
                pendingInfo = "birthday";
            }
            else if (string.IsNullOrEmpty(bot.Recall("location")))
            {
                AddBotMessage("🌍 Where do you live?");
                pendingInfo = "location";
            }
            else
            {
                AddBotMessage($"👋 Welcome back {bot.Recall("name")}! You can chat with me about anything, and I’ll also share cybersecurity tips if you’d like.");
            }

            NameMemory.Text = "Name: " + (bot.Recall("name") ?? "(not set)");
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
            string response = "";

            // Sequential info prompts
            if (pendingInfo == "name")
            {
                bot.Remember("name", question);
                NameMemory.Text = "Name: " + question;
                AddBotMessage($"Nice to meet you, {question}! 🎂 Could you tell me your birthday?");
                pendingInfo = "birthday";
                return;
            }
            else if (pendingInfo == "birthday")
            {
                bot.Remember("birthday", question);
                BirthdayMemory.Text = "Birthday: " + question;
                AddBotMessage($"Got it! I'll remember your birthday as {question}. 🌍 Where do you live?");
                pendingInfo = "location";
                return;
            }
            else if (pendingInfo == "location")
            {
                bot.Remember("location", question);
                LocationMemory.Text = "Location: " + question;
                pendingInfo = "";

                AddBotMessage($"✅ Great, I’ve saved your profile! Name: {bot.Recall("name")}, Birthday: {bot.Recall("birthday")}, Location: {bot.Recall("location")}");
                return;
            }

            // Normal chatbot flow after details are set
            if (question.Contains("hello") || question.Contains("hi") || question.Contains("hey"))
            {
                string name = bot.Recall("name");
                response = !string.IsNullOrEmpty(name) ? $"Hello {name}! How are you today?" : "Hello! How are you today?";
            }
            else if (question.Contains("who are you"))
                response = "I’m your chatbot companion. I can chat casually or share cybersecurity awareness tips if you’d like.";
            else if (question.Contains("password"))
            {
                response = "Passwords are important! Do you want me to explain how to make them stronger? (yes/no)";
                pendingQuestion = "password security";
            }
            else if (question.Contains("phishing"))
            {
                response = "Phishing emails can be dangerous. Would you like me to show you how to spot them? (yes/no)";
                pendingQuestion = "phishing awareness";
            }
            else if (question.Contains("vpn"))
            {
                response = "VPNs protect your privacy. Do you want me to explain safe browsing next? (yes/no)";
                pendingQuestion = "vpn usage";
            }
            else
            {
                response = bot.GetResponse(question);
            }

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

            // Restart sequential prompts
            AddBotMessage("👋 Let's start fresh. What is your name?");
            pendingInfo = "name";
        }

        // User messages: right side, light gray bubble
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

        // Bot messages: left side, gray bubble
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
