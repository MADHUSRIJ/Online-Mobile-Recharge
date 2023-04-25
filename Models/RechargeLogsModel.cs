using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Mobile_Recharge.Models
{
    public class RechargeLogsModel
    {
        [Key]
        public int RechargeLogId { get; set; }
        
        [Required]
        [ForeignKey("RechargePlans")]
        public int RechargePlanId { get; set;}

        [Required]
        [ForeignKey("UserDetails")]
        public int UserId { get; set;}

        [Required]
        public string RechargeDate { get; set;}

        [Required]
        public string RechargeValidity { get; set;}

        public virtual RechargePlansModel RechargePlans { get; set;}

        public virtual UserDetailsModel UserDetails { get; set;}

    }
}
