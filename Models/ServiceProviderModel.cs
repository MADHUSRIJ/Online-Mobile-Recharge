using System.ComponentModel.DataAnnotations;

namespace Online_Mobile_Recharge.Models
{
    public class ServiceProviderModel
    {
        [Key]
        public int ServiceProviderId { get; set; }

        [Required]
        public string ServiceName { get; set; }

        public virtual ICollection<UserDetailsModel> UserDetails { get; set; }

        public virtual ICollection<RechargePlansModel> RechargePlans { get; set; }


    }
}
