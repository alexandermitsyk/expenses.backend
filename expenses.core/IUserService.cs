using expenses.core.DTO;
using expenses.db;

namespace expenses.core
{
    public interface IUserService
    {
        Task<AuthenticatedUser> SignUp(User user);

        Task<AuthenticatedUser> SignIn(User user);

        Task<AuthenticatedUser> ExternalSignIn(User user);
    }
}
