namespace VehicleManangement.Models
{
    public class Branch
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
    }
}
