// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ASP.NET_Custom_Identity_Starter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASP.NET_Custom_Identity_Starter.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [StringLength(100, ErrorMessage = "First name cannot be longer than 100 characters.")]
            public string FirstName { get; set; }

            [StringLength(100, ErrorMessage = "Last name cannot be longer than 100 characters.")]
            public string LastName { get; set; }

            [DataType(DataType.Date)]
             public DateTime DateOfBirth { get; set; }

            [StringLength(50, ErrorMessage = "Gender cannot be longer than 50 characters.")]
            public string Gender { get; set; }

            [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters.")]
            public string Address { get; set; }

            [StringLength(100, ErrorMessage = "City cannot be longer than 100 characters.")]
            public string City { get; set; }

            [StringLength(100, ErrorMessage = "State cannot be longer than 100 characters.")]
            public string State { get; set; }

            [StringLength(100, ErrorMessage = "Country cannot be longer than 100 characters.")]
            public string Country { get; set; }

            [RegularExpression(@"^\d{5}$", ErrorMessage = "Invalid postal code format.")]
            public string PostalCode { get; set; } = "00000";

            [StringLength(100, ErrorMessage = "Preferred language cannot be longer than 100 characters.")]
            public string PreferredLanguage { get; set; }

            [StringLength(100, ErrorMessage = "Time zone cannot be longer than 100 characters.")]
            public string TimeZone { get; set; }

            [StringLength(50, ErrorMessage = "Theme cannot be longer than 50 characters.")]
            public string Theme { get; set; }

            public bool IsSubscribedToNewsletter { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                City = user.City,
                State = user.State,
                PostalCode = user.PostalCode,
                PreferredLanguage = user.PreferredLanguage,
                TimeZone = user.TimeZone,
                Theme = user.Theme,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                Country = user.Country
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }


            user.UpdateProfile(Input.FirstName, Input.LastName, Input.DateOfBirth, Input.Gender);
            user.UpdateContactInfo(Input.Address, Input.City, Input.State, Input.Country, Input.PostalCode);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                StatusMessage = "Your profile has been updated";
                return RedirectToPage();
            } else
            {
                StatusMessage = "Unexpected error when trying to update user information.";
                await LoadAsync(user);
                return Page();
            }

        }
    }
}
