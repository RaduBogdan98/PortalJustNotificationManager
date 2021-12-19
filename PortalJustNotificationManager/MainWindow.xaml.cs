using PortalJustNotificationManager.API;
using PortalJustNotificationManager.Model;
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
         this.DataContext = viewModel = new MainWindowViewModel();

         GetCaseFile();
      }

      private async void GetCaseFile()
      {
         PortalJustHttpClient httpClient = new PortalJustHttpClient();
         CaseFile case1 = await httpClient.FindCaseFile("1904/3/2018/a1");
         CaseFile case2 = await httpClient.FindCaseFile("1904/3/2018/a1");
         
      }

      private void ListViewItem_Selected(object sender, RoutedEventArgs e)
      {
         CaseHandler selectedHandler = ((ListViewItem)sender).DataContext as CaseHandler;
         viewModel.SelectedCaseHandler = selectedHandler;
      }

      private void AddButton_Click(object sender, RoutedEventArgs e)
      {
         AddHandlerView view = new AddHandlerView();
         if (view.ShowDialog() == true)
         {
            viewModel.CaseHandlers.Add(view.RetrievedCaseHandler);
         }
      }
   }
}
