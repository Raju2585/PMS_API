using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Interfaces;
using PMS.Domain.Entities;
using System.Runtime.CompilerServices;

namespace PMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpGet]
        [Route("Notifications")]
        public async Task<ActionResult<Notification>> GetAllNotifications()
        {
            try
            {
                var notifications = await _notificationService.GetAllNotifications();
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}/mark-as-read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var result = await _notificationService.MarkAsRead(id);
            if (!result)
            {
                return NotFound(); 
            }

            return NoContent(); 
        }
        [HttpDelete("clear")]
        public async Task<IActionResult> ClearNotifications()
        {
            await _notificationService.ClearNotifications();
            return NoContent(); 
        }
        [HttpPost("appointment")]
        public async Task<IActionResult> CreateNotificationForAppointment([FromBody] Appointment appointment)
        {
            if (appointment == null)
            {
                return BadRequest("Appointment is null.");
            }

            await _notificationService.CreateNotificationForAppointment(appointment);
            return NoContent(); 
        }
    }
}
