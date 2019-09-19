using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DeadFishStudio.InfnetDevOps.Shared.ViewModels.MarketListViewModels;
using DeadFishStudio.MarketList.Domain.Model.Interfaces.Services;
using GianLuca.Domain.Core.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeadFishStudio.MarketList.Application.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MarketListsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMarketListServiceAsync _marketListServiceAsync;
        private readonly IUnitOfWork _unitOfWork;

        public MarketListsController(IUnitOfWork context,
            IMarketListServiceAsync marketListServiceAsync, IMapper mapper)
        {
            _unitOfWork = context;
            _marketListServiceAsync = marketListServiceAsync;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(MarketListViewModel), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetAllMarketLists()
        {
            var result = await _marketListServiceAsync.GetAllItemsAsync();

            var marketLists = result.ToList();

            if (!marketLists.Any())
                return NotFound("Não foram encontrados itens no banco de dados.");

            var marketListViewModels =
                _mapper.Map<List<Domain.Model.Entities.MarketList>, List<MarketListViewModel>>(marketLists);

            if (marketListViewModels == null)
                return NotFound("Não foi possível converter os objetos encontrados.");

            return Ok(marketListViewModels);
        }

        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(typeof(MarketListViewModel), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetMarketList(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var marketList = await _marketListServiceAsync.GetItemAsync(id);

            if (marketList is null)
                return NotFound();

            var marketListViewModel = _mapper.Map<Domain.Model.Entities.MarketList, MarketListViewModel>(marketList);

            if (marketListViewModel == null)
                return NotFound();

            return Ok(marketListViewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(MarketListViewModel), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> PutMarketList(Guid id, MarketListViewModel pMarketList)
        {
            if (id == Guid.Empty)
                return BadRequest();

            if (id != pMarketList.Id) return BadRequest();

            using (var transaction = _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var marketList = _mapper.Map<MarketListViewModel, Domain.Model.Entities.MarketList>(pMarketList);

                    if (marketList is null)
                        return UnprocessableEntity();

                    _marketListServiceAsync.UpdateItem(id, marketList);
                    await _unitOfWork.CommitTransactionAsync(transaction.Result);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await MarketListExistsAsync(id))
                        return NotFound();
                    else
                        throw;
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

        [HttpPost]
        [ProducesResponseType(typeof(MarketListViewModel), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.UnprocessableEntity)]
        public async Task<ActionResult> PostMarketList(MarketListViewModel pMarketList)
        {
            using (var transaction = _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var marketList = _mapper.Map<MarketListViewModel, Domain.Model.Entities.MarketList>(pMarketList);

                    if (marketList is null)
                        return UnprocessableEntity();

                    await _marketListServiceAsync.AddItemAsync(marketList);
                    await _unitOfWork.CommitTransactionAsync(transaction.Result);
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

            return CreatedAtAction("GetMarketList", new {id = pMarketList.Id}, pMarketList);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMarketList(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            using (var transaction = _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var marketList = await _marketListServiceAsync.GetItemAsync(id);

                    if (marketList is null)
                        return NotFound();

                    _marketListServiceAsync.DeleteItem(marketList);
                    await _unitOfWork.CommitTransactionAsync(transaction.Result);
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

            if (await MarketListExistsAsync(id))
                return BadRequest();

            return Ok();
        }

        private async Task<bool> MarketListExistsAsync(Guid id)
        {
            var result = await _marketListServiceAsync.GetAllItemsAsync();
            return result.ToList().Any(e => e.Id == id);
        }
    }
}