using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Interfaces;
using PMS.Application.Services;
using PMS.Domain.Entities.Request;

namespace PMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceptionistController : Controller
    {
        private readonly IReceptionistService _receptionistService;
        public ReceptionistController(IReceptionistService receptionistService)
        {
            _receptionistService = receptionistService;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginReq patient)
        {
            IActionResult response = Unauthorized();
            var loginRes = await _receptionistService.Login(patient);
            if (loginRes != null)
            {
                return Ok(loginRes);
            }
            return response;
        }
    }
}
