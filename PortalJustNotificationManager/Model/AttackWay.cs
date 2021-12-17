using System;
using System.Xml.Serialization;

namespace PortalJustNotificationManager.Model
{
   [XmlRoot("DosarCaleAtac")]
   public class AttackWay
   {
      [XmlElement("dataDeclarare")]
      public DateTime Date { get; set; }

      [XmlElement("parteDeclaratoare")]
      public string DeclaringSide { get; set; }

      [XmlElement("tipCaleAtac")]
      public string TypeOfAttack { get; set; }

      public AttackWay() { }

      public AttackWay(DateTime data, string parte, string tipCaleDeAtac)
      {
         this.Date = data;
         this.DeclaringSide = parte;
         this.TypeOfAttack = tipCaleDeAtac;
      }

      public override bool Equals(object obj)
      {
         return obj is AttackWay atac &&
                Date == atac.Date &&
                DeclaringSide == atac.DeclaringSide;
      }

      public override string ToString()
      {
         return TypeOfAttack + ": " + DeclaringSide;
      }

      internal void CompareTo(AttackWay attackWay, CaseHandler parentHandler)
      {
         if (this.Equals(attackWay))
         {
            if (this.Equals(attackWay))
            {
               if (TypeOfAttack != attackWay.TypeOfAttack)
               {
                  TypeOfAttack = attackWay.TypeOfAttack;
                  parentHandler.CaseNotifications.Add(new Notification("Cale De Atac Schimbata", "Partea declaratoare: " + DeclaringSide + "si-a schimbat tip caii de atac in: " + TypeOfAttack));
               }        
            }
         }
      }
   }
}
