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
        private readonly IPasswordHasher _passworHasher;

        public UserService(AppDbContext context, IPasswordHasher passworHasher)
        {
            _context = context;
            _passworHasher = passworHasher;
        }

        public async Task<AuthenticatedUser> SignIn(User user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);

            if (dbUser == null 
                || _passworHasher.VerifyHashedPassword(dbUser.Password, user.Password) == PasswordVerificationResult.Failed)
            {
                throw new InvalidUsernamePaswwordException("Invalid username or password");
            }

            return new AuthenticatedUser
            {
                UserName = user.UserName,
                Token = JwtGenerator.GenerateUserToken(user.UserName),
            };
        }

        public async Task<AuthenticatedUser> SignUp(User user)
        {
            var checkUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(user.UserName));

            if (checkUser != null)
            {
                throw new UsernameAlreadyExistsException("Username already exists");
            }

            user.Password = _passworHasher.HashPassword(user.Password);

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return new AuthenticatedUser {
                UserName = user.UserName,
                Token = JwtGenerator.GenerateUserToken(user.UserName),
            }; 
        }
    }
}
