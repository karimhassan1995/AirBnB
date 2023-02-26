using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AirBnB.Data;
using AirBnB.Models;

namespace AirBnB.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropertiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Properties
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Properties.Include(z => z.AppUser).Include(z => z.Area).ThenInclude(z => z.City).Include(z => z.Categoray).Include(z => z.PropertyImgs);
            var categories = _context.Categoraies;
            ViewBag.cat = categories;
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Properties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Properties == null)
            {
                return NotFound();
            }

            var @property = await _context.Properties
                .Include(z => z.AppUser)
                .Include(z => z.Area)
                .Include(z => z.Categoray)
                .FirstOrDefaultAsync(m => m.PropertyId == id);
            if (@property == null)
            {
                return NotFound();
            }

            return View(@property);
        }

        // GET: Properties/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["AreaId"] = new SelectList(_context.Areas, "AreaId", "AreaName");
            ViewData["CategorayId"] = new SelectList(_context.Categoraies, "CategorayId", "CategorayName");
            return View();
        }

        // POST: Properties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PropertyId,PropertyTitle,PropertyDescription,PropertyCapacity,PropertyBedsNum,PropertyBedRooms,PropertyBath,PropertyPricePerNight,PropertyHostInfo,AppUserId,AreaId,CategorayId")] Property @property , IFormFile[] propImg)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@property);
                await _context.SaveChangesAsync();

            }
            if (propImg == null)
            {
                ModelState.AddModelError("", "no emage uploaded , plz upload image");
            }
            else
            {

                for (int i = 0,count=0; i < propImg.Length && count<propImg.Length; i++,count++)
                {
                    string filename;
                    PropertyImg p = new PropertyImg();
                    PropertyImg x = _context.PropertyImgs.OrderByDescending(u => u.PropertyId).FirstOrDefault();

                    if (x != null)
                    {
                        filename = (x.PropertyId +count).ToString() + "." + propImg[i].FileName.Split(".").Last();

                        p.ImgSrc = filename;
                        p.PropertyId = @property.PropertyId;
                        _context.PropertyImgs.Add(p);
                        await _context.SaveChangesAsync();

                        using (var fs = System.IO.File.Create("wwwroot/Images/" + filename))
                        {
                            propImg[i].CopyTo(fs);
                        }
                        
                    }
                    else
                    {
                        filename = i.ToString() + "." + propImg[i].FileName.Split(".").Last();

                        p.ImgSrc = filename;
                        p.PropertyId = @property.PropertyId;
                        _context.PropertyImgs.Add(p);
                        await _context.SaveChangesAsync();
                        using (var fs = System.IO.File.Create("wwwroot/Images/" + filename))
                        {
                            propImg[i].CopyTo(fs);
                        }
                    }

                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", @property.AppUserId);
            ViewData["AreaId"] = new SelectList(_context.Areas, "AreaId", "AreaName", @property.AreaId);
            ViewData["CategorayId"] = new SelectList(_context.Categoraies, "CategorayId", "CategorayName", @property.CategorayId);
            return View(@property);
        }

        // GET: Properties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Properties == null)
            {
                return NotFound();
            }

            var @property = await _context.Properties.FindAsync(id);
            if (@property == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", @property.AppUserId);
            ViewData["AreaId"] = new SelectList(_context.Areas, "AreaId", "AreaName", @property.AreaId);
            ViewData["CategorayId"] = new SelectList(_context.Categoraies, "CategorayId", "CategorayName", @property.CategorayId);
            return View(@property);
        }

        // POST: Properties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PropertyId,PropertyTitle,PropertyDescription,PropertyCapacity,PropertyBedsNum,PropertyBedRooms,PropertyBath,PropertyPricePerNight,PropertyHostInfo,AppUserId,AreaId,CategorayId")] Property @property)
        {
            if (id != @property.PropertyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@property);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyExists(@property.PropertyId))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", @property.AppUserId);
            ViewData["AreaId"] = new SelectList(_context.Areas, "AreaId", "AreaName", @property.AreaId);
            ViewData["CategorayId"] = new SelectList(_context.Categoraies, "CategorayId", "CategorayName", @property.CategorayId);
            return View(@property);
        }

        // GET: Properties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Properties == null)
            {
                return NotFound();
            }

            var @property = await _context.Properties
                .Include(z => z.AppUser)
                .Include(z => z.Area)
                .Include(z => z.Categoray)
                .FirstOrDefaultAsync(m => m.PropertyId == id);
            if (@property == null)
            {
                return NotFound();
            }

            return View(@property);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Properties == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Properties'  is null.");
            }
            var @property = await _context.Properties.FindAsync(id);
            if (@property != null)
            {
                _context.Properties.Remove(@property);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyExists(int id)
        {
          return _context.Properties.Any(e => e.PropertyId == id);
        }
    }
}
