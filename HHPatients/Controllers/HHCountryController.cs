using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HHPatients.Models;

namespace HHPatients.Controllers
{
    public class HHCountryController : Controller
    {
        private readonly PatientsContext _context;

        //constructor
        public HHCountryController(PatientsContext context)
        {
            _context = context;
        }

        // GET: HHCountry
        // this dislays all countries data
        public async Task<IActionResult> Index()
        {
              return View(await _context.Countries.ToListAsync());
        }

        // GET: HHCountry/Details/CA
        // this gives one sepcific country's (selected) details
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.CountryCode == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: HHCountry/Create
        // This will return a create view where user can input data to create a new entry in table
        public IActionResult Create()
        {
            return View();
        }

        // POST: HHCountry/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // This will collect all the details from the form and create a new entry in the table
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountryCode,Name,PostalPattern,PhonePattern,FederalSalesTax")] Country country)
        {
            if (ModelState.IsValid)
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: HHCountry/Edit/CA
        // This will get us to the edit view of the selected country (based on country code)
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: HHCountry/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // This will collect the updated data from the form and update the entry in the table
        // and return us the updated table on UI
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CountryCode,Name,PostalPattern,PhonePattern,FederalSalesTax")] Country country)
        {
            if (id != country.CountryCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.CountryCode))
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
            return View(country);
        }

        // GET: HHCountry/Delete/CA
        // Based on the selected country(code) this will display the delete view of that country
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.CountryCode == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: HHCountry/Delete/CA
        // This will delete the selected country from the table and return the results
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Countries == null)
            {
                return Problem("Entity set 'PatientsContext.Countries'  is null.");
            }
            var country = await _context.Countries.FindAsync(id);
            if (country != null)
            {
                _context.Countries.Remove(country);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // returns true or false depending on if country exsists
        private bool CountryExists(string id)
        {
          return _context.Countries.Any(e => e.CountryCode == id);
        }
    }
}
