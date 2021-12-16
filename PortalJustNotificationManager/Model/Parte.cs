using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalJustNotificationManager.Model
{
   class Parte
   {
      internal string Nume { get; set; }
      internal string Calitate { get; set; }

      public Parte(string nume, string calitate)
      {
         this.Nume = nume;
         this.Calitate = calitate;
      }

      #region Methods
      public override bool Equals(object obj)
      {
         return obj is Parte parte &&
                Nume == parte.Nume &&
                Calitate == parte.Calitate;
      }

      public override string ToString()
      {
         return Nume + ": " + Calitate;
      }
      #endregion
   }
}
