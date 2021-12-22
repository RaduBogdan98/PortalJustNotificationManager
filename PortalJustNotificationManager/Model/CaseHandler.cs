using PortalJustNotificationManager.API;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace PortalJustNotificationManager.Model
{
   [XmlRoot("Handler")]
   public class CaseHandler : INotifyPropertyChanged
   {
      private bool hasNotifications;
      internal bool shouldDisplayNotificationBalloonTip = false;
      private ObservableCollection<Notification> caseNotifications;

      public CaseHandler(string name, CaseFile caseFile)
      {
         this.HandlerName = name;
         this.CaseFile = caseFile;
         this.caseNotifications = new ObservableCollection<Notification>();
         this.HasNotifications = false;
      }

      public CaseHandler() { }

      #region Properties
      [XmlElement("nume")]
      public string HandlerName { get; set; }
      [XmlElement("dosar")]
      public CaseFile CaseFile { get; set; }

      [XmlArray("notificari"), XmlArrayItem("Notificare")]
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

      [XmlElement("are-notificari")]
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
      #endregion

      #region Methods
      internal async void UpdateCase(PortalJustHttpClient httpClient)
      {
         try
         {
            this.shouldDisplayNotificationBalloonTip = false;
            CaseFile updatedCase = await httpClient.FindCaseFile(CaseFile.Number);
            this.CaseFile.CompareTo(updatedCase, this);
         }
         catch (Exception)
         {
            // TO DO
         }
      }

      internal void AddNotification(Notification newNotification)
      {
         if (HasNotifications == false)
         {
            HasNotifications = true;
         }

         if(shouldDisplayNotificationBalloonTip == false)
         {
            shouldDisplayNotificationBalloonTip = true;
            MainWindowViewModel.GetInstance().ShowToastNotification("Dosarul " + CaseFile.Number + " are notificari!");
         }

         System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)delegate
         {
            this.CaseNotifications.Insert(0, newNotification);
         });
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
