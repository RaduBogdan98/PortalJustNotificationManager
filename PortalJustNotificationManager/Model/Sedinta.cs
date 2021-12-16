using System;
using System.Xml.Serialization;

namespace PortalJustNotificationManager.Model
{
   [XmlRoot("DosarSedinta")]
   public class Sedinta
   {
      [XmlElement("complet")]
      public string Complet { get; set; }

      [XmlElement("data")]
      public DateTime Data { get; set; }

      [XmlElement("ora")]
      public string Ora { get; set; }

      [XmlElement("solutie")]
      public string Solutie { get; set; }

      [XmlElement("solutieSumar")]
      public string Sumar { get; set; }

      public Sedinta() { }

      public Sedinta(string complet, DateTime data, string ora)
      {
         Complet = complet;
         Data = data;
         Ora = ora;
         Solutie = "";
         Sumar = "";
      }

      public override bool Equals(object obj)
      {
         return obj is Sedinta sedinta &&
                Complet == sedinta.Complet &&
                Data == sedinta.Data &&
                Ora == sedinta.Ora;
      }
   }
}
