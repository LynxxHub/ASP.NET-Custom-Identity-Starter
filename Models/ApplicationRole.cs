namespace ASP.NET_Custom_Identity_Starter.Models
{
    public class ApplicationRole : IdentityRole
    {
        // Basic attributes
        public string? Description { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; set; }
        public bool IsActive { get; set; } = true;

        // Constructor
        public ApplicationRole() : base() { }

        public ApplicationRole(string roleName) : base(roleName) { }

        // Methods

        /// <summary>
        /// Deactivates the role. This doesn't delete the role, but marks it as inactive.
        /// </summary>
        public void Deactivate()
        {
            IsActive = false;
            UpdateModifiedDate();
        }

        /// <summary>
        /// Activates the role if it was previously deactivated.
        /// </summary>
        public void Activate()
        {
            IsActive = true;
            UpdateModifiedDate();
        }

        /// <summary>
        /// Updates the role's modified date to the current time.
        /// </summary>
        private void UpdateModifiedDate()
        {
            DateModified = DateTime.UtcNow;
        }
    }
}
