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
      public string Number;

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
      public List<AttackWay> AttackWays;

      public CaseFile() { }

      public override bool Equals(object obj)
      {
         return obj is CaseFile dosar &&
                Number == dosar.Number &&
                Date == dosar.Date;
      }

      internal void CompareTo(CaseFile caseFile, CaseHandler parentHandler)
      {
         if (this.Equals(caseFile))
         {
            if (Institution != caseFile.Institution)
            {
               Institution = caseFile.Institution;
               parentHandler.CaseNotifications.Add(new Notification("Institutie Schimbata", "Institutia cazului a fost schimbata cu: " + caseFile.Institution));
            }

            if (Department != caseFile.Department)
            {
               Department = caseFile.Department;
               parentHandler.CaseNotifications.Add(new Notification("Departament Schimbat", "Departamentul a fost schimbata cu: " + caseFile.Department));
            }

            if (CaseCategory != caseFile.CaseCategory)
            {
               CaseCategory = caseFile.CaseCategory;
               parentHandler.CaseNotifications.Add(new Notification("Categoria Cazului Schimbata", "Categoria cazului a fost schimbata cu: " + caseFile.CaseCategory));
            }

            if (ProcessStage != caseFile.ProcessStage)
            {
               ProcessStage = caseFile.ProcessStage;
               parentHandler.CaseNotifications.Add(new Notification("Stadiu Procesual Schimbat", "Stadiul procesual al cazului a fost schimbat cu: " + caseFile.ProcessStage));
            }

            if (CaseObject != caseFile.CaseObject)
            {
               CaseObject = caseFile.CaseObject;
               parentHandler.CaseNotifications.Add(new Notification("Obiect Schimbat", "Obiectul cazului a fost schimbat cu: " + caseFile.CaseObject));
            }

            CompareAtackWays(caseFile.AttackWays, parentHandler);
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
            parentHandler.CaseNotifications.Add(new Notification("Sedinta Noua Adaugata", meeting.ToString()));
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
            parentHandler.CaseNotifications.Add(new Notification("Parte Noua Adaugata", side.ToString()));
         }
      }

      private void CompareAtackWays(List<AttackWay> attackWays, CaseHandler parentHandler)
      {
         attackWays.ForEach(x =>
         {
            AttackWay equivalentWay = AttackWays.Find(y => y.Equals(x));
            if (equivalentWay != null)
            {
               equivalentWay.CompareTo(x, parentHandler);
            }
         });

         List<AttackWay> newAttackWays = attackWays.Where(x => AttackWays.Contains(x) == false).ToList();
         foreach (AttackWay attackWay in newAttackWays)
         {
            parentHandler.CaseNotifications.Add(new Notification("Cale de Atac Noua Adaugata", attackWay.ToString()));
         }
      }
   }
}
