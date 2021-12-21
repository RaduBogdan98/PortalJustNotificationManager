﻿using System;
using System.Xml.Serialization;

namespace PortalJustNotificationManager.Model
{
   [XmlRoot("DosarSedinta")]
   public class Meeting
   {
      [XmlElement("complet")]
      public string Complet { get; set; }

      [XmlElement("data")]
      public DateTime Date { get; set; }

      [XmlElement("ora")]
      public string Hour { get; set; }

      [XmlElement("solutie")]
      public string Solution { get; set; }

      [XmlElement("solutieSumar")]
      public string Summary { get; set; }

      public Meeting() { }

      public Meeting(string complet, DateTime data, string ora)
      {
         Complet = complet;
         Date = data;
         Hour = ora;
      }

      public override string ToString()
      {
         string description = Date.Day + "." + Date.Month + "." + Date.Year + "\n" + Hour;

         if (!String.IsNullOrEmpty(Solution))
         {
            description += "\nSolutie: " + Solution.Trim();
         }

         if (!String.IsNullOrEmpty(Summary))
         {
            description += "\nSumar: " + Summary.Trim() + "\n";
         }

         return description;
      }

      public override bool Equals(object obj)
      {
         return obj is Meeting sedinta &&
                Complet == sedinta.Complet &&
                Date == sedinta.Date &&
                Hour == sedinta.Hour;
      }

      internal void CompareTo(Meeting meeting, CaseHandler parentHandler)
      {
         if (this.Equals(meeting))
         {
            if (Solution != meeting.Solution)
            {
               Solution = meeting.Solution;
               parentHandler.AddNotification(new Notification("Solutie Adaugata", "O solutie fost adaugata sedintei din data de " + Date + " ora " + Hour + ", iar aceasta este: " + Solution));
            }

            if (Summary != meeting.Summary)
            {
               Summary = meeting.Summary;
               parentHandler.AddNotification(new Notification("Sumar Adaugat", "Un sumar fost adaugata sedintei din data de " + Date + " ora " + Hour + ", iar acesta este: " + Summary));
            }
         }
      }
   }
}
