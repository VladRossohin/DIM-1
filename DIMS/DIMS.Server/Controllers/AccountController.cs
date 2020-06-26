using DIMS.BL.Infrastructure;
using DIMS.BL.Interfaces;
using DIMS.BL.Models;
using DIMS.BL.Services;
using DIMS.Server.Models;
using Email.Interfaces;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DIMS.Server.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthService<UserDTO> _authService;
        private readonly ISender _senderService;
        
        public AccountController(IUserService userService, IAuthService<UserDTO> authService, ISender sender)
        {
            _userService = userService;
            _authService = authService;
            _senderService = sender;
        }

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel viewModel)
        {
            //await SetInitialDataAsync().ConfigureAwait(false);
            if (ModelState.IsValid)
            {
                var userDto = new UserDTO { Email = viewModel.Email, Password = viewModel.Password };

                ClaimsIdentity claim = await _userService.Authenticate(userDto).ConfigureAwait(false);

                if (claim == null)
                {
                    ModelState.AddModelError("", "Invalid login or password.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(viewModel);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Register(RegisterViewModel viewModel)
        {
            //await SetInitialDataAsync().ConfigureAwait(false);

            if (ModelState.IsValid)
            {
                var userDto = new UserDTO
                {
                    Email = viewModel.Email,
                    Password = viewModel.Password,
                    Address = viewModel.Address,
                    Name = viewModel.Name,
                    Role = "user"
                };

                OperationDetails operationDetails = await _userService.Create(userDto).ConfigureAwait(false);

                if (operationDetails.Succedeed)
                {
                    await SendEmailConfirmationTokenAsync(userDto);
                    ViewBag.Message =
                        "To complete the registration, check the email and click " +
                        "on the link indicated in the letter. You must be confirmed "
                        + "before you can log in.";
                    return View("SuccessRegister");
                }
                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                }
            }

            return View(viewModel);
        }

        private async Task<string> SendEmailConfirmationTokenAsync(UserDTO user)
        {
            var token = await _authService.GenerateTokenAsync(user).ConfigureAwait(false);

            var callbackUrl = Url.Action(
                            "ConfirmEmail",
                            "Account",
                            new { Username = user.Email, Token = token },
                            protocol: Request.Url.Scheme);

            await _senderService.MessageToUserAsync(user, "Account confirmation",
                    $"<span>Please, confirm your account by following this </span>" +
                    $"<a href='{callbackUrl}'>link</a>");

            return callbackUrl;
        }

        private async Task SetInitialDataAsync()
        {
            await _userService.SetInitialData(new UserDTO
            {
                Email = "alex.meleschenko@gmail.com",
                UserName = "Alex",
                Password = "superadmin",
                Name = "Alex",
                Address = "Revoljucionnaja 11-301",
                Role = "admin",
            }, new List<string> { "user", "admin" }).ConfigureAwait(false);
        }
    }
}