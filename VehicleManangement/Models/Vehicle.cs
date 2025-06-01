namespace VehicleManangement.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public string? Year { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? LicencePlate { get; set; }
        public string? Color { get; set; }
        public string? Odometer { get; set; }
        public int? SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
        public int? BranchId { get; set; }
        public Branch? Branch { get; set; }
        public int? ClientId { get; set; }
        public Client? Client { get; set; }
        public int? DriverId { get; set; }
        public Driver? Driver { get; set; }
    }
}
