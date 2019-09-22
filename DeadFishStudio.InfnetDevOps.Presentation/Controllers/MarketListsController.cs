using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeadFishStudio.MarketList.Domain.Model.Entities;
using DeadFishStudio.MarketList.Infrastructure.Data.Context;

namespace DeadFishStudio.InfnetDevOps.Presentation.Controllers
{
    public class MarketListsController : Controller
    {
        private readonly MarketListContext _context;

        public MarketListsController(MarketListContext context)
        {
            _context = context;
        }

        // GET: MarketLists
        public async Task<IActionResult> Index()
        {
            return View(await _context.MarketList.ToListAsync());
        }

        // GET: MarketLists/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marketList = await _context.MarketList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marketList == null)
            {
                return NotFound();
            }

            return View(marketList);
        }

        // GET: MarketLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MarketLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,DataDeCriacao,DataDeModificacao,Id")] MarketList.Domain.Model.Entities.MarketList marketList)
        {
            if (ModelState.IsValid)
            {
                //marketList.Id = Guid.NewGuid();
                _context.Add(marketList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(marketList);
        }

        // GET: MarketLists/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marketList = await _context.MarketList.FindAsync(id);
            if (marketList == null)
            {
                return NotFound();
            }
            return View(marketList);
        }

        // POST: MarketLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Nome,DataDeCriacao,DataDeModificacao,Id")] MarketList.Domain.Model.Entities.MarketList marketList)
        {
            if (id != marketList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(marketList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarketListExists(marketList.Id))
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
            return View(marketList);
        }

        // GET: MarketLists/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marketList = await _context.MarketList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marketList == null)
            {
                return NotFound();
            }

            return View(marketList);
        }

        // POST: MarketLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var marketList = await _context.MarketList.FindAsync(id);
            _context.MarketList.Remove(marketList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarketListExists(Guid id)
        {
            return _context.MarketList.Any(e => e.Id == id);
        }
    }
}
