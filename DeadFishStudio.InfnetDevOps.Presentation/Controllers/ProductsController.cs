using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DeadFishStudio.InfnetDevOps.Shared.ViewModels.ProductViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeadFishStudio.Product.Domain.Model.Entity;
using DeadFishStudio.Product.Infrastructure.Data.Context;
using Newtonsoft.Json;

namespace DeadFishStudio.InfnetDevOps.Presentation.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _client;

        public ProductsController(IHttpClientFactory httpClientFactory)
        {
            _client  = httpClientFactory.CreateClient("B");
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var result = await _client.GetStringAsync("api/Products/");
            return View(JsonConvert.DeserializeObject<List<ProductViewModel>>(result));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = JsonConvert.DeserializeObject<ProductViewModel>(await _client.GetStringAsync($"api/Products/{id}"));

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Quantity")] ProductViewModel product)
        {
            product.Id = Guid.NewGuid();
            product.Prices = new List<PriceViewModel>
            {
                new PriceViewModel
                {
                    IsActive = false,
                    CreateDate = DateTime.Now,
                    Amount = 10,
                    Currency = "Real"
                }
            };

            //if (!ModelState.IsValid) return View(product);

            var serializedObject = JsonConvert.SerializeObject(product);
            //var buffer = System.Text.Encoding.UTF8.GetBytes(serializeObject);
            //var byteContent = new ByteArrayContent(buffer);
            //byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //var content = new Form

            await _client.PostAsync("api/Products/", new StringContent(serializedObject, Encoding.UTF8, "application/json"));
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = JsonConvert.DeserializeObject<ProductViewModel>(await _client.GetStringAsync($"api/Products/{id}"));
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Quantity,Id")] ProductViewModel product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            product.Prices = new List<PriceViewModel>
            {
                new PriceViewModel
                {
                    IsActive = true,
                    CreateDate = DateTime.Now,
                    Amount = 10,
                    Currency = "Real"
                }
            };

            //if (ModelState.IsValid)
            //{
                try
                {
                    var serializedObject = JsonConvert.SerializeObject(product);

                    await _client.PutAsync($"api/Products/{id}", new StringContent(serializedObject, Encoding.UTF8,"application/json"));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _client.GetStringAsync($"api/Products/{id}");
            if (product == null)
            {
                return NotFound();
            }

            return View(JsonConvert.DeserializeObject<ProductViewModel>(product));
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _client.GetStringAsync($"api/Products/{id}");
            var serializeObject = JsonConvert.SerializeObject(product);
            var buffer = System.Text.Encoding.UTF8.GetBytes(serializeObject);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            await _client.DeleteAsync($"api/Products/{id}");

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductExists(Guid id)
        {
            var result = JsonConvert.DeserializeObject<List<ProductViewModel>>(await _client.GetStringAsync($"api/Products/"));
            return result.Any(e => e.Id == id);
        }
    }
}
