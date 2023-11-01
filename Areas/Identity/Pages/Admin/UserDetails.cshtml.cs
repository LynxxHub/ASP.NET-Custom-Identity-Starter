namespace ASP.NET_Custom_Identity_Starter.Areas.Identity.Pages.Admin
{
    public class UserDetailsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserDetailsModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public ApplicationUser? AppUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound("User ID is missing.");
            }

            AppUser = await _userManager.FindByIdAsync(userId);

            if (User == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            return Page();
        }

    }
}