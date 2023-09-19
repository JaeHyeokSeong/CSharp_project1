using System;
namespace Assignment1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 1. Login Menu implement
            Login login = new Login();
            Boolean loginResult;
            do
            {
                loginResult = login.AttemptLogin();
                // if loginResult is false, then do the below if statement and continue the do while loop until the loginResult became true
                // However, if loginResult is true, then finish the do while loop
                if (!loginResult)
                {
                    Console.SetCursorPosition(0, 9);
                    Console.WriteLine("[Error]\nThat doesn't look right. Enter your User Name and Password again");
                    Console.Write("Please enter to continue");
                    Console.ReadKey();
                }
            } while (!loginResult);
            Console.SetCursorPosition(0, 9);
            Console.Write("Valid credentials!... Please enter");
            Console.ReadKey();

            // 2. Main Menu implement
            MainMenu mainMenu = new MainMenu();
            Account account = new Account(login.Password);
            int userChoice;
            do
            {
                userChoice = mainMenu.readUserChoice();
                // based on userChoice execute required tasks
                if (userChoice == 1)
                {
                    // Create a new account
                    account.CreateAccount();
                }
                else if (userChoice == 2)
                {
                    // Search for an account
                    account.SearchAccount();
                }
                else if (userChoice == 3)
                {
                    // Deposit
                    account.Deposit();
                }
                else if (userChoice == 4)
                {
                    // Withdraw
                    account.Withdrawal();
                }
                else if (userChoice == 5)
                {
                    // A/C statement
                    account.AccountStatement();
                }
                else if (userChoice == 6)
                {
                    // Delete account
                    account.DeleteAccount();
                }
                else if (userChoice == 7)
                {
                    Console.Clear();
                    Console.Write("[WARNNING] Are you sure to finish the program? [y/n]: ");
                    ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                    while (consoleKeyInfo.Key != ConsoleKey.Y && consoleKeyInfo.Key != ConsoleKey.N)
                    {
                        Console.Write("\n[WARNNING] Invalid input. Please enter [y/n]: ");
                        consoleKeyInfo = Console.ReadKey();
                    }
                    // if user press 'n', then cancel for finishing the program
                    if (consoleKeyInfo.Key == ConsoleKey.N)
                    {
                        // change userChoice value to -1 to continue the do while loop
                        // you can assign any value unless 7
                        userChoice = -1;
                    }
                }
                // if userChoice is 7, then finish the program.
            } while (userChoice != 7);
            Console.Clear();
            Console.WriteLine("Finished");
        }
    }
}