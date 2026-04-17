using River_Pollution_Survey.Models.DBModels;

namespace River_Pollution_Survey.Models.ViewModels
{
    public class UserViewModel
    {

        public string? FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string? EmailAddress { get; set; }
        public int Contact { get; set; }
        public  Role? RoleId { get; set; }

    }
}
