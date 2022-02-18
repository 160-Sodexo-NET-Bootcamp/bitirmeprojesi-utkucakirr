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
    public class ParameterController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ParameterController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get colors.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("Colors")]
        public async Task<IActionResult> GetColors()
        {
            var list= await _unitOfWork.ColorRepository.GetAll();
            return Ok(new ResultModel<IEnumerable<Color>>(true, "Completed", list));
        }

        /// <summary>
        /// Add a color.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("AddColor")]
        public async Task<IActionResult> AddColor(ParameterAddModel color)
        {
            var temp = _mapper.Map<Color>(color);
            await _unitOfWork.ColorRepository.AddAsync(temp);
            _unitOfWork.Complete();
            return Ok(new ResultModel(true, "Completed"));
        }

        /// <summary>
        /// Get brands.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("Brands")]
        public async Task<IActionResult> GetBrands()
        {
            var list = await _unitOfWork.BrandRepository.GetAll();
            return Ok(new ResultModel<IEnumerable<Brand>>(true, "Completed", list));
        }

        /// <summary>
        /// Add a brand.
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("AddBrand")]
        public async Task<IActionResult> AddBrand(ParameterAddModel brand)
        {
            var temp = _mapper.Map<Brand>(brand);
            await _unitOfWork.BrandRepository.AddAsync(temp);
            _unitOfWork.Complete();
            return Ok(new ResultModel(true, "Completed"));
        }

        /// <summary>
        /// Get status.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("Status")]
        public async Task<IActionResult> GetStatus()
        {
            var list = await _unitOfWork.StatusRepository.GetAll();
            return Ok(new ResultModel<IEnumerable<Status>>(true, "Completed", list));
        }

        /// <summary>
        /// Add a status.
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("AddStatus")]
        public async Task<IActionResult> AddStatus(ParameterAddModel status)
        {
            var temp = _mapper.Map<Status>(status);
            await _unitOfWork.StatusRepository.AddAsync(temp);
            _unitOfWork.Complete();
            return Ok(new ResultModel(true, "Completed"));
        }
    }
}
