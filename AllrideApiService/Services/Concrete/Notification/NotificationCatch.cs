using AllrideApiRepository;
using AllrideApiRepository.Repositories.Concrete;
using Microsoft.Extensions.Hosting;

namespace AllrideApiService.Services.Concrete.Notification
{
    public class NotificationCatch : BackgroundService
    {
        private readonly AllrideApiDbContext _context;

        public NotificationCatch(AllrideApiDbContext context)
        {
            _context = context;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var currentDate = DateTime.UtcNow;

                var person_notification = _context.person_notification
                                            .Where(n => n.sendDateTime <= currentDate)
                                            .OrderBy(n => Math.Abs((n.sendDateTime - currentDate).TotalSeconds))
                                            .FirstOrDefault();

                var group_notification = _context.group_notification
                            .Where(n => n.sendDateTime <= currentDate)
                            .OrderBy(n => Math.Abs((n.sendDateTime - currentDate).TotalSeconds))
                            .FirstOrDefault();

                DateTime accessTime = new DateTime();
                bool notificationType = false;//person: false || group: true
                int notificationId = 0;
                if(person_notification != null && group_notification != null)
                {
                    accessTime = person_notification.sendDateTime < group_notification.sendDateTime ? person_notification.sendDateTime : group_notification.sendDateTime;
                    notificationType = person_notification.sendDateTime < group_notification.sendDateTime ? false : true;
                    notificationId = person_notification.sendDateTime < group_notification.sendDateTime ? person_notification.Id : group_notification.Id;
                }
                else if(person_notification == null && group_notification != null)
                {
                    accessTime = group_notification.sendDateTime;
                    notificationType = true;
                    notificationId = group_notification.Id;
                }
                else if (person_notification != null && group_notification == null)
                {
                    accessTime = person_notification.sendDateTime;
                    notificationType = false;
                    notificationId = person_notification.Id;
                }

                var catchData = _context.notification_time_catch.FirstOrDefault();
                if (catchData != null)
                {
                    catchData.sendDateTime = accessTime;
                    catchData.type = notificationType;
                    catchData.notification_id = notificationId;

                    _context.SaveChanges();

                }
                else
                {
                    var newCatchData = new NotificationTimeCatch
                    {
                        notification_id = notificationId,
                        type = notificationType,
                        sendDateTime = accessTime,
                    };

                    _context.notification_time_catch.Add(newCatchData);
                    _context.SaveChanges();
                }
            }
        }
    }
}
