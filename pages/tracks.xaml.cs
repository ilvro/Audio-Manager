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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xabe.FFmpeg;
using FFMpegCore;
using Path = System.IO.Path;

namespace Audio_Controller.pages
{
    /// <summary>
    /// Interaction logic for tracks.xaml
    /// </summary>
    public partial class tracks : Page
    {
        public tracks()
        {
            InitializeComponent();
        }

        private void searchBar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (searchBar.Text == " Begin by typing to choose tracks by title...")
            {
                searchBar.Text = "";
            }
        }

        private void searchBar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (searchBar.Text == "")
            {
                searchBar.Text = " Begin by typing to choose tracks by title...";
            }
        }

        private void FileUpload_BtnClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool response = (bool)openFileDialog.ShowDialog();
            if (response == true)
            {
                string filePath = openFileDialog.FileName;
                string fileName = Path.GetFileName(filePath);
                string fileType = filePath.Substring(filePath.Length - 3);
                saveUploadedFile(filePath, fileType, fileName);
            }
        }

        private void FileUpload_BtnDrop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string fileName = Path.GetFileName(files[0]);
                string filePath = Path.GetDirectoryName(files[0]) + "\\"+ fileName;
                string fileType = fileName.Substring(fileName.Length - 3);
                FileUpload_Btn.Content = "Uploading "+fileName+"...";
                saveUploadedFile(filePath, fileType, fileName);
            }
        }

        public void saveUploadedFile(string path, string fileType, string fileName)
        {
            string uploadFolder = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\tracks\\" + fileName;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.WorkingDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            startInfo.Arguments = "/C ffmpeg -i "+path+" tracks\\"+fileName.Substring(0, fileName.Length - 4)+".mp3";
            process.StartInfo = startInfo;
            process.Start();

        }
    }
}
