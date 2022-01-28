namespace APIDemo.Service
{
    public class Utilities
    {
        public static string GenerateUserId(string name)
        {
            return $"{name.Substring(0, 3)}{DateTime.Now}";
        }

        public static string GenerateTransactionId()
        {
            return $"TXN{DateTime.Now}";
        }

        public static string GeneratePassword()
        {
            return $"PWD{DateTime.Now.Millisecond}";
        }

        public static int GenerateAccountNumber()
        {
            return ((new Random()).Next(0, 100000) % 100000) + 100000;
        }
    }
}
