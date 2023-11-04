namespace ASP.NET_Custom_Identity_Starter.Areas.Identity.Pages.Admin
{
    public class RolesModel : PageModel
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RolesModel(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;

        }

        [BindProperty]
        public IList<ApplicationRole> Roles { get; set; }

        public async Task OnGetAsync()
        {
            Roles = await _roleManager.Roles.ToListAsync();

        }

        public async Task<IActionResult> OnPostDeleteAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    // TODO: Add success message
                }
                else
                {
                    // TODO: Add error message
                }
            }
            else
            {
                // TODO: Add not found message
            }

            return RedirectToPage();
        }
    }
}
