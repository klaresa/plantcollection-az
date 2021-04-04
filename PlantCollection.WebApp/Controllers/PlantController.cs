using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlantCollection.Domain.Model.Entities;
using PlantCollection.Domain.Model.Interfaces.Services;
using PlantCollection.Infrastructure.DataAccess;

namespace PlantCollection.WebApp.Controllers
{
    public class PlantController : Controller
    {
        private readonly IPlantService _domainService;

        public PlantController(IPlantService domainService)
        {
            _domainService = domainService;
        }

        // GET: Plant
        public async Task<IActionResult> Index()
        {
            return View(await _domainService.GetAllAsync());
        }

        // GET: Plant/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Plant/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Plant plant)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Form.Files.SingleOrDefault();

                await _domainService.InsertAsync(plant, file?.OpenReadStream());

                return RedirectToAction(nameof(Index));
            }
            return View(plant);
        }

        // GET: Plant/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plant = await _domainService.GetByIdAsync(id);

            if (plant == null)
            {
                return NotFound();
            }

            await _domainService.IncreaseView(id);

            return View(plant);
        }

        // GET: Plant/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plant = await _domainService.GetByIdAsync(id);
            if (plant == null)
            {
                return NotFound();
            }
            return View(plant);
        }

        // POST: Plant/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Plant plant)
        {
            if (Guid.Parse(id) != plant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var file = Request.Form.Files.SingleOrDefault();

                    await _domainService.UpdateAsync(plant, file?.OpenReadStream());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlantExists(id))
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
            return View(plant);
        }

        // GET: Plant/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plant = await _domainService.GetByIdAsync(id);
            if (plant == null)
            {
                return NotFound();
            }

            return View(plant);
        }

        // POST: Plant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var plant = await _domainService.GetByIdAsync(id);

            await _domainService.DeleteAsync(plant);

            return RedirectToAction(nameof(Index));
        }

        private bool PlantExists(string id)
        {
            return _domainService.GetByIdAsync(id) != null;
        }
    }
}
