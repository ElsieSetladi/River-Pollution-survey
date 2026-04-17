namespace River_Pollution_Survey.Models.DBModels
{
    public class User
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string? EmailAddress { get; set; }
        public int Contact {  get; set; }
        public virtual Site? Site { get; set; }
        public virtual Role? Role { get; set; }
    }
}
