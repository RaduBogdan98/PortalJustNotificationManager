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
   }
}
