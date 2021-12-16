using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PortalJustNotificationManager.Model
{
   [XmlRoot("Dosar")]
   public class Dosar
   {
      [XmlElement("numar")]
      public string Numar;

      [XmlElement("data")]
      public DateTime Data;

      [XmlElement("institutie")]
      public string Institutie;

      [XmlElement("departament")]
      public string Departament;

      [XmlElement("categorieCazNume")]
      public string CategorieCaz;

      [XmlElement("stadiuProcesualNume")]
      public string StadiuProcesual;

      [XmlElement("obiect")]
      public string Obiect;

      [XmlArray("parti"),XmlArrayItem("DosarParte")]
      public List<Parte> parti;

      [XmlArray("sedinte"),XmlArrayItem("DosarSedinta")]
      public List<Sedinta> sedinte;
      
      public Dosar() { }

      public override bool Equals(object obj)
      {
         return obj is Dosar dosar &&
                Numar == dosar.Numar &&
                Data == dosar.Data;
      }

      internal void CompareTo(Dosar caseFile)
      {
         if(this.Equals(caseFile))
         {
            if (Institutie != caseFile.Institutie)
            {
               Institutie = caseFile.Institutie;
               new Notification("Institutie Schimbata", "Institutia cazului a fost schimbata cu: " + caseFile.Institutie);
            }

            if (Departament != caseFile.Departament)
            {
               Departament = caseFile.Departament;
               new Notification("Departament Schimbat", "Departamentul a fost schimbata cu: " + caseFile.Departament);
            }

            if (CategorieCaz != caseFile.CategorieCaz)
            {
               CategorieCaz = caseFile.CategorieCaz;
               new Notification("Categoria Cazului Schimbata", "Categoria cazului a fost schimbata cu: " + caseFile.CategorieCaz);
            }

            if (StadiuProcesual != caseFile.StadiuProcesual)
            {
               StadiuProcesual = caseFile.StadiuProcesual;
               new Notification("Stadiu Procesual Schimbat", "Stadiul procesual al cazului a fost schimbat cu: " + caseFile.StadiuProcesual);
            }

            if (Obiect != caseFile.Obiect)
            {
               Obiect = caseFile.Obiect;
               new Notification("Obiect Schimbat", "Obiectul cazului a fost schimbat cu: " + caseFile.Obiect);
            }
         }
      }


   }
}
