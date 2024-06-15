namespace POS_Application.Server.Helpers
{
    public static class Utilities
    {
        private static readonly Random random = new Random();
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string GenerateRandomId(int length)
        {
            var id = new char[length];
            for (int i = 0; i < length; i++)
            {
                id[i] = chars[random.Next(chars.Length)];
            }
            return new string(id);
        }
    }
}
