using System;
using System.Linq;

namespace Rishvi.Modules.Core.Helpers
{
    public class StringHelper
    {
        private static readonly Random Random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        public static string PrepareAddressLine3(string city, string state, string zip)
        {
            string addressLine3 = !string.IsNullOrWhiteSpace(city) ? $"{city}, " : string.Empty;

            addressLine3 += !string.IsNullOrWhiteSpace(state) ? $"{state}, " : string.Empty;

            if (!string.IsNullOrWhiteSpace(zip))
            {
                addressLine3 += $"{zip}, ";
            }

            if (!string.IsNullOrWhiteSpace(addressLine3) && addressLine3.Length > 1)
            {
                addressLine3 = addressLine3.Substring(0, addressLine3.Length - 2);
            }

            return addressLine3;
        }

        public static string PrepareAddressLine1(string addressLine1, string addressLine2)
        {
            string addressLine = !string.IsNullOrWhiteSpace(addressLine1) ? $"{addressLine1}, " : string.Empty;

            addressLine += !string.IsNullOrWhiteSpace(addressLine2) ? $"{addressLine2}, " : string.Empty;


            if (!string.IsNullOrWhiteSpace(addressLine) && addressLine.Length > 1)
            {
                addressLine = addressLine.Substring(0, addressLine.Length - 2);
            }

            return addressLine;
        }
    }
}