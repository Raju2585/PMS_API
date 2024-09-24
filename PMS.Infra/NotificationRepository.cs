using Microsoft.EntityFrameworkCore;
using PMS.Application.Repository_Interfaces;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Infra
{
    public class NotificationRepository : INotificationRepository
    {
      private readonly IApplicationDbContext _context;
    public NotificationRepository(IApplicationDbContext context)
    {
            _context = context;
    }

        public async Task ClearNotifications()
        {
            var notifications=await GetAllNotifications();
            _context.Notifications.RemoveRange(notifications);
        }

        public async Task<Notification> CreateNotification(string message)
        {
            var notification = await _context.Notifications
           .FirstOrDefaultAsync(n => n.Notification_Message == message);

            if (notification != null)
            {
                notification.Notifcation_Count++;
                _context.Notifications.Update(notification);
            }
            else
            {
                notification = new Notification { Notification_Message = message, Notifcation_Count = 1 };
                await _context.Notifications.AddAsync(notification);
            }

            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task<List<Notification>> GetAllNotifications()
        {
            return await _context.Notifications.ToListAsync();
        }

        public async Task<bool> MarkAsRead(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                return false; 
            }

            
            notification.IsRead = true;
            _context.Notifications.Update(notification);

          
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
