using System.Threading;
using System.Threading.Tasks;
using VetTail.Infrastructure.Identity;

namespace VetTail.Infrastructure.Common.Interfaces;

public interface IAuthenticationService
{
    Task<User> AuthenticateAsync(string username, string password, CancellationToken cancellationToken =  default);
    Task SignInAsync(User user, bool rememberMe, CancellationToken cancellationToken = default);
    Task SignOutAsync(CancellationToken cancellationToken = default);
}
