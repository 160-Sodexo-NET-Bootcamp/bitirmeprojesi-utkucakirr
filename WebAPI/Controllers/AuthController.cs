using Core.Auth;
using Core.Result;
using DataAccess.Uow;
using Entities.DataModel;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using WebAPI.Jobs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;

        public AuthController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        /// <summary>
        /// Register with your email and password.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel input)
        {
            var response = await _unitOfWork.UserRepository.Register(input);
            if(!response)
            {
                return Ok(new ResultModel(false, "This email has used before!"));
            }
            await _unitOfWork.MailRepository.AddAsync(new Mail {MailTo=input.Email,Message="Registered succesfully!" });
            MailSendJob job = new(_unitOfWork);
            _unitOfWork.Complete();
            return Ok(new ResultModel(true, "Registered succesfully."));
        }

        /// <summary>
        /// Login with your email and password
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Jwt token for authorization</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel input)
        {
            if(ModelState.IsValid)
            {
                var temp = await _unitOfWork.UserRepository.Login(input);
                if (temp is null)
                {
                    return Ok(new ResultModel(false, "Email or password is wrong. Try again."));
                }

                if (temp.FailCount == 3)
                {
                    await _unitOfWork.MailRepository.AddAsync(new Mail { MailTo = input.Email, Message = "Account blocked!" });
                    _unitOfWork.Complete();
                    return Ok(new ResultModel(false, "Account blocked."));
                }
                
                var response = UserAuthentication.Auth(temp, _configuration);
                return Ok(new ResultModel<Response>(true, "Logined!", response));
            }
            return Ok("Model issue!");
            
        }

        /// <summary>
        /// Reset your password.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("ResetPW")]
        public async Task<IActionResult> ResetPassword(PWResetModel input)
        {
            var result = await _unitOfWork.UserRepository.Reset(input);
            _unitOfWork.Complete();
            if(!result)
            {
                return Ok(new ResultModel(false, "Email or password is wrong. Try again."));
            }
            return Ok(new ResultModel(true, "Completed succesfully."));
        }
    }
}
