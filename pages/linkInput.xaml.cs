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
            }
            this.Close();
        }

        private void URLTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            URLTextBox.Text = "";
        }

        private void downloadVideo(string uploadFolder, string url) // this is kind of slow - there might be a better way to do this
        {
            var youtube = YouTube.Default;
            var vid = youtube.GetVideo(url);
            string videopath = Path.Combine(uploadFolder, vid.FullName);
            File.WriteAllBytes(videopath, vid.GetBytes());

            var inputFile = new MediaFile { Filename = Path.Combine(uploadFolder, vid.FullName) };
            var outputFile = new MediaFile { Filename = Path.Combine(uploadFolder, $"{vid.FullName}.mp3") };

            using (var engine = new Engine())
            {
                engine.GetMetadata(inputFile);


                engine.Convert(inputFile, outputFile);
            }


            File.Delete(Path.Combine(uploadFolder, vid.FullName));
        }
    }
}
