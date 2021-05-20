using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace TestWFP
{
    /// <summary>
    /// Interaction logic for HKXConvPage.xaml
    /// </summary>
    public partial class HKXConvPage : Page
    {
        public HKXConvPage()
        {
            InitializeComponent();
            btnConvHKX.IsEnabled = false;
        }

        string HKXDir = "None";
        private void btnSelectHKX_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = ("HKX Animation file|*anm.hkx");

            if (ofd.ShowDialog() == true)
            {
                btnConvHKX.IsEnabled = true;

                string fullPath = ofd.FileName;
                HKXDir = fullPath.Substring(0, fullPath.LastIndexOf('\\'));

                foreach (String item in Directory.EnumerateFiles(HKXDir, "*.anm.hkx"))
                {
                    listBoxHKXView.Items.Add(item);
                }
            }
        }

        private void btnConvHKX_Click(object sender, RoutedEventArgs e)
        {

            if(HKXVersion == "None")
            {
                MessageBox.Show("No version selected.", "Not all parameters filled.");
            }

            else if(HKXVersion == "HKXUnleashed")
            {
                var sourceDirPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Resources/HKXConv/HKXConvUnl");
                var sourceDirInfo = new DirectoryInfo(sourceDirPath);

                var targetDirPath = HKXDir;
                var targetDirInfo = new DirectoryInfo(targetDirPath);

                CopyFiles(sourceDirInfo, targetDirInfo);

                string batLoc = string.Format($@"{HKXDir}");   //this is where the .bat is 
                Process proc = new Process();
                proc.StartInfo.WorkingDirectory = batLoc;
                proc.StartInfo.FileName = "BatchConvert.bat";
                proc.StartInfo.CreateNoWindow = false;
                proc.Start();

                proc.WaitForExit();

                listBoxHKXView.Items.Clear();
                System.IO.File.Delete($@"{HKXDir}/BatchConvert.bat");
                System.IO.File.Delete($@"{HKXDir}/HKXConverter.exe");

                MessageBox.Show("Converted *.anm.hkx files.");
            }
            else if(HKXVersion == "HKXForces")
            {
                var sourceDirPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Resources/HKXConv/TagTools");
                var sourceDirInfo = new DirectoryInfo(sourceDirPath);

                var targetDirPath = HKXDir;
                var targetDirInfo = new DirectoryInfo(targetDirPath);

                CopyFiles(sourceDirInfo, targetDirInfo);

                string batLoc = string.Format($@"{HKXDir}");   //this is where the .bat is 
                Process proc = new Process();
                proc.StartInfo.WorkingDirectory = batLoc;
                proc.StartInfo.FileName = "BatchConvert.bat";
                proc.StartInfo.CreateNoWindow = false;
                proc.Start();

                proc.WaitForExit();

                listBoxHKXView.Items.Clear();
                System.IO.File.Delete($@"{HKXDir}/BatchConvert.bat");
                System.IO.File.Delete($@"{HKXDir}/TagTools.exe");
                System.IO.File.Delete($@"{HKXDir}/TagTools.VIR");
                System.IO.File.Delete($@"{HKXDir}/TypeDatabase.xml");

                MessageBox.Show("Converted *.anm.hkx files.");
            }
        }

        private void CopyFiles(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            foreach (var file in source.GetFiles())
            {
                file.CopyTo(System.IO.Path.Combine(target.FullName, file.Name), true);
            }
        }

        string HKXVersion = "None";
        private void comboBoxHKXVersion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxHKXVersion == null) return;
            var combo = (ComboBox)sender;
            var item = (ComboBoxItem)combo.SelectedItem;
            HKXVersion = item.Content.ToString();

            if (HKXVersion == "System.Windows.Controls.Label: Unleashed")
            {
                HKXVersion = "HKXUnleashed";
            }
            else if (HKXVersion == "System.Windows.Controls.Label: Forces")
            {
                HKXVersion = "HKXForces";
            }
        }
    }
}
