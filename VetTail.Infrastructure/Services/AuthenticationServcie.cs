using Microsoft.AspNetCore.Identity;
using System.Security.Authentication;
using System.Security.Cryptography.Xml;
using System.Threading;
using System.Threading.Tasks;
using VetTail.Domain.Common.Exceptions;
using VetTail.Infrastructure.Common.Interfaces;
using VetTail.Infrastructure.Identity;

namespace VetTail.Infrastructure.Services;

public sealed class AuthenticationServcie : IAuthenticationService
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;

    public AuthenticationServcie(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }   

    public async Task<User> AuthenticateAsync(string username, string password, CancellationToken cancellationToken = default)
    {
        await this.userManager.CreateAsync(user, password);
        
        
        return await Task.Run(async () =>
        {
            User user = await this.userManager.FindByNameAsync(username) ?? throw EntityNullReferenceException.Build<User, string>(nameof(username), username);
            SignInResult attempt = await this.signInManager.CheckPasswordSignInAsync(user, password, false);
            if (attempt.Succeeded) return user;
            throw new InvalidCredentialException("Invalid username or password");
        }, cancellationToken);
    }

    public async Task SignInAsync(User user, bool rememberMe, CancellationToken cancellationToken = default)
    {
       await Task.Run(async () => await this.signInManager.SignInAsync(user, rememberMe), cancellationToken);
    }

    public async Task SignOutAsync(CancellationToken cancellationToken = default)
    {
        await Task.Run(this.signInManager.SignOutAsync, cancellationToken);
    }
}
