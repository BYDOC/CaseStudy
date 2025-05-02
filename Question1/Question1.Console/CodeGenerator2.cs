using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Question1.Console
{
    public static class CodeGenerator2
    {
        private const string CharSet = "ACDEFGHKLMNPRTXYZ234579"; // 23 characters
        private static readonly int Base = CharSet.Length;
        private const int CodeLength = 8;

        /// <summary>
        /// Generates a single 8-character promo code using a simple parity check.
        /// </summary>
        public static string Generate()
        {
            char[] payload = GeneratePayload();
            char checkChar = ComputeCheckChar(payload);
            return new string(payload) + checkChar;
        }

        /// <summary>
        /// Validates an 8-character code using simple parity check logic.
        /// </summary>
        public static bool IsValid(string code)
        {
            if (string.IsNullOrWhiteSpace(code) || code.Length != CodeLength || code.Any(c => !CharSet.Contains(c)))
                return false;

            string payload = code.Substring(0, 7);
            char expectedCheck = ComputeCheckChar(payload.ToCharArray());

            return code[7] == expectedCheck;
        }

        /// <summary>
        /// Generates a batch of unique codes.
        /// </summary>
        

        private static char[] GeneratePayload()
        {
            var payload = new char[7];
            using var rng = RandomNumberGenerator.Create();
            byte[] buffer = new byte[7];
            rng.GetBytes(buffer);

            for (int i = 0; i < 7; i++)
            {
                payload[i] = CharSet[buffer[i] % Base];
            }

            return payload;
        }

        /// <summary>
        /// Computes the 8th character as a parity check like Turkish ID logic.
        /// </summary>
        private static char ComputeCheckChar(char[] payload)
        {
            int sum = payload.Sum(c => CharSet.IndexOf(c));
            int checkIndex = sum % Base;
            return CharSet[checkIndex];
        }

        public static HashSet<string> GenerateBatch(int count)
        {
            var codes = new ConcurrentDictionary<string, byte>();
            int generated = 0;

            Parallel.For(0, Environment.ProcessorCount, _ =>
            {
                using var rng = RandomNumberGenerator.Create();
                var buffer = new byte[7];

                while (true)
                {
                    if (generated >= count) break;

                    rng.GetBytes(buffer);
                    char[] payload = new char[7];
                    for (int i = 0; i < 7; i++)
                        payload[i] = CharSet[buffer[i] % Base];

                    char check = ComputeCheckChar(payload);
                    string code = new string(payload) + check;

                    if (codes.TryAdd(code, 0))
                    {
                        if (System.Threading.Interlocked.Increment(ref generated) > count)
                            break;
                    }
                }
            });

            return codes.Keys.Take(count).ToHashSet();
        }
    }
}
