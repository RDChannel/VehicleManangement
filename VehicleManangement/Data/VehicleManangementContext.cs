using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleManangement.Models;

namespace VehicleManangement.Data
{
    public class VehicleManangementContext : DbContext
    {
        public VehicleManangementContext (DbContextOptions<VehicleManangementContext> options)
            : base(options)
        {
        }

        public DbSet<VehicleManangement.Models.Country> Country { get; set; } = default!;
        public DbSet<VehicleManangement.Models.Address> Address { get; set; } = default!;
        public DbSet<VehicleManangement.Models.Supplier> Supplier { get; set; } = default!;
        public DbSet<VehicleManangement.Models.Branch> Branch { get; set; } = default!;
        public DbSet<VehicleManangement.Models.Client> Client { get; set; } = default!;
        public DbSet<VehicleManangement.Models.Driver> Driver { get; set; } = default!;
        public DbSet<VehicleManangement.Models.Vehicle> Vehicle { get; set; } = default!;
    }
}
