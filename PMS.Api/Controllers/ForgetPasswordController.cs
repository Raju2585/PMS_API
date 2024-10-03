using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Interfaces;
using PMS.Domain.Entities;

namespace PMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgetPasswordController : ControllerBase
    {
        private readonly IForgetPasswordService _forgetPasswordService;
        public ForgetPasswordController(IForgetPasswordService forgetPasswordService)
        {
            _forgetPasswordService = forgetPasswordService;
        }
        [HttpPost("forget")]
        public async Task<IActionResult> SendResetLink([FromBody] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required");
            }
            var result=await _forgetPasswordService.SendResetLinkAsync(email);
            if (result)
            {
                return Ok("Reset link sent to your email");
            }
            return NotFound("User not found");
        }

        [HttpPost("reset")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordReq request)
        {
            if(string.IsNullOrEmpty(request.Email)|| string.IsNullOrEmpty(request.Token)|| string.IsNullOrEmpty(request.NewPassword))
            {
                return BadRequest("Email,token,and password are required");
            }
            var result=await _forgetPasswordService.ResetPasswordAsync(request.Email, request.Token,request.NewPassword);
            if(result)
            {
                return Ok("Password reset successfull");
                
            }
            return BadRequest("Invalid token or user not found");
        }
    }
}
