using System;
using System.Collections.Generic;
using Assignment1;

namespace Assignment1
{
    public class DeleteAccountForm : Form
    {
        int width = 40, height = 6;
        public DeleteAccountForm()
        {

        }
        public void ConsoleDeleteAccountScreen()
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
            WriteAt("DELETE AN ACCOUNT", 11, 1);
            for (int i = 1; i < width + 1; i++)
            {
                WriteAt("=", i, 2);
            }
            WriteAt("ENTER THE DETAILS", 13, 3);
            WriteAt("Account Number: ", 2, 5);
        }
        public void ConsoleDisplaySearchedAccountInformation(List<string> accountDetails)
        {
            int height1 = 11;
            // left side
            for (int i = 1; i < height1; i++)
            {
                WriteAt("|", 0, i);
            }
            // buttom line
            for (int i = 1; i <= width; i++)
            {
                WriteAt("=", i, height1);
            }

            // right side
            for (int i = height1 - 1; i > 0; i--)
            {
                WriteAt("|", width + 1, i);
            }

            // top line
            for (int i = width; i >= 1; i--)
            {
                WriteAt("=", i, 0);
            }
            WriteAt("ACCOUNT DETAILS", 15, 1);
            for (int i = 1; i < width + 1; i++)
            {
                WriteAt("=", i, 2);
            }
            // options
            WriteAt("Account No: " + accountDetails[1], 4, 4);
            WriteAt("Account Balance $" + accountDetails[2], 4, 5);
            WriteAt("First Name: " + accountDetails[3], 4, 6);
            WriteAt("Last Name: " + accountDetails[4], 4, 7);
            WriteAt("Address: " + accountDetails[5], 4, 8);
            WriteAt("Phone: " + accountDetails[6], 4, 9);
            WriteAt("Email: " + accountDetails[7], 4, 10);
        }
    }
}
