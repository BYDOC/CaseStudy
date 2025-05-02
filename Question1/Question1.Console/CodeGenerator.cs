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
        private const int PAYLOAD_SIZE = 5;
        private const int SIGNATURE_SIZE = 3;

        public static string Generate()
        {
            char[] payload = GeneratePayload();
            string signature = GenerateSignature(payload);
            return new string(payload) + signature;
        }
        private static char[] GeneratePayload()
        {
            var payload = new char[PAYLOAD_SIZE];
            using var rng = RandomNumberGenerator.Create();
            byte[] buffer = new byte[PAYLOAD_SIZE];

            rng.GetBytes(buffer);

            for (int i = 0; i < PAYLOAD_SIZE; i++)
            {
                payload[i] = CharSet[buffer[i] % CharSet.Length];
            }

            return payload;
        }

        private static string GenerateSignature(char[] payload)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(SecretKey));

            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(new string(payload)));

            var chars = new char[SIGNATURE_SIZE];
            for (int i = 0; i < SIGNATURE_SIZE; i++)
                chars[i] = CharSet[hash[i] % CharSet.Length];

            return new string(chars);
        }
        public static bool IsValid(string code)
        {
            if (string.IsNullOrWhiteSpace(code) || code.Length != CodeLength || code.Any(c => !CharSet.Contains(c)))
                return false;

            string payload = code.Substring(0, PAYLOAD_SIZE);
            string signature = code.Substring(PAYLOAD_SIZE, SIGNATURE_SIZE);
            return GenerateSignature(payload.ToCharArray()) == signature;
        }

        
    }

}
