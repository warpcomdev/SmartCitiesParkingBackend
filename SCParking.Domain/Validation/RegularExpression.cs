using System;
using System.Globalization;
using System.Text;

namespace SCParking.Domain.Validation
{
    public static class RegularExpression
    {
        public static string Email = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public static string Numeric = @"^\d+$";

        public static string Password = @"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,32}$";

        public static string Phone = @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$";


        public static  string RemoveAccents(string value)
        {
            try
            {
                if (value == null) return "";
                StringBuilder sbReturn = new StringBuilder();
                var arrayText = value.Normalize(NormalizationForm.FormD).ToCharArray();
                foreach (char letter in arrayText)
                {
                    if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                        sbReturn.Append(letter);
                }
                return sbReturn.ToString();
            }
            catch (Exception)
            {
                return value;
            }
        }

        
    }
}
