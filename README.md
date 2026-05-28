# POE_Part2
# Cybersecurity Awareness Chatbot

A WPF chatbot project built in C# for cybersecurity awareness and casual conversation.  
This project was developed as part of an assignment to demonstrate GUI design, memory management, sentiment detection, and cybersecurity education.

---

## Features

- **Sequential Setup Prompts**  
  Collects user details (Name → Birthday → Location) once at startup.

- **Conversational Flow**  
  Handles greetings, wellbeing questions, jokes, and casual chat naturally.  
  Examples:
  - "Hello" → "Hello Neo! How are you today?"
  - "Tell me a joke" → "Why don’t hackers ever get invited to parties? Because they just keep trying to break the ice."

- **Cybersecurity Awareness**  
  Quick topic buttons provide direct tips:
  - Passwords → Strong password guidelines
  - Phishing → How to spot phishing emails
  - Malware → Protection tips
  - VPN → Safe browsing with VPNs
  - 2FA → Two-factor authentication
  - Safe Browsing → HTTPS and safe downloads
  - Scams → Recognizing scams
  - Privacy → Social media privacy awareness
  - Cybersecurity → General awareness

- **Memory Management**  
  - Sidebar displays stored Name, Birthday, Location.  
  - Clear Memory button resets profile.  
  - Memory is cleared automatically at startup.

- **Sentiment Detection**  
  - Positive replies highlighted in green.  
  - Negative replies highlighted in red.  
  - Neutral replies in gray.

- **Greeting Audio**  
  Plays a welcome sound (`Voice 1.wav`) at startup.

- **UI Design**  
  - ASCII banner header with logo in corner.  
  - Scrollable conversation panel.  
  - Sidebar with memory + quick topics.  
  - Input box with Send button.

---

## Requirements

- Windows OS  
- Visual Studio (with WPF workload installed)  
- .NET 6 or later  

---

## How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/YourUsername/CybersecurityChatbot.git
