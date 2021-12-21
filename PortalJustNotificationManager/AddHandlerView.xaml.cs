using PortalJustNotificationManager.API;
using PortalJustNotificationManager.Model;
using System;
using System.Windows;

namespace PortalJustNotificationManager
{
   public partial class AddHandlerView : Window
   {
      public AddHandlerView()
      {
         InitializeComponent();
      }

      private async void Button_Click(object sender, RoutedEventArgs e)
      {
         PortalJustHttpClient httpClient = new PortalJustHttpClient();
         string caseFileNumber = this.CaseFileNumberTextBox.Text;

         try
         {
            CaseFile retrievedCaseFile = await httpClient.FindCaseFile(caseFileNumber);
            RetrievedCaseHandler = new CaseHandler(this.HandlerNameTextBox.Text, retrievedCaseFile);
            RetrievedCaseHandler.AddNotification(new Notification("Statut Curent", retrievedCaseFile.ToString()));

            this.DialogResult = true;
         }
         catch(Exception)
         {
            MessageBox.Show("Eroare: Numarul dosarului nu exista!");
         }      
      }

      internal CaseHandler RetrievedCaseHandler;
   }
}
