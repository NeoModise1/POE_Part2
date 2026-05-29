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

            // Always clear memory at startup
            bot.ClearMemory();

            PlayGreeting();

            // Start sequential prompts
            AddBotMessage("👋 Hello! I’m your Cybersecurity Awareness Bot. Let’s get to know each other first. What is your name?");
            pendingInfo = "name";

            NameMemory.Text = "Name: (not set)";
            BirthdayMemory.Text = "Birthday: (not set)";
            LocationMemory.Text = "Location: (not set)";
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

            // Conversational flow
            if (question.Contains("hello") || question.Contains("hi") || question.Contains("hey"))
            {
                string name = bot.Recall("name");
                response = !string.IsNullOrEmpty(name) ? $"Hello {name}! How are you today?" : "Hello! How are you today?";
            }
            else if (question.Contains("how are you"))
            {
                response = "I’m doing great, thanks for asking! How are you feeling today?";
            }
            else if (question.Contains("im good and you") || question.Contains("im good") || question.Contains("im good thanks and yourself"))
            {
                response = "I’m glad to hear that! I’m good too. Want to dive into some tips or just chat casually?";
            }
            else if (question.Contains("im not good") || question.Contains("im not well") || question.Contains("im not okay"))
            {
                response = "I’m sorry to hear that. I hope you feel better soon. Maybe some light cybersecurity chat will cheer you up?";
            }
            else if (question.Contains("joke"))
            {
                response = "😂 Why don’t hackers ever get invited to parties? Because they just keep trying to break the ice.";
            }
            else if (question.Contains("who are you"))
            {
                response = "I’m your chatbot companion. I can chat casually or share cybersecurity awareness tips if you’d like.";
            }
            else if (question.Contains("password"))
            {
                response = "🔑 Strong Password Tip: Use at least 12 characters, mix letters, numbers, and symbols.";
            }
            else if (question.Contains("phishing"))
            {
                response = "📧 Phishing Tip: Always check the sender’s email address carefully. Avoid clicking suspicious links, and never share personal info unless you’re sure the source is legitimate.";
            }
            else if (question.Contains("malware"))
            {
                response = "💻 Malware Tip: Keep your software updated and use antivirus tools to protect against malicious programs.";
            }
            else if (question.Contains("vpn"))
            {
                response = "🌐 VPN Tip: Always use a VPN on public Wi‑Fi to keep your data safe.";
            }
            else if (question.Contains("2fa"))
            {
                response = "🔒 2FA Tip: Enable two‑factor authentication on all important accounts for extra security.";
            }
            else if (question.Contains("safe browsing"))
            {
                response = "🛡️ Safe Browsing Tip: Avoid downloading files from untrusted sites and check for HTTPS before entering sensitive info.";
            }
            else if (question.Contains("scam"))
            {
                response = "🚨 Scam Tip: If it sounds too good to be true, it probably is. Always verify offers before acting.";
            }
            else if (question.Contains("privacy"))
            {
                response = "🔐 Privacy Tip: Limit the personal information you share online and review your social media privacy settings.";
            }
            else if (question.Contains("cybersecurity"))
            {
                response = "🧠 Cybersecurity Awareness: Stay alert, update your devices, and always think before you click.";
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
