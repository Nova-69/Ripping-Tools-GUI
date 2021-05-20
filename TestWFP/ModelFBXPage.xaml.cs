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
        private void btnSelectModel_Click(object sender, RoutedEventArgs e)
        {

            // Defines model name

            ofd.Filter = ("Model File|*.model");

            bool? response = ofd.ShowDialog();
            if(response == true)
            {
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

        string ModelFBXVersion = "None";
        private void comboBoxModelFBXVersion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxModelFBXVersion == null) return;
            var combo = (ComboBox)sender;
            var item = (ComboBoxItem)combo.SelectedItem;
            ModelFBXVersion = item.Content.ToString();

            if (ModelFBXVersion == "System.Windows.Controls.Label: Gens/Unleashed")
            {
                ModelFBXVersion = "modelfbxGU";
            }
            else if (ModelFBXVersion == "System.Windows.Controls.Label: Lost World")
            {
                ModelFBXVersion = "modelfbxLW";
            }
            else if (ModelFBXVersion == "System.Windows.Controls.Label: Forces")
            {
                ModelFBXVersion = "modelfbxFor";
            }
            else if (ModelFBXVersion == "System.Windows.Controls.Label: ModelFBX 2012")
            {
                ModelFBXVersion = "modelfbx2012";
            }
        }

        string BatName = "None";
        private void btnBat_Click(object sender, RoutedEventArgs e)
        {
            if(ModelName == "None")
            {
                MessageBox.Show("No *.model selected.", "Not all parameters filled.");
            }
            else if(SklName == "None")
            {
                MessageBox.Show("No *.skl.hkx selected.", "Not all parameters filled.");
            }
            else if(ModelFBXVersion == "None")
            {
                MessageBox.Show("No ModelFBX version selected.", "Not all parameters filled.");
            }
            else if(ConvAnim == true)
            {
                if(ConvAnimNoModel == true)
                {

                    CopyModelFBXVersion();

                    BatName = "ConvAnimNoModel.bat";

                    StreamWriter sw = new StreamWriter($@"{SklDir}/ConvAnimNoModel.bat");
                    sw.WriteLine("md Converted");
                    sw.WriteLine($"for %%f in (*.anm.hkx) do {ModelFBXVersion} {SklName} %%f Converted/%%f.fbx");
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
                    CopyModelFBXVersion();

                    BatName = "ConvertModelAnim.bat";

                    StreamWriter sw = new StreamWriter($@"{SklDir}/ConvertModelAnim.bat");
                    string battext = $"for %%f in (*.anm.hkx) do {ModelFBXVersion} {ModelName} {SklName} %%f Converted/%%f.fbx";
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
            else
            {
                CopyModelFBXVersion();

                BatName = "ConvertModel.bat";

                StreamWriter sw = new StreamWriter($@"{SklDir}/ConvertModel.bat");
                sw.WriteLine("md Converted");
                sw.WriteLine($"{ModelFBXVersion} {ModelName} {SklName} Converted/{ModelName}.fbx");
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

        private void CopyFiles(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            foreach (var file in source.GetFiles())
            {
                file.CopyTo(System.IO.Path.Combine(target.FullName, file.Name), true);
            }
        }

        private void CopyModelFBXVersion()
        {
            if (ModelFBXVersion == "modelfbxGU")
            {
                var sourceDirPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Resources/ModelFBX/GensUnleashed");
                var sourceDirInfo = new DirectoryInfo(sourceDirPath);

                var targetDirPath = SklDir;
                var targetDirInfo = new DirectoryInfo(targetDirPath);

                CopyFiles(sourceDirInfo, targetDirInfo);
            }
            else if(ModelFBXVersion == "modelfbxLW")
            {
                var sourceDirPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Resources/ModelFBX/LostWorld");
                var sourceDirInfo = new DirectoryInfo(sourceDirPath);

                var targetDirPath = SklDir;
                var targetDirInfo = new DirectoryInfo(targetDirPath);

                CopyFiles(sourceDirInfo, targetDirInfo);
            }
            else if (ModelFBXVersion == "modelfbxFor")
            {
                var sourceDirPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Resources/ModelFBX/Forces");
                var sourceDirInfo = new DirectoryInfo(sourceDirPath);

                var targetDirPath = SklDir;
                var targetDirInfo = new DirectoryInfo(targetDirPath);

                CopyFiles(sourceDirInfo, targetDirInfo);
            }
            else if (ModelFBXVersion == "modelfbx2012")
            {
                var sourceDirPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Resources/ModelFBX/2012");
                var sourceDirInfo = new DirectoryInfo(sourceDirPath);

                var targetDirPath = SklDir;
                var targetDirInfo = new DirectoryInfo(targetDirPath);

                CopyFiles(sourceDirInfo, targetDirInfo);
            }
        }

        private void ClearFiles()
        {
            System.IO.File.Delete($@"{SklDir}/allegro-5.0.10-monolith-mt.dll");
            System.IO.File.Delete($@"{SklDir}/{BatName}");
            System.IO.File.Delete($@"{SklDir}/{ModelFBXVersion}.exe");
        }

    }
    
}
