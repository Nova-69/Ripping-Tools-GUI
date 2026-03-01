using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace TestWFP
{
    /// <summary>
    /// Interaction logic for ModelFBXPage.xaml
    /// </summary>
    public partial class ModelFBXPage : Page
    {

        public ModelFBXPage()
        {
            InitializeComponent();
            checkBoxConvAnimNoModel.IsEnabled = false;
        }

        OpenFileDialog ofd = new OpenFileDialog();

        string ModelName = "None";
        string ModelDir = "None";
        private void btnSelectModel_Click(object sender, RoutedEventArgs e)
        {

            // Defines model name

            ofd.Filter = ("Model File|*.model");

            bool? response = ofd.ShowDialog();
            if(response == true)
            {
                string fullPath = ofd.FileName;
                ModelDir = fullPath.Substring(0, fullPath.LastIndexOf('\\'));

                ModelName = ofd.SafeFileName;
                txtBoxModel.Text = ModelName;
            }

        }

        string SklDir = "None";
        string SklName = "None";
        private void btnSelectSkl_Click(object sender, RoutedEventArgs e)
        {

            // Defines skeleton name

            ofd.Filter = ("Skeleton File|*.skl.hkx");

            bool? response = ofd.ShowDialog();
            if (response == true)
            {

                string fullPath = ofd.FileName;
                SklDir = fullPath.Substring(0, fullPath.LastIndexOf('\\'));

                SklName = ofd.SafeFileName;
                txtBoxSkl.Text = SklName;
            }

        }

        private void btnClearSkl_Click(object sender, RoutedEventArgs e)
        {
            SklDir = "None";
            SklName = "None";
            txtBoxSkl.Text = SklName;
        }

        bool ConvAnim = false;
        private void checkBoxConvAnim_Click(object sender, RoutedEventArgs e)
        {

            // Defines if animations are to be converted

            if (checkBoxConvAnim.IsChecked == true)
            {
                ConvAnim = true;
                checkBoxConvAnimNoModel.IsEnabled = true;
            }
            else
            {
                ConvAnim = false;
                checkBoxConvAnimNoModel.IsEnabled = false;
                checkBoxConvAnimNoModel.IsChecked = false;
            }
        }

        bool ConvAnimNoModel = false;
        private void checkBoxConvAnimNoModel_Click(object sender, RoutedEventArgs e)
        {

            // Defines if animations are to be converted without model (for UE4)

            if(checkBoxConvAnimNoModel.IsChecked == true)
            {
                ConvAnimNoModel = true;
            }
            else
            {
                ConvAnimNoModel = false;
            }
        }

        string BatName = "None";
        private void btnBat_Click(object sender, RoutedEventArgs e)
        {
            SklDir = ModelDir;
            if (ModelName == "None")
            {
                MessageBox.Show("No *.model selected.", "Not all parameters filled.");
            }
            /* else if(SklName == "None")
            {
                MessageBox.Show("No *.skl.hkx selected.", "Not all parameters filled.");
            }
            */
            
            else if (ConvAnim == true && SklName != "None")
            {
                if (ConvAnimNoModel == true)
                {

                    CopyModelFBX();

                    BatName = "ConvAnimNoModel.bat";

                    StreamWriter sw = new StreamWriter($@"{SklDir}/ConvAnimNoModel.bat");
                    sw.WriteLine("md Converted");
                    sw.WriteLine($"for %%f in (*.anm.hkx) do modelfbx_2012 {SklName} %%f Converted/%%f.fbx");
                    sw.Close();

                    string targetDir = string.Format($@"{SklDir}");   //this is where the .bat is 
                    Process proc = new Process();
                    proc.StartInfo.WorkingDirectory = targetDir;
                    proc.StartInfo.FileName = "ConvAnimNoModel.bat";
                    proc.StartInfo.CreateNoWindow = false;
                    proc.Start();

                    proc.WaitForExit();
                    ClearFiles();

                    MessageBox.Show("Finished converting.");
                }
                else
                {
                    CopyModelFBX();

                    BatName = "ConvertModelAnim.bat";

                    StreamWriter sw = new StreamWriter($@"{SklDir}/ConvertModelAnim.bat");
                    string battext = $"setlocal enabledelayedexpansion\n" + 
                                     $"for %%f in (*.anm.hkx) do (\n" + 
                                     $"    set \"name=%%f\"\n" + 
                                     $"    set \"name=!name:.anm.hkx=!\"\n" + 
                                     $"    modelfbx_2012 {ModelName} {SklName} %%f Converted/!name!.fbx\n" + 
                                     $")";
                    sw.WriteLine("md Converted");
                    sw.WriteLine(battext);
                    sw.Close();

                    string targetDir = string.Format($@"{SklDir}");   //this is where the .bat is 
                    Process proc = new Process();
                    proc.StartInfo.WorkingDirectory = targetDir;
                    proc.StartInfo.FileName = "ConvertModelAnim.bat";
                    proc.StartInfo.CreateNoWindow = false;
                    proc.Start();

                    proc.WaitForExit();
                    ClearFiles();

                    MessageBox.Show("Finished converting.");
                }

            }
            else if (ConvAnim == true)
            {
                MessageBox.Show("No *.skl.hkx selected.", "Not all parameters filled.");
            }
            else
            {
                if (SklName == "None")
                {
                    CopyModelFBX();

                    BatName = "ConvertModel.bat";

                    StreamWriter sw = new StreamWriter($@"{SklDir}/ConvertModel.bat");
                    sw.WriteLine("md Converted");
                    sw.WriteLine($"modelfbx_2012 {ModelName} Converted/{ModelName}.fbx");
                    sw.Close();

                    string targetDir = string.Format($@"{SklDir}");   //this is where the .bat is
                    Process proc = new Process();
                    proc.StartInfo.WorkingDirectory = targetDir;
                    proc.StartInfo.FileName = "ConvertModel.bat";
                    proc.StartInfo.CreateNoWindow = false;
                    proc.Start();

                    proc.WaitForExit();
                    ClearFiles();

                    MessageBox.Show("Finished converting.");
                }
                else
                {
                    CopyModelFBX();

                    BatName = "ConvertModel.bat";

                    StreamWriter sw = new StreamWriter($@"{SklDir}/ConvertModel.bat");
                    sw.WriteLine("md Converted");
                    sw.WriteLine($"modelfbx_2012 {ModelName} {SklName} Converted/{ModelName}.fbx");
                    sw.Close();

                    string targetDir = string.Format($@"{SklDir}");   //this is where the .bat is
                    Process proc = new Process();
                    proc.StartInfo.WorkingDirectory = targetDir;
                    proc.StartInfo.FileName = "ConvertModel.bat";
                    proc.StartInfo.CreateNoWindow = false;
                    proc.Start();

                    proc.WaitForExit();
                    ClearFiles();

                    MessageBox.Show("Finished converting.");
                }

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

        private void CopyModelFBX()
        {
            var sourceDirPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Resources/ModelFBX");
            var sourceDirInfo = new DirectoryInfo(sourceDirPath);

            var targetDirPath = SklDir;
            var targetDirInfo = new DirectoryInfo(targetDirPath);

            CopyFiles(sourceDirInfo, targetDirInfo);
        }

        private void ClearFiles()
        {
            System.IO.File.Delete($@"{SklDir}/allegro-5.0.10-monolith-mt.dll");
            System.IO.File.Delete($@"{SklDir}/{BatName}");
            System.IO.File.Delete($@"{SklDir}/modelfbx_2012.exe");
        }

    }
    
}
