using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DeadFishStudio.InfnetDevOps.Presentation.Configuration;
using DeadFishStudio.InfnetDevOps.Shared.ViewModels.MarketListViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DeadFishStudio.InfnetDevOps.Presentation.Controllers
{
    public class MarketListsController : Controller
    {
        private readonly HttpClient _client;
        private const string ApiRequestUri = "api/MarketLists/";
        ///<summary>JavaScript Object Notation JSON; Defined in RFC 4627</summary>
        public const string ApplicationJson = "application/json";

        public MarketListsController(IHttpClientFactory httpClientFactory)
        {
            _client  = httpClientFactory.CreateClient(nameof(MarketListApiConfiguration));
        }

        // GET: MarketLists
        public async Task<IActionResult> Index()
        {
            string result;
            try
            {
                result = await _client.GetStringAsync(ApiRequestUri);
            }
            catch (Exception)
            {
                return View(new List<MarketListViewModel>());
            }

            return View(JsonConvert.DeserializeObject<List<MarketListViewModel>>(result));
        }

        // GET: MarketLists/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marketListViewModel = JsonConvert.DeserializeObject<MarketListViewModel>(await _client.GetStringAsync($"{ApiRequestUri}{id}"));

            if (marketListViewModel == null)
            {
                return NotFound();
            }

            return View(marketListViewModel);
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
        public async Task<IActionResult> Create([FromForm] MarketListViewModel marketList)
        {
            //[Bind("Name,DataDeCriacao,DataDeModificacao")]
            marketList.Id = Guid.NewGuid();
            marketList.Name = "A";
            marketList.DataDeCriacao = DateTime.Now;
            marketList.DataDeModificacao = DateTime.Now;
            marketList.ItemViewModels = new ItemViewModel<MarketListProductViewModel>
            {
                new MarketListProductViewModel()
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Bolo de Noz",
                    Quantity = 1,
                    Price = 10
                }
            };

            //if (!ModelState.IsValid) return View(product);

            var serializedObject = JsonConvert.SerializeObject(marketList);
            //var buffer = System.Text.Encoding.UTF8.GetBytes(serializeObject);
            //var byteContent = new ByteArrayContent(buffer);
            //byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //var content = new Form

            await _client.PostAsync(ApiRequestUri, new StringContent(serializedObject, Encoding.UTF8, ApplicationJson));
            return RedirectToAction(nameof(Index));
        }

        // GET: MarketLists/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marketList = JsonConvert.DeserializeObject<MarketListViewModel>(await _client.GetStringAsync($"{ApiRequestUri}{id}"));
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,DataDeCriacao,DataDeModificacao,Id")] MarketListViewModel marketList)
        {
            if (id != marketList.Id)
            {
                return NotFound();
            }

            marketList.ItemViewModels = new ItemViewModel<MarketListProductViewModel>
            {
                new MarketListProductViewModel()
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Bolo de Noz",
                    Quantity = 1,
                    Price = 10
                }
            };

            //if (ModelState.IsValid)
            //{
            try
            {
                var serializedObject = JsonConvert.SerializeObject(marketList);

                await _client.PutAsync($"{ApiRequestUri}{id}", new StringContent(serializedObject, Encoding.UTF8,ApplicationJson));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MarketListExists(marketList.Id))
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
            return View(marketList);
        }

        // GET: MarketLists/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marketList = await _client.GetStringAsync($"{ApiRequestUri}{id}");
            if (marketList == null)
            {
                return NotFound();
            }

            return View(JsonConvert.DeserializeObject<MarketListViewModel>(marketList));
        }

        // POST: MarketLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _client.DeleteAsync($"{ApiRequestUri}{id}");
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MarketListExists(Guid id)
        {
            var result = JsonConvert.DeserializeObject<List<MarketListViewModel>>(await _client.GetStringAsync(ApiRequestUri));
            return result.Any(e => e.Id == id);
        }
    }
}
