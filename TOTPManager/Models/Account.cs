using Framework.View;
using Newtonsoft.Json;
using System;

namespace TOTPManager.Models
{
    public class Account : NotifyBase
    {
        public Guid Id { get; }

        [JsonIgnore]
        private string _accountName;
        public string AccountName
        {
            get => _accountName;
            set => Set(ref _accountName, value);
        }

        public byte[] Secret { get; }

        public int Digits { get; }

        public Algorithm Algorithm { get; }

        [JsonIgnore]
        private double _percentTimeUntilKeyExpire;

        [JsonIgnore]
        public double PercentTimeUntilKeyExpire
        {
            get => _percentTimeUntilKeyExpire;
            set => Set(ref _percentTimeUntilKeyExpire, value);
        }

        [JsonIgnore]
        private string _currentKey;

        [JsonIgnore]
        public string CurrentKey
        {
            get => _currentKey;
            set => Set(ref _currentKey, value);
        }

        [JsonIgnore]
        private bool _willExpireSoon;

        [JsonIgnore]
        public bool WillExpireSoon
        {
            get => _willExpireSoon;
            set => Set(ref _willExpireSoon, value);
        }

        public Account(string accountName, byte[] secret, int digits = 6, Algorithm algorithm = Algorithm.SHA1)
        {
            Id = Guid.NewGuid();
            AccountName = accountName;
            Secret = secret;
            Digits = digits;
            Algorithm = algorithm;
        }
    }
}
