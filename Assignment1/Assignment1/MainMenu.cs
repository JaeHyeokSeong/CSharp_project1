using System;
using Assignment1;

namespace Assignment1
{
    public class MainMenu
    {
        MainMenuForm mainMenuForm = new MainMenuForm();
        public MainMenu()
        {
        }
        public int readUserChoice()
        {
            mainMenuForm.Clear();
            mainMenuForm.ConsoleMainMenuScreen();
            int userChoice;
            try
            {
                userChoice = Convert.ToInt32(Console.ReadLine());
                if (userChoice < 1 || userChoice > 7)
                {
                    // if user input is not inside of the options, then throw an exception
                    throw new ArgumentOutOfRangeException();
                }
            }
            catch (FormatException)
            {
                mainMenuForm.WriteAt("[Warnning]: Invalid input value\nPlease enter to continue", 0, 13);
                Console.ReadKey();
                userChoice = readUserChoice();
            }
            catch (ArgumentOutOfRangeException)
            {
                mainMenuForm.WriteAt("[Warnning]: Invalid input value\nPlease enter to continue", 0, 13);
                Console.ReadKey();
                userChoice = readUserChoice();
            }
            catch (OverflowException)
            {
                mainMenuForm.WriteAt("[Warnning]: Invalid input value\nPlease enter to continue", 0, 13);
                Console.ReadKey();
                userChoice = readUserChoice();
            }
            return userChoice;
        }
    }
}
