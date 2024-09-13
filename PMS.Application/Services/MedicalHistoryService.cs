using AutoMapper;
using PMS.Application.Interfaces;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
using PMS.Domain.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Services
{
    public class MedicalHistoryService:IMedicalhistoryService
    {
        public readonly IMedicalHistoryRepository _medicalHistoryRepository;
        private readonly IMapper _mapper;

        public MedicalHistoryService(IMedicalHistoryRepository _medicalRepository,IMapper mapper)
        {
            _medicalHistoryRepository = _medicalRepository;
            _mapper = mapper;

        }

        public async Task<MedicalHistoryDTOs> AddMedicalHistory(MedicalHistoryDTOs medicalHistorydto)
        {
            var medicalHistorys = _mapper.Map<MedicalHistory>(medicalHistorydto);

            // Add the domain model to the repository
            var addedMedicalHistory = await _medicalHistoryRepository.AddMedicalHistory(medicalHistorys);

            // Map the added domain model back to DTO
            var addedMedicalHistoryDto = _mapper.Map<MedicalHistoryDTOs>(addedMedicalHistory);

            return addedMedicalHistoryDto;

        }



        public async Task<List<MedicalHistoryDTOs>> GetMedicalHistoryByPatient(int patientId)
        {
            // Retrieve the list of domain models from the repository
            var medicalHistories = await _medicalHistoryRepository.GetMedicalHistoryByPatient(patientId);

            // Map the list of domain models to a list of DTOs
            var medicalHistoryDtos = _mapper.Map<List<MedicalHistoryDTOs>>(medicalHistories);

            return medicalHistoryDtos;
        }




    }
}
