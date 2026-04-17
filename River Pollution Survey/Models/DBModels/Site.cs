namespace River_Pollution_Survey.Models.DBModels
{
    public class Site
    {
        public Guid SiteId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image {  get; set; }
        public string? Location { get; set; }
        public string? Action {  get; set; }
        public string? Recommendation { get; set; }
        public virtual Waste? Waste { get; set; }

    }
}
