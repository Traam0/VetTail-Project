using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VetTail.Web.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using VetTail.Infrastructure.Common.Interfaces;
using VetTail.Infrastructure.Identity;
using System.Threading.Tasks;
using System.Threading;
using VetTail.Domain.Common.Exceptions;
using System.Security.Authentication;

namespace VetTail.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthenticationService authenticationService;
        private readonly ILogger<AuthController> logger;

        public AuthController(IAuthenticationService authenticationService, ILogger<AuthController> logger)
        {
            this.authenticationService = authenticationService;
            this.logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(LoginVM viewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return View(viewModel);

            try
            {
                User user = await this.authenticationService.AuthenticateAsync(viewModel.Username, viewModel.Password, cancellationToken);
                await this.authenticationService.SignInAsync(user, viewModel.RememberMe, cancellationToken);
                string? returnUrl = Request.Form["ReturnUrl"];

                if (string.IsNullOrEmpty(returnUrl) || Url.IsLocalUrl(returnUrl) is false)
                {
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(returnUrl);
            }
            catch (EntityNullReferenceException exception)
            {
                this.logger.LogWarning("Failed Authentication Attempt: {Message}\n\t{@Crdentials}", exception.Message, viewModel);
                ModelState.AddModelError("Username", "Invalid Username");
                return View(viewModel);
            }
            catch (InvalidCredentialException exception)
            {
                this.logger.LogWarning("Failed Authentication Attempt: {Message}\n\t{@Credentials}", exception.Message, viewModel);
                ViewData.ModelState.AddModelError("Password", exception.Message);
                return View(viewModel);

            }
        }

        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            if (this.User.Identity?.IsAuthenticated is false)
            {
                return RedirectToAction("Index", "Home");
            }
            await this.authenticationService.SignOutAsync(cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }
}
