using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalJustNotificationManager.Model
{
   class CaleAtac
   {
      internal DateTime Data { get; set; }
      internal string Parte { get; set; }
      internal string TipCaleDeAtac { get; set; }

      public CaleAtac(DateTime data, string parte, string tipCaleDeAtac)
      {
         this.Data = data;
         this.Parte = parte;
         this.TipCaleDeAtac = tipCaleDeAtac;
      }

      public override bool Equals(object obj)
      {
         return obj is CaleAtac atac &&
                Data == atac.Data &&
                Parte == atac.Parte &&
                TipCaleDeAtac == atac.TipCaleDeAtac;
      }

      public override string ToString()
      {
         return Parte + ": " + TipCaleDeAtac;
      }
   }
}
