using PortalJustNotificationManager.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PortalJustNotificationManager
{
   class MainWindowViewModel : INotifyPropertyChanged
   {
      private ObservableCollection<CaseHandler> caseHandlers;
      private CaseHandler selectedCaseHandler;

      public MainWindowViewModel()
      {
         this.caseHandlers = new ObservableCollection<CaseHandler>();
         CaseFile caseFile = new CaseFile();
         caseFile.Number = "1234/12/12";
         CaseHandler handler1 = new CaseHandler("CaseHandler1", caseFile);
         CaseHandler handler2 = new CaseHandler("CaseHandler2", caseFile);
         handler1.HasNotifications = true;
         handler2.HasNotifications = true;

         handler1.CaseNotifications.Add(new Notification("Notificare 1", "Descrierea acestei notificari este aceasta: Notificare 1"));
         handler2.CaseNotifications.Add(new Notification("Notificare 2", "Descrierea acestei notificari este aceasta: Notificare 2"));
         handler2.CaseNotifications.Add(new Notification("Notificare 2", "Descrierea\n acestei\n notificari\n este\n aceasta:\n Notificare 2"));

         caseHandlers.Add(handler1);
         caseHandlers.Add(handler2);
      }

      public ObservableCollection<CaseHandler> CaseHandlers
      {
         get
         {
            return caseHandlers;
         }
         set
         {
            caseHandlers = value;
            NotifyPropertyChanged(nameof(CaseHandlers));
         }
      }
      
      public CaseHandler SelectedCaseHandler
      {
         get
         {
            return selectedCaseHandler;
         }
         set
         {
            selectedCaseHandler = value;
            selectedCaseHandler.HasNotifications = false;
            NotifyPropertyChanged(nameof(SelectedCaseHandler));
         }
      }

      #region PropertyChanged
      public event PropertyChangedEventHandler PropertyChanged;

      private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
      {
         if (PropertyChanged != null)
         {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
         }
      }
      #endregion
   }
}
