using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace PortalJustNotificationManager.Model
{
   [XmlRoot("Notificare")]
   public class Notification : INotifyPropertyChanged
   {
      [XmlElement("titlu")]
      public string Title { get; set; }

      [XmlElement("descriere")]
      public string Description { get; set; }

      [XmlElement("citita")]
      public bool HasBeenRead { get; set; }

      public Notification() { }

      public Notification(string title, string description)
      {
         Title = title;
         Description = description;
         HasBeenRead = false;
      }

      internal void ReadNotification()
      {
         HasBeenRead = true;
         NotifyPropertyChanged(nameof(HasBeenRead));
      }

      #region PropertyChanged
      public event PropertyChangedEventHandler PropertyChanged;

      private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
      {
         if (PropertyChanged != null)
         {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
         }
      }
      #endregion
   }
}
