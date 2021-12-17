using System.Xml.Serialization;

namespace PortalJustNotificationManager.Model
{
   [XmlRoot("DosarParte")]
   public class Side
   {
      [XmlElement("nume")]
      public string Name { get; set; }

      [XmlElement("calitateParte")]
      public string Quality { get; set; }

      public Side() { }

      public Side(string nume, string calitate)
      {
         this.Name = nume;
         this.Quality = calitate;
      }

      #region Methods
      public override bool Equals(object obj)
      {
         return obj is Side parte &&
                Name == parte.Name;
      }

      public override string ToString()
      {
         return Name + ": " + Quality;
      }

      internal void CompareTo(Side side, CaseHandler parentHandler)
      {
         if (this.Equals(side))
         {
            if (Quality != side.Quality)
            {
               Quality = side.Quality;
               parentHandler.CaseNotifications.Add(new Notification("Schimbare Calitate Parte", "Partea " + Name + " a devenit " + Quality));
            }
         }      
      }
      #endregion
   }
}
