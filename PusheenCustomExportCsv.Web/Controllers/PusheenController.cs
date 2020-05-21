using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PusheenCustomExportCsv.Web.Data;
using PusheenCustomExportCsv.Web.Models;
using PusheenCustomExportCsv.Web.Services;

namespace PusheenCustomExportCsv.Web.Controllers
{
    public class PusheenController : Controller
    {
        private readonly IPusheenService _pusheenService;

        public PusheenController(IPusheenService pusheenService)
        {
            _pusheenService = pusheenService;
        }

        // GET: Pusheen
        public async Task<IActionResult> Index()
        {
            return View(await _pusheenService.GetAllAsync());
        }

        public FileResult ExportCsv()
        {
            return File(_pusheenService.GetAllPusheens(), "pusheen.csv");
        }
        
        public virtual PusheenCsvResult File(IEnumerable<Pusheen> pusheenData, string fileDownloadName)
        {
            return new PusheenCsvResult(pusheenData, fileDownloadName);
        }

        // GET: Pusheen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pusheen = await _pusheenService.FindPusheenById(id);

            if (pusheen == null)
            {
                return NotFound();
            }

            return View(pusheen);
        }

        // GET: Pusheen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pusheen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FavouriteFood,SuperPower")] Pusheen pusheen)
        {
            if (ModelState.IsValid)
            {
                await _pusheenService.Create(pusheen);
                return RedirectToAction(nameof(Index));
            }
            return View(pusheen);
        }

        // GET: Pusheen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pusheen = await _pusheenService.FindPusheenAsync(id);
            if (pusheen == null)
            {
                return NotFound();
            }
            return View(pusheen);
        }

        // POST: Pusheen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FavouriteFood,SuperPower")] Pusheen pusheen)
        {
            if (id != pusheen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _pusheenService.Update(pusheen);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_pusheenService.PusheenExists(pusheen.Id))
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
            return View(pusheen);
        }

        // GET: Pusheen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pusheen = await _pusheenService.FindPusheenById(id);
            if (pusheen == null)
            {
                return NotFound();
            }

            return View(pusheen);
        }

        // POST: Pusheen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pusheen = await _pusheenService.FindPusheenAsync(id);
            await _pusheenService.Delete(pusheen);
            return RedirectToAction(nameof(Index));
        }

    }
}
