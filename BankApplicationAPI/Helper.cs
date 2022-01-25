using System;

namespace BankApplication
{
    public class Utilities
    {
        public static (int name, string password) GetUserIdPassword(string message)
        {
            int id = GetInteger($"Enter your {message} ID number: ");
            string password = GetString($"Enter {message} password: ");
            return (id, password);
        }

        public static string GetString(string message, bool isRequired)
        {
            message.Print();
            return Console.ReadLine();
        }

        public static int GetInteger(string message)
        {
            if (int.TryParse(GetString(message), out int n)) return n;
            else
            {
                ("Input a valid number").Println();
                return GetInteger(message);
            }
        }

        public static double GetDouble(string message)
        {
            if (double.TryParse(GetString(message), out double n)) return n;
            else
            {
                ("Input a valid number").Println();
                return GetDouble(message);
            }
        }

    }

}
