using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Assignment1;

namespace Assignment1
{
    public class Account
    {
        /*
         * password is used when we find an account
         * (step1) compare account number
         * (step2) if same account number is found then compare password which is on second line
         */
        private string password, accountNumber, emailAddress = "";
        CreateAccountForm createAccountForm;

        public Account(string password)
        {
            this.password = password;
        }
        public void CreateAccount()
        {
            CreateAccountFile(ReadAccountInformation());
            Console.WriteLine("\n\nAccount Created! details will be provided via email\n");
            Console.WriteLine("Account number is: " + accountNumber);
            // send email
            string accountFileName = accountNumber + ".txt";
            List<string> accountInformation = FileReadAll(FileOpen(accountFileName));
            Mail mail = new Mail(accountInformation);
            Console.WriteLine("The email is being sent. Please wait a few seconds...");
            mail.SendMail();
            Console.WriteLine("Press Enter...");
            Console.ReadKey();
        }
        public string[] ReadAccountInformation()
        {
            string[] accountInformations = new string[5];
            // Form Testing
            Console.Clear();
            createAccountForm = new CreateAccountForm();
            createAccountForm.CreateAccountScreen();
            createAccountForm.WriteAt("ENTER THE DETAILS", 13, 3);
            createAccountForm.WriteAt("First Name: ", 3, 5);
            createAccountForm.WriteAt("Last Name: ", 3, 6);
            createAccountForm.WriteAt("Address: ", 3, 7);
            createAccountForm.WriteAt("Phone: ", 3, 8);
            createAccountForm.WriteAt("Email: ", 3, 9);
            // save First Name
            accountInformations[0] = ReadUserInput(15, 5);
            // save Last Name
            accountInformations[1] = ReadUserInput(14, 6);
            // save Address
            accountInformations[2] = ReadUserInput(12, 7);
            // save Phone number
            accountInformations[3] = ReadUserInput(10, 8);
            // save Email
            accountInformations[4] = ReadUserInput(10, 9);
            // ask user to check inputed values
            createAccountForm.WriteAt("Is the Information correct (y/n)? ", 1, 12);
            // user reply from (y/n). y returns true, n returns false
            bool userReply = ReadYesNoChoice();
            if (!userReply)
            {
                accountInformations = ReadAccountInformation();
            }
            // check Phone number
            bool isCorrectPhoneNumber = IsPhoneNumberCorrect(accountInformations[3]);
            // check Email address
            bool isCoorectEmailAddress = IsEmailAddressCorrect(accountInformations[4]);

            if (!isCorrectPhoneNumber || !isCoorectEmailAddress)
            {
                if (!isCorrectPhoneNumber)
                {
                    Console.WriteLine("\n[WARNNING]: The phone number is wrong.");
                }
                if (!isCoorectEmailAddress)
                {
                    Console.WriteLine("{0}[WARNNING]: The email address is wrong.", (!isCorrectPhoneNumber) ? "" : "\n");
                }
                Console.WriteLine("Please enter...");
                Console.ReadKey();
                accountInformations = ReadAccountInformation();
            }

            // is everthing all correct then return the account informations
            return accountInformations;
        }
        private void CreateAccountFile(string[] accountInformations)
        {
            accountNumber = GetAvailableAccountNumber();
            FileStream fs = FileCreate(accountNumber);
            // write accountNumber and password in the file
            string[] updatedAccountInformations = new string[accountInformations.Length + 3];
            updatedAccountInformations[0] = password;
            updatedAccountInformations[1] = accountNumber;
            updatedAccountInformations[2] = "0"; // balance
            for (int i = 3; i < updatedAccountInformations.Length; i++)
            {
                updatedAccountInformations[i] = accountInformations[i - 3];
            }
            /*
             * In this file the information are written in the following order
             * 
             * password
             * accountNumber
             * balance
             * firstName
             * lastName
             * address
             * phoneNumber
             * email address
             */
            FileWrite(fs, updatedAccountInformations);
        }
        // 21st August
        public void SearchAccount()
        {
            int tempAccountNumber = ReadAccoutNumber();
            if (IsUserAccount(tempAccountNumber))
            {
                Console.WriteLine("\nAccout found!");
                // ask to user do you like to see account details
                Console.Write("\nDo you like to see the account details? (y/n) ");
                if (ReadYesNoChoice()) // y return true, n return false
                {
                    DisplayFoundAccount(tempAccountNumber);
                }
            }
            else
            {
                Console.WriteLine("\nAccount not found!");
            }
            Console.Write("\n\n\nCheck another account (y/n)? ");
            if (ReadYesNoChoice()) // y return true, n return false
            {
                SearchAccount();
            }
        }
        private int ReadAccoutNumber()
        {
            int tempAccountNumber = 0;
            Console.Clear();
            SearchAccountForm searchAccountForm = new SearchAccountForm();
            searchAccountForm.ConsoleSearchAccountScreen();
            try
            {
                tempAccountNumber = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                searchAccountForm.WriteAt("Please enter correct account number. Press Enter...", 1, 8);
                Console.ReadKey();
                tempAccountNumber = ReadAccoutNumber();
            }
            catch (OverflowException)
            {
                searchAccountForm.WriteAt("Please enter correct account number. Press Enter...", 1, 8);
                Console.ReadKey();
                tempAccountNumber = ReadAccoutNumber();
            }
            return tempAccountNumber;
        }
        private bool IsAccountNumberExist(string fileName)
        {
            // this method return true if account number text file is found
            // if account number text file is not found, then return false
            // reference: https://mirwebma.tistory.com/130
            FileInfo fileInfo = new FileInfo(fileName);
            return fileInfo.Exists;
        }
        private bool IsUserAccount(int accountNumber)
        {
            // this method will compare user password in the file
            // password is saved at the first line of the file
            bool isUserAccount = false;
            string fileName = accountNumber.ToString() + ".txt";
            if (IsAccountNumberExist(fileName))
            {

                string tempPassword = FileReadAt(FileOpen(fileName), 1);
                if (this.password == tempPassword)
                {
                    isUserAccount = true;
                }
            }
            return isUserAccount;
        }
        private void DisplayFoundAccount(int accountNumber)
        {
            Console.Clear();
            SearchAccountForm searchAccountForm = new SearchAccountForm();
            List<string> contents = FileReadAll(FileOpen(accountNumber.ToString()));
            searchAccountForm.ConsoleDisplaySearchedAccount(contents);
        }
        public void Deposit()
        {
            int accountNumber = 0;
            double amount = 0.0;
            DepositForm depositForm;
            do
            {
                Console.Clear();
                depositForm = new DepositForm();
                depositForm.ConsoleDepositAccountScreen();
                depositForm.WriteAt("Account Number: ", 2, 5);
                accountNumber = ReadIntegerTypeUserInput();
                if (accountNumber == -1)
                {
                    if (ReadYesNoChoice() == false)
                    {
                        return;
                    }
                }
                if (accountNumber != -1)
                {
                    // if account number is not -1, then user has entered the correct format of number in the console
                    // however, we need to check whether this account number is user's account number
                    if (IsUserAccount(accountNumber))
                    {
                        double tempAmount = 0.0;
                        do
                        {
                            Console.Clear();
                            depositForm = new DepositForm();
                            depositForm.ConsoleDepositAccountScreen();
                            depositForm.WriteAt("Account Number: " + accountNumber, 2, 5);
                            depositForm.WriteAt("Account found! Enter the amount...", 0, 9);
                            depositForm.WriteAt("Amount: $", 2, 6);
                            tempAmount = ReadDoubleTypeUserInput();
                            if (tempAmount == -1)
                            {
                                if (ReadYesNoChoice() == false)
                                {
                                    return;
                                }
                            }
                            // set the amount of deposit
                            amount = tempAmount;
                        } while (tempAmount == -1);

                        // testing
                        string depositRecord = "[DEPOSIT] Amount: $" + amount
                            + ", Time: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        // update the file
                        // step1 read all contents in the file
                        string fileName = accountNumber.ToString() + ".txt";
                        List<string> contents = FileReadAll(FileOpen(fileName));
                        // update balance information
                        // balance is stored at index 2
                        double originalBalance = Convert.ToDouble(contents[2]);
                        double updatedBalacne = originalBalance + amount;
                        contents[2] = updatedBalacne.ToString();
                        // Clear all contents in the file
                        FileClearAll(fileName);
                        // add deposit record in a contents
                        contents.Add(depositRecord);
                        // write updated information in the file
                        FileWrite(FileOpen(fileName), contents);
                        Console.SetCursorPosition(0, 10);
                        Console.WriteLine("Deposit successfull! Press Enter...");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.SetCursorPosition(0, 10);
                        Console.WriteLine("Account not found!\nRetry (Y/N)? ");
                        if (!ReadYesNoChoice())
                        {
                            return;
                        }
                        accountNumber = -1;
                    }
                }
            } while (accountNumber == -1);
        }
        private int ReadIntegerTypeUserInput()
        {
            int userInput = 0;
            try
            {
                userInput = Convert.ToInt32(Console.ReadLine());
                if (userInput < 0)
                {
                    throw new FormatException();
                }
            }
            catch (FormatException)
            {
                Console.SetCursorPosition(0, 10);
                Console.WriteLine("Enter a correct number.");
                Console.Write("Continue (Y), Terminate the process (N) : ");
                userInput = -1;
            }
            catch (OverflowException)
            {
                Console.SetCursorPosition(0, 10);
                Console.Write("Continue (Y), Terminate the process (N) : ");
                userInput = -1;
            }
            return userInput;
        }
        private double ReadDoubleTypeUserInput()
        {
            double userInput = 0;
            try
            {
                userInput = Convert.ToDouble(Console.ReadLine());
                if (userInput < 0)
                {
                    throw new FormatException();
                }
            }
            catch (FormatException)
            {
                Console.SetCursorPosition(0, 10);
                Console.WriteLine("Enter a correct number.");
                Console.Write("Continue (Y), Terminate the process (N) : ");
                userInput = -1;
            }
            catch (OverflowException)
            {
                Console.SetCursorPosition(0, 10);
                Console.Write("Continue (Y), Terminate the process (N) : ");
                userInput = -1;
            }
            return userInput;
        }
        public void Withdrawal()
        {
            int accountNumber = 0;
            double finalWithdrawAmount = 0.0;
            WithdrawalForm withdrawalForm;
            do
            {
                Console.Clear();
                withdrawalForm = new WithdrawalForm();
                withdrawalForm.ConsoleWithdrawalAccountScreen();
                withdrawalForm.WriteAt("Account Number: ", 2, 5);
                accountNumber = ReadIntegerTypeUserInput();
                if (accountNumber == -1)
                {
                    if (ReadYesNoChoice() == false)
                    {
                        return;
                    }
                }
                if (accountNumber != -1)
                {
                    // if account number is not -1, then user has entered the correct format of number in the console
                    // however, we need to check whether this account number is user's account number
                    if (IsUserAccount(accountNumber))
                    {
                        // step1 read all contents in the file
                        string fileName = accountNumber.ToString() + ".txt";
                        List<string> contents = FileReadAll(FileOpen(fileName));
                        // update balance information
                        // balance is stored at index 2
                        double originalBalance = Convert.ToDouble(contents[2]);
                        double tempWithdrawAmount = 0.0;
                        do
                        {
                            Console.Clear();
                            withdrawalForm = new WithdrawalForm();
                            withdrawalForm.ConsoleWithdrawalAccountScreen();
                            withdrawalForm.WriteAt("Account Number: " + accountNumber, 2, 5);
                            withdrawalForm.WriteAt("Account found! Enter the amount...", 0, 9);
                            withdrawalForm.WriteAt("Amount: $", 2, 6);
                            tempWithdrawAmount = ReadDoubleTypeUserInput();
                            if (tempWithdrawAmount == -1)
                            {
                                if (ReadYesNoChoice() == false)
                                {
                                    return;
                                }
                                continue;
                            }
                            if (tempWithdrawAmount != -1)
                            {
                                if (tempWithdrawAmount == 0)
                                {
                                    // if withdraw amount is 0, then ask again to enter
                                    Console.SetCursorPosition(0, 10);
                                    Console.WriteLine("\n[WARNNING] The withdraw amount should be bigger than 0");
                                    Console.Write("Continue (Y), Terminate (N) : ");
                                    if (!ReadYesNoChoice())
                                    {
                                        Console.WriteLine("\n\nThe withdraw process has canceled. Press Enter...");
                                        Console.ReadKey();
                                        return;
                                    }
                                    // set -1 to continue the do while loop
                                    tempWithdrawAmount = -1;
                                }
                                else if (tempWithdrawAmount <= originalBalance)
                                {
                                    // enough balance
                                    // set withdraw amount
                                    finalWithdrawAmount = tempWithdrawAmount;
                                }
                                else
                                {
                                    // low balance
                                    Console.SetCursorPosition(0, 10);
                                    Console.WriteLine("\n[WARNNING] low balance");
                                    Console.WriteLine("[Current balance] : ${0}", originalBalance);
                                    Console.WriteLine("[Attempted withdraw amount] : ${0}", tempWithdrawAmount);
                                    Console.Write("[OPTIONS](Y / N) : ");
                                    if (!ReadYesNoChoice())
                                    {
                                        Console.WriteLine("\n\nThe withdraw process has canceled. Press Enter...");
                                        Console.ReadKey();
                                        return;
                                    }
                                    // set the loop to continue because of low balance condition
                                    tempWithdrawAmount = -1;
                                }
                            }

                        } while (tempWithdrawAmount == -1);

                        // confirm to the user. Is this correct
                        Console.SetCursorPosition(0, 10);
                        Console.WriteLine("\n[CHECK] Withdraw amount : ${0}", finalWithdrawAmount);
                        Console.Write("[OPTIONS] (Y/N) : ");
                        if (!ReadYesNoChoice())
                        {
                            Console.WriteLine("\n\nThe withdraw process has canceled. Press Enter...");
                            Console.ReadKey();
                            return;
                        }
                        // record withdraw transaction
                        string withdrawRecord = "[WITHDRAW] Amount: $" + finalWithdrawAmount
                            + ", Time: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        // update the file
                        // from the original balance subtract the withdraw amount
                        double updatedBalacne = originalBalance - finalWithdrawAmount;
                        contents[2] = updatedBalacne.ToString();
                        // Clear all contents in the file
                        FileClearAll(fileName);
                        // add withdraw record in the contents
                        contents.Add(withdrawRecord);
                        // write updated information in the file
                        FileWrite(FileOpen(fileName), contents);
                        Console.WriteLine("\n\nWithdraw successfull! Press Enter...");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.SetCursorPosition(0, 10);
                        Console.WriteLine("Account not found!\nRetry (Y/N)? ");
                        if (!ReadYesNoChoice())
                        {
                            return;
                        }
                        accountNumber = -1;
                    }
                }
            } while (accountNumber == -1);
        }
        public void AccountStatement()
        {
            // 24th August
            int tempAccountNumber;
            AccountStatementForm accountStatementForm;
            do
            {
                Console.Clear();
                accountStatementForm = new AccountStatementForm();
                accountStatementForm.ConsoleStatementAccountScreen();
                tempAccountNumber = ReadIntegerTypeUserInput();

                // if -1 is returned that means user entered a wrong number
                if (tempAccountNumber == -1)
                {
                    if (!ReadYesNoChoice())
                    {
                        return;
                    }
                    continue;
                }
                // if tempAccountNumber don't have a -1 value, then check is this account number
                // is exist
                if (!IsUserAccount(tempAccountNumber))
                {
                    // if this account number is not an user's account, then show error message
                    Console.SetCursorPosition(0, 10);
                    Console.WriteLine("Account not found\n");
                    Console.Write("Continue (Y), Terminate the process (N) : ");
                    if (!ReadYesNoChoice())
                    {
                        // if user press N (terminate), then finish this method
                        Console.WriteLine("\n\nAccount Statement process has terminated");
                        return;
                    }
                    // if user press Y (continue), then set tempAccountNumber to -1 to continue
                    // the do while loo
                    tempAccountNumber = -1;
                }
                else
                {
                    Console.SetCursorPosition(0, 8);
                    Console.WriteLine("Account found!");
                    Console.Write("View details (Y), Terminate the process (N) : ");
                    if (!ReadYesNoChoice())
                    {
                        // if user press N (terminate), then finish this method
                        Console.WriteLine("\n\nAccount Statement process has terminated");
                        return;
                    }
                    else
                    {
                        Console.Clear();
                        // read contents from the file
                        List<string> contents = FileReadAll(FileOpen(tempAccountNumber.ToString()));
                        accountStatementForm.ConsoleDisplaySearchedAccountStatement(contents);

                        // ask email option
                        Console.SetCursorPosition(0, 14);
                        Console.WriteLine("Detailed transaction details are available via email.");
                        Console.Write("Email statement (Y/N) : ");
                        if (ReadYesNoChoice())
                        {
                            Mail mail = new Mail(contents);
                            Console.WriteLine("\nThe email is being sent... Please wait");
                            mail.SendMail();
                        }
                        // ask to users do you like to the account statement process again
                        Console.Write("\n\nFind another account statment (Y), Terminate the process (N) : ");
                        if (ReadYesNoChoice())
                        {
                            tempAccountNumber = -1;
                        }
                    }
                }
            } while (tempAccountNumber == -1);
        }
        public void DeleteAccount()
        {
            // 26th August
            int tempAccountNumber;
            DeleteAccountForm deleteAccountForm;
            do
            {
                Console.Clear();
                deleteAccountForm = new DeleteAccountForm();
                deleteAccountForm.ConsoleDeleteAccountScreen();
                tempAccountNumber = ReadIntegerTypeUserInput();

                // if -1 is returned that means user entered a wrong number
                if (tempAccountNumber == -1)
                {
                    if (!ReadYesNoChoice())
                    {
                        return;
                    }
                    // if user entered true (continue), then execute the continue keyword
                    continue;
                }
                // if tempAccountNumber don't have a -1 value, then check is this account number
                // is exist
                if (!IsUserAccount(tempAccountNumber))
                {
                    // if this account number is not an user's account, then show error message
                    Console.SetCursorPosition(0, 10);
                    Console.WriteLine("Account not found\n");
                    Console.Write("Continue (Y), Terminate the process (N) : ");
                    if (!ReadYesNoChoice())
                    {
                        // if user press N (terminate), then finish this method
                        Console.WriteLine("\n\nAccount Statement process has terminated");
                        return;
                    }
                    // if user press Y (continue), then set tempAccountNumber to -1 to continue
                    // the do while loo
                    tempAccountNumber = -1;
                }
                else
                {
                    Console.SetCursorPosition(0, 8);
                    Console.WriteLine("Account found!");
                    Console.Write("View details (Y), Terminate the process (N) : ");
                    if (!ReadYesNoChoice())
                    {
                        // if user pressed N (terminate), then finish this method
                        Console.WriteLine("\n\nDelete Account process has terminated");
                        return;
                    }
                    else
                    {
                        // if user pressed Y (view details), then show the details of the account
                        Console.Clear();
                        // read contents from the file
                        List<string> contents = FileReadAll(FileOpen(tempAccountNumber.ToString()));
                        deleteAccountForm.ConsoleDisplaySearchedAccountInformation(contents);

                        // ask to user. are you sure to delete this account?
                        Console.SetCursorPosition(0, 14);
                        Console.Write("Delete (y/n) : ");
                        if (!ReadYesNoChoice())
                        {
                            // if user entered false (cancel the deleting account process), then finish the method
                            Console.WriteLine("\n\nDelete Account process has terminated.");
                            Console.WriteLine("Press enter to return to the main menu page...");
                            Console.ReadKey();
                            return;
                        }
                        // if user entered true (delete the file), then show the warnning message and read user input again
                        Console.Write("\n[WARNNING]: If the file is erased, everyting in the file disappears.\nDo you still want to continue? (y/n) : ");
                        if (!ReadYesNoChoice())
                        {
                            // if user entered false (cancel the deleting account process), then finish the method
                            Console.WriteLine("\n\nDelete Account process has terminated.");
                            Console.WriteLine("Press enter to return to the main menu page...");
                            Console.ReadKey();
                            return;
                        }
                        // if user pressed y (delete), then delete the file
                        try
                        {
                            Console.WriteLine("\nProcessing...");
                            File.Delete(tempAccountNumber + ".txt");
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            Console.WriteLine("[Error happened] -> Failed to delete the file\nPress Enter...");
                            Console.ReadKey();
                            return;
                        }
                        Console.WriteLine("\nAccount number {0} successfully deleted." +
                            "\nPress Enter to return to the main menu page...", tempAccountNumber);
                        Console.ReadKey();
                    }
                }
            } while (tempAccountNumber == -1);
        }
        private FileStream FileCreate(string fileName)
        {
            if (!fileName.Contains(".txt"))
            {
                // if file name don't have .txt format, then add .txt into the file name
                fileName += ".txt";
            }
            return new FileStream(fileName, FileMode.Create);
        }
        private FileStream FileOpen(string fileName)
        {
            if (!fileName.Contains(".txt"))
            {
                fileName += ".txt";
            }
            return new FileStream(fileName, FileMode.Open);
        }
        private void FileWrite(FileStream fs, string[] contents)
        {
            StreamWriter sw = new StreamWriter(fs);
            foreach (string content in contents)
            {
                sw.WriteLine(content);
            }
            sw.Close();
        }
        private void FileWrite(FileStream fs, List<string> contents)
        {
            StreamWriter sw = new StreamWriter(fs);
            foreach (string content in contents)
            {
                sw.WriteLine(content);
            }
            sw.Close();
        }
        private void FileWrite(FileStream fs, string content)
        {
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(content);
            sw.Close();
        }
        private List<string> FileReadAll(FileStream fs)
        {
            string content = "";
            List<string> contents = new List<string>();
            StreamReader sr = new StreamReader(fs);
            do
            {
                content = sr.ReadLine();
                if (content != null)
                {
                    contents.Add(content);
                }
            } while (content != null);
            sr.Close();
            return contents;
        }
        private string FileReadOneLine(FileStream fs)
        {
            string content = "";
            StreamReader sr = new StreamReader(fs);
            content = sr.ReadLine();
            sr.Close();
            return content;
        }
        private string FileReadAt(FileStream fs, int index)
        {
            string tempContent = "", realContent = "";
            StreamReader sr = new StreamReader(fs);
            for (int i = 1; i <= index; i++)
            {
                tempContent = sr.ReadLine();
                if (i == index)
                {
                    realContent = tempContent;
                }
            }
            sr.Close();
            return realContent;
        }
        private void FileClearAll(string fileName)
        {
            FileCreate(fileName).Close();
        }
        private string GetAvailableAccountNumber()
        {
            string fileName = "accountNumber";
            // read available account number from accountNumber.txt
            string accountNumber = FileReadOneLine(FileOpen(fileName));
            // set new account number
            // For example, if 100000 is used then set 100001
            FileClearAll(fileName);
            int newAccountNumber = Convert.ToInt32(accountNumber) + 1;
            FileWrite(FileOpen(fileName), newAccountNumber.ToString());
            return accountNumber;
        }
        private string ReadUserInput(int x, int y)
        {
            // x, y value is used to set cursor position
            Console.SetCursorPosition(x, y);
            return Console.ReadLine();
        }
        private bool IsPhoneNumberCorrect(string phoneNumber)
        {
            bool isCorrectPhoneNumber = true;
            int tempPhoneNumber;
            try
            {
                if (phoneNumber.Length > 10)
                {
                    throw new FormatException();
                }
                tempPhoneNumber = Convert.ToInt32(phoneNumber);
            }
            catch (FormatException)
            {
                isCorrectPhoneNumber = false;
            }
            catch (OverflowException)
            {
                isCorrectPhoneNumber = false;
            }
            return isCorrectPhoneNumber;
        }
        private bool IsEmailAddressCorrect(string emailAddress)
        {
            bool status = false;
            /* check situation if user only wrote
             * case1 -> @gmail.com
             * case2 -> @outlook.com
             * case3 -> @uts.edu.au
            */
            if (emailAddress.IndexOf('@') == 0)
            {
                return status;
            }
            // check email address format
            if (emailAddress.Contains("@"))
            {
                if (emailAddress.Contains("gmail.com"))
                {
                    status = true;
                    this.emailAddress = emailAddress;
                }
                else if (emailAddress.Contains("outlook.com"))
                {
                    status = true;
                    this.emailAddress = emailAddress;
                }
                else if (emailAddress.Contains("uts.edu.au"))
                {
                    status = true;
                    this.emailAddress = emailAddress;
                }
                else if (emailAddress.Contains("naver.com"))
                {
                    // testing purpose
                    status = true;
                    this.emailAddress = emailAddress;
                }
            }
            return status;
        }
        private bool ReadYesNoChoice()
        {
            bool choice;
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            if (consoleKeyInfo.Key == ConsoleKey.Y)
            {
                choice = true;
            }
            else if (consoleKeyInfo.Key == ConsoleKey.N)
            {
                choice = false;
            }
            else
            {
                Console.Write("\n[WARNNING] Enter (y/n): ");
                choice = ReadYesNoChoice();
            }
            return choice;
        }
    }
}