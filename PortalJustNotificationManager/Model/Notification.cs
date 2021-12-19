namespace PortalJustNotificationManager.Model
{
   class Notification
   {
      public string Title { get; }
      public string Description { get; }

      public Notification(string title, string description)
      {
         Title = title;
         Description = description;
      }
   }
}
