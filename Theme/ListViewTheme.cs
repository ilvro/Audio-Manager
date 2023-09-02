using Audio_Controller.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Audio_Controller.Theme
{
    public partial class ListViewTheme
    {
        Globals globals = new Globals();
        private MediaPlayer mediaPlayer = new MediaPlayer(); // should this be here??

        private void PlayBtn_Click(object sender, RoutedEventArgs e)
        {
            Button playButton = sender as Button;

            if (playButton != null)
            {
                Song selectedSong = playButton.DataContext as Song;

                if (selectedSong != null)
                {
                    string songPath = selectedSong.Path;
                    mediaPlayer.Open(new Uri(songPath));
                    mediaPlayer.Play();
                    globals.lastPlayed = selectedSong; // save last played
                    globals.isPaused = false;
                }
            }
        }
    }
}
