using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestDataProtector.Data;
using TestDataProtector.Models;

namespace TestDataProtector.Controllers
{
    public class ProvinsisController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        private readonly IDataProtector _protector;

        public ProvinsisController(ApplicationDbContext context, IDataProtectionProvider provider)
        {
            _context = context;
            _protector = provider.CreateProtector("ProvinsisController");
        }

        // GET: Provinsis
        public async Task<IActionResult> Index()
        {
            var data = _context.Provinsi.AsEnumerable();
            var selectlist = data.Select(p=> new ProvinsiIndexViewModels()
            {
                No = p.No,
                Nama = p.Nama,
                EncryptedId = Convert.ToString(_protector.Protect(p.Id.ToString()))
            });
            
            return View(selectlist.ToList());
        }

        // GET: Provinsis/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            var data = int.Parse(_protector.Unprotect(id));
            if (id == null)
            {
                return NotFound();
            }

            var provinsi = await _context.Provinsi
                .FirstOrDefaultAsync(m => m.Id == data);
            if (provinsi == null)
            {
                return NotFound();
            }

            return View(provinsi);
        }

        // GET: Provinsis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Provinsis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Provinsi provinsi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(provinsi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(provinsi);
        }

        // GET: Provinsis/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }
            var provid = Convert.ToInt32(_protector.Unprotect(id));
            var provinsidb = await _context.Provinsi.FindAsync(provid);
            if (provinsidb == null)
            {
                return NotFound();
            }
            var modelvm = new ProvinsiVm()
            {
                Provinsi = provinsidb,
                EncryptedId = id,
            };

            return View(modelvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ProvinsiVm model)
        {
            var doesProvinsiExists = _context.Provinsi.Where(s => s.Id == Convert.ToInt32(_protector.Unprotect(id))).Count();
            // var doesSubCatAndCatExists = _db.SubCategory.Where(s => s.Name == model.SubCategory.Name && s.CategoryId == model.SubCategory.CategoryId).Count();
            if (doesProvinsiExists == 0)
            {
                   ModelState.AddModelError(nameof(model.Provinsi.Nama), "tidak ada provinsi");
            } 
            
            if (ModelState.IsValid)
            {
                var provinsidb = _context.Provinsi.Find(Convert.ToInt32(_protector.Unprotect(id)));
                provinsidb.Nama = model.Provinsi.Nama;
                provinsidb.No = model.Provinsi.No;
                
                _context.Update(provinsidb);
                 await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ProvinsiVm modelVm = new ProvinsiVm()
            {
                Provinsi = new Provinsi(),
                EncryptedId = Convert.ToString(_protector.Unprotect(id)),
            };
            return View(modelVm);
        }
        void Populate(Provinsi original, Provinsi source)
        {
            original.Id = source.Id;
            original.No = source.No;
            original.Nama = source.Nama;
        }
        // GET: Provinsis/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            var data = Int32.Parse(_protector.Unprotect(id));
            if (id == null)
            {
                return NotFound();
            }

            var provinsi = await _context.Provinsi
                .FirstOrDefaultAsync(m => m.Id == data);
            if (provinsi == null)
            {
                return NotFound();
            }

            return View(provinsi);
        }

        // POST: Provinsis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var data = Int32.Parse(_protector.Unprotect(id));
            var provinsi = await _context.Provinsi.FindAsync(data);
            _context.Provinsi.Remove(provinsi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProvinsiExists(int id)
        {
            return _context.Provinsi.Any(e => e.Id == id);
        }
    }
}
