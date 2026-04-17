namespace River_Pollution_Survey.Models.DBModels
{
    public class Waste
    {
        public Guid WasteId { get; set; }
        public int Quantity { get; set; }
        public string? WasteType { get; set; }
        public string? Location { get; set; }
        public string? Image { get; set; }
    }
}
