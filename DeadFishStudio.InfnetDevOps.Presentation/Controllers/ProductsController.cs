using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DeadFishStudio.InfnetDevOps.Presentation.Configuration;
using DeadFishStudio.InfnetDevOps.Shared.ViewModels.ProductViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DeadFishStudio.InfnetDevOps.Presentation.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _client;
        private const string ApiRequestUri = "api/Products";
        ///<summary>JavaScript Object Notation JSON; Defined in RFC 4627</summary>
        public const string ApplicationJson = "application/json";

        public ProductsController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient(nameof(ProductApiConfiguration));
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            string result;
            try
            {
                result = await _client.GetStringAsync(ApiRequestUri);
            }
            catch (Exception)
            {
                return View(new List<ProductViewModel>());
            }
            return View(JsonConvert.DeserializeObject<List<ProductViewModel>>(result));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = JsonConvert.DeserializeObject<ProductViewModel>(await _client.GetStringAsync($"{ApiRequestUri}/{id}"));

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
        public async Task<IActionResult> Create([FromForm] ProductViewModel product)
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

            var serializedObject = JsonConvert.SerializeObject(product);

            await _client.PostAsync($"{ApiRequestUri}", new StringContent(serializedObject, Encoding.UTF8, ApplicationJson));


            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = JsonConvert.DeserializeObject<ProductViewModel>(await _client.GetStringAsync($"{ApiRequestUri}/{id}"));
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

            try
            {
                var serializedObject = JsonConvert.SerializeObject(product);

                await _client.PutAsync($"{ApiRequestUri}/{id}", new StringContent(serializedObject, Encoding.UTF8, ApplicationJson));
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
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _client.GetStringAsync($"{ApiRequestUri}/{id}");
            if (product == null)
            {
                return NotFound();
            }

            return View(JsonConvert.DeserializeObject<ProductViewModel>(product));
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _client.GetStringAsync($"{ApiRequestUri}/{id}");
            var serializeObject = JsonConvert.SerializeObject(product);
            var buffer = System.Text.Encoding.UTF8.GetBytes(serializeObject);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue(ApplicationJson);

            await _client.DeleteAsync($"{ApiRequestUri}/{id}");

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductExists(Guid id)
        {
            var result = JsonConvert.DeserializeObject<List<ProductViewModel>>(await _client.GetStringAsync(ApiRequestUri));
            return result.Any(e => e.Id == id);
        }
    }
}
