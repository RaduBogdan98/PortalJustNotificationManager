using PortalJustNotificationManager.API;
using PortalJustNotificationManager.Model;
using System.ComponentModel;
using System.Windows;
using PortalJustNotificationManager.Persistence;
using System.Collections.ObjectModel;

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
         this.DataContext = viewModel = new MainWindowViewModel();
      }

      protected override void OnClosing(CancelEventArgs e)
      {
         viewModel.persistanceManager.Serialize("data.bin", viewModel.CaseHandlers);
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
   }
}
