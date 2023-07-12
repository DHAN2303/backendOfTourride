using AllrideApi.Hubs;
using AllrideApiRepository;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace AllrideApiService.Services.Concrete.Notification
{
    public class NotificationService : BackgroundService
    {
        private readonly AllrideApiDbContext _context;
        private readonly IHubContext<ChatHubs> _hubContext;

        public NotificationService(AllrideApiDbContext context, IHubContext<ChatHubs> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Retrieve the scheduled notifications from the database based on the date and time

                var notificationCatch = _context.notification_time_catch.FirstOrDefault();

                var tasks = new List<Task>();

                if (notificationCatch != null)
                {
                    if (notificationCatch.type == false)
                    {
                        var person_notifications = _context.person_notification
                            .Where(n => n.Id == notificationCatch.notification_id)
                            .ToList();
                        Parallel.ForEach(person_notifications, notification =>
                        {
                            tasks.Add(_hubContext.Clients.User(notification.userId.ToString()).SendAsync("ReceivePersonNotification", notification));
                        });

                    }
                    else
                    {
                        var group_notifications = _context.group_notification
                            .Where(n => n.Id == notificationCatch.notification_id)
                            .ToList();

                        Parallel.ForEach(group_notifications, notification =>
                        {
                            tasks.Add(_hubContext.Clients.Group(notification.groupId.ToString()).SendAsync("ReceiveGroupNotification", notification));
                        });
                    }

                    await Task.WhenAll(tasks);

                    var currentDate = DateTime.UtcNow;
                    var sendTime = notificationCatch.sendDateTime - currentDate;

                    // Delay for a specific interval before checking for new notifications
                    await Task.Delay(TimeSpan.FromMinutes(sendTime.TotalMinutes), stoppingToken);
                }

            }
        }
    }
}
