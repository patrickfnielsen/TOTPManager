using System;
using TOTPManager.Models;

namespace TOTP.Services.TOTP
{
	public class TOTPGenerator
	{
        private readonly byte[] _secret;
        private readonly Algorithm _algorithm;

        private readonly DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public readonly int Digits;
        public readonly double Period;
        public double TimeLeft => Period - (int)(DateTime.UtcNow.Subtract(unixEpoch).TotalSeconds % Period);

        public TOTPGenerator(byte[] secret, int digits, int period, Algorithm algorithm)
        {
            _secret = secret;
            Digits = digits;
            Period = period;
            _algorithm = algorithm;
        }

        public int Generate()
        {
            return TotpHasher.Hash(_secret, GetCurrentCounter(), Digits, _algorithm);
        }

        private long GetCurrentCounter()
        {
            return (long)(DateTime.UtcNow - unixEpoch).TotalSeconds / (long)Period;
        }
    }
}