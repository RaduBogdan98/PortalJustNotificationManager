using PortalJustNotificationManager.API;
using PortalJustNotificationManager.Model;
using System.Windows;

namespace PortalJustNotificationManager
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      public MainWindow()
      {
         InitializeComponent();

         GetCaseFile();
      }

      private async void GetCaseFile()
      {
         PortalJustHttpClient httpClient = new PortalJustHttpClient();
         CaseFile case1 = await httpClient.FindCaseFile("1904/3/2018/a1");
         CaseFile case2 = await httpClient.FindCaseFile("1904/3/2018/a1");
         case1.CompareTo(case2);
      }
   }
}
