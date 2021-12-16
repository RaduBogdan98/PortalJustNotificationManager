using System;

namespace PortalJustNotificationManager.Model
{
   class Sedinta
   {
      internal string Complet { get; set; }
      internal DateTime Data { get; set; }
      internal string Ora { get; set; }
      internal string Solutie { get; set; }
      internal string Sumar { get; set; }

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
