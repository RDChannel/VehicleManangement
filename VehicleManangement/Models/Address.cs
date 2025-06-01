namespace VehicleManangement.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? ComplexNumber { get; set; }
        public string? ComplexName { get; set; }
        public int? StreetNumber { get; set; }
        public string? StreetName { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public int? CountryId { get; set; }
        public Country? Country { get; set; }
        public string? LinkedParentId { get; set; }
    }
}
