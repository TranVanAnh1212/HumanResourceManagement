using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HRMana.Common.Commons
{
    public static class StringHelper
    {
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public static bool IsPhoneNumber(string text)
        {
            // Sử dụng biểu thức chính quy để kiểm tra xem chỉ có chữ số và ký tự '+' hay không
            Regex regex = new Regex("[^0-9+]");
            return !regex.IsMatch(text);
        }

        public static bool IsValidDate(string inputDate, string format)
        {
            // Kiểm tra xem chuỗi ngày có đúng định dạng không
            return DateTime.TryParseExact(inputDate, format,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out _);
        }

        public static decimal ConvertSalary(string text)
        {
            decimal salaryConvert = 0;
            if (!string.IsNullOrEmpty(text))
            {
                string[] a = text.Split('.');
                string b = string.Concat(a);
                salaryConvert = Convert.ToInt32(b);
            }

            return salaryConvert;
        }

        public static bool IsValidEmail(string email)
        {
            // Biểu thức chính quy để kiểm tra định dạng email
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            // Kiểm tra chuỗi sử dụng biểu thức chính quy
            return Regex.IsMatch(email, pattern);
        }

        public static bool IsNumeric(string input)
        {
            // Kiểm tra xem chuỗi có thể chuyển đổi thành số không
            return int.TryParse(input, out _);
        }

        public static bool CheckStringContainsLetter(string input)
        {
            foreach (char c in input)
            {
                if (char.IsLetter(c))
                {
                    return false; // Nếu có ký tự chữ cái, trả về false
                }
            }
            return true; // Nếu không có ký tự chữ cái, trả về true
        }
    }
}
