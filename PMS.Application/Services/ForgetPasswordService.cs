using Microsoft.AspNetCore.Identity;
using PMS.Application.Interfaces;
using PMS.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace PMS.Application.Services
{
    public class ForgetPasswordService : IForgetPasswordService
    {
        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPatientService _patientService;
        private readonly InMemoryTokenStore _inMemoryTokenStore;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher; // Use PasswordHasher

        public ForgetPasswordService(IEmailService emailService, IPatientService patientService,
                                     InMemoryTokenStore inMemoryTokenStore,
                                     IPasswordHasher<ApplicationUser> passwordHasher,
                                      UserManager<ApplicationUser> userManager) // Inject PasswordHasher
        {
            _emailService = emailService;
            _patientService = patientService;
            _inMemoryTokenStore = inMemoryTokenStore;
            _passwordHasher = passwordHasher;
            _userManager = userManager;
        }

        public async Task<bool> SendResetLinkAsync(string email)
        {
            var patient = await _patientService.GetPatientByEmail(email);
            var token = await _patientService.GenerateToken(email);

            _inMemoryTokenStore.StoreToken(email, token, TimeSpan.FromHours(1));

            var resetLink = $"http://localhost:3000/root/PasswordReset?token={token}&email={email}";

            var message = $"<p>To reset your password, click the link below:</p><p><a href='{resetLink}'>Reset Password</a></p>";
            _emailService.SendEmailNotification(email, resetLink, message);
            return true;
        }

        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            // Validate the token
            var isValidToken = await ValidateTokenAsync(email, token);
            if (!isValidToken)
            {
                Console.WriteLine("Invalid or expired token.");
                return false;
            }

            // Retrieve the user by email
            //var userDtl = await _patientService.GetPatientByEmail(email);
            var userDtl = await _userManager.FindByEmailAsync(email);
            if (userDtl == null)
            {
                Console.WriteLine("User not found.");
                return false;
            }
            await _userManager.RemovePasswordAsync(userDtl);
            var result = await _userManager.AddPasswordAsync(userDtl, newPassword);

            // Map Patient (or custom model) to ApplicationUser
            //var applicationUser = new ApplicationUser
            //{
            //    Id = userDtl.Id,         // Map necessary properties
            //    Email = userDtl.Email,
            //    PasswordHash = userDtl.PasswordHash
            //};

            // Hash the new password using PasswordHasher
           
            // Invalidate the token after a successful password update
            _inMemoryTokenStore.InvalidateToken(email);

            return true; // Successful password reset
        }

        public async Task<bool> ValidateTokenAsync(string email, string token)
        {
            var storedTokenInfo = _inMemoryTokenStore.GetToken(email);
            if (storedTokenInfo == null)
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
