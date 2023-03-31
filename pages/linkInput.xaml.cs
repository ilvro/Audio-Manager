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
                //downloadVideo(uploadFolder, videoUrl);
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

            // get file count, works for updating the file list later
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(uploadFolder);
            int count = dir.GetFiles().Length;

            System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
            try
            {
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.FileName = "cmd.exe";
                myProcess.StartInfo.WorkingDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
                myProcess.StartInfo.CreateNoWindow = false;
                myProcess.StartInfo.Arguments = $"/C yt-dlp.exe --output \"\\tracks\\%(title)s.%(ext)s\" -f bestaudio -x --audio-format mp3 " + url;
                myProcess.Start(); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            tracks tracksPage = new tracks();
            tracksPage.updateFileList();

            System.Threading.Thread.Sleep(5000);
            tracksPage.updateFileList();
            
        }
    }
}
