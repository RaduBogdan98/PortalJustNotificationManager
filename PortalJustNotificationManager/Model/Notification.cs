namespace PortalJustNotificationManager.Model
{
   class Notification
   {
      internal string Title { get; }
      internal string Description { get; }

      public Notification(string title, string description)
      {
         Title = title;
         Description = description;
      }
   }
}
