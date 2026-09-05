namespace SurveyBasket.Entities
{
    public class AuditableEntity
    {
        public string CreatedById { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;



        public string? UpdatedById { get; set; } 
        public DateTime? UpdatedOn { get; set; }



        public User CreatedBy { get; set; } = default!;




        public User? UpdatedBy { get; set; }
    }
}
