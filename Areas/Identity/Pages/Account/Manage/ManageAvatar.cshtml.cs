using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Win32.SafeHandles;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Custom_Identity_Starter.Areas.Identity.Pages.Account.Manage
{
    public class ManageAvatar : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _environment;

        public ManageAvatar(UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _environment = environment;
        }

        [BindProperty]
        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Please upload a profile picture by clicking on the current profile picture.")]
        public IFormFile UploadedAvatar { get; set; }

        public string CurrentAvatarPath { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await SetCurrentAvatarPathAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (UploadedAvatar == null || UploadedAvatar.Length == 0)
            {
                ModelState.AddModelError("", "Invalid file.");

                if (!ModelState.IsValid)
                {
                    await SetCurrentAvatarPathAsync();
                    return Page();
                }
            }

            var supportedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
            if (!supportedTypes.Contains(UploadedAvatar.ContentType))
            {
                ModelState.AddModelError("UploadedAvatar", "Invalid file type.");
                if (!ModelState.IsValid)
                {
                    await SetCurrentAvatarPathAsync();
                    return Page();
                }
            }


            ApplicationUser updatedUser = await UpdateUser();

            await _userManager.UpdateAsync(updatedUser);

            return RedirectToPage();
        }

        private async Task<ApplicationUser> UpdateUser()
        {
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(UploadedAvatar.FileName);
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "img/avatars");
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);


            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await UploadedAvatar.CopyToAsync(fileStream);
            }

            var user = await _userManager.GetUserAsync(User);
            if (!string.IsNullOrEmpty(user.ProfilePictureUrl))
            {
                // Optionally, delete the old avatar from the system
                var oldAvatarPath = Path.Combine(_environment.WebRootPath, user.ProfilePictureUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldAvatarPath))
                {
                    System.IO.File.Delete(oldAvatarPath);
                }
            }

            string path = $"/img/avatars/{uniqueFileName}";
            user.UpdateProfilePicture(path);

            return user;
        }

        private async Task SetCurrentAvatarPathAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null) 
            CurrentAvatarPath = user.ProfilePictureUrl;
        }
    }
}
