using Microsoft.AspNetCore.Identity;

namespace IdentityManager.Data
{
    // Add profile data for application roles by adding properties to the ApplicationRole class
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
