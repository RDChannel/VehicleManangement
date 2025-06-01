using Microsoft.EntityFrameworkCore;
using VehicleManangement.Data;

namespace VehicleManangement.Models
{
    public class Seed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new VehicleManangementContext(
                serviceProvider.GetRequiredService<DbContextOptions<VehicleManangementContext>>()))
            {
                // Skip if already seeded
                if (context.Vehicle.Any()) return;

                // Countries
                var countries = new List<Country>
                {
                    new Country { Name = "South Africa" },
                    new Country { Name = "Namibia" },
                    new Country { Name = "Botswana" },
                    new Country { Name = "Zimbabwe" }
                };
                context.Country.AddRange(countries);
                context.SaveChanges();

                // Addresses
                var addresses = new List<Address>
                {
                    new Address { Name = "HQ", StreetNumber = 1, StreetName = "Main St", City = "Cape Town", State = "Western Cape", PostalCode = "8001", CountryId = countries[0].Id },
                    new Address { Name = "Branch North", StreetNumber = 22, StreetName = "North Rd", City = "Windhoek", State = "Khomas", PostalCode = "1000", CountryId = countries[1].Id },
                    new Address { Name = "Client Office", StreetNumber = 35, StreetName = "Market Rd", City = "Gaborone", State = "South-East", PostalCode = "0001", CountryId = countries[2].Id },
                    new Address { Name = "Supplier Yard", StreetNumber = 7, StreetName = "Industrial Rd", City = "Harare", State = "Harare", PostalCode = "2000", CountryId = countries[3].Id }
                };
                context.Address.AddRange(addresses);
                context.SaveChanges();

                // Branches
                var branches = new List<Branch>
                {
                    new Branch { Name = "Cape Town Branch", Description = "Main branch", RegistrationNumber = "BR001", PhoneNumber = "0211111111", EmailAddress = "capetown@company.com", AddressId = addresses[0].Id },
                    new Branch { Name = "Namibia Branch", Description = "Windhoek office", RegistrationNumber = "BR002", PhoneNumber = "0612222222", EmailAddress = "namibia@company.com", AddressId = addresses[1].Id },
                    new Branch { Name = "Botswana Branch", Description = "Gaborone office", RegistrationNumber = "BR003", PhoneNumber = "0313333333", EmailAddress = "botswana@company.com", AddressId = addresses[2].Id },
                    new Branch { Name = "Zimbabwe Branch", Description = "Harare office", RegistrationNumber = "BR004", PhoneNumber = "0414444444", EmailAddress = "zimbabwe@company.com", AddressId = addresses[3].Id }
                };
                context.Branch.AddRange(branches);
                context.SaveChanges();

                // Clients
                var clients = new List<Client>
                {
                    new Client { Name = "Client A", Description = "Client A Desc", RegistrationNumber = "CL001", ContactName = "Alice", PhoneNumber = "0811111111", EmailAddress = "alice@client.com", AddressId = addresses[2].Id },
                    new Client { Name = "Client B", Description = "Client B Desc", RegistrationNumber = "CL002", ContactName = "Bob", PhoneNumber = "0822222222", EmailAddress = "bob@client.com", AddressId = addresses[2].Id },
                    new Client { Name = "Client C", Description = "Client C Desc", RegistrationNumber = "CL003", ContactName = "Carol", PhoneNumber = "0833333333", EmailAddress = "carol@client.com", AddressId = addresses[2].Id },
                    new Client { Name = "Client D", Description = "Client D Desc", RegistrationNumber = "CL004", ContactName = "Dave", PhoneNumber = "0844444444", EmailAddress = "dave@client.com", AddressId = addresses[2].Id }
                };
                context.Client.AddRange(clients);
                context.SaveChanges();

                // Suppliers
                var suppliers = new List<Supplier>
                {
                    new Supplier { Name = "Supplier 1", TradingName = "S1", Description = "Cars", RegistrationNumber = "SUP001", ContactName = "Sam", PhoneNumber = "0741111111", EmailAddress = "sam@supplier.com", AddressId = addresses[3].Id },
                    new Supplier { Name = "Supplier 2", TradingName = "S2", Description = "Vans", RegistrationNumber = "SUP002", ContactName = "Sue", PhoneNumber = "0742222222", EmailAddress = "sue@supplier.com", AddressId = addresses[3].Id },
                    new Supplier { Name = "Supplier 3", TradingName = "S3", Description = "Trucks", RegistrationNumber = "SUP003", ContactName = "Sid", PhoneNumber = "0743333333", EmailAddress = "sid@supplier.com", AddressId = addresses[3].Id },
                    new Supplier { Name = "Supplier 4", TradingName = "S4", Description = "Bikes", RegistrationNumber = "SUP004", ContactName = "Sally", PhoneNumber = "0744444444", EmailAddress = "sally@supplier.com", AddressId = addresses[3].Id }
                };
                context.Supplier.AddRange(suppliers);
                context.SaveChanges();

                // Drivers
                var drivers = new List<Driver>
                {
                    new Driver { Name = "Driver 1", Surname = "One", LicenceNumber = "D001", PhoneNumber = "0731111111", EmailAddress = "driver1@company.com", AddressId = addresses[2].Id, ClientId = clients[0].Id },
                    new Driver { Name = "Driver 2", Surname = "Two", LicenceNumber = "D002", PhoneNumber = "0732222222", EmailAddress = "driver2@company.com", AddressId = addresses[2].Id, ClientId = clients[1].Id },
                    new Driver { Name = "Driver 3", Surname = "Three", LicenceNumber = "D003", PhoneNumber = "0733333333", EmailAddress = "driver3@company.com", AddressId = addresses[2].Id, ClientId = clients[2].Id },
                    new Driver { Name = "Driver 4", Surname = "Four", LicenceNumber = "D004", PhoneNumber = "0734444444", EmailAddress = "driver4@company.com", AddressId = addresses[2].Id, ClientId = clients[3].Id }
                };
                context.Driver.AddRange(drivers);
                context.SaveChanges();

                // Vehicles
                var vehicles = new List<Vehicle>
                {
                    new Vehicle { Manufacturer = "Toyota", Model = "Corolla", Year = "2020", RegistrationNumber = "REG001", LicencePlate = "ABC123", Color = "White", Odometer = "50000", SupplierId = suppliers[0].Id, BranchId = branches[0].Id, ClientId = clients[0].Id, DriverId = drivers[0].Id },
                    new Vehicle { Manufacturer = "Ford", Model = "Focus", Year = "2019", RegistrationNumber = "REG002", LicencePlate = "DEF456", Color = "Blue", Odometer = "40000", SupplierId = suppliers[1].Id, BranchId = branches[1].Id, ClientId = clients[1].Id, DriverId = drivers[1].Id },
                    new Vehicle { Manufacturer = "Nissan", Model = "Sentra", Year = "2021", RegistrationNumber = "REG003", LicencePlate = "GHI789", Color = "Red", Odometer = "30000", SupplierId = suppliers[2].Id, BranchId = branches[2].Id, ClientId = clients[2].Id, DriverId = drivers[2].Id },
                    new Vehicle { Manufacturer = "Hyundai", Model = "i20", Year = "2022", RegistrationNumber = "REG004", LicencePlate = "JKL012", Color = "Black", Odometer = "15000", SupplierId = suppliers[3].Id, BranchId = branches[3].Id, ClientId = clients[3].Id, DriverId = drivers[3].Id }
                };
                context.Vehicle.AddRange(vehicles);
                context.SaveChanges();
            }
        }
    }
}
