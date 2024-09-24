using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Interfaces
{
    public interface INotificationService
    {
        Task CreateNotificationForAppointment(Appointment appointment);
        Task<List<Notification>> GetAllNotifications();
        Task<bool> MarkAsRead(int id);
        Task ClearNotifications();
        
    }
}
