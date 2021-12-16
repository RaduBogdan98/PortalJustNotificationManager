using PortalJustNotificationManager.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

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

         //GetCaseFile();

         DataMapper mapper = new DataMapper();
         mapper.MapXmlResponseToCaseFile(null);
      }

      private async void GetCaseFile()
      {
         PortalJustHttpClient httpClient = new PortalJustHttpClient();
         XmlDocument doc = await httpClient.FindCaseFile("1904/3/2018/a1");
      }
   }
}
