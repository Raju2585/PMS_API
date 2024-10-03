using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.Application.Interfaces;

namespace PMS.Application.Services
{
    public class ForgetPasswordService : IForgetPasswordService
    {
        private IEmailService _emailService;

        private readonly IPatientService _patientService;
        private readonly InMemoryTokenStore _inMemoryTokenStore;
        public ForgetPasswordService(IEmailService emailService, IPatientService patientService, InMemoryTokenStore inMemoryTokenStore)
        {
            _emailService = emailService;
            _patientService = patientService;
            _inMemoryTokenStore = inMemoryTokenStore;
        }
        public async Task<bool> SendResetLinkAsync(string email)
        {
            var patient = await _patientService.GetPatientByEmail(email);

            var token = await _patientService.GenerateToken(email);

            _inMemoryTokenStore.StoreToken(email, token, TimeSpan.FromHours(1));

            var resetLink = $"http://localhost:3000/root/PasswordReset?token={token}&email={email}";

            var message = $"<p>To reset your password,click the link below:</p><p><a href='{resetLink}'>Reset Password</a></p>";
            _emailService.SendEmailNotification(email, resetLink, message);
            return true;
        }
        public async Task<bool> ResetPasswordAsync(string email, string token, string password)
        {
            // Validate the token first
            var isValidToken = await ValidateTokenAsync(email, token);
            if (!isValidToken)
            {
                Console.WriteLine($"Invalid token for email: {email}");
                return false; // Token is invalid
            }

            // Retrieve the user by email
            var userDtl = await _patientService.GetPatientByEmail(email);
            if (userDtl == null)
            {
                Console.WriteLine($"User not found for email: {email}");
                return false; // User not found
            }

            // Hash the new password before saving
            userDtl.PasswordHash = password; // Ensure the password is hashed

            // Update the user's password
            var isUpdated = await _patientService.UpdatePatientPassword(email, password); // Pass the hashed password
            if (!isUpdated)
            {
                Console.WriteLine($"Failed to update password for user: {email}");
                return false; // Update failed
            }

            // Invalidate the token after a successful password update
            _inMemoryTokenStore.InvalidateToken(email);

            // Successful reset
            return true;
        }



        public async Task<bool> ValidateTokenAsync(string email,string token)
        {
            var storedTokenInfo = _inMemoryTokenStore.GetToken(email);
            if(storedTokenInfo == null)
            {
                return false;
            }
            if (storedTokenInfo.Value.Token != token)
            {
                return false;
            }
            if (DateTime.UtcNow > storedTokenInfo.Value.Expiration)
            {
                return false;
            }
            return true;
        }
    }
}
