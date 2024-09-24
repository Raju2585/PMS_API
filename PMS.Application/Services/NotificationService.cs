using PMS.Application.Interfaces;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public async Task CreateNotificationForAppointment(Appointment appointment)
        {
            if(appointment.StatusId == 1)
            {
                var message = $"Your appointment at {appointment.HospitalName} has been booked successfully.";
                await _notificationRepository.CreateNotification(message);
            }
            
        }
        public async Task<List<Notification>> GetAllNotifications()
        {
            return await _notificationRepository.GetAllNotifications();
        }
        public async Task<bool> MarkAsRead(int id)
        {
            return await _notificationRepository.MarkAsRead(id);
        }
        public async Task ClearNotifications()
        {
             await _notificationRepository.ClearNotifications();

        }



    }
}
