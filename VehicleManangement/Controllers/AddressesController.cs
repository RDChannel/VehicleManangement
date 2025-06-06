﻿using System;
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
    public class AddressesController : Controller
    {
        private readonly VehicleManangementContext _context;

        public AddressesController(VehicleManangementContext context)
        {
            _context = context;
        }

        // GET: Addresses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Address.Include(a => a.Country).ToListAsync());
        }

        // GET: Addresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Address
                .Include(a => a.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (address == null)
            {
                return NotFound();
            }
            return View(address);
        }

        // GET: Addresses/Create
        public IActionResult Create(string? parentLinkId)
        {
            var model = new Address();
            if (parentLinkId != null)
            {
                model.LinkedParentId = parentLinkId;
            }
            ViewBag.CountryList = new SelectList(_context.Country, "Id", "Name");
            return View(model);
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ComplexNumber,ComplexName,StreetNumber,StreetName,City,State,PostalCode,CountryId")] Address address)
        {
            if (ModelState.IsValid)
            {
                var country = await _context.Country.FindAsync(address.CountryId);
                address.Country = country;
                _context.Add(address);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

        // GET: Addresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Address.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            ViewBag.CountryList = new SelectList(_context.Country, "Id", "Name");
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ComplexNumber,ComplexName,StreetNumber,StreetName,City,State,PostalCode,CountryId")] Address address)
        {
            if (id != address.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var country = await _context.Country.FindAsync(address.CountryId);
                    address.Country = country;
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.Id))
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
            return View(address);
        }

        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Address
                .Include(a => a.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var address = await _context.Address.FindAsync(id);
            if (address != null)
            {
                _context.Address.Remove(address);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(int id)
        {
            return _context.Address.Any(e => e.Id == id);
        }
    }
}
