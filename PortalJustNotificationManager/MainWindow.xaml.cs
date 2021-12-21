using PortalJustNotificationManager.Model;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace PortalJustNotificationManager
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      MainWindowViewModel viewModel;

      public MainWindow()
      {
         InitializeComponent();
         this.DataContext = viewModel = MainWindowViewModel.GetInstance();
      }

      protected override void OnClosing(CancelEventArgs e)
      {
         viewModel.persistanceManager.Serialize(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\portal_data.bin", viewModel.CaseHandlers);
      }

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
   }
}
