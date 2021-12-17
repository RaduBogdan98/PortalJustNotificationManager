using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PortalJustNotificationManager.Model
{
   class CaseHandler : INotifyPropertyChanged
   {
      private ObservableCollection<Notification> caseNotifications;

      public CaseHandler(string name, CaseFile caseFile)
      {
         this.Name = name;
         this.CaseFile = caseFile;
         this.caseNotifications = new ObservableCollection<Notification>();
      }

      internal string Name { get; set; }
      internal CaseFile CaseFile { get; set; }

      internal ObservableCollection<Notification> CaseNotifications
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

      internal bool HasNotifications
      {
         get
         {
            return caseNotifications != null && caseNotifications.Count > 0;
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
