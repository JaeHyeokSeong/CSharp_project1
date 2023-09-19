using System;
using System.IO;

namespace Assignment1
{
    public class Login
    {
        private string userName, password;
        private const string filePath = "login.txt"; // write a file name in here
        LoginForm loginForm = new LoginForm();

        public string Password
        {
            get
            {
                return password;
            }
        }
        public Login()
        {
        }
        private void ReadUserInputs()
        {
            readUserName();
            readPassword();
        }
        private void readUserName()
        {
            loginForm.WriteAt("User Name: ", 3, 5);
            userName = Console.ReadLine();
        }
        private void readPassword()
        {
            // reference: https://m.blog.naver.com/PostView.naver?isHttpsRedirect=true&blogId=lysis2jt&logNo=40118777155
            loginForm.WriteAt("Password: ", 3, 6);
            // print asterisk instead of password inputs
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
            while (consoleKeyInfo.Key != ConsoleKey.Enter)
            {
                if (consoleKeyInfo.Key != ConsoleKey.Backspace)
                {
                    password += consoleKeyInfo.KeyChar;
                    Console.Write("*"); // print asterisk if keyboard buttion is pressed
                    consoleKeyInfo = Console.ReadKey(true);
                }
                else if (consoleKeyInfo.Key == ConsoleKey.Backspace)
                {
                    // check is password is empty string(null)
                    if (!string.IsNullOrEmpty(password))
                    {
                        // erase one asterisk(*) in the console
                        Console.Write("\b");
                        password = password.Substring(0, password.Length - 1);
                    }
                    consoleKeyInfo = Console.ReadKey(true);
                }
            }
            Console.WriteLine();
        }
        private StreamReader OpenLoginFile(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            return new StreamReader(fs);
        }
        public Boolean AttemptLogin()
        {
            loginForm.Clear(); // clear the console
            loginForm.ConsoleLoginScreen();
            userName = "";
            password = "";
            string[] tempDatas = new string[2]; // index 0 means userName, index 1 means password
            StreamReader fs = OpenLoginFile(filePath);
            Boolean status = true, matchedUserNamePassword = false;
            ReadUserInputs();
            while (status)
            {
                for (int i = 0; i < 2; i++)
                {
                    string data = fs.ReadLine();
                    if (data != null)
                    {
                        tempDatas[i] = data;
                    }
                    else
                    {
                        // nothing to read in a file
                        status = false;
                    }
                }
                if (userName == tempDatas[0] && password == tempDatas[1])
                {
                    status = false;
                    matchedUserNamePassword = true;
                }
            }
            fs.Close(); // close the Stream
            return matchedUserNamePassword;
        }
    }
}

