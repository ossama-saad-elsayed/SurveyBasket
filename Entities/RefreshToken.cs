using Microsoft.EntityFrameworkCore;

namespace SurveyBasket.Entities
{
    [Owned]
    public class RefreshToken
    {
        public  string Token {  get; set; } = string.Empty;
        public DateTime ExpiresOn { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? RevokOn { get; set; }
         public bool IsExpired => DateTime.UtcNow > ExpiresOn;

        public bool IsActive => RevokOn is null && ! IsExpired;
    }
}
