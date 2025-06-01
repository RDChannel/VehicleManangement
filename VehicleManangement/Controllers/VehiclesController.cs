using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VehicleManangement.Data;
using VehicleManangement.Models;

namespace VehicleManangement.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly VehicleManangementContext _context;

        public VehiclesController(VehicleManangementContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var vehicleManangementContext = _context.Vehicle.Include(v => v.Branch).Include(v => v.Client).Include(v => v.Driver).Include(v => v.Supplier);
            return View(await vehicleManangementContext.ToListAsync());
        }

        public async Task<IActionResult> IndexByClients(int? clientId)
        {
            var vehicleManangementContext = _context.Vehicle.Include(v => v.Branch).Include(v => v.Client).Include(v => v.Driver).Include(v => v.Supplier).Where(m => m.ClientId == clientId);
            ViewBag.clientId = clientId;
            return View(await vehicleManangementContext.ToListAsync());
        }

        public async Task<IActionResult> IndexLease(int? clientId)
        {
            var vehicleManangementContext = _context.Vehicle.Include(v => v.Branch).Include(v => v.Client).Include(v => v.Driver).Include(v => v.Supplier).Where(m => m.ClientId == null);
            ViewBag.clientId = clientId;
            return View(await vehicleManangementContext.ToListAsync());
        }
        
        // GET: Vehicles/Details/5
        public async Task<IActionResult> AddLease(int? id, int? clientId)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = await _context.Client
                .Include(c => c.Address)
                .FirstOrDefaultAsync(m => m.Id == clientId);
            var vehicle = await _context.Vehicle
                .Include(v => v.Branch)
                .Include(v => v.Client)
                .Include(v => v.Driver)
                .Include(v => v.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            try
            {
                vehicle.Client = client;
                vehicle.ClientId = client.Id;
                _context.Update(vehicle);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(vehicle.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(IndexByClients), new { clientId = clientId });
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> RemoveLease(int? id, int? clientId = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(v => v.Branch)
                .Include(v => v.Client)
                .Include(v => v.Driver)
                .Include(v => v.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            try
                {
                vehicle.Client = null;
                vehicle.ClientId = null;
                vehicle.Driver = null;
                vehicle.DriverId = null;
                _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexByClients), new { clientId = clientId });
        }


        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(v => v.Branch)
                .Include(v => v.Client)
                .Include(v => v.Driver)
                .Include(v => v.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        public async Task<IActionResult> IndexSummary()
        {
            var vehicleManangementContext = _context.Vehicle.Include(v => v.Branch).Include(v => v.Client).Include(v => v.Driver).Include(v => v.Supplier);
            return View(await vehicleManangementContext.ToListAsync());
        }
        

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            ViewData["BranchId"] = new SelectList(_context.Branch, "Id", "Name");
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name");
            ViewData["DriverId"] = new SelectList(_context.Driver, "Id", "Name");
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "Name");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Manufacturer,Model,Year,RegistrationNumber,LicencePlate,Color,Odometer,SupplierId,BranchId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchId"] = new SelectList(_context.Branch, "Id", "Name", vehicle.BranchId);
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name", vehicle.ClientId);
            ViewData["DriverId"] = new SelectList(_context.Driver, "Id", "Name", vehicle.DriverId);
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "Name", vehicle.SupplierId);
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id,int? clientId = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["BranchId"] = new SelectList(_context.Branch, "Id", "Name", vehicle.BranchId);
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name", vehicle.ClientId);
            ViewData["DriverId"] = new SelectList(_context.Driver, "Id", "Name", vehicle.DriverId);
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "Name", vehicle.SupplierId);
            ViewBag.clientMId = clientId;
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Manufacturer,Model,Year,RegistrationNumber,LicencePlate,Color,Odometer,SupplierId,BranchId,ClientId,DriverId")] Vehicle vehicle, int? clientId = null)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if(clientId != null)
                {
                    return RedirectToAction(nameof(IndexByClients), new { clientId = clientId });
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchId"] = new SelectList(_context.Branch, "Id", "Name", vehicle.BranchId);
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name", vehicle.ClientId);
            ViewData["DriverId"] = new SelectList(_context.Driver, "Id", "Name", vehicle.DriverId);
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "Name", vehicle.SupplierId);
            ViewBag.clientMId = clientId;
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(v => v.Branch)
                .Include(v => v.Client)
                .Include(v => v.Driver)
                .Include(v => v.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicle.Remove(vehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicle.Any(e => e.Id == id);
        }
    }
}
