using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace PortalJustNotificationManager.Model
{
   [XmlRoot("Dosar")]
   public class CaseFile
   {
      [XmlElement("numar")]
      public string Number { get; set; }

      [XmlElement("data")]
      public DateTime Date;

      [XmlElement("institutie")]
      public string Institution;

      [XmlElement("departament")]
      public string Department;

      [XmlElement("categorieCazNume")]
      public string CaseCategory;

      [XmlElement("stadiuProcesualNume")]
      public string ProcessStage;

      [XmlElement("obiect")]
      public string CaseObject;

      [XmlArray("parti"), XmlArrayItem("DosarParte")]
      public List<Side> Sides;

      [XmlArray("sedinte"), XmlArrayItem("DosarSedinta")]
      public List<Meeting> Meetings;

      [XmlArray("caiAtac"), XmlArrayItem("DosarCaleAtac")]
      public List<AttackWay> CaseAttackWays;

      public CaseFile() { }

      public override bool Equals(object obj)
      {
         return obj is CaseFile dosar &&
                Number == dosar.Number &&
                Date == dosar.Date;
      }

      public override string ToString()
      {
         string description =
               "Numar dosar: " + Number +
                "\nData: " + Date.Day + "." + Date.Month + "." + Date.Year +
                "\nInstitutie: " + Institution +
                "\nDepartament: " + Department +
                "\nCategorie Caz: " + CaseCategory +
                "\nStadiu Procesual: " + ProcessStage +
                "\nObiect: " + CaseObject;

         if (Sides.Count > 0)
         {
            description += "\n\nParti:\n________________________________________________________________";

            foreach (Side s in Sides)
            {
               description += "\n" + s.ToString();
            }
         }

         if (CaseAttackWays.Count > 0)
         {
            description += "\n\nCai de Atac:\n________________________________________________________________";
            foreach (AttackWay a in CaseAttackWays)
            {
               description += "\n" + a.ToString();
            }
         }

         if (Meetings.Count > 0)
         {
            description += "\n\nSedinte:\n________________________________________________________________";
            foreach (Meeting m in Meetings)
            {
               description += "\n" + m.ToString();
            }
         }

         return description;
      }

      internal void CompareTo(CaseFile caseFile, CaseHandler parentHandler)
      {
         if (this.Equals(caseFile))
         {
            if (Institution != caseFile.Institution)
            {
               Institution = caseFile.Institution;
               parentHandler.AddNotification(new Notification("Institutie Schimbata", "Institutia cazului a fost schimbata cu: " + caseFile.Institution));
            }

            if (Department != caseFile.Department)
            {
               Department = caseFile.Department;
               parentHandler.AddNotification(new Notification("Departament Schimbat", "Departamentul a fost schimbata cu: " + caseFile.Department));
            }

            if (CaseCategory != caseFile.CaseCategory)
            {
               CaseCategory = caseFile.CaseCategory;
               parentHandler.AddNotification(new Notification("Categoria Cazului Schimbata", "Categoria cazului a fost schimbata cu: " + caseFile.CaseCategory));
            }

            if (ProcessStage != caseFile.ProcessStage)
            {
               ProcessStage = caseFile.ProcessStage;
               parentHandler.AddNotification(new Notification("Stadiu Procesual Schimbat", "Stadiul procesual al cazului a fost schimbat cu: " + caseFile.ProcessStage));
            }

            if (CaseObject != caseFile.CaseObject)
            {
               CaseObject = caseFile.CaseObject;
               parentHandler.AddNotification(new Notification("Obiect Schimbat", "Obiectul cazului a fost schimbat cu: " + caseFile.CaseObject));
            }

            CompareAtackWays(caseFile.CaseAttackWays, parentHandler);
            CompareCaseSides(caseFile.Sides, parentHandler);
            CompareMeetings(caseFile.Meetings, parentHandler);
         }
      }

      private void CompareMeetings(List<Meeting> meetings, CaseHandler parentHandler)
      {
         meetings.ForEach(x =>
         {
            Meeting equivalentMeeting = Meetings.Find(y => y.Equals(x));
            if (equivalentMeeting != null)
            {
               equivalentMeeting.CompareTo(x, parentHandler);
            }          
         });

         List<Meeting> newMeetings = meetings.Where(x => Meetings.Contains(x) == false).ToList();
         foreach(Meeting meeting in newMeetings)
         {
            parentHandler.AddNotification(new Notification("Sedinta Noua Adaugata", meeting.ToString()));
            this.Meetings.Add(meeting);
         }
      }

      private void CompareCaseSides(List<Side> sides, CaseHandler parentHandler)
      {
         sides.ForEach(x =>
         {
            Side equivalentSide = Sides.Find(y => y.Equals(x));
            if (equivalentSide != null)
            {
               equivalentSide.CompareTo(x, parentHandler);
            }
         });

         List<Side> newSides = sides.Where(x => Sides.Contains(x) == false).ToList();
         foreach (Side side in newSides)
         {
            parentHandler.AddNotification(new Notification("Parte Noua Adaugata", side.ToString()));
            this.Sides.Add(side);
         }
      }

      private void CompareAtackWays(List<AttackWay> attackWays, CaseHandler parentHandler)
      {
         attackWays.ForEach(x =>
         {
            AttackWay equivalentWay = CaseAttackWays.Find(y => y.Equals(x));
            if (equivalentWay != null)
            {
               equivalentWay.CompareTo(x, parentHandler);
            }
         });

         List<AttackWay> newAttackWays = attackWays.Where(x => CaseAttackWays.Contains(x) == false).ToList();
         foreach (AttackWay attackWay in newAttackWays)
         {
            parentHandler.AddNotification(new Notification("Cale de Atac Noua Adaugata", attackWay.ToString()));
            this.CaseAttackWays.Add(attackWay);
         }
      }
   }
}
