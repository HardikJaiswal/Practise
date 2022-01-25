namespace APIDemo
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
    }
}
