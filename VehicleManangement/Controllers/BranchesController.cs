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
    public class BranchesController : Controller
    {
        private readonly VehicleManangementContext _context;

        public BranchesController(VehicleManangementContext context)
        {
            _context = context;
        }

        // GET: Branches
        public async Task<IActionResult> Index()
        {
            var vehicleManangementContext = _context.Branch.Include(b => b.Address);
            return View(await vehicleManangementContext.ToListAsync());
        }

        // GET: Branches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branch
                .Include(b => b.Address)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (branch == null)
            {
                return NotFound();
            }
            var vehicleList = await _context.Vehicle.Include(v => v.Branch)
                .Where(m => m.BranchId == branch.Id)
                .GroupBy(m => m.Manufacturer)
                .Select(g => new VehicleManangement.Models.VehicleReportView
                {
                    Manufacturer = g.Key,
                    VehicleCount = g.Count()
                })
                .ToListAsync();
            ViewData["Vehicles"] = vehicleList;
            ViewData["TotalVehicles"] = vehicleList.Sum(v => v.VehicleCount ?? 0);
            return View(branch);
        }

        

        // GET: Branches/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Address, "Id", "Name");
            return View();
        }

        // POST: Branches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,RegistrationNumber,PhoneNumber,EmailAddress,AddressId")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(branch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "Id", "Name", branch.AddressId);
            return View(branch);
        }

        // GET: Branches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branch.FindAsync(id);
            if (branch == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "Id", "Name", branch.AddressId);
            return View(branch);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,RegistrationNumber,PhoneNumber,EmailAddress,AddressId")] Branch branch)
        {
            if (id != branch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(branch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BranchExists(branch.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "Id", "Name", branch.AddressId);
            return View(branch);
        }

        // GET: Branches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branch
                .Include(b => b.Address)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var branch = await _context.Branch.FindAsync(id);
            if (branch != null)
            {
                _context.Branch.Remove(branch);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BranchExists(int id)
        {
            return _context.Branch.Any(e => e.Id == id);
        }
    }
}
