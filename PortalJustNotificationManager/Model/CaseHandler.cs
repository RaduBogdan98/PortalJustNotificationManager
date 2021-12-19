using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PortalJustNotificationManager.Model
{
   class CaseHandler : INotifyPropertyChanged
   {
      private bool hasNotifications;
      private ObservableCollection<Notification> caseNotifications;

      public CaseHandler(string name, CaseFile caseFile)
      {
         this.HandlerName = name;
         this.CaseFile = caseFile;
         this.caseNotifications = new ObservableCollection<Notification>();
         this.HasNotifications = false;
      }

      public string HandlerName { get; set; }
      public CaseFile CaseFile { get; set; }

      public ObservableCollection<Notification> CaseNotifications
      {
         get
         {
            return caseNotifications;
         }
         set
         {
            caseNotifications = value;
            NotifyPropertyChanged(nameof(CaseNotifications));
         }
      }

      public bool HasNotifications
      {
         get
         {
            return hasNotifications;
         }
         set
         {
            hasNotifications = value;
            NotifyPropertyChanged(nameof(HasNotifications));
         }
      }

      #region Methods
      internal void CompareCases(CaseFile caseFile)
      {
         this.CaseFile.CompareTo(caseFile, this);
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
