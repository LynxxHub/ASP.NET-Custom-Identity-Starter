namespace ASP.NET_Custom_Identity_Starter.Areas.Identity.Pages.Admin
{
    public class AddRoleModel : PageModel
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        [BindProperty]
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [BindProperty]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [BindProperty]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        public AddRoleModel(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var role = new ApplicationRole(RoleName)
            {
                Description = Description,
                IsActive = IsActive
            };

            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                // TODO: Add success message or redirect to the roles list
                return RedirectToPage("./Roles");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
        }
    }
}
