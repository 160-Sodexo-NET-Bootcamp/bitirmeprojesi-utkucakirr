using AutoMapper;
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
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var listOfCategories = await _unitOfWork.CategoryRepository.GetAll();
            if (listOfCategories is null)
            {
                return Ok(new ResultModel(false, "No category found."));
            }
            return Ok(new ResultModel<IEnumerable<Category>>(true, "Categories found!", listOfCategories));
        }

        /// <summary>
        /// Insert a category.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(CategoryViewModel entity)
        {
            var item = _mapper.Map<Category>(entity);
            await _unitOfWork.CategoryRepository.AddAsync(item);
            _unitOfWork.Complete();
            return Ok(new ResultModel(true, "Added succesfully."));
        }

        /// <summary>
        /// Update a category.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public IActionResult Update(Category entity)
        {   
            var result = _unitOfWork.CategoryRepository.Update(entity);
            if(result)
            {
                _unitOfWork.Complete();
                return Ok(new ResultModel(true, "Updated!"));
            }
            return Ok(new ResultModel(false, "Category not found. Try again."));
        }

        /// <summary>
        /// Getting a specific category from database using its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Category.</returns>
        [Authorize]
        [HttpPost("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if(category is null)
            {
                return Ok(new ResultModel(false, "Category not found!"));
            }
            return Ok(new ResultModel<Category>(true, "Category found!", category));
        }

        /// <summary>
        /// Getting products of a specific category using category's id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Products</returns>
        [Authorize]
        [HttpPost("Products/{id}")]
        public async Task<IActionResult> GetProductsByCategory(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                var products = await _unitOfWork.ProductRepository.GetAll();
                var list = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);
                return Ok(new ResultModel<IEnumerable<ProductViewModel>>(true, "Entered id is not represent a category. Try again. All products returned.", list));
            }
            var list2 = _unitOfWork.ProductRepository.GetByCategoryId(category);
            return Ok(new ResultModel<CategoryViewModelWithProducts>(true, "Products found!", list2));
        }
    }
}
