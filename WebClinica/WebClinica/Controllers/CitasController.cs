using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebClinica.Models;
using WebClinica.Models.DBClinica;

namespace WebClinica.Controllers
{
    public class CitasController : Controller
    {
        private readonly DB_CLINICAContext _context = new DB_CLINICAContext();

        public CitasController()//(DB_CLINICAContext context)
        {
            //_context = context;
        }

        // GET: Citas
        public async Task<IActionResult> Index()
        {
            var dB_CLINICAContext = _context.Citas.Include(c => c.IdTipoCitaNavigation);
            return View(await dB_CLINICAContext.ToListAsync());
        }

        // GET: Citas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citas = await _context.Citas
                .Include(c => c.IdTipoCitaNavigation)
                .SingleOrDefaultAsync(m => m.IdCita == id);
            if (citas == null)
            {
                return NotFound();
            }

            return View(citas);
        }

        // GET: Citas/Create
        public IActionResult Create()
        {
            ViewData["IdTipoCita"] = new SelectList(_context.TiposCitas, "IdTipoCita", "DescripcionTipo");
            return View();
        }

        // POST: Citas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCita,DescripcionCita,IdTipoCita,FechaCita")] Citas citas, int iduser)
        {
            if (ModelState.IsValid)
            {
                List<Citas> misCitas = _context.Citas.Where(c => c.FechaCita.Value == citas.FechaCita.Value && c.IdUser.Value == iduser).ToList();
                if (misCitas.Count == 0)
                {
                    citas.Cancelada = false;
                    citas.IdUser = iduser;
                    _context.Add(citas);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), "Home");
                }
                else {
                    ViewData["msgError"] = "No se puede crear otra cita para el mismo paciente en el mismo día.";
                }
            }
            ViewData["IdTipoCita"] = new SelectList(_context.TiposCitas, "IdTipoCita", "DescripcionTipo", citas.IdTipoCita);
            return View(citas);
        }

        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citas = await _context.Citas.SingleOrDefaultAsync(m => m.IdCita == id);
            if (citas == null)
            {
                return NotFound();
            }
            ViewData["IdTipoCita"] = new SelectList(_context.TiposCitas, "IdTipoCita", "DescripcionTipo", citas.IdTipoCita);
            return View(citas);
        }

        // POST: Citas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCita,DescripcionCita,IdTipoCita,Cancelada,FechaCita,IdUser")] Citas citas)
        {
            if (id != citas.IdCita)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(citas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitasExists(citas.IdCita))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), "Home");
            }
            ViewData["IdTipoCita"] = new SelectList(_context.TiposCitas, "IdTipoCita", "DescripcionTipo", citas.IdTipoCita);
            return View(citas);
        }

        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citas = await _context.Citas
                .Include(c => c.IdTipoCitaNavigation)
                .SingleOrDefaultAsync(m => m.IdCita == id);
            if (citas == null)
            {
                return NotFound();
            }

            return View(citas);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var citas = await _context.Citas.SingleOrDefaultAsync(m => m.IdCita == id);
            _context.Citas.Remove(citas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitasExists(int id)
        {
            return _context.Citas.Any(e => e.IdCita == id);
        }
    }
}
