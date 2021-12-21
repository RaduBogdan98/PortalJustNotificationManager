using System.Xml.Serialization;

namespace PortalJustNotificationManager.Model
{
   [XmlRoot("Notificare")]
   public class Notification
   {
      [XmlElement("titlu")]
      public string Title { get; set; }
      [XmlElement("descriere")]
      public string Description { get; set; }

      public Notification() { }

      public Notification(string title, string description)
      {
         Title = title;
         Description = description;
      }
   }
}
