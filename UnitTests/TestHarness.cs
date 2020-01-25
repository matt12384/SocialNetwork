using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using System;

namespace UnitTests
{
    internal class TestHarness
    {
        internal static AppDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                         .UseInMemoryDatabase(Guid.NewGuid().ToString())
                         .Options;

            return new AppDbContext(options);
        }
    }
}
