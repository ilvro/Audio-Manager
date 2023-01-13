using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Audio_Manager
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
            switchColors("tracks");
        }

        private void TrackBtn_Click(object sender, RoutedEventArgs e)
        {
            Main.NavigationService.Navigate(new Uri("pages/tracks.xaml", UriKind.Relative));
            switchColors("tracks");
        }

        private void PlaylistBtn_Click(object sender, RoutedEventArgs e)
        {
            Main.NavigationService.Navigate(new Uri("pages/playlists.xaml", UriKind.Relative));
            switchColors("playlists");
        }

        public void switchColors(string newPage)
        {
            if (newPage == "playlists")
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
        
    }
}
