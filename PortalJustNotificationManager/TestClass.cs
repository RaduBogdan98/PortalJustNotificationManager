using PortalJustNotificationManager.Model;
using System;
using System.Threading;

namespace PortalJustNotificationManager
{
   class TestClass
   {
      internal void Test()
      {
         MainWindowViewModel.GetInstance().CaseHandlers[0].shouldDisplayNotificationBalloonTip = false;
         CaseFile updatedCase = this.cloneCaseFile(MainWindowViewModel.GetInstance().CaseHandlers[0].CaseFile);
         updatedCase.Institution = "Institutia lui Radu";
         MainWindowViewModel.GetInstance().CaseHandlers[0].CaseFile.CompareTo(updatedCase, MainWindowViewModel.GetInstance().CaseHandlers[0]);
         Thread.Sleep(10000);

         MainWindowViewModel.GetInstance().CaseHandlers[0].shouldDisplayNotificationBalloonTip = false;
         updatedCase = this.cloneCaseFile(MainWindowViewModel.GetInstance().CaseHandlers[0].CaseFile);
         updatedCase.Department = "Departament nou";
         updatedCase.CaseObject = "Obiect nou";
         MainWindowViewModel.GetInstance().CaseHandlers[0].CaseFile.CompareTo(updatedCase, MainWindowViewModel.GetInstance().CaseHandlers[0]);
         Thread.Sleep(10000);

         MainWindowViewModel.GetInstance().CaseHandlers[0].shouldDisplayNotificationBalloonTip = false;
         updatedCase = this.cloneCaseFile(MainWindowViewModel.GetInstance().CaseHandlers[0].CaseFile);
         updatedCase.CaseCategory = "Categorie noua";
         updatedCase.ProcessStage = "Atac";
         updatedCase.Meetings[0].Solution = "Solutie noua";
         updatedCase.Meetings[0].Summary = "Sumar nou";
         MainWindowViewModel.GetInstance().CaseHandlers[0].CaseFile.CompareTo(updatedCase, MainWindowViewModel.GetInstance().CaseHandlers[0]);
         Thread.Sleep(10000);

         MainWindowViewModel.GetInstance().CaseHandlers[0].shouldDisplayNotificationBalloonTip = false;
         updatedCase = this.cloneCaseFile(MainWindowViewModel.GetInstance().CaseHandlers[0].CaseFile);
         updatedCase.CaseAttackWays[0].DeclaringSide = "Radu";
         updatedCase.Meetings.Add(new Meeting("Complet Timisoara", new DateTime(), "09:00"));
         MainWindowViewModel.GetInstance().CaseHandlers[0].CaseFile.CompareTo(updatedCase, MainWindowViewModel.GetInstance().CaseHandlers[0]);
         Thread.Sleep(10000);

         MainWindowViewModel.GetInstance().CaseHandlers[0].shouldDisplayNotificationBalloonTip = false;
         updatedCase = this.cloneCaseFile(MainWindowViewModel.GetInstance().CaseHandlers[0].CaseFile);
         updatedCase.Sides[0].Quality = "Calitate noua";
         updatedCase.Sides.Add(new Side("Ionica", "Inculpat"));
         MainWindowViewModel.GetInstance().CaseHandlers[0].CaseFile.CompareTo(updatedCase, MainWindowViewModel.GetInstance().CaseHandlers[0]);
         Thread.Sleep(10000);

         MainWindowViewModel.GetInstance().CaseHandlers[0].shouldDisplayNotificationBalloonTip = false;
         updatedCase = this.cloneCaseFile(MainWindowViewModel.GetInstance().CaseHandlers[0].CaseFile);
         updatedCase.CaseAttackWays.Add(new AttackWay(new DateTime(), "Ionica", "Apel"));
         MainWindowViewModel.GetInstance().CaseHandlers[0].CaseFile.CompareTo(updatedCase, MainWindowViewModel.GetInstance().CaseHandlers[0]);
         Thread.Sleep(10000);
      }

      private CaseFile cloneCaseFile(CaseFile caseFile)
      {
         CaseFile clone = new CaseFile();
         clone.Number = caseFile.Number;
         clone.Date = caseFile.Date;
         clone.Institution = caseFile.Institution;
         clone.Department = caseFile.Department;
         clone.CaseCategory = caseFile.CaseCategory;
         clone.ProcessStage = caseFile.ProcessStage;
         clone.CaseObject = caseFile.CaseObject;

         clone.Sides = new System.Collections.Generic.List<Side>();
         foreach (var side in caseFile.Sides)
         {
            Side cloneSide = new Side(side.Name, side.Quality);
            clone.Sides.Add(cloneSide);
         }

         clone.Meetings = new System.Collections.Generic.List<Meeting>();
         foreach (var meeting in caseFile.Meetings)
         {
            Meeting cloneMeeting = new Meeting(meeting.Complet, meeting.Date, meeting.Hour);
            cloneMeeting.Solution = meeting.Solution;
            cloneMeeting.Summary = meeting.Summary;
            clone.Meetings.Add(cloneMeeting);
         }

         clone.CaseAttackWays = new System.Collections.Generic.List<AttackWay>();
         foreach (var attackWay in caseFile.CaseAttackWays)
         {
            AttackWay cloneAttack = new AttackWay(attackWay.Date, attackWay.DeclaringSide, attackWay.TypeOfAttack);
            clone.CaseAttackWays.Add(cloneAttack);
         }

         return clone;
      }
   }
}
