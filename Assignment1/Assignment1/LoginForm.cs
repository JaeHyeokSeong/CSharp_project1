using System;
using Assignment1;

namespace Assignment1
{
    public class LoginForm : Form
    {
        int width = 40, height = 7;
        public LoginForm()
        {
        }
        public void ConsoleLoginScreen()
        {
            // left side
            for (int i = 1; i < height; i++)
            {
                WriteAt("|", 0, i);
            }
            // buttom line
            for (int i = 1; i <= width; i++)
            {
                WriteAt("=", i, height);
            }

            // right side
            for (int i = height - 1; i > 0; i--)
            {
                WriteAt("|", width + 1, i);
            }

            // top line
            for (int i = width; i >= 1; i--)
            {
                WriteAt("=", i, 0);
            }
            WriteAt("WELCOME TO SIMPLE BANKING SYSTEM", 5, 1);
            for (int i = 1; i < width + 1; i++)
            {
                WriteAt("=", i, 2);
            }
            WriteAt("LOGIN TO START", 13, 3);
        }
    }
}
