using System;
using Assignment1;

namespace Assignment1
{
    public class MainMenuForm : Form
    {
        int width = 40, height = 10;
        public MainMenuForm()
        {
            ConsoleMainMenuScreen();
        }
        public void ConsoleMainMenuScreen()
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
            WriteAt("WELCOME TO SIMPLE BANKING SYSTEM", 4, 1);
            for (int i = 1; i < width + 1; i++)
            {
                WriteAt("=", i, 2);
            }
            // options
            WriteAt("1. Create a new account", 4, 3);
            WriteAt("2. Search for an account", 4, 4);
            WriteAt("3. Deposit", 4, 5);
            WriteAt("4. Withdraw", 4, 6);
            WriteAt("5. A/C statement", 4, 7);
            WriteAt("6. Delete account", 4, 8);
            WriteAt("7. Exit", 4, 9);
            // the lowest
            // left
            WriteAt("|", 0, height);
            WriteAt("|", 0, height + 1);
            for (int i = 1; i <= width; i++)
            {
                WriteAt("=", i, height + 2);
            }
            WriteAt("|", width + 1, height);
            WriteAt("|", width + 1, height + 1);
            WriteAt("Enter your choice (1-7): ", 3, height + 1);
        }
    }
}
