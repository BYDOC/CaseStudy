using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;

namespace Question1.Console
{
    public static class CodeGenerator
    {
        private const string CharSet = "ACDEFGHKLMNPRTXYZ234579";
        private const int CodeLength = 8;
        private const string SecretKey = "SuperSecretKey";

        public static string Generate()
        {
            char[] payload = GeneratePayload();
            string signature = GenerateSignature(payload);
            return new string(payload) + signature;
        }
        private static char[] GeneratePayload()
        {
            var payload = new char[5];
            using var rng = RandomNumberGenerator.Create();
            byte[] buffer = new byte[5];

            rng.GetBytes(buffer);

            for (int i = 0; i < 5; i++)
            {
                payload[i] = CharSet[buffer[i] % CharSet.Length];
            }

            return payload;
        }

        private static string GenerateSignature(char[] payload)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(SecretKey));

            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(new string(payload)));

            var chars = new char[3];
            for (int i = 0; i < 3; i++)
                chars[i] = CharSet[hash[i] % CharSet.Length];

            return new string(chars);
        }
        public static bool IsValid(string code)
        {
            if (string.IsNullOrWhiteSpace(code) || code.Length != CodeLength || code.Any(c => !CharSet.Contains(c)))
                return false;

            string payload = code.Substring(0, 5);
            string signature = code.Substring(5, 3);
            return GenerateSignature(payload.ToCharArray()) == signature;
        }

        
    }

}
