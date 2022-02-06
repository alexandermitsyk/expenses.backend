using expenses.core.CustomException;
using expenses.core.DTO;
using expenses.core.Utilities;
using expenses.db;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;

namespace expenses.core
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(AppDbContext context, IPasswordHasher passworHasher)
        {
            _context = context;
            _passwordHasher = passworHasher;
        }

        public async Task<AuthenticatedUser> SignIn(User user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);

            if (dbUser == null 
                || _passwordHasher.VerifyHashedPassword(dbUser.Password, user.Password) == PasswordVerificationResult.Failed)
            {
                throw new InvalidUsernamePaswwordException("Invalid username or password");
            }

            return new AuthenticatedUser
            {
                UserName = user.UserName,
                Token = JwtGenerator.GenerateAuthToken(user.UserName),
            };
        }

        public async Task<AuthenticatedUser> SignUp(User user)
        {
            var checkUsername = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName.Equals(user.UserName));

            if (checkUsername != null)
            {
                throw new UsernameAlreadyExistsException("Username already exists");
            }

            if (!string.IsNullOrEmpty(user.Password))
            {
                user.Password = _passwordHasher.HashPassword(user.Password);
            }

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return new AuthenticatedUser
            {
                UserName = user.UserName,
                Token = JwtGenerator.GenerateAuthToken(user.UserName)
            };

        }
        public async Task<AuthenticatedUser> ExternalSignIn(User user)
        {
            var dbUser = await _context.Users
                .FirstOrDefaultAsync(u => u.ExternalId.Equals(user.ExternalId) && u.ExternalType.Equals(user.ExternalType));

            if (dbUser == null)
            {
                user.UserName = CreateUniqueUsernameFromEmail(user.Email);
                return await SignUp(user);
            }

            return new AuthenticatedUser()
            {
                UserName = dbUser.UserName,
                Token = JwtGenerator.GenerateAuthToken(dbUser.UserName),
            };
        }

        private string CreateUniqueUsernameFromEmail(string email)
        {
            var emailSplit = email.Split('@').First();
            var random = new Random();
            var username = emailSplit;

            while (_context.Users.Any(u => u.UserName.Equals(username)))
            {
                username = emailSplit + random.Next(10000000);
            }

            return username;
        }

    }
}
