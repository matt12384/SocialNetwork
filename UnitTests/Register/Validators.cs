using FluentValidation.TestHelper;
using SocialNetwork;
using SocialNetwork.Data;
using SocialNetwork.Security;
using System.Threading;
using Xunit;

namespace UnitTests.Register
{
    public class Validators
    {
        private readonly AppDbContext _context;
        private RegisterUser.Validator _validator;
        private readonly RegisterUser.Handler _commandHandler;
        private readonly IPasswordHasher _hasher;

        public Validators()
        {
            _context = TestHarness.GetContext();
            _validator = new RegisterUser.Validator(_context);
            _hasher = new PasswordHasher();
            _commandHandler = new RegisterUser.Handler(_context, _hasher);
        }

        [Fact]
        public void ShouldHaveErrorWhenFirstNameIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.FirstName, null as string);
        }

        [Fact]
        public void ShouldHaveErrorWhenFirstNameIsEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.FirstName, "");
        }

        [Fact]
        public void ShouldNotHaveErrorWhenFirstNameIsPopulated()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.FirstName, "John");
        }

        [Fact]
        public void ShouldHaveErrorWhenLastNameIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.LastName, null as string);
        }

        [Fact]
        public void ShouldHaveErrorWhenLastNameIsEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.LastName, "");
        }

        [Fact]
        public void ShouldNotHaveErrorWhenLastNameIsPopulated()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.LastName, "Smith");
        }

        [Fact]
        public void ShouldHaveErrorWhenEmailIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Email, null as string);
        }

        [Fact]
        public void ShouldHaveErrorWhenEmailIsEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Email, "");
        }

        [Fact]
        public void ShouldHaveErrorWhenEmailIsNotValid()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Email, "123");
        }

        [Fact]
        public async void ShouldHaveErrorWhenEmailAlreadyExists()
        {
            var command = new RegisterUser.Command
            {
                Email = "john@smith.com",
                Password = "Password!1",
                FirstName = "John",
                LastName = "Smith"
            };

            await _commandHandler.Handle(command, new CancellationToken());

            _validator.ShouldHaveValidationErrorFor(x => x.Email, command);
        }

        [Fact]
        public void ShouldNotHaveErrorWhenEmailIsValid()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Email, "matt12384@live.co.uk");
        }

        [Fact]
        public void ShouldHaveErrorWhenPasswordIsNull()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Password, null as string);
        }

        [Fact]
        public void ShouldHaveErrorWhenPasswordIsEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Password, "");
        }

        [Fact]
        public void ShouldNotHaveErrorWhenPasswordIsPopulated()
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Password, "John");
        }

        [Fact]
        public void ShouldHaveErrorWhenPasswordsDoNotMatch()
        {
            var user = new RegisterUser.Command 
            {
                FirstName = "John",
                LastName = "Smith",
                Email = "matt12384@live.co.uk",
                Password = "Password1!",
                ConfirmPassword = "Password2!"
            };

            _validator.ShouldHaveValidationErrorFor(x => x.ConfirmPassword, user);
        }

        [Fact]
        public void ShouldNotHaveErrorWhenPasswordsDoMatch()
        {
            var user = new RegisterUser.Command
            {
                FirstName = "John",
                LastName = "Smith",
                Email = "matt12384@live.co.uk",
                Password = "Password1!",
                ConfirmPassword = "Password1!"
            };

            _validator.ShouldNotHaveValidationErrorFor(x => x.ConfirmPassword, user);
        }
    }
}
