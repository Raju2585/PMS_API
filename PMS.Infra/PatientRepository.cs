using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
using PMS.Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Infra
{
    public class PatientRepository:IPatientRepository
    {
        public readonly IApplicationDbContext _applicationDbContext;
        public readonly IMapper _mapper;
        public PatientRepository(IApplicationDbContext applicationDbContext,IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<PatientDtl> GetPatientById(string patientId)
        {
            var patient = await _applicationDbContext.Patients
                .FirstOrDefaultAsync(p => p.Id == patientId);
            var result = _mapper.Map<PatientDtl>(patient);
            return result;
        }

       

    }
}
