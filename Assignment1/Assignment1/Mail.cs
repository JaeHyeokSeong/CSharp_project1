using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
namespace Assignment1
{
    public class Mail
    {
        // set contents in the linked list
        private List<string> accountInformation;
        string contents = "";
        public Mail(string[] contents)
        {
            foreach (string content in contents)
            {
                this.contents += content + "\n";
            }
        }
        public Mail(List<string> accountInformation)
        {
            this.accountInformation = accountInformation;
            WriteBodyContents();
        }
        public List<string> BodyContents
        {
            set { accountInformation = value; }
        }
        private void WriteBodyContents()
        {
            contents += "Hi " + accountInformation[3] + ",\n\n";
            contents += ("Account number: " + accountInformation[1] + "\n");
            contents += ("Balance: $" + accountInformation[2] + "\n");
            contents += ("First name: " + accountInformation[3] + "\n");
            contents += ("Last name: " + accountInformation[4] + "\n");
            contents += ("Address: " + accountInformation[5] + "\n");
            contents += ("Phone number: " + accountInformation[6] + "\n");
            contents += ("Email address: " + accountInformation[7] + "\n");
            // check is their more contents
            if (accountInformation.Count == 8)
            {
                contents += "\nYours sincerely,\nSIMPLE BANKING SYSTEM";
                return;
            }
            contents += "\nTotal Transaction Histories\n\n";
            for (int i = 8; i < accountInformation.Count; i++)
            {
                contents += accountInformation[i] + "\n";
            }
            string[][] sortedTransactions = SortTransactions(accountInformation);
            contents += "\n\nTotal Deposit Histories\n\n";
            foreach (string depositRecord in sortedTransactions[0])
            {
                contents += depositRecord + "\n";
            }
            contents += "Total Withdraw Histories\n\n";
            foreach (string withdrawRecord in sortedTransactions[1])
            {
                contents += withdrawRecord + "\n";
            }
            contents += "\nYours sincerely,\nSIMPLE BANKING SYSTEM";
        }
        private string[][] SortTransactions(List<string> transactions)
        {
            // transactions are started from index 8
            string[][] sortedTransactions = new string[2][];
            // index 0 contains deposit transactions array
            // index 1 contains withdraw transactions array
            sortedTransactions[0] = new string[transactions.Count]; // this array is used to record deposit record
            sortedTransactions[1] = new string[transactions.Count]; // this array is used to record withdraw record
            int i = 0, j = 0;
            foreach (string transaction in transactions)
            {
                if (transaction.Contains("DEPOSIT"))
                {
                    sortedTransactions[0][i++] = transaction;
                }
                else if (transaction.Contains("WITHDRAW"))
                {
                    sortedTransactions[1][j++] = transaction;
                }
            }
            return sortedTransactions;
        }
        public void SendMail()
        {
            try
            {
                var mail = new MailMessage()
                {
                    From = new MailAddress("seong5763@gmail.com"),
                    Subject = "Account Details",
                    Body = contents
                };
                // email address is saved at index 7
                mail.To.Add(new MailAddress(accountInformation[7]));

                var client = new SmtpClient()
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Credentials = new NetworkCredential("seong5763@gmail.com", "wvhliamlsscpkbki")
                };

                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in sending email: " + ex.Message);
                return;
            }

            Console.WriteLine("Email successfully sent");
        }
    }
}
