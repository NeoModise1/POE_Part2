-- Create Tasks table
CREATE TABLE IF NOT EXISTS Tasks (
    TaskID INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL,
    Description TEXT NOT NULL,
    ReminderDate TEXT
);

-- Create QuizQuestions table
CREATE TABLE IF NOT EXISTS QuizQuestions (
    QuestionID INTEGER PRIMARY KEY AUTOINCREMENT,
    QuestionText TEXT NOT NULL,
    OptionA TEXT NOT NULL,
    OptionB TEXT NOT NULL,
    OptionC TEXT NOT NULL,
    OptionD TEXT NOT NULL,
    CorrectIndex INTEGER NOT NULL
);

-- Create ActivityLog table
CREATE TABLE IF NOT EXISTS ActivityLog (
    LogID INTEGER PRIMARY KEY AUTOINCREMENT,
    ActionText TEXT NOT NULL,
    Timestamp TEXT NOT NULL
);

-- Create Users table
CREATE TABLE IF NOT EXISTS Users (
    UserID INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT,
    Birthday TEXT,
    Location TEXT
);

-- Insert 10 quiz questions
INSERT INTO QuizQuestions (QuestionText, OptionA, OptionB, OptionC, OptionD, CorrectIndex) VALUES
('What should you do if you receive an email asking for your password?', 'Reply', 'Delete', 'Report as phishing', 'Ignore', 2),
('Which of these is the strongest password?', '123456', 'Password!', 'MyDog2026!', 'Qx!9$zT7#Lm', 3),
('What does 2FA stand for?', 'Two-Factor Authentication', 'Two-Firewall Access', 'Two-Files Authorization', 'Two-Frequency Alert', 0),
('Which of these is a sign of a phishing email?', 'Generic greeting', 'Suspicious links', 'Urgent tone', 'All of the above', 3),
('What should you do before clicking a link in an email?', 'Click immediately', 'Hover to preview', 'Forward to friends', 'Ignore it', 1),
('Which of these is malware?', 'Virus', 'Trojan', 'Worm', 'All of the above', 3),
('Why should you use a VPN?', 'To play games faster', 'To hide your IP and encrypt traffic', 'To increase battery life', 'To block ads', 1),
('What is the safest way to store passwords?', 'Write them on paper', 'Save in browser', 'Use a password manager', 'Use one password for all accounts', 2),
('Which of these is NOT a safe browsing practice?', 'Checking HTTPS', 'Downloading from trusted sites', 'Sharing personal info on unknown sites', 'Keeping software updated', 2),
('What should you do if your device is infected with malware?', 'Ignore it', 'Run antivirus software', 'Sell the device', 'Turn off Wi-Fi only', 1);
