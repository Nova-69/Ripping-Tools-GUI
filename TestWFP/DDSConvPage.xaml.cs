using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace TestWFP
{
    /// <summary>
    /// Interaction logic for DDSConvPage.xaml
    /// </summary>
    public partial class DDSConvPage : Page
    {
        public DDSConvPage()
        {
            InitializeComponent();
            btnConvDDS.IsEnabled = false;
            btnFixForcesDDS.IsEnabled = false;
            btnFixStreaming.IsEnabled = false;
        }

        string StreamingDir;
        string DDSDir;
        private void btnSelectDDS_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = ("DDS Texture|*.dds");

            if (ofd.ShowDialog()==true)
            {
                btnConvDDS.IsEnabled = true;
                btnFixForcesDDS.IsEnabled = true;
                btnFixStreaming.IsEnabled = true;

                string fullPath = ofd.FileName;
                DDSDir = fullPath.Substring(0, fullPath.LastIndexOf('\\'));

                foreach (String item in Directory.EnumerateFiles(DDSDir, "*.dds"))
                {
                    listBox.Items.Add(item);
                }
            }
        }

        private void btnConvDDS_Click(object sender, RoutedEventArgs e)
        {

            var sourceDirPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Resources/DDSConv");
            var sourceDirInfo = new DirectoryInfo(sourceDirPath);

            var targetDirPath = DDSDir;
            var targetDirInfo = new DirectoryInfo(targetDirPath);

            CopyFiles(sourceDirInfo, targetDirInfo);

            string batLoc = string.Format($@"{DDSDir}");   //this is where the .bat is 
            Process proc = new Process();
            proc.StartInfo.WorkingDirectory = batLoc;
            proc.StartInfo.FileName = "DDS2PNG.bat";
            proc.StartInfo.CreateNoWindow = false;
            proc.Start();

            proc.WaitForExit();

            listBox.Items.Clear();
            System.IO.File.Delete($@"{DDSDir}/texconv.exe");
            System.IO.File.Delete($@"{DDSDir}/DDS2PNG.bat");
            System.IO.File.Delete($@"{DDSDir}/ForcesDDSFix.bat");

            btnConvDDS.IsEnabled = false;
            btnFixForcesDDS.IsEnabled = false;

            MessageBox.Show("Converted all *.dds textures to *.png.");
        }

        private void CopyFiles(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            foreach(var file in source.GetFiles())
            {
                file.CopyTo(System.IO.Path.Combine(target.FullName, file.Name), true);
            }
        }

        private void btnFixForcesDDS_Click(object sender, RoutedEventArgs e)
        {
            var sourceDirPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Resources/DDSConv");
            var sourceDirInfo = new DirectoryInfo(sourceDirPath);

            var targetDirPath = DDSDir;
            var targetDirInfo = new DirectoryInfo(targetDirPath);

            CopyFiles(sourceDirInfo, targetDirInfo);

            string batLoc = string.Format($@"{DDSDir}");   //this is where the .bat is 
            Process proc = new Process();
            proc.StartInfo.WorkingDirectory = batLoc;
            proc.StartInfo.FileName = "ForcesDDSFix.bat";
            proc.StartInfo.CreateNoWindow = false;
            proc.Start();

            proc.WaitForExit();

            System.IO.File.Delete($@"{DDSDir}/texconv.exe");
            System.IO.File.Delete($@"{DDSDir}/DDS2PNG.bat");
            System.IO.File.Delete($@"{DDSDir}/ForcesDDSFix.bat");
            btnFixForcesDDS.IsEnabled = false;

            MessageBox.Show("Fixed all Forces *.dds textures.");
        }

        private void btnSelectStreamingPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = ("NTSP File|*.ntsp");

            if (ofd.ShowDialog() == true)
            {
                string fullPath = ofd.FileName;
                StreamingDir = fullPath.Substring(0, fullPath.LastIndexOf('\\'));

                StreamWriter sw = new StreamWriter("Resources/Streaming/needle_texture_streaming_path.txt");
                sw.WriteLine(StreamingDir);
                sw.Close();
            }
        }
        private void btnFixStreaming_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(System.IO.Path.Combine(Environment.CurrentDirectory, "Resources/Streaming/NeedleTextureStreamingPackage.exe"), DDSDir);
            MessageBox.Show("Converting. If it didn't work, select streaming path ('game_name'/image/x64/raw/texture_streaming)");
        }
    }
}
