using IdentitySignInResult = Microsoft.AspNetCore.Identity.SignInResult;
namespace ASP.NET_Custom_Identity_Starter.Services
{

    public class CustomSignInManager : SignInManager<ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public CustomSignInManager(UserManager<ApplicationUser> userManager,
                                   IHttpContextAccessor contextAccessor,
                                   IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
                                   IOptions<IdentityOptions> optionsAccessor,
                                   ILogger<SignInManager<ApplicationUser>> logger,
                                   IAuthenticationSchemeProvider schemes,
                                   IUserConfirmation<ApplicationUser> confirmation) // Added this parameter
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation) // And passed it here
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        public override async Task<IdentitySignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                user.LogIn();
                await _userManager.UpdateAsync(user);
            }

            return await base.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        }

        public override async Task SignInAsync(ApplicationUser user, bool isPersistent, string authenticationMethod = null)
        {
            user.LogIn();
            await _userManager.UpdateAsync(user);
            await base.SignInAsync(user, isPersistent, authenticationMethod);
        }

        public override async Task SignOutAsync()
        {
            // Get the currently signed-in user
            ApplicationUser? user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);

            if (user != null)
            {
                user.LogOut();
                await _userManager.UpdateAsync(user);
            }

            // Now call the base sign-out logic
            await base.SignOutAsync();
        }
    }
}