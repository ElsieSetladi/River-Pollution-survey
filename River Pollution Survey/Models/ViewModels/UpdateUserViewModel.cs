using River_Pollution_Survey.Models.DBModels;

namespace River_Pollution_Survey.Models.ViewModels
{
    public class UpdateUserViewModel
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string? EmailAddress { get; set; }
        public int Contact { get; set; }
        public Role? RoleId { get; set; }
    }
}
