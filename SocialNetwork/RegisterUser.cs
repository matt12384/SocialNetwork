using FluentValidation;
using MediatR;
using SocialNetwork.Data;
using SocialNetwork.Data.Models;
using SocialNetwork.Security;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork
{
    public class RegisterUser
    {
        public class Command : IRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Email).EmailAddress().NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).NotEmpty();
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly AppDbContext _context;
            private readonly IPasswordHasher _hasher;

            public Handler(AppDbContext context, IPasswordHasher hasher)
                => (_context, _hasher) = (context, hasher);

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = new AppUser
                {
                    Email = request.Email,
                    Password = _hasher.Hash(request.Password),
                    FirstName = request.FirstName,
                    LastName = request.LastName
                };

                await _context.Users.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return default;
            }
        }
    }
}
