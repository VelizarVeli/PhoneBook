using System.Text.RegularExpressions;

namespace PhoneBook.App.Core
{
    public class PhoneNumberCheck
    {
        public bool Check(string number)
        {
            string pattern = @"^(\+359|0)(87|88|89)[2-9][0-9]{6}$";

            string checkZeros = number.Substring(0, 2);

            if (checkZeros.ToString() == "00")
            {
                pattern = @"^(00359)(87|88|89)[2-9][0-9]{6}$";
            }

            Regex rgx = new Regex(pattern);

            if (rgx.IsMatch(number))
            {
                return true;
            }

            return false;
        }
    }
}
