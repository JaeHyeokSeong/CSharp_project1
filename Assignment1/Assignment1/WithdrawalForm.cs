using System;
using Assignment1;

namespace Assignment1
{
    public class WithdrawalForm : Form
    {
        int width = 40, height = 7;
        public WithdrawalForm()
        {
        }
        public void ConsoleWithdrawalAccountScreen()
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
            WriteAt("WITHDRAW", 18, 1);
            for (int i = 1; i < width + 1; i++)
            {
                WriteAt("=", i, 2);
            }
            WriteAt("ENTER THE DETAILS", 13, 3);
            //WriteAt("Account Number: ", 2, 5);
            //WriteAt("Amoust: $", 2, 6);
        }
    }
}
