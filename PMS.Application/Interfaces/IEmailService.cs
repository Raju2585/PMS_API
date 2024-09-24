using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Interfaces
{
    public interface IEmailService
    {
        Task<String> GenerateEmailBody(Domain.Entities.Response.PatientDtl patient, Appointment appointment, Doctor doctor);
        void SendEmailNotification(string toEmail, string subject, string body);
    }
}
