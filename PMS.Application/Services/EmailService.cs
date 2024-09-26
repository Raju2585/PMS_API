using PMS.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PMS.Domain.Entities;

namespace PMS.Application.Services
{
    public class EmailService:IEmailService
    {
        public void SendEmailNotification(string toEmail, string subject, string body)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("burkashruthi001@gmail.com", GetPassword());

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("burkashruthi001@gmail.com");
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            smtpClient.Send(mailMessage);
        }

        public async Task<String> GenerateEmailBody(Domain.Entities.Response.PatientDtl patient, Appointment appointment, Doctor doctor)
        {
            var hospitalName = appointment.HospitalName;
           
            var appointmentTime = appointment.AppointmentDate.ToString("f");



            var emailBody = $@"
        <html>
        <body>
            <h2>Appointment Confirmation</h2>
            <p>Dear {patient.PatientName},</p>
            <p>Thank you for booking an appointment with us. Here are the details:</p>
            <ul>
                <li><strong>Hospital:</strong> {hospitalName}</li>
               <li><strong>Doctor:</strong> Dr. {doctor.DoctorName}</li>
                <li><strong>Date and Time:</strong> {appointmentTime}</li>
                <li><strong>Reason for Visit:</strong> {appointment.Reason}</li>
            </ul>
            <p>If you have any questions or need to reschedule, please contact us.</p>
            <p>Best regards,<br/>Your Hospital Team</p>
        </body>
        </html>";

            return emailBody;



        }

        public async Task SendForgetPasswordEmail(string toEmail, string resetLink)
        {
            string subject = "Password Reset Request";
            string body = await GenerateForgetPasswordEmailBody(resetLink);

            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("pavanimaddu07@gmail.com", GetPassword());

                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress("pavanimaddu07@gmail.com");
                    mailMessage.To.Add(toEmail);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
        }

        public async Task<string> GenerateForgetPasswordEmailBody(string resetLink)
        {
            var emailBody = $@"
            <html>
            <body>
                <h2>Password Reset Request</h2>
                <p>We received a request to reset your password. You can reset it using the link below:</p>
                <p><a href='{resetLink}'>Reset Password</a></p>
                <p>If you didn't request a password reset, you can ignore this email.</p>
                <p>Best regards,<br/>Your Hospital Team</p>
            </body>
            </html>";

            return emailBody;
        }

        private string GetPassword()
        {
            return "xvacuzdxgonhvvwq";
        }
    }
}
