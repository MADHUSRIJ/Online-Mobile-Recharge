using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Mobile_Recharge.Models
{
    public class WalletModel
    {
        [Key]
        public int WalletId { get; set; }

        [Required]
        [ForeignKey("UserDetails")]
        public int UserId { get; set; }

        [Required]
        public int Amount { get; set; } 

        public virtual UserDetailsModel UserDetails { get; set; }
    }
}
