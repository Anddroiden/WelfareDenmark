using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace WelfareDenmark.Models {
    public class ApplicationUser : IdentityUser {
        public ApplicationUser() {
            Patients = new HashSet<ApplicationUser>();
        }

        [DisplayName("Patienter")]
        public virtual ICollection<ApplicationUser> Patients { get; set; }
    }
}