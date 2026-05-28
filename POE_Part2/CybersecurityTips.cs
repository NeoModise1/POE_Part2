using System.Collections.Generic;

namespace Assignment_1_Part1
{
    public class CybersecurityTips
    {
        private Dictionary<string, string> cyberTips = new Dictionary<string, string>()
        {
            {"vpn", "A VPN protects your privacy when using public WiFi by encrypting your connection."},
            {"2fa", "Two-factor authentication (2FA) adds an extra layer of protection to your accounts."},
            {"scam", "Be cautious with unsolicited emails or messages asking for personal information."},
            {"privacy", "Always review your account security settings to protect your privacy."},
            {"malware", "Malware is malicious software. Use antivirus software and avoid untrusted downloads."},
            {"phishing", "Phishing tricks you into revealing sensitive information. Verify senders before clicking links."},
            {"password", "Strong passwords are essential. Use a mix of uppercase, lowercase, numbers, and symbols."},
            {"safe browsing", "Safe browsing means avoiding suspicious sites and ensuring HTTPS connections."},
            {"cybersecurity", "Cybersecurity protects systems and data from attacks using firewalls, encryption, and safe practices."}
        };

        public string GetTip(string question)
        {
            foreach (var tip in cyberTips)
            {
                if (question.Contains(tip.Key))
                {
                    return tip.Value;
                }
            }

            return "I didn’t quite understand that. Try asking about passwords, phishing, malware, VPN, 2FA, scams, privacy, safe browsing, or cybersecurity.";
        }
    }
}
