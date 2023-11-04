using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP.NET_Custom_Identity_Starter.Areas.Identity.Pages.Admin
{
    public class RoleDetailsModel : PageModel
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager; // Assuming you have an ApplicationUser class

        public ApplicationRole Role { get; set; }
        public IList<ApplicationUser> AssignedUsers { get; set; }

        public RoleDetailsModel(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string roleId)
        {
            Role = await _roleManager.FindByIdAsync(roleId);


            if (Role == null)
            {
                return NotFound();
            }

            var usersInRole = await _userManager.GetUsersInRoleAsync(Role.Name);
            AssignedUsers = usersInRole.ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string roleId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<ApplicationRole>(
                role,
                "Role", // Prefix for form value.
                r => r.Name, r => r.Description, a => a.IsActive))
            {

                await _roleManager.UpdateAsync(role);
                // TODO: Add success message
                return RedirectToPage("./Roles");
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
