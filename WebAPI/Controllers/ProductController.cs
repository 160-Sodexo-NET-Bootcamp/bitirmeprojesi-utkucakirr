using AutoMapper;
using Core.Filters;
using Core.Result;
using DataAccess.Uow;
using Entities.DataModel;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all products from database.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list= await _unitOfWork.ProductRepository.GetAll();
            var newList = _mapper.Map<List<ProductViewModel>>(list);
            return Ok(new ResultModel<List<ProductViewModel>>(true, "Products listed.", newList));
        }

        /// <summary>
        /// Insert a product to database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [TypeFilter(typeof(ProductAddFilter))]
        [Authorize]
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] ProductModel entity)
        {
            var item = _mapper.Map<Product>(entity);
            await _unitOfWork.ProductRepository.AddAsync(item);
            _unitOfWork.Complete();
            return Ok(new ResultModel(true, "Product added."));
        }

        /// <summary>
        /// Update a product. Product must be yours.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        [TypeFilter(typeof(CheckIdFilter))]
        [Authorize]
        [HttpPost("Update")]
        public async Task<IActionResult> Update(int UserId,ProductUpdateModel entity)
        {
            var result = await _unitOfWork.ProductRepository.UpdateWithConfirmation(UserId, entity);
            if(result)
            {
                return Ok(new ResultModel(true, "Updated!"));
            }
            return Ok(new ResultModel(false, "This item is not belong to you!"));
        }

        /// <summary>
        /// Delete a product. Product must be yours.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [TypeFilter(typeof(CheckIdFilter))]
        [Authorize]
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int UserId, int id)
        {
            var result = await _unitOfWork.ProductRepository.DeleteWithConfirmation(UserId, id);
            if (result)
            {
                return Ok(new ResultModel(true, "Deleted!"));
            }
            return Ok(new ResultModel(false, "This item is not belong to you!"));
        }
    }
}
