using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DeadFishStudio.InfnetDevOps.Shared.ViewModels.ProductViewModels;
using DeadFishStudio.Product.Domain.Model.Interfaces.Services;
using GianLuca.Domain.Core.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace DeadFishStudio.Product.Application.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductServiceAsync _productServiceAsync;

        public ProductsController(IUnitOfWork context, IProductServiceAsync productServiceAsync, IMapper mapper)
        {
            _unitOfWork = context;
            _productServiceAsync = productServiceAsync;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProductViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
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
        [ProducesResponseType(typeof(ProductViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
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
        [ProducesResponseType(typeof(ProductViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public async Task<ActionResult> Post([FromBody] ProductViewModel item)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var product = _mapper.Map<ProductViewModel, Domain.Model.Entity.Product>(item);

                    if (product is null)
                        return UnprocessableEntity();

                    await _productServiceAsync.AddItemAsync(product);
                    var a = await _unitOfWork.SaveEntitiesAsync();
                    await _unitOfWork.CommitTransactionAsync(transaction);
                }
                catch (Exception ex)
                {
                    _unitOfWork.RollbackTransaction();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    _unitOfWork.Dispose();
                }
            }

            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProductViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> Put(Guid id, [FromBody] ProductViewModel item)
        {
            if (id == Guid.Empty)
                return ValidationProblem();

            using (var transaction = _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var product = _mapper.Map<ProductViewModel, Domain.Model.Entity.Product>(item);

                    if (product is null)
                        return UnprocessableEntity();

                    var updatedProduct = _productServiceAsync.UpdateItem(id, product);
                    await _unitOfWork.CommitTransactionAsync(transaction.Result);
                    return Ok(_mapper.Map<Domain.Model.Entity.Product, ProductViewModel>(updatedProduct));
                }
                catch (Exception ex)
                {
                    _unitOfWork.RollbackTransaction();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    _unitOfWork.Dispose();
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return ValidationProblem();

            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var product = await _productServiceAsync.GetItemAsync(id);

                    if (product is null)
                        return UnprocessableEntity();

                    _productServiceAsync.DeleteItem(product);
                    await _unitOfWork.SaveEntitiesAsync();
                    await _unitOfWork.CommitTransactionAsync(transaction);

                    if (await ProductExistsAsync(id))
                        return Ok();
                }
                catch (Exception ex)
                {
                    _unitOfWork.RollbackTransaction();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    _unitOfWork.Dispose();
                }
            }

            return BadRequest();
        }

        private async Task<bool> ProductExistsAsync(Guid id)
        {
            var result = await _productServiceAsync.GetAllItemsAsync();
            return result.ToList().Any(e => e.Id == id);
        }
    }
}