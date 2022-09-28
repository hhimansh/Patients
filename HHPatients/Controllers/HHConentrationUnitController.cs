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
    public class HHConcentrationUnitController : Controller
    {
        private readonly PatientsContext _context;

        // constructor
        public HHConcentrationUnitController(PatientsContext context)
        {
            _context = context;
        }

        // GET: HHConcentrationUnit
        // index method displays all the details in the table with the same name in the database
        public async Task<IActionResult> Index()
        {
              return View(await _context.ConcentrationUnits.ToListAsync());
        }

        // GET: HHConcentrationUnit/Details/5
        // this gives details of the specific concentration id selected (id)
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ConcentrationUnits == null)
            {
                return NotFound();
            }

            var concentrationUnit = await _context.ConcentrationUnits
                .FirstOrDefaultAsync(m => m.ConcentrationCode == id);
            if (concentrationUnit == null)
            {
                return NotFound();
            }

            return View(concentrationUnit);
        }

        // GET: HHConcentrationUnit/Create
        // this method is to create a new concentration unit
        public IActionResult Create()
        {
            return View();
        }

        // POST: HHConcentrationUnit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // This will collect the data from create view and add it to the DB and return details of all
        // the concentration units present
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConcentrationCode")] ConcentrationUnit concentrationUnit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(concentrationUnit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(concentrationUnit);
        }

        // GET: HHConcentrationUnit/Edit/5
        // This will edit the concentration unit selected (id)
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ConcentrationUnits == null)
            {
                return NotFound();
            }

            var concentrationUnit = await _context.ConcentrationUnits.FindAsync(id);
            if (concentrationUnit == null)
            {
                return NotFound();
            }
            return View(concentrationUnit);
        }

        // POST: HHConcentrationUnit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // this will edit the concentration code selected and reflect the changes 
        // on the UI by displaying back the results
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ConcentrationCode")] ConcentrationUnit concentrationUnit)
        {
            if (id != concentrationUnit.ConcentrationCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(concentrationUnit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConcentrationUnitExists(concentrationUnit.ConcentrationCode))
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
            return View(concentrationUnit);
        }

        // GET: HHConcentrationUnit/Delete/5
        // This will get us to the delete view of the selected id 
        // to confirm before deleting
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ConcentrationUnits == null)
            {
                return NotFound();
            }

            var concentrationUnit = await _context.ConcentrationUnits
                .FirstOrDefaultAsync(m => m.ConcentrationCode == id);
            if (concentrationUnit == null)
            {
                return NotFound();
            }

            return View(concentrationUnit);
        }

        // POST: HHConcentrationUnit/Delete/5
        // this will delete the selected concentration unit and return the result
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ConcentrationUnits == null)
            {
                return Problem("Entity set 'PatientsContext.ConcentrationUnits'  is null.");
            }
            var concentrationUnit = await _context.ConcentrationUnits.FindAsync(id);
            if (concentrationUnit != null)
            {
                _context.ConcentrationUnits.Remove(concentrationUnit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // returns true or false depending on if concentration unit exsists
        private bool ConcentrationUnitExists(string id)
        {
          return _context.ConcentrationUnits.Any(e => e.ConcentrationCode == id);
        }
    }
}
