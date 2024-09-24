using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Repository_Interfaces
{
    public interface INotificationRepository
    {
        Task<Notification> CreateNotification(string message);
        Task<List<Notification>> GetAllNotifications();
        Task<bool> MarkAsRead(int id);
        Task ClearNotifications();

    }
}
