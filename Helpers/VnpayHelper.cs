using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace infertility_system.Helpers
{
    public class VnpayHelper
    {
        public static string HmacSHA512(string key, string input)
        {
            using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        public static string BuildQuery(SortedList<string, string> data)
        {
            return string.Join("&", data.Select(p =>
                $"{WebUtility.UrlEncode(p.Key)}={WebUtility.UrlEncode(p.Value)}"));
        }
    }
}
