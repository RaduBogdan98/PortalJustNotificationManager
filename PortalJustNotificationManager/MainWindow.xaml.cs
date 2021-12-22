using PortalJustNotificationManager.Model;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Forms = System.Windows.Forms;

namespace PortalJustNotificationManager
{
   public partial class MainWindow : Window
   {
      private MainWindowViewModel viewModel;
      private Forms.NotifyIcon notifyIcon;

      public MainWindow()
      {
         InitializeComponent();
         BuildNotifyIcon();
         this.DataContext = viewModel = MainWindowViewModel.GetInstance();
         viewModel.ShowNotificationEvent += OnShowNotification;       
      }

      #region Methods
      protected override void OnStateChanged(EventArgs e)
      {
         if (this.WindowState == WindowState.Minimized)
         {
            this.Hide();
         }

         base.OnStateChanged(e);
      }

      protected override void OnClosing(CancelEventArgs e)
      {
         viewModel.persistanceManager.Serialize(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\portal_data.bin", viewModel.CaseHandlers);
         notifyIcon.Dispose();
      }

      #region Notify Icon
      private void BuildNotifyIcon()
      {
         notifyIcon = new Forms.NotifyIcon();
         notifyIcon.Text = "Portal Notificari";
         notifyIcon.Icon = new System.Drawing.Icon("Resources/appicon.ico");
         notifyIcon.MouseClick += OnTooltipClicked;
         notifyIcon.ContextMenuStrip = new Forms.ContextMenuStrip();
         notifyIcon.ContextMenuStrip.Items.Add("Open", null, OnOpenClicked);
         notifyIcon.ContextMenuStrip.Items.Add("Close", null, OnCloseClicked);
         notifyIcon.Visible = true;
      }

      private void OnTooltipClicked(object sender, Forms.MouseEventArgs e)
      {
         if (e.Button.Equals(Forms.MouseButtons.Left))
         {
            this.Show();
            this.WindowState = WindowState.Normal;
            this.Activate();
         }
      }

      private void OnOpenClicked(object sender, EventArgs e)
      {
         this.Show();
         this.WindowState = WindowState.Normal;
         this.Activate();
      }

      private void OnCloseClicked(object sender, EventArgs e)
      {
         this.Close();
      }

      private void OnShowNotification(string message)
      {
         notifyIcon.ShowBalloonTip(5000, "Notificare", message, Forms.ToolTipIcon.Info);
      }
      #endregion

      #region UI Click Events
      private void AddButton_Click(object sender, RoutedEventArgs e)
      {
         AddHandlerView view = new AddHandlerView();
         if (view.ShowDialog() == true)
         {
            viewModel.CaseHandlers.Add(view.RetrievedCaseHandler);
         }
      }

      private void DeleteButton_Click(object sender, RoutedEventArgs e)
      {
         viewModel.CaseHandlers.Remove(viewModel.SelectedCaseHandler);
      }

      private void Info_Click(object sender, RoutedEventArgs e)
      {
         CaseHandler caseHandler = ((Button)sender).DataContext as CaseHandler;
         caseHandler.AddNotification(new Notification("Status Curent", caseHandler.CaseFile.ToString()));
      }

      private void Expander_Expanded(object sender, RoutedEventArgs e)
      {
         viewModel.SelectedCaseHandler.HasNotifications = false;
         ((TextBlock)((Expander)sender).Header).FontWeight = FontWeights.Normal;
      }
      #endregion
      #endregion
   }
}
