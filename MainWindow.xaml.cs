using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Audio_Controller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Audio Manager";

            // start with the main page, tracks
            Main.NavigationService.Navigate(new Uri("pages/tracks.xaml", UriKind.Relative));


            // check for files
            if (File.Exists("currentPage=tracks.txt") || File.Exists("currentPage=playlists.txt"))
            {

            }
            else
            {
                File.Create("currentPage=tracks.txt");
            }

            // make the app always start on the tracks page
            if (getCurrentPage() == "playlists")
            {
                changeCurrentPage();
            }
            switchColors();
        }

        private void TrackBtn_Click(object sender, RoutedEventArgs e)
        {
            Main.NavigationService.Navigate(new Uri("pages/tracks.xaml", UriKind.Relative));
            if (getCurrentPage() != "tracks")
            {
                changeCurrentPage();
                switchColors();
            }
        }

        private void PlaylistBtn_Click(object sender, RoutedEventArgs e)
        {
            Main.NavigationService.Navigate(new Uri("pages/playlists.xaml", UriKind.Relative));
            if (getCurrentPage() != "playlists")
            {
                changeCurrentPage();
                switchColors();
            }
        }

        public void switchColors()
        {
            if (getCurrentPage() == "playlists")
            {
                TracksBtn.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                TracksBtn.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                PlaylistBtn.Background = new SolidColorBrush(Color.FromRgb(218, 218, 218));
                PlaylistBtn.Foreground = new SolidColorBrush(Color.FromRgb(170, 170, 170));
            }
            else
            {
                TracksBtn.Background = new SolidColorBrush(Color.FromRgb(218, 218, 218));
                TracksBtn.Foreground = new SolidColorBrush(Color.FromRgb(170, 170, 170));
                PlaylistBtn.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                PlaylistBtn.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
        }

        public string getCurrentPage()
        {
            if (File.Exists("currentPage=tracks.txt"))
            {
                return "tracks";
            }
            else
            {
                return "playlists";
            }
        }

        public void changeCurrentPage()
        {
            if (getCurrentPage() == "tracks")
            {
                File.Move("currentPage=tracks.txt", "currentPage=playlists.txt");
            }
            else
            {
                File.Move("currentPage=playlists.txt", "currentPage=tracks.txt");
            }
        }

        private void PlaylistBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            if (getCurrentPage() != "playlists")
            {
                PlaylistBtn.Background = new SolidColorBrush(Color.FromRgb(227, 227, 227));
            }
        }

        private void PlaylistBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (getCurrentPage() != "playlists")
            {
                PlaylistBtn.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
        }

        private void TracksBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            if (getCurrentPage() != "tracks")
            {
                TracksBtn.Background = new SolidColorBrush(Color.FromRgb(227, 227, 227));
            }
        }

        private void TracksBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (getCurrentPage() != "tracks")
            {
                TracksBtn.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
        }

        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Main.Focus();
        }
    }
}
