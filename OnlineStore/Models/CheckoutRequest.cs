using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class CheckoutRequest
    {
        [Required]

        public string UserId { get; set; }
    }
}
