using System;
using System.Security.Cryptography;
using TOTPManager.Models;

namespace TOTPManager.Services.TOTP
{
	public class TOTPGenerator
	{
        private readonly byte[] _secret;
        private readonly Algorithm _algorithm;
        private readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public readonly int Digits;
        public readonly double Period;
        public double TimeLeft => Period - (int)(DateTime.UtcNow.Subtract(_unixEpoch).TotalSeconds % Period);

        public TOTPGenerator(byte[] secret, int digits, int period, Algorithm algorithm)
        {
            _secret = secret;
            Digits = digits;
            Period = period;
            _algorithm = algorithm;
        }

        public int Generate()
        {
            return Hash(_secret, Digits, _algorithm);
        }

        internal int Hash(byte[] secret, int digits = 6, Algorithm algorithm = Algorithm.SHA1)
        {
            var hmac = GetAlgorithm(algorithm, secret);
            return Hash(hmac, digits);
        }

        private int Hash(HMAC hmac, int digits = 6)
        {
            var iterationNumber = GetCurrentCounter();
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

        private long GetCurrentCounter()
        {
            return (long)(DateTime.UtcNow - _unixEpoch).TotalSeconds / (long)Period;
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