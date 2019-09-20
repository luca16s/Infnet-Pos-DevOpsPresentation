using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DeadFishStudio.InfnetDevOps.Shared.ViewModels.ProductViewModels;
using DeadFishStudio.Product.Domain.Model.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeadFishStudio.Product.Application.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductServiceAsync _productServiceAsync;

        public ProductsController(IProductServiceAsync productServiceAsync, IMapper mapper)
        {
            _productServiceAsync = productServiceAsync;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProductViewModel), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult> Get()
        {
            var result = await _productServiceAsync.GetAllItemsAsync();

            var products = result.ToList();

            if (!products.Any())
                return NotFound("Não foram encontrados itens no banco de dados.");

            var productsViewModels = _mapper.Map<List<Domain.Model.Entity.Product>, List<ProductViewModel>>(products);

            if (productsViewModels == null)
                return NotFound("Não foi possível converter os objetos encontrados.");

            return Ok(productsViewModels);
        }

        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(typeof(ProductViewModel), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult> Get(Guid id)
        {
            var products = await _productServiceAsync.GetItemAsync(id);

            if (products is null)
                return NotFound();

            var productsViewModel = _mapper.Map<Domain.Model.Entity.Product, ProductViewModel>(products);

            if (productsViewModel == null)
                return NotFound();

            return Ok(productsViewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductViewModel), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.UnprocessableEntity)]
        public async Task<ActionResult> Post([FromBody] ProductViewModel item)
        {
            var product = _mapper.Map<ProductViewModel, Domain.Model.Entity.Product>(item);

            if (product is null)
                return UnprocessableEntity();

            await _productServiceAsync.AddItemAsync(product);

            return Ok(_mapper.Map<Domain.Model.Entity.Product, ProductViewModel>(
                await _productServiceAsync.GetItemAsync(product.Id)));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProductViewModel), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.UnprocessableEntity)]
        public IActionResult Put(Guid id, [FromBody] ProductViewModel item)
        {
            if (id == Guid.Empty)
                return ValidationProblem();

            var product = _mapper.Map<ProductViewModel, Domain.Model.Entity.Product>(item);

            if (product is null)
                return UnprocessableEntity();

            var updatedProduct = _productServiceAsync.UpdateItem(id, product);

            return Ok(_mapper.Map<Domain.Model.Entity.Product, ProductViewModel>(updatedProduct));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromBody] ProductViewModel item)
        {
            if (item.Id == Guid.Empty)
                return ValidationProblem();

            var product = _mapper.Map<ProductViewModel, Domain.Model.Entity.Product>(item);

            if (product is null)
                return UnprocessableEntity();

            _productServiceAsync.DeleteItem(product);

            if (_mapper.Map<Domain.Model.Entity.Product, ProductViewModel>(
                await _productServiceAsync.GetItemAsync(product.Id)) is null)
                return Ok();

            return BadRequest();
        }
    }
}