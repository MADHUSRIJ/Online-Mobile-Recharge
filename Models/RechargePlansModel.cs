using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Mobile_Recharge.Models
{
    public class RechargePlansModel
    {
        [Key]
        public int RechargePlanId { get; set; }

        [Required]
        [ForeignKey("ServiceProvider")]
        public int ServiceProviderId { get; set; }

        [Required]
        public int RechargePlanName { get; set; }

        [Required]
        public int RechargePlanValidity { get; set; }

        [Required]
        public int RechargePlanPrice { get; set; }

        [Required]
        public int RechargePlanData { get; set; }

        public virtual ServiceProviderModel ServiceProvider { get; set; }

        public virtual ICollection<UserDetailsModel> UserDetails { get; set; }

        public virtual ICollection<RechargeLogsModel> RechargeLogs { get; set; }




    }
}
