using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TOTPManager.Models;

namespace TOTP.Services.TOTP
{

    internal static class TotpHasher
    {
        internal static int Hash(byte[] secret, long iterationNumber, int digits = 6, Algorithm algorithm = Algorithm.SHA1)
        {
            //var key = Base32.ToBytes(secret);
            var hmac = GetAlgorithm(algorithm, secret);
            return Hash(hmac, iterationNumber, digits);
        }

        private static int Hash(HMAC hmac, long iterationNumber, int digits = 6)
        {
            var counter = BitConverter.GetBytes(iterationNumber);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(counter);
            }

            var hash = hmac.ComputeHash(counter);

            var offset = hash[hash.Length - 1] & 0xf;

            // Convert the 4 bytes into an integer, ignoring the sign.
            var binary =
                ((hash[offset] & 0x7f) << 24)
                | (hash[offset + 1] << 16)
                | (hash[offset + 2] << 8)
                | (hash[offset + 3]);

            var password = binary % (int)Math.Pow(10, digits);
            return password;
        }

        private static HMAC GetAlgorithm(Algorithm algorithm, byte[] secretBytes)
        {
            switch (algorithm)
            {
                case Algorithm.SHA1:
                    return new HMACSHA1(secretBytes);
                case Algorithm.SHA256:
                    return new HMACSHA256(secretBytes);
                case Algorithm.SHA512:
                    return new HMACSHA512(secretBytes);
                case Algorithm.MD5:
                    return new HMACMD5(secretBytes);
                default:
                    return new HMACSHA1(secretBytes);
            }
        }
    }
}
