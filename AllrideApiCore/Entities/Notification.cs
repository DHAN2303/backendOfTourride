namespace AllrideApiRepository.Repositories.Concrete
{
    public class PersonNotificationMuteSettings
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public bool isMute { get; set; }
    }

    public class PersonNotification
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public string content { get; set; }
        public DateTime sendDateTime { get; set; }
    }

    public class GroupNotificationMuteSettings
    {
        public int Id { get; set; }
        public int groupId { get; set; }
        public bool isMute { get; set; }
    }

    public class GroupNotification
    {
        public int Id { get; set; }
        public int groupId { get; set; }
        public string content { get; set; }
        public DateTime sendDateTime { get; set; }
    }

    public class ClubNotificationMuteSettings
    {
        public int Id { get; set; }
        public int clubId { get; set; }
        public bool isMute { get; set; }
    }

    public class ClubNotification
    {
        public int Id { get; set; }
        public int clubId { get; set; }
        public string content { get; set; }
        public DateTime sendDateTime { get; set; }
    }

    public class NotificationTimeCatch
    {
        public int Id { get; set; }
        public int notification_id { get; set; }
        public bool type { get; set; }
        public DateTime sendDateTime { get; set; }
    }
}
