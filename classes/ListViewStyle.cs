using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using NAudio.Wave;
using Xabe.FFmpeg;

namespace Audio_Controller.classes
{
    public partial class ListViewStyle : ResourceDictionary
    {
        private SongPlayer songPlayer;
        private void ProgressBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ProgressBar progressBar = (ProgressBar)sender;

                // calculate the position where the user clicked as a percentage of the total width
                double clickPosition = e.GetPosition(progressBar).X / progressBar.ActualWidth;

                // update the song's playback position based on the click position
                double newPlaybackPosition = clickPosition * songPlayer.CurrentSong.TotalDuration;
                songPlayer.CurrentSong.SetPlaybackPosition(TimeSpan.FromSeconds(newPlaybackPosition));

                // update the MediaPlayer's position
                songPlayer.mediaPlayer.Position = TimeSpan.FromSeconds(newPlaybackPosition);
            }
        }

    }
}