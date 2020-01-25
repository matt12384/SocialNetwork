using FluentValidation.TestHelper;
using SocialNetwork;
using Xunit;

namespace UnitTests.Register
{
    public class Validators
    {
        private RegisterUser.Validator validator = new RegisterUser.Validator();

        [Fact]
        public void ShouldHaveErrorWhenFirstNameIsNull()
        {
            validator.ShouldHaveValidationErrorFor(x => x.FirstName, null as string);
        }

        [Fact]
        public void ShouldHaveErrorWhenFirstNameIsEmpty()
        {
            validator.ShouldHaveValidationErrorFor(x => x.FirstName, "");
        }

        [Fact]
        public void ShouldNotHaveErrorWhenFirstNameIsPopulated()
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.FirstName, "John");
        }

        [Fact]
        public void ShouldHaveErrorWhenLastNameIsNull()
        {
            validator.ShouldHaveValidationErrorFor(x => x.LastName, null as string);
        }

        [Fact]
        public void ShouldHaveErrorWhenLastNameIsEmpty()
        {
            validator.ShouldHaveValidationErrorFor(x => x.LastName, "");
        }

        [Fact]
        public void ShouldNotHaveErrorWhenLastNameIsPopulated()
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.LastName, "Smith");
        }

        [Fact]
        public void ShouldHaveErrorWhenEmailIsNull()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Email, null as string);
        }

        [Fact]
        public void ShouldHaveErrorWhenEmailIsEmpty()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Email, "");
        }

        [Fact]
        public void ShouldHaveErrorWhenEmailIsNotValid()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Email, "123");
        }

        [Fact]
        public void ShouldNotHaveErrorWhenEmailIsValid()
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.Email, "matt12384@live.co.uk");
        }

        [Fact]
        public void ShouldHaveErrorWhenPasswordIsNull()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Password, null as string);
        }

        [Fact]
        public void ShouldHaveErrorWhenPasswordIsEmpty()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Password, "");
        }

        [Fact]
        public void ShouldNotHaveErrorWhenPasswordIsPopulated()
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.Password, "John");
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

            validator.ShouldHaveValidationErrorFor(x => x.ConfirmPassword, user);
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

            validator.ShouldNotHaveValidationErrorFor(x => x.ConfirmPassword, user);
        }
    }
}
