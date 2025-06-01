namespace VehicleManangement.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? TradingName { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? ContactName {  get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
    }
}
