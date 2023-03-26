using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Audio_Controller.classes;
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
using System.Collections.ObjectModel;

namespace Audio_Controller.pages
{
    /// <summary>
    /// Interaction logic for tracks.xaml
    /// </summary>
    public partial class tracks : Page
    {
        public ObservableCollection<Song> Songs { get; set; }
        public tracks()
        {
            InitializeComponent();

            // add songs to the listview
            Songs = new ObservableCollection<Song>
            {
                new Song("Song 1", "Artist 1", "Album 1", "3:25"),
                new Song("Song 2", "Artist 2", "Album 2", "4:12"),
                new Song("Song 3", "Artist 3", "Album 3", "2:58")
            };
            TracksView.ItemsSource = Songs;
        }

        private void searchBar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (searchBar.Text == " Begin by typing to choose tracks by title...")
            {
                searchBar.Text = "";
                searchBar.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void searchBar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (searchBar.Text == "")
            {
                searchBar.Text = " Begin by typing to choose tracks by title...";
                searchBar.Foreground = new SolidColorBrush(Colors.DarkGray);
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
                saveUploadedFile(filePath, fileName);
            }
        }

        private void FileUpload_BtnDrop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string fileName = Path.GetFileName(files[0]);
                string filePath = Path.GetDirectoryName(files[0]) + "\\"+ fileName;;
                FileUpload_Btn.Content = "Uploading "+fileName+"...";
                saveUploadedFile(filePath, fileName);
            }
        }

        public void saveUploadedFile(string path, string fileName)
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
