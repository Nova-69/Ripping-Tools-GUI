using System.Windows;

namespace TestWFP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new ModelFBXPage();
            btnTopModelFBX.IsEnabled = false;
        }

        private void btnTopModelFBX_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ModelFBXPage();
            btnTopModelFBX.IsEnabled = false;
            btnTopDDSConv.IsEnabled = true;
            btnTopHKXConv.IsEnabled = true;
        }

        private void btnTopDDSConv_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new DDSConvPage();
            btnTopModelFBX.IsEnabled = true;
            btnTopDDSConv.IsEnabled = false;
            btnTopHKXConv.IsEnabled = true;
        }

        private void btnTopHKXConv_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new HKXConvPage();
            btnTopModelFBX.IsEnabled = true;
            btnTopDDSConv.IsEnabled = true;
            btnTopHKXConv.IsEnabled = false;
        }
    }
}
