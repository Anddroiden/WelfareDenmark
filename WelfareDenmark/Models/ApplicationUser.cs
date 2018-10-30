using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WelfareDenmark.Models {
    public class ApplicationUser : IdentityUser {
        public ApplicationUser() {
            Patients = new HashSet<ApplicationUser>();
        }

        public virtual ICollection<ApplicationUser> Patients { get; set; }
    }
}