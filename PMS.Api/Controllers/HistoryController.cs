using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Interfaces;
using PMS.Application.Services;
using PMS.Domain.Entities;
using PMS.Domain.Entities.DTOs;

namespace PMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IMedicalhistoryService _medicalhistoryService;


        public HistoryController(IMedicalhistoryService medicalhistoryService)
        {
            _medicalhistoryService = medicalhistoryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddMedicalHistory([FromBody] MedicalHistoryDTOs medicalHistorydto)
        {
            var MedicalHistoryAdded = await _medicalhistoryService.AddMedicalHistory(medicalHistorydto);
            return Ok(MedicalHistoryAdded);
        }

        [HttpGet]
        public async Task<ActionResult<List<MedicalHistoryDTOs>>> GetMedicalHistoryByPatient(int patientId)
        {
            var medicalHistories = await _medicalhistoryService.GetMedicalHistoryByPatient(patientId);
            return Ok(medicalHistories);
        }

    }
}
