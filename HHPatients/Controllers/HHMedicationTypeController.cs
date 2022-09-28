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
    public class HHMedicationTypeController : Controller
    {
        private readonly PatientsContext _context;

        // constructor
        public HHMedicationTypeController(PatientsContext context)
        {
            _context = context;
        }

        // GET: HHMedicationType
        // will give the view with medication type id with name and links to modify
        public async Task<IActionResult> Index()
        {
              return View(await _context.MedicationTypes.ToListAsync());
        }

        // GET: HHMedicationType/Details/5
        // will return the details of selected medication type 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MedicationTypes == null)
            {
                return NotFound();
            }

            var medicationType = await _context.MedicationTypes
                .FirstOrDefaultAsync(m => m.MedicationTypeId == id);
            if (medicationType == null)
            {
                return NotFound();
            }

            return View(medicationType);
        }

        // GET: HHMedicationType/Create
        // returns the create view for creating a new medication type (id and name)
        public IActionResult Create()
        {
            return View();
        }

        // POST: HHMedicationType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // returns the updated view with newly created medication type
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicationTypeId,Name")] MedicationType medicationType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicationType);
        }

        // GET: HHMedicationType/Edit/5
        // will give the edit view where user can edit the selected medication type
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MedicationTypes == null)
            {
                return NotFound();
            }

            var medicationType = await _context.MedicationTypes.FindAsync(id);
            if (medicationType == null)
            {
                return NotFound();
            }
            return View(medicationType);
        }

        // POST: HHMedicationType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // will return the updated view, with updated value of the edited medication type
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicationTypeId,Name")] MedicationType medicationType)
        {
            if (id != medicationType.MedicationTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicationTypeExists(medicationType.MedicationTypeId))
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
            return View(medicationType);
        }

        // GET: HHMedicationType/Delete/5
        // will give us the delete view where user can choose if they want to delete the selected medication type
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MedicationTypes == null)
            {
                return NotFound();
            }

            var medicationType = await _context.MedicationTypes
                .FirstOrDefaultAsync(m => m.MedicationTypeId == id);
            if (medicationType == null)
            {
                return NotFound();
            }

            return View(medicationType);
        }

        // POST: HHMedicationType/Delete/5
        // will return the updated view of the data after deleting the selected medication type
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MedicationTypes == null)
            {
                return Problem("Entity set 'PatientsContext.MedicationTypes'  is null.");
            }
            var medicationType = await _context.MedicationTypes.FindAsync(id);
            if (medicationType != null)
            {
                _context.MedicationTypes.Remove(medicationType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // returns true or false depending on if Medication type exsists
        private bool MedicationTypeExists(int id)
        {
          return _context.MedicationTypes.Any(e => e.MedicationTypeId == id);
        }
    }
}
