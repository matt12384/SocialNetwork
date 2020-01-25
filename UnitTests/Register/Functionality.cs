using Microsoft.EntityFrameworkCore;
using SocialNetwork;
using SocialNetwork.Data;
using SocialNetwork.Security;
using System.Threading;
using Xunit;

namespace UnitTests.Register
{
    public class Functionality
    {
        private readonly AppDbContext _context;
        private readonly RegisterUser.Handler _commandHandler;
        private readonly IPasswordHasher _hasher;

        public Functionality()
        {
            _context = TestHarness.GetContext();
            _hasher = new PasswordHasher();
            _commandHandler = new RegisterUser.Handler(_context, _hasher);
        }

        [Fact]
        public async void OnlyOneUserGetsRegistered()
        {
            var command = new RegisterUser.Command
            {
                Email = "john@smith.com",
                Password = "Password1!",
                FirstName = "John",
                LastName = "Smith",
                ConfirmPassword = "Password1!"
            };

            await _commandHandler.Handle(command, new CancellationToken());

            var users = await _context.Users.ToListAsync();
            var expectedNumberOfUsers = 1;

            Assert.Equal(expectedNumberOfUsers, users.Count);
        }

        [Fact]
        public async void RegisterUser()
        {
            var command = new RegisterUser.Command
            {
                Email = "john@smith.com",
                Password = "Password1!",
                FirstName = "John",
                LastName = "Smith",
                ConfirmPassword = "Password1!"
            };

            await _commandHandler.Handle(command, new CancellationToken());

            var user = await _context.Users.FirstAsync(x => x.Email == command.Email);

            Assert.NotNull(user);
            Assert.Equal(command.Email, user.Email);
            Assert.True(_hasher.Validate(command.Password, user.Password));
            Assert.Equal(command.FirstName, user.FirstName);
            Assert.Equal(command.LastName, user.LastName);
        }
    }
}
