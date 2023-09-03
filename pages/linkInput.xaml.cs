using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using MediaToolkit;
using MediaToolkit.Model;
using VideoLibrary;
using Path = System.IO.Path;
using Audio_Controller.pages;

namespace Audio_Controller.pages
{
    /// <summary>
    /// Interaction logic for linkInput.xaml
    /// </summary>
    public partial class linkInput : Window
    {
        public linkInput()
        {
            InitializeComponent();
        }

        private void LinkDownloadBtn_Click(object sender, RoutedEventArgs e)
        {
            string uploadFolder = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\tracks\\";
            string videoUrl = URLTextBox.Text;
            if (videoUrl.Contains("youtube.com") && videoUrl.Contains("/watch?v="))
            {
                downloadVideo(uploadFolder, videoUrl);
                this.Close();
            }
        }

        private void URLTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            URLTextBox.Text = "";
        }

        private void downloadVideo(string uploadFolder, string url)
        {
            System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
            try
            {
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.FileName = "cmd.exe";
                myProcess.StartInfo.WorkingDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
                myProcess.StartInfo.CreateNoWindow = false;
                myProcess.StartInfo.Arguments = $"/C yt-dlp.exe --output \"\\tracks\\%(title)s.%(ext)s\" -f bestaudio -x --audio-format mp3 " + url; // download file and output it in the tracks folder as a .mp3
                myProcess.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            myProcess.WaitForExit(); // wait until its done to prevent the file from being used by another process

            
            // get the file name
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(uploadFolder);
            System.IO.FileInfo[] files = dir.GetFiles("*.mp3");
            System.IO.FileInfo latestFile = files.OrderByDescending(f => f.CreationTime).FirstOrDefault();
            string fileName = latestFile.FullName;

            while (!File.Exists(fileName) || IsFileLocked(new FileInfo(fileName))) // wait until its not being used by another process
            {
                System.Threading.Thread.Sleep(500);
            }
           
        }

        private static bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                // The file is locked and cannot be accessed
                return true;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            // The file is not locked and can be accessed
            return false;
        }
    }
}
