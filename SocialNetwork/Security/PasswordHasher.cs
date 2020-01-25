using Scrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly ScryptEncoder _encoder;

        public PasswordHasher() => _encoder = new ScryptEncoder();

        public string Hash(string password) => _encoder.Encode(password);

        public bool Validate(string password, string hashedPassword) => _encoder.Compare(password, hashedPassword);
    }
}
