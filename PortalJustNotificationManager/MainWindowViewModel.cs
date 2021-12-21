using PortalJustNotificationManager.API;
using PortalJustNotificationManager.Model;
using PortalJustNotificationManager.Persistence;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace PortalJustNotificationManager
{
   class MainWindowViewModel : INotifyPropertyChanged
   {
      private ObservableCollection<CaseHandler> caseHandlers;
      private CaseHandler selectedCaseHandler;
      private PortalJustHttpClient httpClient;
      internal PersistanceManager<ObservableCollection<CaseHandler>> persistanceManager;

      public MainWindowViewModel()
      {
         persistanceManager = new PersistanceManager<ObservableCollection<CaseHandler>>();
         ObservableCollection<CaseHandler> persistedCaseHandlers = persistanceManager.Deserialize("data.bin");
         this.caseHandlers = persistedCaseHandlers!=null? persistedCaseHandlers : new ObservableCollection<CaseHandler>();

         this.httpClient = new PortalJustHttpClient();
         this.RunNotificationRetrievingBackgroundWorker();
      }

      #region Background Worker
      private BackgroundWorker getCaseNotificationsWorker;

      private void RunNotificationRetrievingBackgroundWorker()
      {
         this.getCaseNotificationsWorker = new BackgroundWorker();
         this.getCaseNotificationsWorker.DoWork += (o, ea) => this.RunCaseUpdaterOnTimer();
         this.getCaseNotificationsWorker.RunWorkerAsync();
      }

      private void RunCaseUpdaterOnTimer()
      {
         while (true)
         {
            if (caseHandlers.Count > 0)
            {
               UpdateCaseFiles();
               Thread.Sleep(3600000);
            }
         }
      }

      private void UpdateCaseFiles()
      {
         foreach (var caseHandler in CaseHandlers)
         {
            caseHandler.UpdateCase(httpClient);
         }
      }

      #endregion

      #region Properties
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
            if (selectedCaseHandler != null)
            {
               selectedCaseHandler.HasNotifications = false;
            }

            NotifyPropertyChanged(nameof(SelectedCaseHandler));
            NotifyPropertyChanged(nameof(IsAnythingSelected));
         }
      }

      public bool IsAnythingSelected
      {
         get
         {
            return this.SelectedCaseHandler != null && this.CaseHandlers.Count > 0;
         }
      }
      #endregion

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
