using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Security
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Validate(string password, string hashedPassword);
    }
}
