namespace ASP.NET_Custom_Identity_Starter.Models
{
    public class ApplicationUser : IdentityUser
    {

        // Profile Information
        private string _firstName;
        private string _lastName;
        private DateTime _dateOfBirth;
        private string _profilePictureUrl;
        private string? _gender;  // Consider using an Enum if you have a predefined set of genders.

        public string FirstName => _firstName;
        public string LastName => _lastName;
        public DateTime DateOfBirth => _dateOfBirth;
        public string ProfilePictureUrl => _profilePictureUrl;
        public string? Gender => _gender;

        // Contact Information
        private string _address;
        private string _city;
        private string _state;
        private string _country;
        private string _postalCode;

        public string? Address => _address;
        public string? City => _city;
        public string? State => _state;
        public string? Country => _country;
        public string? PostalCode => _postalCode;

        // Account Metadata
        public DateTime RegistrationDate { get; private set;  } = DateTime.UtcNow;
        public DateTime? LastLoginDate { get; private set; }
        public bool IsProfileComplete { get; private set; } = false;

        // Preferences
        private string _preferredLanguage;
        private string _timeZone;
        private string _theme;

        public string? PreferredLanguage => _preferredLanguage;
        public string? TimeZone => _timeZone;
        public string? Theme => _theme;

        // Custom Flags or Toggles
        public bool IsSubscribedToNewsletter { get; private set; } = false;
        public bool IsAccountLocked { get; private set; } = false;
        public bool IsLoggedIn { get; private set; } = false;

        // Update methods
        public void UpdateProfile(string firstName, string lastName, DateTime dateOfBirth,
            string profilePictureUrl, string? gender = null)
        {
            _firstName = firstName;
            _lastName = lastName;
            _dateOfBirth = dateOfBirth;
            _profilePictureUrl = profilePictureUrl;
            _gender = gender;
        }

        public void UpdateContactInfo(string address, string city, string state, string country, string postalCode)
        {
            _address = address;
            _city = city;
            _state = state;
            _country = country;
            _postalCode = postalCode;
        }

        public void UpdatePreferences(string preferredLanguage, string timeZone, string theme)
        {
            _preferredLanguage = preferredLanguage;
            _timeZone = timeZone;
            _theme = theme;
        }

        public void LogIn()
        {
            LastLoginDate = DateTime.UtcNow;
            IsLoggedIn = true;
        }

        public void LogOut()
        {
            IsLoggedIn = false;
        }

        public void ToggleSubscription()
        {
            IsSubscribedToNewsletter = !IsSubscribedToNewsletter;
        }

        public void LockAccount()
        {
            IsAccountLocked = true;
        }

        public void UnlockAccount()
        {
            IsAccountLocked = false;
        }

        public void CompleteProfile()
        {
            IsProfileComplete = true;
        }
    }
}

