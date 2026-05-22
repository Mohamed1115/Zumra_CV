using System.ComponentModel.DataAnnotations;
using Zumra.Data;

namespace Zumra.Models;

  public class PayFac
    {
        public int Id { get; set; }
    
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    
        [Required]
        public int FacilityId { get; set; }
        public Facility Facility { get; set; }
    
        [Required]
        public string status { get; set; }
    
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    
        [Required]
        public string StripeSessionId { get; set; }
    
        public decimal Amount { get; set; }
    }
