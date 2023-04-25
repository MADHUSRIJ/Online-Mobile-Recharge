using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Mobile_Recharge.Models
{
    public class UserDetailsModel

    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public int Number { get; set; }

        [ForeignKey("ServiceProvider")]
        public int ServiceProviderId { get; set; }

        [Required]
        [ForeignKey("RechargePlans")]
        public int RechargePlanId { get; set; }

        [Required]
        public string MailId { get; set; }

        [Required]
        public string Password { get; set; }


        public virtual ServiceProviderModel ServiceProvider { get; set; }

        public virtual RechargePlansModel RechargePlans { get; set; }
        
        public virtual ICollection<RechargeLogsModel> RechargeLogs { get; set; }


    }
}
