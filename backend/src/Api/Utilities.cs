using System.Net.Mail;

namespace Api
{
    public static class Utilities
    {
        public static string DateFormat => "dd/MM/yyyy HH:mm:ss";

        public static string LongDateFormat => "dd/MM/yyyy HH:mm:ss.ffffff";

        public static bool ContainAtLeastOneDigit(string text)
        {
            for (int i = 0; i <= 9; i++)
            {
                if (text.Contains(i.ToString()))
                    return true;
            }
            return false;
        }

        public static bool ContainAtLeastOneUppercase(string text)
        {
            for (int i = 65; i <= 90; i++)
            {
                char c = (char)i;

                if (text.Contains(c.ToString()))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ContainAtLeastOneLowercase(string text)
        {
            for (int i = 97; i <= 122; i++)
            {
                char c = (char)i;

                if (text.Contains(c.ToString()))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ContainAtLeastOneSpecialCharacter(string text)
        {
            if (!(text.Contains("@") || text.Contains("#")
            || text.Contains("!") || text.Contains("~")
            || text.Contains("$") || text.Contains("%")
            || text.Contains("^") || text.Contains("&")
            || text.Contains("*") || text.Contains("(")
            || text.Contains(")") || text.Contains("-")
            || text.Contains("+") || text.Contains("/")
            || text.Contains(":") || text.Contains(".")
            || text.Contains(", ") || text.Contains("<")
            || text.Contains(">") || text.Contains("?")
            || text.Contains("|")))
            {
                return false;
            }
            return true;
        }

        public static bool BeValidEmail(string email) => IsValidEmail(email);

        public static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
